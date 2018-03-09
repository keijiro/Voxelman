using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Assertions;

namespace Unity.Entities
{
    struct ComponentTypeInArchetype
    {
        public readonly int TypeIndex;
        public readonly int FixedArrayLength;

        public bool IsFixedArray => FixedArrayLength != -1;
        public int  FixedArrayLengthMultiplier => FixedArrayLength != -1 ? FixedArrayLength : 1;

        public ComponentTypeInArchetype(ComponentType type)
        {
            TypeIndex = type.TypeIndex;
            FixedArrayLength = type.FixedArrayLength;
        }

        public static bool operator ==(ComponentTypeInArchetype lhs, ComponentTypeInArchetype rhs)
        {
            return lhs.TypeIndex == rhs.TypeIndex && lhs.FixedArrayLength == rhs.FixedArrayLength;
        }
        public static bool operator !=(ComponentTypeInArchetype lhs, ComponentTypeInArchetype rhs)
        {
            return lhs.TypeIndex != rhs.TypeIndex || lhs.FixedArrayLength != rhs.FixedArrayLength;
        }
        public static bool operator <(ComponentTypeInArchetype lhs, ComponentTypeInArchetype rhs)
        {
            return lhs.TypeIndex != rhs.TypeIndex ? lhs.TypeIndex < rhs.TypeIndex : lhs.FixedArrayLength < rhs.FixedArrayLength;
        }
        public static bool operator >(ComponentTypeInArchetype lhs, ComponentTypeInArchetype rhs)
        {
            return lhs.TypeIndex != rhs.TypeIndex ? lhs.TypeIndex > rhs.TypeIndex : lhs.FixedArrayLength > rhs.FixedArrayLength;
        }

