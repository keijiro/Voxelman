using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Assertions;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Collections.LowLevel.Unsafe;

[assembly:InternalsVisibleTo("Unity.Entities.Editor")]

namespace Unity.Entities
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class RequireComponentTagAttribute : Attribute
    {
        public Type[] TagComponents;

        public RequireComponentTagAttribute(params Type[] tagComponents)
        {
            TagComponents = tagComponents;
        }
    }

    [AttributeUsage(AttributeTargets.Struct)]
    public class RequireSubtractiveComponentAttribute : Attribute
    {
        public Type[] SubtractiveComponents;

        public RequireSubtractiveComponentAttribute(params Type[] subtractiveComponents)
        {
            SubtractiveComponents = subtractiveComponents;
        }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public interface IBaseJobProcessComponentData
    {
    }

    //@TODO: It would be nice to get rid of these interfaces completely.
    //Right now implementation needs it, but they pollute public API in annoying ways.
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public interface IBaseJobProcessComponentData_3 : IBaseJobProcessComponentData
    {
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public interface IBaseJobProcessComponentData_2 : IBaseJobProcessComponentData
    {
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public interface IBaseJobProcessComponentData_1 : IBaseJobProcessComponentData
    {
    }

    public interface IJobProcessComponentData<T0> : IBaseJobProcessComponentData_1
        where T0 : struct, IComponentData
    {
        void Execute(ref T0 data);
    }

    public interface IJobProcessComponentData<T0, T1> : IBaseJobProcessComponentData_2
        where T0 : struct, IComponentData
        where T1 : struct, IComponentData
    {
        void Execute(ref T0 data0, ref T1 data1);
    }

    public interface IJobProcessComponentData<T0, T1, T2> : IBaseJobProcessComponentData_3
        where T0 : struct, IComponentData
        where T1 : struct, IComponentData
        where T2 : struct, IComponentData
    {
        void Execute(ref T0 data0, ref T1 data1, ref T2 data2);
    }

    struct JobProcessComponentDataCache
    {
        public IntPtr              JobReflectionData;
        public IntPtr              JobReflectionDataParallelFor;
        public ComponentType[]     Types;

        public int                 ProcessTypesCount;

        public ComponentGroup      ComponentGroup;
        public ComponentSystemBase ComponentSystem;
    }

    [NativeContainer]
    [NativeContainerSupportsMinMaxWriteRestriction]

    [StructLayout(LayoutKind.Sequential)]
    struct ProcessIterationData
    {
        public ComponentChunkIterator    Iterator0;
        public ComponentChunkIterator    Iterator1;
        public ComponentChunkIterator    Iterator2;

        public int                 IsReadOnly0;
        public int                 IsReadOnly1;
        public int                 IsReadOnly2;

        public bool                m_IsParallelFor;

        public int                 m_Length;

#if ENABLE_UNITY_COLLECTIONS_CHECKS

        public int m_MinIndex;
        public int m_MaxIndex;

#pragma warning disable 414
        public int                         m_SafetyReadOnlyCount;
        public int                         m_SafetyReadWriteCount;
        public AtomicSafetyHandle          m_Safety0;
        public AtomicSafetyHandle          m_Safety1;
        public AtomicSafetyHandle          m_Safety2;
#pragma warning restore
#endif
    }

    static class IJobProcessComponentDataUtility
    {
        public static ComponentType[] GetComponentTypes(Type jobType)
        {
            var interfaceType = GetIJobProcessComponentDataInterface(jobType);
            if (interfaceType != null)
            {
                int temp;
                return GetComponentTypes(jobType, interfaceType, out temp);
            }
            else
                return null;
        }

        static ComponentType[] GetComponentTypes(Type jobType, Type interfaceType, out int processCount)
        {
            var genericArgs = interfaceType.GetGenericArguments();

            var executeMethodParameters = jobType.GetMethod("Execute").GetParameters();

            var componentTypes = new List<ComponentType>();

            for (var i = 0; i < genericArgs.Length; i++)
            {
                var isReadonly = executeMethodParameters[i].GetCustomAttribute(typeof(Unity.Collections.ReadOnlyAttribute)) != null || 
                                 executeMethodParameters[i].GetCustomAttribute(typeof(System.Runtime.CompilerServices.IsReadOnlyAttribute)) != null;
                componentTypes.Add(new ComponentType(genericArgs[i], isReadonly ? ComponentType.AccessMode.ReadOnly : ComponentType.AccessMode.ReadWrite));
            }

            var subtractive = jobType.GetCustomAttribute<RequireSubtractiveComponentAttribute>();
            if (subtractive != null)
            {
                foreach (var type in subtractive.SubtractiveComponents)
                    componentTypes.Add(ComponentType.Subtractive(type));
            }

            var requiredTags = jobType.GetCustomAttribute<RequireComponentTagAttribute>();
            if (requiredTags != null)
            {
                //@TODO: Add Special component type which doesn't capture job dependencies...
                foreach (var type in requiredTags.TagComponents)
                    componentTypes.Add(ComponentType.ReadOnly(type));
            }

            processCount = genericArgs.Length;
            return componentTypes.ToArray();
        }

        static IntPtr GetJobReflection(Type jobType, Type wrapperJobType, Type interfaceType, bool isIJobParallelFor)
        {
            Assert.AreNotEqual(null, wrapperJobType);
            Assert.AreNotEqual(null, interfaceType);
    
            var genericArgs = interfaceType.GetGenericArguments();

            var jobTypeAndGenericArgs = new List<Type>();
            jobTypeAndGenericArgs.Add(jobType);
            jobTypeAndGenericArgs.AddRange(genericArgs);
            var resolvedWrapperJobType = wrapperJobType.MakeGenericType(jobTypeAndGenericArgs.ToArray());

            object[] parameters = { isIJobParallelFor ? JobType.ParallelFor : JobType.Single };
            var reflectionDataRes = resolvedWrapperJobType.GetMethod("Initialize").Invoke(null, parameters);
            return (IntPtr)reflectionDataRes;
        }

        static Type GetIJobProcessComponentDataInterface(Type jobType)
        {
            foreach (var iType in jobType.GetInterfaces())
            {
                if (iType.Assembly == typeof(IBaseJobProcessComponentData).Assembly && iType.Name.StartsWith("IJobProcessComponentData"))
                    return iType;
            }

            return null;
        }

        unsafe internal static void Initialize(ComponentSystemBase system, Type jobType, Type wrapperJobType, bool isParallelFor, ref JobProcessComponentDataCache cache, out ProcessIterationData iterator)
        {
            if ((isParallelFor && cache.JobReflectionDataParallelFor == IntPtr.Zero) ||
                (!isParallelFor && cache.JobReflectionData == IntPtr.Zero))
            {
                var iType = GetIJobProcessComponentDataInterface(jobType);
                if (cache.Types == null)
                    cache.Types = GetComponentTypes(jobType, iType, out cache.ProcessTypesCount);

                var res = GetJobReflection(jobType, wrapperJobType, iType, isParallelFor);

                if (isParallelFor)
                    cache.JobReflectionDataParallelFor = res;
                else
                    cache.JobReflectionData = res;
            }

            if (cache.ComponentSystem != system)
            {
                cache.ComponentGroup = system.GetComponentGroup(cache.Types);
                cache.ComponentSystem = system;
            }

            var group = cache.ComponentGroup;

            // Readonly
            iterator.IsReadOnly0 = iterator.IsReadOnly1 = iterator.IsReadOnly2 = 0;
            fixed (int* isReadOnly = &iterator.IsReadOnly0)
            {
                for (var i = 0; i != cache.ProcessTypesCount; i++)
                    isReadOnly[i] = cache.Types[i].AccessModeType == ComponentType.AccessMode.ReadOnly ? 1 : 0;
            }

            // Iterator & length
            iterator.Iterator0 = default(ComponentChunkIterator);
            iterator.Iterator1 = default(ComponentChunkIterator);
            iterator.Iterator2 = default(ComponentChunkIterator);
            var length = -1;
            fixed (ComponentChunkIterator* iterators = &iterator.Iterator0)
            {
                for (var i = 0; i != cache.ProcessTypesCount; i++)
                {
                    group.GetComponentChunkIterator(out length, out iterators[i]);
                    iterators[i].IndexInComponentGroup = group.GetIndexInComponentGroup(cache.Types[i].TypeIndex);
                }
            }

            iterator.m_IsParallelFor = isParallelFor;
            iterator.m_Length = length;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            iterator.m_MaxIndex = length-1;
            iterator.m_MinIndex = 0;

            // Safety
            iterator.m_Safety0 = iterator.m_Safety1 = iterator.m_Safety2 = default(AtomicSafetyHandle);

            iterator.m_SafetyReadOnlyCount = 0;
            fixed (AtomicSafetyHandle* safety = &iterator.m_Safety0)
            {
                for (var i = 0; i != cache.ProcessTypesCount; i++)
                {
                    if (cache.Types[i].AccessModeType == ComponentType.AccessMode.ReadOnly)
                    {
                        safety[iterator.m_SafetyReadOnlyCount] = group.GetSafetyHandle(group.GetIndexInComponentGroup(cache.Types[i].TypeIndex));
                        iterator.m_SafetyReadOnlyCount++;
                    }
                }
            }

            iterator.m_SafetyReadWriteCount = 0;
            fixed (AtomicSafetyHandle* safety = &iterator.m_Safety0)
            {
                for (var i = 0; i != cache.ProcessTypesCount; i++)
                {
                    if (cache.Types[i].AccessModeType == ComponentType.AccessMode.ReadWrite)
                    {
                        safety[iterator.m_SafetyReadOnlyCount + iterator.m_SafetyReadWriteCount] = group.GetSafetyHandle(group.GetIndexInComponentGroup(cache.Types[i].TypeIndex));
                        iterator.m_SafetyReadWriteCount++;
                    }
                }
            }
            Assert.AreEqual(cache.ProcessTypesCount, iterator.m_SafetyReadWriteCount + iterator.m_SafetyReadOnlyCount);
#endif
        }
    }

    public static class JobProcessComponentDataExtensions
    {
        //NOTE: It would be much better if C# could resolve the branch with generic resolving,
        //      but apparently the interface constraint is not enough..

        public static JobHandle Schedule<T>(this T jobData, ComponentSystemBase system, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle))
            where T : struct, IBaseJobProcessComponentData
        {
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (innerloopBatchCount <= 0)
                throw new System.ArgumentException($"innerloopBatchCount must be larger than 0.");
            #endif

            var typeT = typeof(T);
            if (typeof(IBaseJobProcessComponentData_1).IsAssignableFrom(typeT))
                return ScheduleInternal_1(ref jobData, system, innerloopBatchCount, dependsOn, ScheduleMode.Batched);
            else if (typeof(IBaseJobProcessComponentData_2).IsAssignableFrom(typeT))
                return ScheduleInternal_2(ref jobData, system, innerloopBatchCount, dependsOn, ScheduleMode.Batched);
            else
                return ScheduleInternal_3(ref jobData, system, innerloopBatchCount, dependsOn, ScheduleMode.Batched);
        }

        public static JobHandle Schedule<T>(this T jobData, ComponentSystemBase system, JobHandle dependsOn = default(JobHandle))
            where T : struct, IBaseJobProcessComponentData
        {
            var typeT = typeof(T);
            if (typeof(IBaseJobProcessComponentData_1).IsAssignableFrom(typeT))
                return ScheduleInternal_1(ref jobData, system, -1, dependsOn, ScheduleMode.Batched);
            else if (typeof(IBaseJobProcessComponentData_2).IsAssignableFrom(typeT))
                return ScheduleInternal_2(ref jobData, system, -1, dependsOn, ScheduleMode.Batched);
            else
                return ScheduleInternal_3(ref jobData, system, -1, dependsOn, ScheduleMode.Batched);
        }

        public static void Run<T>(this T jobData, ComponentSystemBase system)
            where T : struct, IBaseJobProcessComponentData
        {
            var typeT = typeof(T);
            if (typeof(IBaseJobProcessComponentData_1).IsAssignableFrom(typeT ))
                ScheduleInternal_1(ref jobData, system, -1, default(JobHandle), ScheduleMode.Run);
            else if (typeof(IBaseJobProcessComponentData_2).IsAssignableFrom(typeT))
                ScheduleInternal_2(ref jobData, system, -1, default(JobHandle), ScheduleMode.Run);
            else
                ScheduleInternal_3(ref jobData, system, -1, default(JobHandle), ScheduleMode.Run);
        }

        internal unsafe static JobHandle ScheduleInternal_1<T>(ref T jobData, ComponentSystemBase system, int innerloopBatchCount,
            JobHandle dependsOn, ScheduleMode mode)
            where T : struct
        {
            JobStruct_ProcessInfer_1<T> fullData;
            fullData.Data = jobData;

            bool isParallelFor = innerloopBatchCount != -1; 
            IJobProcessComponentDataUtility.Initialize(system, typeof(T), typeof(JobStruct_Process1<,>), isParallelFor, ref JobStruct_ProcessInfer_1<T>.Cache, out fullData.Iterator);

            if (isParallelFor)
            {
                var length = fullData.Iterator.m_Length;
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_1<T>.Cache.JobReflectionDataParallelFor, dependsOn, mode);
                return JobsUtility.ScheduleParallelFor(ref scheduleParams, length, innerloopBatchCount);
            }
            else
            {
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_1<T>.Cache.JobReflectionData, dependsOn, mode);
                return JobsUtility.Schedule(ref scheduleParams);                
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct JobStruct_ProcessInfer_1<T> where T : struct
        {
            public static JobProcessComponentDataCache Cache;

            public ProcessIterationData      Iterator;
            public T Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobStruct_Process1<T, U0>
            where T : struct, IJobProcessComponentData<U0>
            where U0 : struct, IComponentData
        {
            public ProcessIterationData      Iterator;
            public T Data;

            public static IntPtr Initialize(JobType jobType)
            {
                return JobsUtility.CreateJobReflectionData(typeof(JobStruct_Process1<T, U0>), typeof(T), jobType, (ExecuteJobFunction) Execute);
            }

            delegate void ExecuteJobFunction(ref JobStruct_Process1<T, U0> data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

            static unsafe void ExecuteInnerLoop(ref JobStruct_Process1<T, U0> jobData, int begin, int end)
            {
                ComponentChunkCache cache0;

                while (begin != end)
                {
                    jobData.Iterator.Iterator0.UpdateCache(begin, out cache0);
                    var ptr0 = cache0.CachedPtr;

                    var curEnd = Math.Min(end, cache0.CachedEndIndex);

                    for (var i = begin; i != curEnd; i++)
                    {
                        //@TODO: use ref returns to pass by ref instead of double copy
                        var value0 = UnsafeUtility.ReadArrayElement<U0>(ptr0, i);

                        jobData.Data.Execute(ref value0);

                        if (jobData.Iterator.IsReadOnly0 == 0)
                            UnsafeUtility.WriteArrayElement(ptr0, i, value0);
                    }

                    begin = curEnd;
                }
            }

            static unsafe void Execute(ref JobStruct_Process1<T, U0> jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                if (jobData.Iterator.m_IsParallelFor)
                {
                    int begin;
                    int end;
                    while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end))
                    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                        JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf(ref jobData), begin, end - begin);
#endif
                        ExecuteInnerLoop(ref jobData, begin, end);
                    }
                }
                else
                {
                    ExecuteInnerLoop(ref jobData, 0, jobData.Iterator.m_Length);
                }
            }
        }

        internal unsafe static JobHandle ScheduleInternal_2<T>(ref T jobData, ComponentSystemBase system, int innerloopBatchCount, JobHandle dependsOn, ScheduleMode mode)
            where T : struct
        {
            JobStruct_ProcessInfer_2<T> fullData;
            fullData.Data = jobData;

            bool isParallelFor = innerloopBatchCount != -1;
            IJobProcessComponentDataUtility.Initialize(system, typeof(T), typeof(JobStruct_Process2<,,>), isParallelFor, ref JobStruct_ProcessInfer_2<T>.Cache, out fullData.Iterator);

            if (isParallelFor)
            {
                var length = fullData.Iterator.m_Length;
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_2<T>.Cache.JobReflectionDataParallelFor, dependsOn, mode);
                return JobsUtility.ScheduleParallelFor(ref scheduleParams, length, innerloopBatchCount);
            }
            else
            {
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_2<T>.Cache.JobReflectionData, dependsOn, mode);
                return JobsUtility.Schedule(ref scheduleParams);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct JobStruct_ProcessInfer_2<T> where T : struct
        {
            public static JobProcessComponentDataCache Cache;

            public ProcessIterationData      Iterator;
            public T                         Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobStruct_Process2<T, U0, U1>
            where T : struct, IJobProcessComponentData<U0, U1>
            where U0 : struct, IComponentData
            where U1 : struct, IComponentData
        {
            public ProcessIterationData      Iterator;
            public T                         Data;

            public static IntPtr Initialize(JobType jobType)
            {
                return JobsUtility.CreateJobReflectionData(typeof(JobStruct_Process2<T, U0, U1>), typeof(T), jobType, (ExecuteJobFunction)Execute);
            }

            delegate void ExecuteJobFunction(ref JobStruct_Process2<T, U0, U1> data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);


            static unsafe void ExecuteInnerLoop(ref JobStruct_Process2<T, U0, U1> jobData, int begin, int end)
            {
                ComponentChunkCache cache0, cache1;
                
                while (begin != end)
                {
                    jobData.Iterator.Iterator0.UpdateCache(begin, out cache0);
                    var ptr0 = cache0.CachedPtr;

                    jobData.Iterator.Iterator1.UpdateCache(begin, out cache1);
                    var ptr1 = cache1.CachedPtr;

                    var curEnd = Math.Min(end, cache0.CachedEndIndex);

                    for (var i = begin; i != curEnd; i++)
                    {
                        //@TODO: use ref returns to pass by ref instead of double copy
                        var value0 = UnsafeUtility.ReadArrayElement<U0>(ptr0, i);
                        var value1 = UnsafeUtility.ReadArrayElement<U1>(ptr1, i);

                        jobData.Data.Execute(ref value0, ref value1);

                        if (jobData.Iterator.IsReadOnly0 == 0)
                            UnsafeUtility.WriteArrayElement(ptr0, i, value0);
                        if (jobData.Iterator.IsReadOnly1 == 0)
                            UnsafeUtility.WriteArrayElement(ptr1, i, value1);
                    }

                    begin = curEnd;
                }
            }

            static unsafe void Execute(ref JobStruct_Process2<T, U0, U1> jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                if (jobData.Iterator.m_IsParallelFor)
                {
                    int begin;
                    int end;
                    while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end))
                    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                        JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf(ref jobData), begin, end - begin);
#endif
                        ExecuteInnerLoop(ref jobData, begin, end);
                    }
                }
                else
                {
                    ExecuteInnerLoop(ref jobData, 0, jobData.Iterator.m_Length);
                }
            }
        }

        internal unsafe static JobHandle ScheduleInternal_3<T>(ref T jobData, ComponentSystemBase system, int innerloopBatchCount, JobHandle dependsOn, ScheduleMode mode)
            where T : struct
        {
            JobStruct_ProcessInfer_3<T> fullData;
            fullData.Data = jobData;

            bool isParallelFor = innerloopBatchCount != -1;
            IJobProcessComponentDataUtility.Initialize(system, typeof(T), typeof(JobStruct_Process3<,,,>), isParallelFor, ref JobStruct_ProcessInfer_3<T>.Cache, out fullData.Iterator);

            if (isParallelFor)
            {
                var length = fullData.Iterator.m_Length;
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_3<T>.Cache.JobReflectionDataParallelFor, dependsOn, mode);
                return JobsUtility.ScheduleParallelFor(ref scheduleParams, length, innerloopBatchCount);
            }
            else
            {
                var scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref fullData), JobStruct_ProcessInfer_3<T>.Cache.JobReflectionData, dependsOn, mode);
                return JobsUtility.Schedule(ref scheduleParams);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct JobStruct_ProcessInfer_3<T> where T : struct
        {
            public static JobProcessComponentDataCache Cache;

            public ProcessIterationData      Iterator;
            public T                                       Data;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JobStruct_Process3<T, U0, U1, U2>
            where T : struct, IJobProcessComponentData<U0, U1, U2>
            where U0 : struct, IComponentData
            where U1 : struct, IComponentData
            where U2 : struct, IComponentData
        {
            public ProcessIterationData      Iterator;
            public T                       Data;

            public static IntPtr Initialize(JobType jobType)
            {
                return JobsUtility.CreateJobReflectionData(typeof(JobStruct_Process3<T, U0, U1, U2>), typeof(T), jobType, (ExecuteJobFunction)Execute);
            }

            delegate void ExecuteJobFunction(ref JobStruct_Process3<T, U0, U1, U2> data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

            static unsafe void ExecuteInnerLoop(ref JobStruct_Process3<T, U0, U1, U2> jobData, int begin, int end)
            {
                ComponentChunkCache cache0, cache1, cache2;

                while (begin != end)
                {
                    jobData.Iterator.Iterator0.UpdateCache(begin, out cache0);
                    var ptr0 = cache0.CachedPtr;

                    jobData.Iterator.Iterator1.UpdateCache(begin, out cache1);
                    var ptr1 = cache1.CachedPtr;

                    jobData.Iterator.Iterator2.UpdateCache(begin, out cache2);
                    var ptr2 = cache2.CachedPtr;

                    var curEnd = Math.Min(end, cache0.CachedEndIndex);

                    for (var i = begin; i != curEnd; i++)
                    {
                        //@TODO: use ref returns to pass by ref instead of double copy
                        var value0 = UnsafeUtility.ReadArrayElement<U0>(ptr0, i);
                        var value1 = UnsafeUtility.ReadArrayElement<U1>(ptr1, i);
                        var value2 = UnsafeUtility.ReadArrayElement<U2>(ptr2, i);

                        jobData.Data.Execute(ref value0, ref value1, ref value2);

                        if (jobData.Iterator.IsReadOnly0 == 0)
                            UnsafeUtility.WriteArrayElement(ptr0, i, value0);
                        if (jobData.Iterator.IsReadOnly1 == 0)
                            UnsafeUtility.WriteArrayElement(ptr1, i, value1);
                        if (jobData.Iterator.IsReadOnly2 == 0)
                            UnsafeUtility.WriteArrayElement(ptr2, i, value2);
                    }

                    begin = curEnd;
                }
            }


            static unsafe void Execute(ref JobStruct_Process3<T, U0, U1, U2> jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
            {
                if (jobData.Iterator.m_IsParallelFor)
                {
                    int begin;
                    int end;
                    while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end))
                    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                        JobsUtility.PatchBufferMinMaxRanges(bufferRangePatchData, UnsafeUtility.AddressOf(ref jobData), begin, end - begin);
#endif
                        ExecuteInnerLoop(ref jobData, begin, end);
                    }
                }
                else
                {
                    ExecuteInnerLoop(ref jobData, 0, jobData.Iterator.m_Length);
                }
            }
        }
    }
}
