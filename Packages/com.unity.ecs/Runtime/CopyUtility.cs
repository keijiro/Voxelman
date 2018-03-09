using Unity.Collections;
using Unity.Jobs;

namespace Unity.Entities
{
    public interface ISingleValue<T>
    {
       T Value { get; set; } 
    }
    
    /// <summary>
    /// Copy ComponentDataArray to NativeArray Job.
    /// </summary>
    /// <typeparam name="TSource">Component data type of ISingleValue<T></typeparam>
    /// <typeparam name="TDestination">Blittable type matching T in source</typeparam>
    
    [ComputeJobOptimization]
    public struct CopyComponentData<TSource,TDestination> : IJobParallelFor
    where TSource : struct, IComponentData, ISingleValue<TDestination>
    where TDestination : struct
    {
        [ReadOnly] public ComponentDataArray<TSource> source;
        public NativeArray<TDestination> results;

        public void Execute(int index)
        {
            results[index] = source[index].Value;
        }
    }
}
