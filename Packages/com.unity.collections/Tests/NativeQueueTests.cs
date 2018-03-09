using NUnit.Framework;
using Unity.Collections;

public class NativeQueueTests
{
	[Test]
	public void Enqueue_Dequeue()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 16; ++i)
			queue.Enqueue(i);
		Assert.AreEqual(16, queue.Count);
		for (int i = 0; i < 16; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}
	[Test]
	public void ConcurrentEnqueue_Dequeue()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		NativeQueue<int>.Concurrent cQueue = queue;
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 16; ++i)
			cQueue.Enqueue(i);
		Assert.AreEqual(16, queue.Count);
		for (int i = 0; i < 16; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}

	[Test]
	public void Enqueue_Dequeue_Peek()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 16; ++i)
			queue.Enqueue(i);
		Assert.AreEqual(16, queue.Count);
		for (int i = 0; i < 16; ++i)
		{
			Assert.AreEqual(i, queue.Peek(), "Got the wrong value from the queue");
			queue.Dequeue();
		}
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}

	[Test]
	public void Enqueue_Dequeue_Clear()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 16; ++i)
			queue.Enqueue(i);
		Assert.AreEqual(16, queue.Count);
		for (int i = 0; i < 8; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(8, queue.Count);
		queue.Clear();
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}

	[Test]
	public void Double_Deallocate_Throws()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		queue.Dispose ();
		Assert.Throws<System.InvalidOperationException> (() => { queue.Dispose (); });
	}

	[Test]
	public void EnqueueScalability()
	{
		var queue = new NativeQueue<int> (Allocator.Persistent);
		for (int i = 0; i != 1000 * 100; i++)
		{
			queue.Enqueue (i);
		}

		Assert.AreEqual (1000 * 100, queue.Count);

		for (int i = 0; i != 1000 * 100; i++)
			Assert.AreEqual (i, queue.Dequeue());
		Assert.AreEqual (0, queue.Count);

		queue.Dispose ();
	}

	[Test]
	public void Enqueue_Wrap()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 256; ++i)
			queue.Enqueue(i);
		Assert.AreEqual(256, queue.Count);
		for (int i = 0; i < 128; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(128, queue.Count);
		for (int i = 0; i < 128; ++i)
			queue.Enqueue(i);
		Assert.AreEqual(256, queue.Count);
		for (int i = 128; i < 256; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(128, queue.Count);
		for (int i = 0; i < 128; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}
	[Test]
	public void ConcurrentEnqueue_Wrap()
	{
		var queue = new NativeQueue<int> (Allocator.Temp);
		NativeQueue<int>.Concurrent cQueue = queue;
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		for (int i = 0; i < 256; ++i)
			cQueue.Enqueue(i);
		Assert.AreEqual(256, queue.Count);
		for (int i = 0; i < 128; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(128, queue.Count);
		for (int i = 0; i < 128; ++i)
			cQueue.Enqueue(i);
		Assert.AreEqual(256, queue.Count);
		for (int i = 128; i < 256; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(128, queue.Count);
		for (int i = 0; i < 128; ++i)
			Assert.AreEqual(i, queue.Dequeue(), "Got the wrong value from the queue");
		Assert.AreEqual(0, queue.Count);
		Assert.Throws<System.InvalidOperationException> (()=> {queue.Dequeue(); });
		queue.Dispose ();
	}
}
