using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Collections;

namespace Unity.Entities
{
    unsafe class ComponentJobSafetyManager
    {
        struct ComponentSafetyHandle
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            public AtomicSafetyHandle SafetyHandle;
#endif
            public JobHandle WriteFence;
            public int NumReadFences;
        }

        const int kMaxReadJobHandles = 17;
        const int kMaxTypes = TypeManager.MaximumTypesCount;

        bool m_HasCleanHandles;
        bool m_IsInTransaction;

        JobHandle* m_ReadJobFences;
        ComponentSafetyHandle* m_ComponentSafetyHandles;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        readonly AtomicSafetyHandle m_TempSafety;
        AtomicSafetyHandle m_ExclusiveTransactionSafety;
#endif

        JobHandle m_ExclusiveTransactionDependency;

        readonly JobHandle* m_JobDependencyCombineBuffer;
        readonly int m_JobDependencyCombineBufferCount;

        public ComponentJobSafetyManager()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_TempSafety = AtomicSafetyHandle.Create();
#endif


            m_ReadJobFences = (JobHandle*) UnsafeUtility.Malloc(sizeof(JobHandle) * kMaxReadJobHandles * kMaxTypes, 16,
                Allocator.Persistent);
            UnsafeUtility.MemClear(m_ReadJobFences, sizeof(JobHandle) * kMaxReadJobHandles * kMaxTypes);

            m_ComponentSafetyHandles =
                (ComponentSafetyHandle*) UnsafeUtility.Malloc(sizeof(ComponentSafetyHandle) * kMaxTypes, 16,
                    Allocator.Persistent);
            UnsafeUtility.MemClear(m_ComponentSafetyHandles, sizeof(ComponentSafetyHandle) * kMaxTypes);

            m_JobDependencyCombineBufferCount = 4 * 1024;
            m_JobDependencyCombineBuffer = (JobHandle*) UnsafeUtility.Malloc(
                sizeof(ComponentSafetyHandle) * m_JobDependencyCombineBufferCount, 16, Allocator.Persistent);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i != kMaxTypes; i++)
            {
                m_ComponentSafetyHandles[i].SafetyHandle = AtomicSafetyHandle.Create();
                AtomicSafetyHandle.SetAllowSecondaryVersionWriting(m_ComponentSafetyHandles[i].SafetyHandle, false);
            }
#endif

            m_HasCleanHandles = true;
        }

        //@TODO: Optimize as one function call to in batch bump version on every single handle...
        public void CompleteAllJobsAndInvalidateArrays()
        {
            if (m_HasCleanHandles)
                return;

            UnityEngine.Profiling.Profiler.BeginSample("CompleteAllJobsAndInvalidateArrays");

            var count = TypeManager.GetTypeCount();
            for (var t = 0; t != count; t++)
            {
                m_ComponentSafetyHandles[t].WriteFence.Complete();

                var readFencesCount = m_ComponentSafetyHandles[t].NumReadFences;
                var readFences = m_ReadJobFences + t * kMaxReadJobHandles;
                for (var r = 0; r != readFencesCount; r++)
                    readFences[r].Complete();
                m_ComponentSafetyHandles[t].NumReadFences = 0;
            }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i != count; i++)
                AtomicSafetyHandle.CheckDeallocateAndThrow(m_ComponentSafetyHandles[i].SafetyHandle);

            for (var i = 0; i != count; i++)
            {
                AtomicSafetyHandle.Release(m_ComponentSafetyHandles[i].SafetyHandle);
                m_ComponentSafetyHandles[i].SafetyHandle = AtomicSafetyHandle.Create();
                AtomicSafetyHandle.SetAllowSecondaryVersionWriting(m_ComponentSafetyHandles[i].SafetyHandle, false);
            }
