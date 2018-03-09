using NUnit.Framework;
using System;
using Unity.Jobs;
using Unity.Collections;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
public class NativeMultiHashMapTests_JobDebugger : NativeMultiHashMapTestsFixture
{
	[Test]
	public void Read_And_Write_Without_Fences()
	{
		var hashMap = new NativeMultiHashMap<int, int>(hashMapSize, Allocator.Temp);
		var writeStatus = new NativeArray<int>(hashMapSize, Allocator.Temp);
		var readValues = new NativeArray<int>(hashMapSize, Allocator.Temp);

		var writeData = new MultiHashMapWriteParallelForJob();
		writeData.hashMap = hashMap;
		writeData.status = writeStatus;
		writeData.keyMod = hashMapSize;
		var readData = new MultiHashMapReadParallelForJob();
		readData.hashMap = hashMap;
		readData.values = readValues;
		readData.keyMod = writeData.keyMod;
		var writeJob = writeData.Schedule(hashMapSize, 1);
		Assert.Throws<InvalidOperationException> (() => { readData.Schedule(hashMapSize, 1); } );
		writeJob.Complete();

		hashMap.Dispose();
		writeStatus.Dispose();
		readValues.Dispose();
	}
}
#endif
