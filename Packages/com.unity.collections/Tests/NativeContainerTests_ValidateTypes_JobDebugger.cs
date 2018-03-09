using Unity.Jobs;
using NUnit.Framework;
using Unity.Collections;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
public class NativeContainderTests_ValidateTypes_JobDebugger : NativeContainerTests_ValidateTypesFixture
{
	struct WriteOnlyHashMapParallelForJob : IJobParallelFor
	{
        [WriteOnly]
		NativeHashMap<int, int> value;

		public void Execute(int index) {}
	}
	
	struct ReadWriteMultiHashMapParallelForJob : IJobParallelFor
	{
		NativeMultiHashMap<int, int> value;

		public void Execute(int index) {}
	}
	
	struct DeallocateOnJobCompletionOnUnsupportedType : IJob
	{
		[DeallocateOnJobCompletion]
		NativeList<float> value;

		public void Execute() {}
	}

	[Test]
	public void ValidatedUnsupportedTypes()
	{
        CheckNativeContainerReflectionExceptionParallelFor<WriteOnlyHashMapParallelForJob> ("WriteOnlyHashMapParallelForJob.value is not declared [ReadOnly] in a IJobParallelFor job. The container does not support parallel writing. Please use a more suitable container type.");
		CheckNativeContainerReflectionException<DeallocateOnJobCompletionOnUnsupportedType> ("DeallocateOnJobCompletionOnUnsupportedType.value uses [DeallocateOnJobCompletion] but the native container does not support deallocation of the memory from a job.");

		// ReadWrite against atomic write only container
		CheckNativeContainerReflectionExceptionParallelFor<ReadWriteMultiHashMapParallelForJob> ("ReadWriteMultiHashMapParallelForJob.value is not declared [ReadOnly] in a IJobParallelFor job. The container does not support parallel writing. Please use a more suitable container type.");
	}
}
#endif