#endif

            m_HasCleanHandles = true;

            UnityEngine.Profiling.Profiler.EndSample();
        }

        public void Dispose()
        {
            for (var i = 0; i < kMaxTypes; i++)
                m_ComponentSafetyHandles[i].WriteFence.Complete();

            for (var i = 0; i < kMaxTypes * kMaxReadJobHandles; i++)
                m_ReadJobFences[i].Complete();

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i < kMaxTypes; i++)
            {
                var res = AtomicSafetyHandle.EnforceAllBufferJobsHaveCompletedAndRelease(m_ComponentSafetyHandles[i]
                    .SafetyHandle);
                if (res == EnforceJobResult.DidSyncRunningJobs)
                {
                    //@TODO: EnforceAllBufferJobsHaveCompletedAndRelease should probably print the error message and locate the exact job...
                    Debug.LogError(
                        "Disposing EntityManager but a job is still running against the ComponentData. It appears the job has not been registered with JobComponentSystem.AddDependency.");
                }
            }

            AtomicSafetyHandle.Release(m_TempSafety);

#endif

            UnsafeUtility.Free(m_JobDependencyCombineBuffer, Allocator.Persistent);

            UnsafeUtility.Free(m_ComponentSafetyHandles, Allocator.Persistent);
            m_ComponentSafetyHandles = null;

            UnsafeUtility.Free(m_ReadJobFences, Allocator.Persistent);
            m_ReadJobFences = null;
        }

        public void CompleteDependencies(int* readerTypes, int readerTypesCount, int* writerTypes, int writerTypesCount)
        {
            for (var i = 0; i != writerTypesCount; i++)
                CompleteReadAndWriteDependency(writerTypes[i]);

            for (var i = 0; i != readerTypesCount; i++)
                CompleteWriteDependency(readerTypes[i]);
        }

        public JobHandle GetDependency(int* readerTypes, int readerTypesCount, int* writerTypes, int writerTypesCount)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (readerTypesCount * kMaxReadJobHandles + writerTypesCount > m_JobDependencyCombineBufferCount)
                throw new System.ArgumentException("Too many readers & writers in GetDependency");
