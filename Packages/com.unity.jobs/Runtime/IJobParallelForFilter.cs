using System;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Jobs
{
    [JobProducerType(typeof(JobParallelIndexListExtensions.JobStructProduce<>))]
    public interface IJobParallelForFilter
    {
        bool Execute(int index);
    }

    public static class JobParallelIndexListExtensions
    {
        public struct JobStructProduce<T> where T : struct, IJobParallelForFilter
        {
            public struct JobDataWithFiltering
            {
                [NativeDisableParallelForRestriction]
                public NativeList<int> outputIndices;
                public int appendCount;
                public T data;
            }

            static public IntPtr jobReflectionData;

            public static IntPtr Initialize()
            {
                if (jobReflectionData == IntPtr.Zero)
                    // @TODO: Use parallel for job... (Need to expose combine jobs)

                    jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(JobDataWithFiltering), typeof(T), JobType.Single, (ExecuteJobFunction)Execute);

                return jobReflectionData;
            }
            public delegate void ExecuteJobFunction(ref JobDataWithFiltering data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

            // @TODO: Use parallel for job... (Need to expose combine jobs)

            public unsafe static void Execute(ref JobDataWithFiltering jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                if (jobData.appendCount == -1)
                    ExecuteFilter(ref jobData, bufferRangePatchData);
                else
                    ExecuteAppend(ref jobData, bufferRangePatchData);
            }

            public unsafe static void ExecuteAppend(ref JobDataWithFiltering jobData, System.IntPtr bufferRangePatchData)
            {
                int oldLength = jobData.outputIndices.Length;
                jobData.outputIndices.Capacity = math.max(jobData.appendCount + oldLength, jobData.outputIndices.Capacity);

                int* outputPtr = (int*)jobData.outputIndices.GetUnsafePtr();
                int outputIndex = oldLength;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
                JobsUtility.PatchBufferMinMaxRanges (bufferRangePatchData, UnsafeUtility.AddressOf (ref jobData), 0, jobData.appendCount);
#endif
                for (int i = 0;i != jobData.appendCount;i++)
                {
					if (jobData.data.Execute (i))
					{
						outputPtr[outputIndex] = i;
						outputIndex++;
					}
                }

                jobData.outputIndices.ResizeUninitialized(outputIndex);
            }

            public unsafe static void ExecuteFilter(ref JobDataWithFiltering jobData, System.IntPtr bufferRangePatchData)
            {
                int* outputPtr = (int*)jobData.outputIndices.GetUnsafePtr();
                int inputLength = jobData.outputIndices.Length;

                int outputCount = 0;
                for (int i = 0;i != inputLength;i++)
                {
                    int inputIndex = outputPtr[i];

#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    JobsUtility.PatchBufferMinMaxRanges (bufferRangePatchData, UnsafeUtility.AddressOf (ref jobData), inputIndex, 1);
#endif

                    if (jobData.data.Execute(inputIndex))
                    {
                        outputPtr[outputCount] = inputIndex;
                        outputCount++;
                    }
                }

                jobData.outputIndices.ResizeUninitialized(outputCount);
            }
        }

        unsafe static public JobHandle ScheduleAppend<T>(this T jobData, NativeList<int> indices, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = new JobHandle()) where T : struct, IJobParallelForFilter
        {
            JobStructProduce<T>.JobDataWithFiltering fullData;
            fullData.data = jobData;
            fullData.outputIndices = indices;
            fullData.appendCount = arrayLength;

            var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStructProduce<T>.Initialize(), dependsOn, ScheduleMode.Batched);

            return JobsUtility.Schedule(ref scheduleParams);
        }

        unsafe static public JobHandle ScheduleFilter<T>(this T jobData, NativeList<int> indices, int innerloopBatchCount, JobHandle dependsOn = new JobHandle()) where T : struct, IJobParallelForFilter
        {
            JobStructProduce<T>.JobDataWithFiltering fullData;
            fullData.data = jobData;
            fullData.outputIndices = indices;
            fullData.appendCount = -1;

            var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStructProduce<T>.Initialize(), dependsOn, ScheduleMode.Batched);

            return JobsUtility.Schedule(ref scheduleParams);
        }

        //@TODO: RUN
    }
}
