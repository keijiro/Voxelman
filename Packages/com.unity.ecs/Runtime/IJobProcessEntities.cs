using System;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    public interface IJobProcessEntities<U0> where U0 : struct
    {
        void Execute(U0 entity);
    }

    public static class ProcessEntityJobExtensions
    {
        public static unsafe JobHandle Schedule<T, U0>(this T jobData, ComponentGroupArray<U0> array, int innerloopBatchCount, JobHandle dependsOn = new JobHandle())
            where T : struct, IJobProcessEntities<U0>
            where U0 : struct
        {
            JobStruct<T, U0> fullData;
            fullData.Data = jobData;
            fullData.Array = array.m_Data;

            var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct<T, U0>.Initialize(), dependsOn, ScheduleMode.Batched);
            return JobsUtility.ScheduleParallelFor(ref scheduleParams, array.Length, innerloopBatchCount);
        }

        public static unsafe void Run<T, U0>(this T jobData, ComponentGroupArray<U0> array)
            where T : struct, IJobProcessEntities<U0>
            where U0 : struct
        {
            JobStruct<T, U0> fullData;
            fullData.Data = jobData;
            fullData.Array = array.m_Data;

            var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct<T, U0>.Initialize(), new JobHandle(), ScheduleMode.Run);
            var entityCount = array.Length;
            JobsUtility.ScheduleParallelFor(ref scheduleParams, entityCount , entityCount);
        }

        struct JobStruct<T, U0>
            where T : struct, IJobProcessEntities<U0>
            where U0 : struct
        {
            static IntPtr s_JobReflectionData;

            public ComponentGroupArrayData       Array;
            public T                             Data;

            public static IntPtr Initialize()
            {
                if (s_JobReflectionData == IntPtr.Zero)
                    s_JobReflectionData = JobsUtility.CreateJobReflectionData(typeof(JobStruct<T, U0>), typeof(T), JobType.ParallelFor, (ExecuteJobFunction)Execute);

                return s_JobReflectionData;
            }

            delegate void ExecuteJobFunction(ref JobStruct<T, U0> data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

            static unsafe void Execute(ref JobStruct<T, U0> jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                int begin;
                int end;
                var entity = default(U0);
                while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end))
                {
                    while (begin != end)
                    {
                        if (begin < jobData.Array.CacheBeginIndex || begin >= jobData.Array.CacheEndIndex)
                            jobData.Array.UpdateCache(begin);

                        var endLoop = Math.Min(end, jobData.Array.CacheEndIndex);

                        for (var i = begin; i != endLoop; i++)
                        {
                            jobData.Array.PatchPtrs(i, (byte*)UnsafeUtility.AddressOf(ref entity));
                            jobData.Data.Execute(entity);
                        }

                        begin = endLoop;
                    }
                }
            }
        }
    }
}