#endif

            var count = 0;
            for (var i = 0; i != readerTypesCount; i++)
            {
                m_JobDependencyCombineBuffer[count++] = m_ComponentSafetyHandles[readerTypes[i]].WriteFence;
            }

            for (var i = 0; i != writerTypesCount; i++)
            {
                var writerType = writerTypes[i];

                m_JobDependencyCombineBuffer[count++] = m_ComponentSafetyHandles[writerType].WriteFence;

                var numReadFences = m_ComponentSafetyHandles[writerType].NumReadFences;
                for (var j = 0; j != numReadFences; j++)
                    m_JobDependencyCombineBuffer[count++] = m_ReadJobFences[writerType * kMaxReadJobHandles + j];
            }

            return Jobs.LowLevel.Unsafe.JobHandleUnsafeUtility.CombineDependencies(m_JobDependencyCombineBuffer,
                count);
        }

        public void AddDependency(int* readerTypes, int readerTypesCount, int* writerTypes, int writerTypesCount,
            JobHandle dependency)
        {
            for (var i = 0; i != writerTypesCount; i++)
            {
                var writer = writerTypes[i];
                m_ComponentSafetyHandles[writer].WriteFence = dependency;
            }

            for (var i = 0; i != readerTypesCount; i++)
            {
                var reader = readerTypes[i];
                m_ReadJobFences[reader * kMaxReadJobHandles + m_ComponentSafetyHandles[reader].NumReadFences] =
                    dependency;
                m_ComponentSafetyHandles[reader].NumReadFences++;

                if (m_ComponentSafetyHandles[reader].NumReadFences == kMaxReadJobHandles)
                    CombineReadDependencies(reader);
            }

            if (readerTypesCount != 0 || writerTypesCount != 0)
                m_HasCleanHandles = false;
        }

        public void CompleteWriteDependency(int type)
        {
            m_ComponentSafetyHandles[type].WriteFence.Complete();
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckReadAndThrow(m_ComponentSafetyHandles[type].SafetyHandle);
#endif
        }

        public void CompleteReadAndWriteDependency(int type)
        {
            for (var i = 0; i < m_ComponentSafetyHandles[type].NumReadFences; ++i)
                m_ReadJobFences[type * kMaxReadJobHandles + i].Complete();
            m_ComponentSafetyHandles[type].NumReadFences = 0;

            m_ComponentSafetyHandles[type].WriteFence.Complete();
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(m_ComponentSafetyHandles[type].SafetyHandle);
#endif
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        public AtomicSafetyHandle GetSafetyHandle(int type, bool isReadOnly)
        {
            m_HasCleanHandles = false;
            var handle = m_ComponentSafetyHandles[type].SafetyHandle;
            if (isReadOnly)
                AtomicSafetyHandle.UseSecondaryVersion(ref handle);

            return handle;
        }
#endif

        void CombineReadDependencies(int type)
        {
            var combined = Jobs.LowLevel.Unsafe.JobHandleUnsafeUtility.CombineDependencies(
                m_ReadJobFences + type * kMaxReadJobHandles, m_ComponentSafetyHandles[type].NumReadFences);

            m_ReadJobFences[type * kMaxReadJobHandles] = combined;
            m_ComponentSafetyHandles[type].NumReadFences = 1;
        }

        public bool IsInTransaction => m_IsInTransaction;

        public JobHandle ExclusiveTransactionDependency
        {
            get
            {
                return m_ExclusiveTransactionDependency;
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if (!m_IsInTransaction)
                    throw new System.InvalidOperationException("EntityManager.TransactionDependency can only after EntityManager.BeginExclusiveEntityTransaction has been called.");

                if (!JobHandle.CheckFenceIsDependencyOrDidSyncFence(m_ExclusiveTransactionDependency, value))
                    throw new System.InvalidOperationException("EntityManager.TransactionDependency must depend on the Entity Transaction job.");
#endif
                m_ExclusiveTransactionDependency = value;
            }
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        public AtomicSafetyHandle ExclusiveTransactionSafety => m_ExclusiveTransactionSafety;
#endif

        public void BeginExclusiveTransaction()
        {
            if (m_IsInTransaction)
                return;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i != TypeManager.GetTypeCount(); i++)
                AtomicSafetyHandle.CheckDeallocateAndThrow(m_ComponentSafetyHandles[i].SafetyHandle);
#endif

            m_IsInTransaction = true;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_ExclusiveTransactionSafety = AtomicSafetyHandle.Create();
#endif
            m_ExclusiveTransactionDependency = GetAllDependencies();

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i != TypeManager.GetTypeCount(); i++)
                AtomicSafetyHandle.Release(m_ComponentSafetyHandles[i].SafetyHandle);
#endif

        }

        public void EndExclusiveTransaction()
        {
            if (!m_IsInTransaction)
                return;

            m_ExclusiveTransactionDependency.Complete();

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            var res = AtomicSafetyHandle.EnforceAllBufferJobsHaveCompletedAndRelease(m_ExclusiveTransactionSafety);
            if (res != EnforceJobResult.AllJobsAlreadySynced)
                //@TODO: Better message
                Debug.LogError("ExclusiveEntityTransaction job has not been registered");
#endif
            m_IsInTransaction = false;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var i = 0; i != TypeManager.GetTypeCount(); i++)
            {
                m_ComponentSafetyHandles[i].SafetyHandle = AtomicSafetyHandle.Create();
                AtomicSafetyHandle.SetAllowSecondaryVersionWriting(m_ComponentSafetyHandles[i].SafetyHandle, false);
            }
#endif
        }

        JobHandle GetAllDependencies()
        {
            var jobHandles = new NativeArray<JobHandle>(TypeManager.GetTypeCount() * (kMaxReadJobHandles + 1), Allocator.Temp);

            var count = 0;
            for (var i = 0; i != TypeManager.GetTypeCount(); i++)
            {
                jobHandles[count++] = m_ComponentSafetyHandles[i].WriteFence;

                var numReadFences = m_ComponentSafetyHandles[i].NumReadFences;
                for (var j = 0; j != numReadFences; j++)
                    jobHandles[count++] = m_ReadJobFences[i * kMaxReadJobHandles + j];
            }

            var combined = JobHandle.CombineDependencies(jobHandles);
            jobHandles.Dispose();

            return combined;
        }
    }
}
