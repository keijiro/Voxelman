using NUnit.Framework;
using Unity.Collections;

public class NativeHashMapTests
{
	[Test]
	public void TryAdd_TryGetValue_Clear()
	{
		var hashMap = new NativeHashMap<int, int> (16, Allocator.Temp);
		int iSquared;
		// Make sure GetValue fails if hash map is empty
		Assert.IsFalse(hashMap.TryGetValue(0, out iSquared), "TryGetValue on empty hash map did not fail");
		// Make sure inserting values work
		for (int i = 0; i < 16; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i*i), "Failed to add value");
		// Make sure inserting duplicate keys fails
		for (int i = 0; i < 16; ++i)
			Assert.IsFalse(hashMap.TryAdd(i, i), "Adding duplicate keys did not fail");
		// Make sure reading the inserted values work
		for (int i = 0; i < 16; ++i)
		{
			Assert.IsTrue(hashMap.TryGetValue(i, out iSquared), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
		}
		// Make sure clearing removes all keys
		hashMap.Clear();
		for (int i = 0; i < 16; ++i)
			Assert.IsFalse(hashMap.TryGetValue(i, out iSquared), "Got value from hash table after clearing");
		hashMap.Dispose ();
	}

	[Test]
	public void Full_HashMap_Throws()
	{
		var hashMap = new NativeHashMap<int, int> (16, Allocator.Temp);
		// Fill the hash map
		for (int i = 0; i < 16; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i), "Failed to add value");
		// Make sure overallocating throws and exception if using the Concurrent version - normal hash map would grow
		NativeHashMap<int, int>.Concurrent cHashMap = hashMap;
		Assert.Throws<System.InvalidOperationException> (()=> {cHashMap.TryAdd(100, 100); });
		hashMap.Dispose ();
	}

	[Test]
	public void Double_Deallocate_Throws()
	{
		var hashMap = new NativeHashMap<int, int> (16, Allocator.Temp);
		hashMap.Dispose ();
		Assert.Throws<System.InvalidOperationException> (() => { hashMap.Dispose (); });
	}

	[Test]
	public void Key_Collisions()
	{
		var hashMap = new NativeHashMap<int, int> (16, Allocator.Temp);
		int iSquared;
		// Make sure GetValue fails if hash map is empty
		Assert.IsFalse(hashMap.TryGetValue(0, out iSquared), "TryGetValue on empty hash map did not fail");
		// Make sure inserting values work
		for (int i = 0; i < 8; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i*i), "Failed to add value");
		// The bucket size is capacity * 2, adding that number should result in hash collisions
		for (int i = 0; i < 8; ++i)
			Assert.IsTrue(hashMap.TryAdd(i + 32, i), "Failed to add value with potential hash collision");
		// Make sure reading the inserted values work
		for (int i = 0; i < 8; ++i)
		{
			Assert.IsTrue(hashMap.TryGetValue(i, out iSquared), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
		}
		for (int i = 0; i < 8; ++i)
		{
			Assert.IsTrue(hashMap.TryGetValue(i+32, out iSquared), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i, "Got the wrong value from the hash table");
		}
		hashMap.Dispose ();
	}

	[Test]
	public void HashMapSupportsAutomaticCapacityChange()
	{
		var hashMap = new NativeHashMap<int, int> (1, Allocator.Temp);
		int iSquared;
		// Make sure inserting values work and grows the capacity
		for (int i = 0; i < 8; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i*i), "Failed to add value");
		Assert.IsTrue(hashMap.Capacity >= 8, "Capacity was not updated correctly");
		// Make sure reading the inserted values work
		for (int i = 0; i < 8; ++i)
		{
			Assert.IsTrue(hashMap.TryGetValue(i, out iSquared), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
		}
		hashMap.Dispose ();
	}

	[Test]
	public void HashMapSameKey()
	{
		var hashMap = new NativeHashMap<int, int> (0, Allocator.Persistent);
		hashMap.TryAdd (0, 0);
		hashMap.TryAdd (0, 0);
		hashMap.Dispose ();//@TODO CHECKS
	}

	[Test]
	public void HashMapEmptyCapacity()
	{
		var hashMap = new NativeHashMap<int, int> (0, Allocator.Persistent);
		hashMap.TryAdd (0, 0);
		Assert.AreEqual (1, hashMap.Capacity);
		Assert.AreEqual (1, hashMap.Length);
		hashMap.Dispose ();
	}

	[Test]
	public void Remove()
	{
		var hashMap = new NativeHashMap<int, int> (8, Allocator.Temp);
		int iSquared;
		// Make sure inserting values work
		for (int i = 0; i < 8; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i*i), "Failed to add value");
		Assert.AreEqual(8, hashMap.Capacity, "HashMap grew larger than expected");
		// Make sure reading the inserted values work
		for (int i = 0; i < 8; ++i)
		{
			Assert.IsTrue(hashMap.TryGetValue(i, out iSquared), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
		}
		for (int rm = 0; rm < 8; ++rm)
		{
			hashMap.Remove(rm);
			Assert.IsFalse(hashMap.TryGetValue(rm, out iSquared), "Failed to remove value from hash table");
			for (int i = rm+1; i < 8; ++i)
			{
				Assert.IsTrue(hashMap.TryGetValue(i, out iSquared), "Failed get value from hash table");
				Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
			}
		}
		// Make sure entries were freed
		for (int i = 0; i < 8; ++i)
			Assert.IsTrue(hashMap.TryAdd(i, i*i), "Failed to add value");
		Assert.AreEqual(8, hashMap.Capacity, "HashMap grew larger than expected");
		hashMap.Dispose ();
	}
	[Test]
	public void RemoveFromMultiHashMap()
	{
		var hashMap = new NativeMultiHashMap<int, int> (16, Allocator.Temp);
		int iSquared;
		// Make sure inserting values work
		for (int i = 0; i < 8; ++i)
			hashMap.Add(i, i*i);
		for (int i = 0; i < 8; ++i)
			hashMap.Add(i, i);
		Assert.AreEqual(16, hashMap.Capacity, "HashMap grew larger than expected");
		// Make sure reading the inserted values work
		for (int i = 0; i < 8; ++i)
		{
			NativeMultiHashMapIterator<int> it;
			Assert.IsTrue(hashMap.TryGetFirstValue(i, out iSquared, out it), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i, "Got the wrong value from the hash table");
			Assert.IsTrue(hashMap.TryGetNextValue(out iSquared, ref it), "Failed get value from hash table");
			Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
		}
		for (int rm = 0; rm < 8; ++rm)
		{
			hashMap.Remove(rm);
			NativeMultiHashMapIterator<int> it;
			Assert.IsFalse(hashMap.TryGetFirstValue(rm, out iSquared, out it), "Failed to remove value from hash table");
			for (int i = rm+1; i < 8; ++i)
			{
				Assert.IsTrue(hashMap.TryGetFirstValue(i, out iSquared, out it), "Failed get value from hash table");
				Assert.AreEqual(iSquared, i, "Got the wrong value from the hash table");
				Assert.IsTrue(hashMap.TryGetNextValue(out iSquared, ref it), "Failed get value from hash table");
				Assert.AreEqual(iSquared, i*i, "Got the wrong value from the hash table");
			}
		}
		// Make sure entries were freed
		for (int i = 0; i < 8; ++i)
			hashMap.Add(i, i*i);
		for (int i = 0; i < 8; ++i)
			hashMap.Add(i, i);
		Assert.AreEqual(16, hashMap.Capacity, "HashMap grew larger than expected");
		hashMap.Dispose ();
	}

	[Test]
	public void TryAddScalability()
	{
		var hashMap = new NativeHashMap<int, int> (1, Allocator.Persistent);
		for (int i = 0; i != 1000 * 100; i++)
		{
			hashMap.TryAdd (i, i);
		}

		int value;
		Assert.IsFalse(hashMap.TryGetValue (-1, out value));
		Assert.IsFalse(hashMap.TryGetValue (1000 * 1000, out value));

		for (int i = 0; i != 1000 * 100; i++)
		{
			Assert.IsTrue (hashMap.TryGetValue (i, out value));
			Assert.AreEqual (i, value);
		}

		hashMap.Dispose ();
	}
}
