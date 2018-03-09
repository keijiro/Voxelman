using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    internal unsafe struct EntityCommandBufferData
    {
        [NativeDisableUnsafePtrRestriction]
        public EntityCommandBuffer.Chunk* m_Tail;

        [NativeDisableUnsafePtrRestriction]
        public EntityCommandBuffer.Chunk* m_Head;

        public int m_MinimumChunkSize;

        public Allocator m_Allocator;
    }

    /// <summary>
    /// A thread-safe command buffer that can buffer commands that affect entities and components for later playback.
    /// </summary>
    ///
    /// Command buffers are not created in user code directly, you get them from either a ComponentSystem or a Barrier.
    [StructLayout(LayoutKind.Sequential)]
    [NativeContainer]
    public unsafe struct EntityCommandBuffer
    {
        /// <summary>
        /// The minimum chunk size to allocate from the job allocator.
        /// </summary>
        ///
        /// We keep this relatively small as we don't want to overload the temp allocator in case people make a ton of command buffers.
        /// <summary>
        /// Organized in memory like a single block with Chunk header followed by Size bytes of data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Chunk
        {
            internal int Used;
            internal int Size;
            internal Chunk* Next;
            internal Chunk* Prev;

            internal int Capacity => Size - Used;

            internal int Bump(int size)
            {
                var off = Used;
                Used += size;
                return off;
            }
        }

        const int kDefaultMinimumChunkSize = 4 * 1024;

        [NativeDisableUnsafePtrRestriction]
        EntityCommandBufferData* m_Data;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        AtomicSafetyHandle m_Safety;
#endif

        /// <summary>
        /// Allows controlling the size of chunks allocated from the temp job allocator to back the command buffer.
        /// </summary>
        /// Larger sizes are more efficient, but create more waste in the allocator.
        public int MinimumChunkSize
        {
            get { return m_Data->m_MinimumChunkSize > 0 ? m_Data->m_MinimumChunkSize : kDefaultMinimumChunkSize; }
            set { m_Data->m_MinimumChunkSize = Math.Max(0, value); }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct BasicCommand
        {
            public int CommandType;
            public int TotalSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CreateCommand
        {
            public BasicCommand Header;
            public EntityArchetype Archetype;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct EntityCommand
        {
            public BasicCommand Header;
            public Entity Entity;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct EntityComponentCommand
        {
            public EntityCommand Header;
            public int ComponentTypeIndex;
            public int ComponentSize;
            // Data follows if command has an associated component payload
        }

        byte* Reserve(int size)
        {
            EntityCommandBufferData* data = m_Data;

            if (data->m_Tail == null || data->m_Tail->Capacity < size)
            {
                var c = (Chunk*)UnsafeUtility.Malloc(sizeof(Chunk) + size, 16, data->m_Allocator);
                c->Next = null;
                c->Prev = data->m_Tail != null ? data->m_Tail : null;
                c->Used = 0;
                c->Size = size;

                if (data->m_Tail != null)
                {
                    data->m_Tail->Next = c;
                }

                if (data->m_Head == null)
                {
                    data->m_Head = c;
                }

                data->m_Tail = c;
            }

            var offset = data->m_Tail->Bump(size);
            var ptr = ((byte*)data->m_Tail) + sizeof(Chunk) + offset;
            return ptr;
        }

        private void AddCreateCommand(Command op, EntityArchetype archetype)
        {
            var data = (CreateCommand*) Reserve(sizeof(CreateCommand));

            data->Header.CommandType = (int) op;
            data->Header.TotalSize = sizeof(CreateCommand);
            data->Archetype = archetype;
        }

        private void AddEntityCommand(Command op, Entity e)
        {
            var data = (EntityCommand*) Reserve(sizeof(EntityCommand));

            data->Header.CommandType = (int) op;
            data->Header.TotalSize = sizeof(EntityCommand);
            data->Entity = e;
        }

        private void AddEntityComponentCommand<T>(Command op, Entity e, T component) where T : struct
        {
            var typeSize = UnsafeUtility.SizeOf<T>();
            var typeIndex = TypeManager.GetTypeIndex<T>();
            var sizeNeeded = Align(sizeof(EntityComponentCommand) + typeSize, 8);

            var data = (EntityComponentCommand*) Reserve(sizeNeeded);

            data->Header.Header.CommandType = (int) op;
            data->Header.Header.TotalSize = sizeNeeded;
            data->Header.Entity = e;
            data->ComponentTypeIndex = typeIndex;
            data->ComponentSize = typeSize;

            UnsafeUtility.CopyStructureToPtr (ref component, (byte*) (data + 1));
        }

        private static int Align(int size, int alignmentPowerOfTwo)
        {
            return (size + alignmentPowerOfTwo - 1) & ~(alignmentPowerOfTwo - 1);
        }

        private void AddEntityComponentTypeCommand<T>(Command op, Entity e) where T : struct
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
            var sizeNeeded = Align(sizeof(EntityComponentCommand), 8);

            var data = (EntityComponentCommand*) Reserve(sizeNeeded);

            data->Header.Header.CommandType = (int) op;
            data->Header.Header.TotalSize = sizeNeeded;
            data->Header.Entity = e;
            data->ComponentTypeIndex = typeIndex;
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void EnforceSingleThreadOwnership()
        {
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(this.m_Safety);
            #endif
        }

        /// <summary>
        /// Creates a new command buffer. Note that this is internal and not exposed to user code.
        /// </summary>
        /// <param name="label">Memory allocator to use for chunks and data</param>
        internal EntityCommandBuffer(Allocator label)
        {
            m_Data = (EntityCommandBufferData*) UnsafeUtility.Malloc(sizeof(EntityCommandBufferData), UnsafeUtility.AlignOf<EntityCommandBufferData>(), label);
            m_Data->m_Allocator = label;
            m_Data->m_Tail = null;
            m_Data->m_Head = null;
            m_Data->m_MinimumChunkSize = kDefaultMinimumChunkSize;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_Safety = AtomicSafetyHandle.Create();
#endif
        }

        internal void Dispose()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckDeallocateAndThrow(m_Safety);
#endif
            if (m_Data != null)
            {
                var label = m_Data->m_Allocator;

                while (m_Data->m_Tail != null)
                {
                    var prev = m_Data->m_Tail->Prev;
                    UnsafeUtility.Free(m_Data->m_Tail, m_Data->m_Allocator);
                    m_Data->m_Tail = prev;
                }

                m_Data->m_Head = null;
                UnsafeUtility.Free(m_Data, label);
                m_Data = null;
            }
        }

        public void CreateEntity()
        {
            EnforceSingleThreadOwnership();
            AddCreateCommand(Command.CreateEntityImplicit, new EntityArchetype());
        }

        public void CreateEntity(EntityArchetype archetype)
        {
            EnforceSingleThreadOwnership();
            AddCreateCommand(Command.CreateEntityImplicit, archetype);
        }

        public void DestroyEntity(Entity e)
        {
            EnforceSingleThreadOwnership();
            AddEntityCommand(Command.DestroyEntityExplicit, e);
        }

        public void AddComponent<T>(Entity e, T component) where T: struct, IComponentData
        {
            EnforceSingleThreadOwnership();
            AddEntityComponentCommand(Command.AddComponentExplicit, e, component);
        }

        public void SetComponent<T>(T component) where T: struct, IComponentData
        {
            EnforceSingleThreadOwnership();
            AddEntityComponentCommand(Command.SetComponentImplicit, Entity.Null, component);
        }

        public void SetComponent<T>(Entity e, T component) where T: struct, IComponentData
        {
            EnforceSingleThreadOwnership();
            AddEntityComponentCommand(Command.SetComponentExplicit, e, component);
        }

        public void RemoveComponent<T>(Entity e) where T: struct, IComponentData
        {
            EnforceSingleThreadOwnership();
            AddEntityComponentTypeCommand<T>(Command.RemoveComponentExplicit, e);
        }

        public void AddComponent<T>(T component) where T: struct, IComponentData
        {
            EnforceSingleThreadOwnership();
            AddEntityComponentCommand(Command.AddComponentImplicit, Entity.Null, component);
        }

        enum Command
        {
            // Commands that operate on a known entity
            DestroyEntityExplicit,
            AddComponentExplicit,
            RemoveComponentExplicit,
            SetComponentExplicit,

            // Commands that either create a new entity or operate implicitly on a just-created entity (which doesn't yet exist when the command is buffered)
            CreateEntityImplicit,
            AddComponentImplicit,
            SetComponentImplicit
        }

        /// <summary>
        /// Play back all recorded operations against an entity manager.
        /// </summary>
        /// <param name="mgr">The entity manager that will receive the operations</param>
        public void Playback(EntityManager mgr)
        {
            if (mgr == null)
                throw new NullReferenceException($"{nameof(mgr)} cannot be null");

            EnforceSingleThreadOwnership();

            var head = m_Data->m_Head;
            var lastEntity = new Entity();

            while (head != null)
            {
                var off = 0;
                var buf = ((byte*)head) + sizeof(Chunk);

                while (off < head->Used)
                {
                    var header = (BasicCommand*)(buf + off);

                    switch ((Command)header->CommandType)
                    {
                        case Command.DestroyEntityExplicit:
                            mgr.DestroyEntity(((EntityCommand*)header)->Entity);
                            break;

                        case Command.AddComponentExplicit:
                            {
                                var cmd = (EntityComponentCommand*)header;
                                var componentType = (ComponentType)TypeManager.GetType(cmd->ComponentTypeIndex);
                                mgr.AddComponent(cmd->Header.Entity, componentType);
                                mgr.SetComponentRaw(cmd->Header.Entity, cmd->ComponentTypeIndex, (cmd + 1), cmd->ComponentSize);
                            }
                            break;

                        case Command.RemoveComponentExplicit:
                            {
                                var cmd = (EntityComponentCommand*)header;
                                mgr.RemoveComponent(cmd->Header.Entity, TypeManager.GetType(cmd->ComponentTypeIndex));
                            }
                            break;

                        case Command.SetComponentExplicit:
                            {
                                var cmd = (EntityComponentCommand*)header;
                                mgr.SetComponentRaw(cmd->Header.Entity, cmd->ComponentTypeIndex, (cmd + 1), cmd->ComponentSize);
                            }
                            break;

                        case Command.CreateEntityImplicit:
                            {
                                var cmd = (CreateCommand*)header;
                                if (cmd->Archetype.Valid)
                                    lastEntity = mgr.CreateEntity(cmd->Archetype);
                                else
                                    lastEntity = mgr.CreateEntity();
                                break;
                            }

                        case Command.AddComponentImplicit:
                            {
                                var cmd = (EntityComponentCommand*)header;
                                var componentType = (ComponentType)TypeManager.GetType(cmd->ComponentTypeIndex);
                                mgr.AddComponent(lastEntity, componentType);
                                mgr.SetComponentRaw(lastEntity, cmd->ComponentTypeIndex, (cmd + 1), cmd->ComponentSize);
                            }
                            break;

                        case Command.SetComponentImplicit:
                            {
                                var cmd = (EntityComponentCommand*)header;
                                //var componentType = (ComponentType)TypeManager.GetType(cmd->ComponentTypeIndex);
                                mgr.SetComponentRaw(lastEntity, cmd->ComponentTypeIndex, (cmd + 1), cmd->ComponentSize);
                            }
                            break;
                    }

                    off += header->TotalSize;
                }

                head = head->Next;
            }
        }
    }
}
