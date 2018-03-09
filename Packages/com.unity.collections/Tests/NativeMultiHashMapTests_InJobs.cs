using NUnit.Framework;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Collections;

public class NativeMultiHashMapTests_InJobs : NativeMultiHashMapTestsFixture
{
	[Test]
	public void Read_And_Write()
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
		var readJob = readData.Schedule(hashMapSize, 1, writeJob);
		readJob.Complete();

		for (int i = 0; i < hashMapSize; ++i)
		{
			Assert.AreEqual(0, writeStatus[i], "Job failed to write value to hash map");
			Assert.AreEqual(1, readValues[i], "Job failed to read from hash map");
		}

		hashMap.Dispose();
		writeStatus.Dispose();
		readValues.Dispose();
	}

	[Test]
	public void Read_And_Write_Full()
	{
		var hashMap = new NativeMultiHashMap<int, int>(hashMapSize/2, Allocator.Temp);
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
		var readJob = readData.Schedule(hashMapSize, 1, writeJob);
		readJob.Complete();

		var missing = new HashSet<int>();
		for (int i = 0; i < hashMapSize; ++i)
		{
			if (writeStatus[i] == -2)
			{
				missing.Add(i);
				Assert.AreEqual(-1, readValues[i], "Job read a value form hash map which should not be there");
			}
			else
			{
				Assert.AreEqual(0, writeStatus[i], "Job failed to write value to hash map");
				Assert.AreEqual(1, readValues[i], "Job failed to read from hash map");
			}
		}
		Assert.AreEqual(hashMapSize - hashMapSize/2, missing.Count, "Wrong indices written to hash map");

		hashMap.Dispose();
		writeStatus.Dispose();
		readValues.Dispose();
	}

	[Test]
	public void Key_Collisions()
	{
		var hashMap = new NativeMultiHashMap<int, int>(hashMapSize, Allocator.Temp);
		var writeStatus = new NativeArray<int>(hashMapSize, Allocator.Temp);
		var readValues = new NativeArray<int>(hashMapSize, Allocator.Temp);

		var writeData = new MultiHashMapWriteParallelForJob();
		writeData.hashMap = hashMap;
		writeData.status = writeStatus;
		writeData.keyMod = 16;
		var readData = new MultiHashMapReadParallelForJob();
		readData.hashMap = hashMap;
		readData.values = readValues;
		readData.keyMod = writeData.keyMod;
		var writeJob = writeData.Schedule(hashMapSize, 1);
		var readJob = readData.Schedule(hashMapSize, 1, writeJob);
		readJob.Complete();

		for (int i = 0; i < hashMapSize; ++i)
		{
			Assert.AreEqual(0, writeStatus[i], "Job failed to write value to hash map");
			Assert.AreEqual(hashMapSize / readData.keyMod, readValues[i], "Job failed to read from hash map");
		}

		hashMap.Dispose();
		writeStatus.Dispose();
		readValues.Dispose();
	}
}
