using System;
using System.Reflection;
using Unity.Collections;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Entities
{
    public unsafe abstract class ComponentSystemBase : ScriptBehaviourManager
    {
        InjectComponentGroupData[] 			m_InjectedComponentGroups;
        InjectFromEntityData                m_InjectFromEntityData;

        ComponentGroupArrayStaticCache[] 	m_CachedComponentGroupArrays;
        ComponentGroup[] 				    m_ComponentGroups;
        
        NativeList<int>                     m_JobDependencyForReadingManagers;
        NativeList<int>                     m_JobDependencyForWritingManagers;
        
        internal int*                       m_JobDependencyForReadingManagersPtr;
        internal int                        m_JobDependencyForReadingManagersLength;

        internal int*                       m_JobDependencyForWritingManagersPtr;
        internal int                        m_JobDependencyForWritingManagersLength;
        
        internal ComponentJobSafetyManager  m_SafetyManager;
        EntityManager                       m_EntityManager;
        World                               m_World;
        
        bool                                m_AlwaysUpdateSystem;

        public ComponentGroup[] 			ComponentGroups => m_ComponentGroups;

        protected bool ShouldRunSystem()
        {
            if (m_AlwaysUpdateSystem)
                return true;

            int length = m_ComponentGroups.Length;

            if (length == 0)
                return true;
            
            // If all the groups are empty, skip it.
            // (There’s no way to know what they key value is without other markup)
            for (int i = 0;i != length;i++)
            {
                if (!m_ComponentGroups[i].IsEmpty)
                    return true;
            }

            return false;
        }

        protected override void OnBeforeCreateManagerInternal(World world, int capacity)
        {
            m_World = world;
            m_EntityManager = world.GetOrCreateManager<EntityManager>();
            m_SafetyManager = m_EntityManager.ComponentJobSafetyManager;
            m_AlwaysUpdateSystem = GetType().GetCustomAttributes(typeof(AlwaysUpdateSystemAttribute), true).Length != 0;

            m_ComponentGroups = new ComponentGroup[0];
            m_CachedComponentGroupArrays = new ComponentGroupArrayStaticCache[0];
            m_JobDependencyForReadingManagers = new NativeList<int>(10, Allocator.Persistent);
            m_JobDependencyForWritingManagers = new NativeList<int>(10, Allocator.Persistent);
            
            ComponentSystemInjection.Inject(this, world, m_EntityManager, out m_InjectedComponentGroups, out m_InjectFromEntityData);
            m_InjectFromEntityData.ExtractJobDependencyTypes(this);

            InjectNestedIJobProcessComponentDataJobs();
            
            UpdateInjectedComponentGroups();
        }

        void InjectNestedIJobProcessComponentDataJobs()
        {
            // Create ComponentGroup for all nested IJobProcessComponentData jobs
            foreach (var nestedType in GetType().GetNestedTypes(BindingFlags.FlattenHierarchy | BindingFlags.NonPublic))
            {
                var componentTypes = IJobProcessComponentDataUtility.GetComponentTypes(nestedType);
                if (componentTypes != null)
                    GetComponentGroup(componentTypes);
            }
        }
        
        protected sealed override void OnAfterDestroyManagerInternal()
        {

            foreach (var group in m_ComponentGroups)
            {
                #if ENABLE_UNITY_COLLECTIONS_CHECKS
                group.DisallowDisposing = null;
                #endif
                group.Dispose();
            }
            m_ComponentGroups = null;
            m_InjectedComponentGroups = null;
            m_CachedComponentGroupArrays = null;

            if (m_JobDependencyForReadingManagers.IsCreated)
                m_JobDependencyForReadingManagers.Dispose();
            if (m_JobDependencyForWritingManagers.IsCreated)
                m_JobDependencyForWritingManagers.Dispose();
        }

        protected override void OnBeforeDestroyManagerInternal()
        {
            CompleteDependencyInternal();
            UpdateInjectedComponentGroups();
        }

        protected EntityManager EntityManager => m_EntityManager;
        protected World World => m_World;

        // TODO: this should be made part of UnityEngine?
        static void ArrayUtilityAdd<T>(ref T[] array, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        internal void AddReaderWriter(ComponentType componentType)
        {
            if (CalculateReaderWriterDependency.Add(componentType, m_JobDependencyForReadingManagers, m_JobDependencyForWritingManagers))
            {
                m_JobDependencyForReadingManagersPtr = (int*)m_JobDependencyForReadingManagers.GetUnsafePtr();
                m_JobDependencyForWritingManagersPtr = (int*)m_JobDependencyForWritingManagers.GetUnsafePtr();

                m_JobDependencyForReadingManagersLength = m_JobDependencyForReadingManagers.Length;
                m_JobDependencyForWritingManagersLength = m_JobDependencyForWritingManagers.Length;
                CompleteDependencyInternal();
            }
        }
        
        internal ComponentGroup GetComponentGroup(ComponentType* componentTypes, int count)
        {
            for (var i = 0; i != m_ComponentGroups.Length; i++)
            {
                if (m_ComponentGroups[i].CompareComponents(componentTypes, count))
                    return m_ComponentGroups[i];
            }

            var group = EntityManager.CreateComponentGroup(componentTypes, count);
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            group.DisallowDisposing = "ComponentGroup.Dispose() may not be called on a ComponentGroup created with ComponentSystem.GetComponentGroup. The ComponentGroup will automatically be disposed by the ComponentSystem.";
            #endif
            ArrayUtilityAdd(ref m_ComponentGroups, group);

            for (int i = 0;i != count;i++)
                AddReaderWriter(componentTypes[i]);        
            
            //@TODO: Shouldn't this sync fence on the newly depent types?

            return group;
        }

        public ComponentGroup GetComponentGroup(params ComponentType[] componentTypes)
        {
            fixed (ComponentType* typesPtr = componentTypes)
            {
                return GetComponentGroup(typesPtr, componentTypes.Length);
            }
        }
        
        public ComponentGroupArray<T> GetEntities<T>() where T : struct
        {
            for (var i = 0; i != m_CachedComponentGroupArrays.Length; i++)
            {
                if (m_CachedComponentGroupArrays[i].CachedType == typeof(T))
                    return new ComponentGroupArray<T>(m_CachedComponentGroupArrays[i]);
            }

            var cache = new ComponentGroupArrayStaticCache(typeof(T), EntityManager, this);
            ArrayUtilityAdd(ref m_CachedComponentGroupArrays, cache);
            return new ComponentGroupArray<T>(cache);
        }

        public void UpdateInjectedComponentGroups()
        {
            if (null == m_InjectedComponentGroups)
                return;

            ulong gchandle;
            var pinnedSystemPtr = (byte*)UnsafeUtility.PinGCObjectAndGetAddress(this, out gchandle);

            try
            {
                foreach (var group in m_InjectedComponentGroups)
                    group.UpdateInjection (pinnedSystemPtr);

                m_InjectFromEntityData.UpdateInjection(pinnedSystemPtr, EntityManager);
            }
            catch
            {
                UnsafeUtility.ReleaseGCObject(gchandle);
                throw;
            }
            UnsafeUtility.ReleaseGCObject(gchandle);
        }

        internal void CompleteDependencyInternal()
        {
            m_SafetyManager.CompleteDependencies(m_JobDependencyForReadingManagersPtr, m_JobDependencyForReadingManagersLength, m_JobDependencyForWritingManagersPtr, m_JobDependencyForWritingManagersLength);
        }
    }

    public abstract class ComponentSystem : ComponentSystemBase
    {
        EntityCommandBuffer m_DeferredEntities;

        public EntityCommandBuffer PostUpdateCommands => m_DeferredEntities;

        void BeforeOnUpdate()
        {
            CompleteDependencyInternal();
            UpdateInjectedComponentGroups ();

            m_DeferredEntities = new EntityCommandBuffer(Allocator.TempJob);
        }

        void AfterOnUpdate()
        {
            JobHandle.ScheduleBatchedJobs();

            m_DeferredEntities.Playback(EntityManager);
            m_DeferredEntities.Dispose();
        }

        internal sealed override void InternalUpdate()
        {
            if (ShouldRunSystem())
            {
                BeforeOnUpdate();
                OnUpdate();
                AfterOnUpdate();
            }
        }

        protected sealed override void OnBeforeCreateManagerInternal(World world, int capacity)
        {
            base.OnBeforeCreateManagerInternal(world, capacity);
        }

        protected sealed override void OnBeforeDestroyManagerInternal()
        {
            base.OnBeforeDestroyManagerInternal();
        }

        /// <summary>
        /// Called once per frame on the main thread.
        /// </summary>
        protected abstract void OnUpdate();
    }

    public abstract class JobComponentSystem : ComponentSystemBase
    {
        JobHandle m_PreviousFrameDependency;
        BarrierSystem[] m_BarrierList;

        JobHandle BeforeOnUpdate()
        {
            UpdateInjectedComponentGroups();

            // We need to wait on all previous frame dependencies, otherwise it is possible that we create infinitely long dependency chains
            // without anyone ever waiting on it
            m_PreviousFrameDependency.Complete();

            return GetDependency();
        }

        unsafe void AfterOnUpdate(JobHandle outputJob)
        {
            JobHandle.ScheduleBatchedJobs();

            AddDependencyInternal(outputJob);
            m_PreviousFrameDependency = outputJob;

            // Notify all injected barrier systems that they will need to sync on any jobs we spawned.
            // This is conservative currently - the barriers will sync on too much if we use more than one.
            for (int i = 0; i < m_BarrierList.Length; ++i)
            {
                m_BarrierList[i].AddJobHandleForProducer(outputJob);
            }

#if ENABLE_UNITY_COLLECTIONS_CHECKS

            if (!JobsUtility.JobDebuggerEnabled)
                return;

            // Check that all reading and writing jobs are a dependency of the output job, to
            // catch systems that forget to add one of their jobs to the dependency graph.
            //
            // Note that this check is not strictly needed as we would catch the mistake anyway later,
            // but checking it here means we can flag the system that has the mistake, rather than some
            // other (innocent) system that is doing things correctly.

            //@TODO: It is not ideal that we call m_SafetyManager.GetDependency,
            //       it can result in JobHandle.CombineDependencies calls.
            //       Which seems like debug code might have side-effects
            
            try
            {
                for (var index = 0; index < m_JobDependencyForReadingManagersLength; index++)
                {
                    var type = m_JobDependencyForReadingManagersPtr[index];
                    var readerDependency = m_SafetyManager.GetDependency(&type, 1, null, 0);
                    CheckJobDependencies(type, true, readerDependency);

                    var writerDependency = m_SafetyManager.GetDependency(null, 0, &type, 1);
                    CheckJobDependencies(type, false, writerDependency);
                }

                for (var index = 0; index < m_JobDependencyForWritingManagersLength; index++)
                {
                    var type = m_JobDependencyForWritingManagersPtr[index];
                    var readerDependency = m_SafetyManager.GetDependency(&type, 1, null, 0);
                    CheckJobDependencies(type, true, readerDependency);

                    var writerDependency = m_SafetyManager.GetDependency(null, 0, &type, 1);
                    CheckJobDependencies(type, false, writerDependency);
                }
            }
            catch (InvalidOperationException)
            {
                EmergencySyncAllJobs();
                throw;
            }
#endif
        }

        internal sealed override void InternalUpdate()
        {
            if (ShouldRunSystem())
            {
                var inputJob = BeforeOnUpdate();
                var outputJob = OnUpdate(inputJob);
                AfterOnUpdate(outputJob);
            }
        }

        protected sealed override void OnBeforeCreateManagerInternal(World world, int capacity)
        {
            base.OnBeforeCreateManagerInternal(world, capacity);

            m_BarrierList = ComponentSystemInjection.GetAllInjectedManagers<BarrierSystem>(this, world);
        }

        protected sealed override void OnBeforeDestroyManagerInternal()
        {
            base.OnBeforeDestroyManagerInternal();
            m_PreviousFrameDependency.Complete();
        }
        
        public ComponentDataFromEntity<T> GetComponentDataFromEntity<T>(bool isReadOnly = false) where T : struct, IComponentData
        {
            AddReaderWriter(isReadOnly ? ComponentType.ReadOnly<T>() : ComponentType.Create<T>());        
            return EntityManager.GetComponentDataFromEntity<T>(TypeManager.GetTypeIndex<T>(), isReadOnly);
        }
        
        public FixedArrayFromEntity<T> GetFixedArrayFromEntity<T>(bool isReadOnly = false) where T : struct
        {
            AddReaderWriter(isReadOnly ? ComponentType.ReadOnly<T>() : ComponentType.Create<T>());        
            return EntityManager.GetFixedArrayFromEntity<T>(TypeManager.GetTypeIndex<T>(), isReadOnly);
        }

        protected virtual JobHandle OnUpdate(JobHandle inputDeps)
        {
            return inputDeps;
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        void CheckJobDependencies(int type, bool isReading, JobHandle dependency)
        {
            var h = m_SafetyManager.GetSafetyHandle(type, true);

            unsafe
            {
                if (!isReading)
                {
                    var readerCount = AtomicSafetyHandle.GetReaderArray(h, 0, IntPtr.Zero);
                    JobHandle* readers = stackalloc JobHandle[readerCount];
                    AtomicSafetyHandle.GetReaderArray(h, readerCount, (IntPtr) readers);

                    for (var i = 0; i < readerCount; ++i)
                    {
                        if (!JobHandle.CheckFenceIsDependencyOrDidSyncFence(readers[i], dependency))
                        {
                            throw new InvalidOperationException($"The system {GetType()} reads {TypeManager.GetType(type)} via {AtomicSafetyHandle.GetReaderName(h, i)} but that type was not returned as a job dependency. To ensure correct behavior of other systems, the job or a dependency of it must be returned from the OnUpdate method.");
                        }
                    }
                }

                var writer = AtomicSafetyHandle.GetWriter(h);
                if (!JobHandle.CheckFenceIsDependencyOrDidSyncFence(writer, dependency))
                {
                    throw new InvalidOperationException($"The system {GetType()} writes {TypeManager.GetType(type)} via {AtomicSafetyHandle.GetWriterName(h)} but that was not returned as a job dependency. To ensure correct behavior of other systems, the job or a dependency of it must be returned from the OnUpdate method.");
                }
            }
        }

        unsafe void EmergencySyncAllJobs()
        {
            for (int i = 0;i != m_JobDependencyForReadingManagersLength;i++)
            {
                int type = m_JobDependencyForReadingManagersPtr[i];
                AtomicSafetyHandle.EnforceAllBufferJobsHaveCompleted(m_SafetyManager.GetSafetyHandle(type, true));
            }

            for (int i = 0;i != m_JobDependencyForWritingManagersLength;i++)
            {
                int type = m_JobDependencyForWritingManagersPtr[i];
                AtomicSafetyHandle.EnforceAllBufferJobsHaveCompleted(m_SafetyManager.GetSafetyHandle(type, true));
            }
        }
#endif

        unsafe JobHandle GetDependency ()
        {
            return m_SafetyManager.GetDependency(m_JobDependencyForReadingManagersPtr, m_JobDependencyForReadingManagersLength, m_JobDependencyForWritingManagersPtr, m_JobDependencyForWritingManagersLength);
        }

        unsafe void AddDependencyInternal(JobHandle dependency)
        {
            m_SafetyManager.AddDependency(m_JobDependencyForReadingManagersPtr, m_JobDependencyForReadingManagersLength, m_JobDependencyForWritingManagersPtr, m_JobDependencyForWritingManagersLength, dependency);
        }
    }

    public unsafe abstract class BarrierSystem : ComponentSystem
    {
        private NativeList<EntityCommandBuffer> m_PendingBuffers;
        private JobHandle m_ProducerHandle;

        public EntityCommandBuffer CreateCommandBuffer()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);

            m_PendingBuffers.Add(cmds);

            return cmds;
        }

        internal void AddJobHandleForProducer(JobHandle foo)
        {
            m_ProducerHandle = JobHandle.CombineDependencies(m_ProducerHandle, foo);
        }

        protected override void OnCreateManager(int capacity)
        {
            base.OnCreateManager(capacity);

            m_PendingBuffers = new NativeList<EntityCommandBuffer>(Allocator.Persistent);
        }

        protected override void OnDestroyManager()
        {
            m_PendingBuffers.Dispose();

            base.OnDestroyManager();
        }

        protected sealed override void OnUpdate()
        {
            m_ProducerHandle.Complete();
            m_ProducerHandle = new JobHandle();

            for (int i = 0; i < m_PendingBuffers.Length; ++i)
            {
                m_PendingBuffers[i].Playback(EntityManager);
                m_PendingBuffers[i].Dispose();
            }

            m_PendingBuffers.Clear();
        }
    }
}