        public static unsafe bool CompareArray(ComponentTypeInArchetype* type1, int typeCount1, ComponentTypeInArchetype* type2, int typeCount2)
        {
            if (typeCount1 != typeCount2)
                return false;
            for (var i = 0; i < typeCount1; ++i)
            {
                if (type1[i] != type2[i])
                    return false;
            }
            return true;
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        public override string ToString()
        {
            ComponentType type;
            type.FixedArrayLength = FixedArrayLength;
            type.TypeIndex = TypeIndex;
            type.AccessModeType = ComponentType.AccessMode.ReadWrite;
            return type.ToString();
        }
#endif
        public override bool Equals(object obj)
        {
            if (obj is ComponentTypeInArchetype)
            {
                return (ComponentTypeInArchetype) obj == this;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (TypeIndex * 5819) ^ FixedArrayLength;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct Chunk
    {
        // NOTE: Order of the UnsafeLinkedListNode is required to be this in order
        //       to allow for casting & grabbing Chunk* from nodes...
        public UnsafeLinkedListNode  ChunkListNode;                 // 16 | 8 
        public UnsafeLinkedListNode  ChunkListWithEmptySlotsNode;   // 32 | 16

        public Archetype*            Archetype;                     // 40 | 20
        public int* 		         SharedComponentValueArray;     // 48 | 24

        // This is meant as read-only.
        // ArchetypeManager.SetChunkCount should be used to change the count.
        public int 		             Count;                         // 52 | 28
        public int 		             Capacity;                      // 56 | 32

        public int                   ManagedArrayIndex;             // 60 | 36

        public int                   Padding0;                      // 64 | 40
        public void*                 Padding1;                      // 72 | 44
        public void*                 Padding2;                      // 80 | 48

        
        // Component data buffer
        public fixed byte 		     Buffer[4];


        public const int kChunkSize = 16 * 1024;
        public const int kMaximumEntitiesPerChunk = kChunkSize / 8;

        public static int GetChunkBufferSize(int numSharedComponents)
        {
            var bufferSize = kChunkSize - (sizeof(Chunk) - 4 + numSharedComponents * sizeof(int));
            return bufferSize;
        }

        public static int GetSharedComponentOffset(int numSharedComponents)
        {
            return kChunkSize - numSharedComponents * sizeof(int);
        }

        public bool MatchesFilter(MatchingArchetypes* match, int* filteredSharedComponents)
        {
            var sharedComponentsInChunk = SharedComponentValueArray;
            var filteredCount = filteredSharedComponents[0];
            var filtered = filteredSharedComponents + 1;
            for(var i=0; i<filteredCount; ++i)
            {
                var componetIndexInComponentGroup = filtered[i * 2];
                var sharedComponentIndex = filtered[i * 2 + 1];
                var componentIndexInArcheType = match->TypeIndexInArchetypeArray[componetIndexInComponentGroup];
                var componentIndexInChunk = match->Archetype->SharedComponentOffset[componentIndexInArcheType];
                if (sharedComponentsInChunk[componentIndexInChunk] != sharedComponentIndex)
                    return false;
            }

            return true;
        }
    }

    unsafe struct Archetype
    {
        public UnsafeLinkedListNode         ChunkList;
        public UnsafeLinkedListNode         ChunkListWithEmptySlots;

        public int                          EntityCount;
        public int                          ChunkCapacity;

        public ComponentTypeInArchetype*    Types;
        public int              		    TypesCount;

        // Index matches archetype types
        public int*   	                    Offsets;
        public int*                         SizeOfs;

        public int*                         ManagedArrayOffset;
        public int                          NumManagedArrays;

        public int*                         SharedComponentOffset;
        public int                          NumSharedComponents;

        public Archetype*                   PrevArchetype;
    }

    unsafe class ArchetypeManager : IDisposable
    {
        NativeMultiHashMap<uint, IntPtr>    m_TypeLookup;
        ChunkAllocator                      m_ArchetypeChunkAllocator;

        internal Archetype*                 m_LastArchetype;

        readonly SharedComponentDataManager m_SharedComponentManager;
        readonly UnsafeLinkedListNode*      m_EmptyChunkPool;

        struct ManagedArrayStorage
        {
            // For patching when we start releasing chunks
            public Chunk*    Chunk;
            public object[]  ManagedArray;
        }

        readonly List<ManagedArrayStorage> m_ManagedArrays = new List<ManagedArrayStorage>();

        public ArchetypeManager(SharedComponentDataManager sharedComponentManager)
        {
            m_SharedComponentManager = sharedComponentManager;
            m_TypeLookup = new NativeMultiHashMap<uint, IntPtr>(256, Allocator.Persistent);

            m_EmptyChunkPool = (UnsafeLinkedListNode*)m_ArchetypeChunkAllocator.Allocate(sizeof(UnsafeLinkedListNode), UnsafeUtility.AlignOf<UnsafeLinkedListNode>());
            UnsafeLinkedListNode.InitializeList(m_EmptyChunkPool);

#if UNITY_ASSERTIONS
            // Buffer should be 16 byte aligned to ensure component data layout itself can gurantee being aligned
            var offset = UnsafeUtility.GetFieldOffset(typeof(Chunk).GetField("Buffer"));
            Assert.IsTrue(offset % 16 == 0, "Chunk buffer must be 16 byte aligned");
#endif
        }

        public void Dispose()
        {
            // Free all allocated chunks for all allocated archetypes
            while (m_LastArchetype != null)
            {
                while (!m_LastArchetype->ChunkList.IsEmpty)
                {
                    var chunk = m_LastArchetype->ChunkList.Begin;
                    chunk->Remove();
                    UnsafeUtility.Free(chunk, Allocator.Persistent);
                }
                m_LastArchetype = m_LastArchetype->PrevArchetype;
            }

            // And all pooled chunks
            while (!m_EmptyChunkPool->IsEmpty)
            {
                var chunk = m_EmptyChunkPool->Begin;
                chunk->Remove();
                UnsafeUtility.Free(chunk, Allocator.Persistent);
            }

            m_TypeLookup.Dispose();
            m_ArchetypeChunkAllocator.Dispose();
        }

        [System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        public static void AssertArchetypeComponents(ComponentTypeInArchetype* types, int count)
        {
            if (count < 1)
                throw new ArgumentException($"Invalid component count");
            if (types[0].TypeIndex != TypeManager.GetTypeIndex<Entity>())
                throw new ArgumentException($"The Entity ID must always be the first component");

            for (var i = 1; i < count; i++)
            {
                if (!TypeManager.IsValidComponentTypeForArchetype(types[i].TypeIndex, types[i].IsFixedArray))
                    throw new ArgumentException($"{types[i]} is not a valid component type.");
                if (types[i - 1].TypeIndex == types[i].TypeIndex)
                    throw new ArgumentException($"It is not allowed to have two components of the same type on the same entity. ({types[i-1]} and {types[i]})");
            }
        }

        public Archetype* GetExistingArchetype(ComponentTypeInArchetype* types, int count)
        {
            IntPtr typePtr;
            NativeMultiHashMapIterator<uint> it;

            if (!m_TypeLookup.TryGetFirstValue(GetHash(types, count), out typePtr, out it))
                return null;

            do
            {
                var type = (Archetype*)typePtr;
                if (ComponentTypeInArchetype.CompareArray(type->Types, type->TypesCount, types, count))
                    return type;
            }
            while (m_TypeLookup.TryGetNextValue(out typePtr, ref it));

            return null;
        }

        static uint GetHash(ComponentTypeInArchetype* types, int count)
        {
            var hash = HashUtility.Fletcher32((ushort*)types, count * sizeof(ComponentTypeInArchetype) / sizeof(ushort));
            return hash;
        }

        public Archetype* GetOrCreateArchetype(ComponentTypeInArchetype* types, int count, EntityGroupManager groupManager)
        {
            var type = GetExistingArchetype(types, count);
            if (type != null)
                return type;

            AssertArchetypeComponents(types, count);

            // This is a new archetype, allocate it and add it to the hash map
            type = (Archetype*)m_ArchetypeChunkAllocator.Allocate(sizeof(Archetype), 8);
            type->TypesCount = count;
            type->Types = (ComponentTypeInArchetype*)m_ArchetypeChunkAllocator.Construct(sizeof(ComponentTypeInArchetype) * count, 4, types);
            type->EntityCount = 0;

            type->NumSharedComponents = 0;
            type->SharedComponentOffset = null;

            for (var i = 0; i < count; ++i)
            {
                if (TypeManager.GetComponentType(types[i].TypeIndex).Category == TypeManager.TypeCategory.ISharedComponentData)
                    ++type->NumSharedComponents;
            }

            var chunkDataSize = Chunk.GetChunkBufferSize(type->NumSharedComponents);

            // FIXME: proper alignment
            type->Offsets = (int*)m_ArchetypeChunkAllocator.Allocate(sizeof(int) * count, 4);
            type->SizeOfs = (int*)m_ArchetypeChunkAllocator.Allocate(sizeof(int) * count, 4);

            var bytesPerInstance = 0;

            for (var i = 0; i < count; ++i)
            {
                var cType = TypeManager.GetComponentType(types[i].TypeIndex);
                var sizeOf = cType.SizeInChunk * types[i].FixedArrayLengthMultiplier;
                type->SizeOfs[i] = sizeOf;

                bytesPerInstance += sizeOf;
            }

            type->ChunkCapacity = chunkDataSize / bytesPerInstance;
            Assert.IsTrue(Chunk.kMaximumEntitiesPerChunk >= type->ChunkCapacity);
            var usedBytes = 0;
            for (var i = 0; i < count; ++i)
            {
                var sizeOf = type->SizeOfs[i];

                type->Offsets[i] = usedBytes;

                usedBytes += sizeOf * type->ChunkCapacity;
            }
            type->NumManagedArrays = 0;
            type->ManagedArrayOffset = null;

            for (var i = 0; i < count; ++i)
            {
                if (TypeManager.GetComponentType(types[i].TypeIndex).Category == TypeManager.TypeCategory.Class)
                    ++type->NumManagedArrays;
            }

            if (type->NumManagedArrays > 0)
            {
                type->ManagedArrayOffset = (int*)m_ArchetypeChunkAllocator.Allocate (sizeof(int) * count, 4);
                var mi = 0;
                for (var i = 0; i < count; ++i)
                {
                    var cType = TypeManager.GetComponentType(types[i].TypeIndex);
                    if (cType.Category == TypeManager.TypeCategory.Class)
                        type->ManagedArrayOffset[i] = mi++;
                    else
                        type->ManagedArrayOffset[i] = -1;
                }
            }

            if (type->NumSharedComponents > 0)
            {
                type->SharedComponentOffset = (int*)m_ArchetypeChunkAllocator.Allocate (sizeof(int) * count, 4);
                var mi = 0;
                for (var i = 0; i < count; ++i)
                {
                    var cType = TypeManager.GetComponentType(types[i].TypeIndex);
                    if (cType.Category == TypeManager.TypeCategory.ISharedComponentData)
                        type->SharedComponentOffset[i] = mi++;
                    else
                        type->SharedComponentOffset[i] = -1;
                }
            }

            // Update the list of all created archetypes
            type->PrevArchetype = m_LastArchetype;
            m_LastArchetype = type;

            UnsafeLinkedListNode.InitializeList(&type->ChunkList);
            UnsafeLinkedListNode.InitializeList(&type->ChunkListWithEmptySlots);

            m_TypeLookup.Add(GetHash(types, count), (IntPtr)type);

            groupManager.OnArchetypeAdded(type);

            return type;
        }

        public static Chunk* GetChunkFromEmptySlotNode(UnsafeLinkedListNode* node)
        {
            return (Chunk*) (node - 1);
        }

        public Chunk* AllocateChunk(Archetype* archetype, int* sharedComponentDataIndices)
        {
            var buffer = (byte*) UnsafeUtility.Malloc(Chunk.kChunkSize, 64, Allocator.Persistent);
            var chunk = (Chunk*)buffer;
            ConstructChunk(archetype, chunk, sharedComponentDataIndices);
            return chunk;
        }

        public static void CopySharedComponentDataIndexArray(int* dest, int* src, int count)
        {
            if (src == null)
            {
                for (var i = 0; i < count; ++i)
                {
                    dest[i] = 0;
                }
            }
            else
            {
                for (var i = 0; i < count; ++i)
                {
                    dest[i] = src[i];
                }
            }
        }

        public void ConstructChunk(Archetype* archetype, Chunk* chunk, int* sharedComponentDataIndices)
        {
            chunk->Archetype = archetype;

            chunk->Count = 0;
            chunk->Capacity = archetype->ChunkCapacity;
            chunk->ChunkListNode = new UnsafeLinkedListNode();
            chunk->ChunkListWithEmptySlotsNode = new UnsafeLinkedListNode();
            chunk->SharedComponentValueArray = (int*)((byte*)(chunk) + Chunk.GetSharedComponentOffset(archetype->NumSharedComponents));

            archetype->ChunkList.Add(&chunk->ChunkListNode);
            archetype->ChunkListWithEmptySlots.Add(&chunk->ChunkListWithEmptySlotsNode);

            Assert.IsTrue(!archetype->ChunkList.IsEmpty);
            Assert.IsTrue(!archetype->ChunkListWithEmptySlots.IsEmpty);

            Assert.IsTrue(chunk == (Chunk*)(archetype->ChunkList.Back));
            Assert.IsTrue(chunk == GetChunkFromEmptySlotNode(archetype->ChunkListWithEmptySlots.Back));

            if (archetype->NumManagedArrays > 0)
            {
                chunk->ManagedArrayIndex = m_ManagedArrays.Count;
                var man = new ManagedArrayStorage();
                man.Chunk = chunk;
                man.ManagedArray = new object[archetype->NumManagedArrays * chunk->Capacity];
                m_ManagedArrays.Add(man);
            }
            else
                chunk->ManagedArrayIndex = -1;


            if (archetype->NumSharedComponents <= 0)
                return;

            var sharedComponentValueArray = chunk->SharedComponentValueArray;
            CopySharedComponentDataIndexArray(sharedComponentValueArray, sharedComponentDataIndices, chunk->Archetype->NumSharedComponents);

            if (sharedComponentDataIndices == null)
                return;

            for (var i = 0; i < archetype->NumSharedComponents; ++i)
                m_SharedComponentManager.AddReference(sharedComponentValueArray[i]);
        }

        static bool ChunkHasSharedComponents(Chunk* chunk, int* sharedComponentDataIndices)
        {
            var sharedComponentValueArray = chunk->SharedComponentValueArray;
            var numSharedComponents = chunk->Archetype->NumSharedComponents;
            if (sharedComponentDataIndices == null)
            {
                for (var i = 0; i < numSharedComponents; ++i)
                {
                    if (sharedComponentValueArray[i] != 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (var i = 0; i < numSharedComponents; ++i)
                {
                    if (sharedComponentValueArray[i] != sharedComponentDataIndices[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Chunk* GetChunkWithEmptySlots(Archetype* archetype, int* sharedComponentDataIndices)
        {
            // Try existing archetype chunks
            if (!archetype->ChunkListWithEmptySlots.IsEmpty)
            {
                if (archetype->NumSharedComponents == 0)
                {
                    var chunk = GetChunkFromEmptySlotNode(archetype->ChunkListWithEmptySlots.Begin);
                    Assert.AreNotEqual(chunk->Count, chunk->Capacity);
                    return chunk;
                }

                var end = archetype->ChunkListWithEmptySlots.End;
                for (var it = archetype->ChunkListWithEmptySlots.Begin; it != end; it = it->Next)
                {
                    var chunk = GetChunkFromEmptySlotNode(it);
                    Assert.AreNotEqual(chunk->Count, chunk->Capacity);
                    if (ChunkHasSharedComponents(chunk, sharedComponentDataIndices))
                    {
                        return chunk;
                    }
                }
            }

            // Try empty chunk pool
            if (m_EmptyChunkPool->IsEmpty)
                return AllocateChunk(archetype, sharedComponentDataIndices);

            var pooledChunk = (Chunk*)m_EmptyChunkPool->Begin;
            pooledChunk->ChunkListNode.Remove();

            ConstructChunk(archetype, pooledChunk, sharedComponentDataIndices);
            return pooledChunk;

            // Allocate new chunk
        }

        public int AllocateIntoChunk(Chunk* chunk)
        {
            int outIndex;
            var res = AllocateIntoChunk(chunk, 1, out outIndex);
            Assert.AreEqual(1, res);
            return outIndex;
        }

        public int AllocateIntoChunk(Chunk* chunk, int count, out int outIndex)
        {
            var allocatedCount = Math.Min(chunk->Capacity - chunk->Count, count);
            outIndex = chunk->Count;
            SetChunkCount(chunk, chunk->Count + allocatedCount);
            chunk->Archetype->EntityCount += allocatedCount;
            return allocatedCount;
        }

        public void SetChunkCount(Chunk* chunk, int newCount)
        {
            Assert.AreNotEqual(newCount, chunk->Count);

            var capacity = chunk->Capacity;

            // Chunk released to empty chunk pool
            if (newCount == 0)
            {
                //@TODO: Support pooling when there are managed arrays...
                if (chunk->Archetype->NumManagedArrays == 0)
                {
                    //Remove references to shared components
                    if (chunk->Archetype->NumSharedComponents > 0)
                    {
                        var sharedComponentValueArray = chunk->SharedComponentValueArray;

                        for (var i = 0; i < chunk->Archetype->NumSharedComponents; ++i)
                        {
                            m_SharedComponentManager.RemoveReference(sharedComponentValueArray[i]);
                        }
                    }

                    chunk->Archetype = null;
                    chunk->ChunkListNode.Remove();
                    chunk->ChunkListWithEmptySlotsNode.Remove();

                    m_EmptyChunkPool->Add(&chunk->ChunkListNode);
                }
            }
            // Chunk is now full
            else if (newCount == capacity)
            {
                chunk->ChunkListWithEmptySlotsNode.Remove();
            }
            // Chunk is no longer full
            else if (chunk->Count == capacity)
            {
                Assert.IsTrue(newCount < chunk->Count);

                chunk->Archetype->ChunkListWithEmptySlots.Add(&chunk->ChunkListWithEmptySlotsNode);
            }

            chunk->Count = newCount;
        }

        public object GetManagedObject(Chunk* chunk, ComponentType type, int index)
        {
            var typeOfs = ChunkDataUtility.GetIndexInTypeArray(chunk->Archetype, type.TypeIndex);
            if (typeOfs < 0 || chunk->Archetype->ManagedArrayOffset[typeOfs] < 0)
                throw new InvalidOperationException("Trying to get managed object for non existing component");
            return GetManagedObject(chunk, typeOfs, index);
        }

        internal object GetManagedObject(Chunk* chunk, int type, int index)
        {
            var managedStart = chunk->Archetype->ManagedArrayOffset[type] * chunk->Capacity;
            return m_ManagedArrays[chunk->ManagedArrayIndex].ManagedArray[index + managedStart];
        }

        public object[] GetManagedObjectRange(Chunk* chunk, int type, out int rangeStart, out int rangeLength)
        {
            rangeStart = chunk->Archetype->ManagedArrayOffset[type] * chunk->Capacity;
            rangeLength = chunk->Count;
            return m_ManagedArrays[chunk->ManagedArrayIndex].ManagedArray;
        }

        public void SetManagedObject(Chunk* chunk, int type, int index, object val)
        {
            var managedStart = chunk->Archetype->ManagedArrayOffset[type] * chunk->Capacity;
            m_ManagedArrays[chunk->ManagedArrayIndex].ManagedArray[index + managedStart] = val;
        }

        public void SetManagedObject(Chunk* chunk, ComponentType type, int index, object val)
        {
            var typeOfs = ChunkDataUtility.GetIndexInTypeArray(chunk->Archetype, type.TypeIndex);
            if (typeOfs < 0 || chunk->Archetype->ManagedArrayOffset[typeOfs] < 0)
                throw new InvalidOperationException("Trying to set managed object for non existing component");
            SetManagedObject(chunk, typeOfs, index, val);
        }

        public static void MoveChunks(ArchetypeManager srcArchetypeManager, EntityDataManager* srcEntityDataManager, SharedComponentDataManager srcSharedComponents, ArchetypeManager dstArchetypeManager, EntityGroupManager dstGroupManager, SharedComponentDataManager dstSharedComponentDataManager, EntityDataManager* dstEntityDataManager, SharedComponentDataManager dstSharedComponents)
        {
            var entitiesArray = new NativeArray<Entity>(Chunk.kMaximumEntitiesPerChunk, Allocator.Temp);
            var entitiesPtr = (Entity*) entitiesArray.GetUnsafePtr();

            var srcArchetype = srcArchetypeManager.m_LastArchetype;
            while (srcArchetype != null)
            {
                if (srcArchetype->EntityCount != 0)
                {
                    if (srcArchetype->NumManagedArrays != 0)
                        throw new ArgumentException("MoveEntitiesFrom is not supported with managed arrays");
                    var dstArchetype = dstArchetypeManager.GetOrCreateArchetype(srcArchetype->Types, srcArchetype->TypesCount, dstGroupManager);

                    for (var c = srcArchetype->ChunkList.Begin;c != srcArchetype->ChunkList.End;c = c->Next)
                    {
                        Chunk* chunk = (Chunk*) c;

                        EntityDataManager.FreeDataEntitiesInChunk(srcEntityDataManager, chunk, chunk->Count);
                        dstEntityDataManager->AllocateEntities(dstArchetype, chunk, 0, chunk->Count, entitiesPtr);

                        chunk->Archetype = dstArchetype;

                        if (dstArchetype->NumSharedComponents > 0)
                            dstSharedComponents.MoveSharedComponents(srcSharedComponents, chunk->SharedComponentValueArray, dstArchetype->NumSharedComponents);
                    }

                    //@TODO: Patch Entity references in IComponentData...

                    UnsafeLinkedListNode.InsertListBefore(dstArchetype->ChunkList.End, &srcArchetype->ChunkList);
                    UnsafeLinkedListNode.InsertListBefore(dstArchetype->ChunkListWithEmptySlots.End, &srcArchetype->ChunkListWithEmptySlots);

                    dstArchetype->EntityCount += srcArchetype->EntityCount;
                    srcArchetype->EntityCount = 0;
                }

                srcArchetype = srcArchetype->PrevArchetype;
            }
            entitiesArray.Dispose();
        }

        public int CheckInternalConsistency()
        {
            var archetype = m_LastArchetype;
            var totalCount = 0;
            while (archetype != null)
            {
                var countInArchetype = 0;
                for (var c = archetype->ChunkList.Begin;c != archetype->ChunkList.End;c = c->Next)
                {
                    var chunk = (Chunk*) c;
                    Assert.IsTrue(chunk->Archetype == archetype);
                    Assert.IsTrue(chunk->Capacity >= chunk->Count);
                    Assert.AreEqual(chunk->ChunkListWithEmptySlotsNode.IsInList, chunk->Capacity != chunk->Count);

                    countInArchetype += chunk->Count;
                }

                Assert.AreEqual(countInArchetype, archetype->EntityCount);

                totalCount += countInArchetype;
                archetype = archetype->PrevArchetype;
            }

            return totalCount;
        }

        internal SharedComponentDataManager GetSharedComponentDataManager()
        {
            return m_SharedComponentManager;
        }
    }
}
