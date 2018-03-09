using NUnit.Framework;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Collections;

public class NativeQueueTests_InJobs
{
	struct ConcurrentEnqueue : IJobParallelFor
	{
		public NativeQueue<int>.Concurrent queue;
		public NativeArray<int> result;

		public void Execute(int index)
		{
			result[index] = 1;
			try
			{
				queue.Enqueue(index);
			}
			catch (System.Exception)
			{
				result[index] = 0;
			}
		}
	}

	[Test]
	public void Enqueue()
	{
		const int queueSize = 100*1024;
		var queue = new NativeQueue<int>(Allocator.Temp);
		var writeStatus = new NativeArray<int>(queueSize, Allocator.Temp);

		var enqueueJob = new ConcurrentEnqueue();
		enqueueJob.queue = queue;
		enqueueJob.result = writeStatus;

		enqueueJob.Schedule(queueSize, 1).Complete();

		Assert.AreEqual(queueSize, queue.Count, "Job enqueued the wrong number of values");
		var allValues = new HashSet<int>();
		for (int i = 0; i < queueSize; ++i)
		{
			Assert.AreEqual(1, writeStatus[i], "Job failed to enqueue value");
			int enqueued = queue.Dequeue();
			Assert.IsTrue(enqueued >= 0 && enqueued < queueSize, "Job enqueued invalid value");
			Assert.IsTrue(allValues.Add(enqueued), "Job enqueued same value multiple times");
		}

		queue.Dispose();
		writeStatus.Dispose();
	}
}
