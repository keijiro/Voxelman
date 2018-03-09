using UnityEngine;
using NUnit.Framework;
using System;
using Unity.Jobs;
using Unity.Collections;

#pragma warning disable 0219
#pragma warning disable 0414

public class NativeListJobDebuggerTests
{
	struct NativeListAddJob : IJob
	{
		NativeList<int> list;

		public NativeListAddJob(NativeList<int> list) { this.list = list; }

		public void Execute()
		{
			list.Add (1);
		}
	}

	struct NativeArrayTest : IJob
	{
		NativeArray<int> array;

		public NativeArrayTest(NativeArray<int> array) { this.array = array; }

		public void Execute()
		{
		}
	}


	[Test]
	public void AddElementToListFromJobInvalidatesArray()
	{
		var list = new NativeList<int> (Allocator.TempJob);
		list.Add (0);

		NativeArray<int> arrayBeforeSchedule = list;
		Assert.AreEqual (list.Capacity, 1);

		var jobData = new NativeListAddJob (list);
		var job = jobData.Schedule ();

		Assert.Throws<System.InvalidOperationException> (()=> { Debug.Log(arrayBeforeSchedule[0]); });
		Assert.Throws<System.InvalidOperationException> (()=> { NativeArray<int> array = list; Debug.Log(array.Length); });
		Assert.Throws<System.InvalidOperationException> (()=> { Debug.Log(list.Capacity); });
		Assert.Throws<System.InvalidOperationException> (()=> { list.Dispose(); });
		Assert.Throws<System.InvalidOperationException> (()=> { Debug.Log(list[0]); });

		job.Complete ();

		Assert.AreEqual (1, arrayBeforeSchedule.Length);
		Assert.Throws<System.InvalidOperationException> (()=> { Debug.Log(arrayBeforeSchedule[0]); });

		Assert.AreEqual (3, list.Capacity);
		Assert.AreEqual (2, list.Length);
		Assert.AreEqual (0, list[0]);
		Assert.AreEqual (1, list[1]);

		NativeArray<int> arrayAfter = list;
		Assert.AreEqual (2, arrayAfter.Length);
		Assert.AreEqual (0, arrayAfter[0]);
		Assert.AreEqual (1, arrayAfter[1]);

		list.Dispose ();
	}


	[Test]
	public void ScheduleDerivedArrayAllowDerivingArrayAgain() 
	{
		var list = new NativeList<int> (1, Allocator.Persistent);

		// The scheduled job only receives a NativeArray thus it can't be resized
		var writeJobHandle = new NativeArrayTest(list).Schedule();

		// For that reason casting here is legal, as opposed to AddElementToListFromJobInvalidatesArray case where it is not legal
		// Since we NativeList is passed to the job
		NativeArray<int> array = list;

		writeJobHandle.Complete ();
		list.Dispose();
	}

	[Test]
	public void ScheduleDerivedArrayExceptions() 
	{
		var list = new NativeList<int> (1, Allocator.Persistent);

		var addListJobHandle = new NativeListAddJob(list).Schedule();
		Assert.Throws<System.InvalidOperationException> (()=> { NativeArray<int> array = list; });

		addListJobHandle.Complete ();
		list.Dispose();
	}

	[Test]
	public void ScheduleDerivedArrayExceptions2() 
	{
		var list = new NativeList<int> (1, Allocator.Persistent);
		NativeArray<int> array = list;

		var addListJobHandle = new NativeListAddJob(list).Schedule();
		// The array previously cast should become invalid
		// as soon as the job is scheduled, since we can't predict if an element will be added or not
		Assert.Throws<System.InvalidOperationException> (()=> { new NativeArrayTest(array).Schedule(); });

		addListJobHandle.Complete ();
		list.Dispose();
	}


	struct NativeListSetValue42 : IJob
	{
		NativeList<int> list;

		public NativeListSetValue42(NativeList<int> list) { this.list = list; }

		public void Execute()
		{
			list[0] = 42;
		}
	}

	[Test]
	public void SetListValueKeepsExtractedArrayValid()
	{
		var list = new NativeList<int> (Allocator.TempJob);
		list.Add (0);
		NativeArray<int> arrayBeforeSchedule = list;

		var jobData = new NativeListSetValue42 (list);
		var job = jobData.Schedule ();
		job.Complete ();

		Assert.AreEqual (42, arrayBeforeSchedule[0]);
		Assert.AreEqual (42, list[0]);

		list.Dispose ();
	}


	struct NativeListToArrayConversionFromJob : IJob
	{
		public NativeList<int> list;

		public void Execute()
		{
			list.Add (0);
			list.Add (0);

			NativeArray<int> arr = list;
			arr [0] = 1;
			arr [1] = 2;
		}
	}

	[Test]
	public void CastListToArrayInsideJob() 
	{
		var jobData = new NativeListToArrayConversionFromJob ();
		jobData.list = new NativeList<int> (1, Allocator.Persistent);
		jobData.Schedule ().Complete ();

		Assert.AreEqual (new int[] {1, 2}, jobData.list.ToArray());
		jobData.list.Dispose();
	}

	struct WriteJob : IJobParallelFor
	{
		public NativeArray<float> output;

		public void Execute(int i)
		{
			output[i] = i;
		}
	}

	[Test]
	public void WriteToArrayFromJobThenReadListFromMainThread() 
	{
		var list = new NativeList<float> (1, Allocator.Persistent);
		list.Add (0);
		list.Add (1);

		for (int i = 0; i < 2; i++)
		{
			var writeJob = new WriteJob();
			writeJob.output = list;
			var writeJobHandle = writeJob.Schedule (list.Length, 1);

			Assert.Throws<InvalidOperationException> (() => { Debug.Log(writeJob.output[0]); } );

			writeJobHandle.Complete ();
		}
		list.Dispose();
	}

}
//@TODO: Test for List read writing protection checks..

//@TODO: Test for List + Array derived from list have aliasing checks
