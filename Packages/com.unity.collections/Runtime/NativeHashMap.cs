using System;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	public struct NativeMultiHashMapIterator<TKey>
		where TKey: struct
	{
		internal TKey key;
		internal int NextEntryIndex;
		//@TODO: Make unnecessary, is only used by SetValue API...
		internal int EntryIndex;
	}

	[StructLayout (LayoutKind.Sequential)]
	internal unsafe struct NativeHashMapData
	{
		public byte*            values;
		public byte*	        keys;
		public byte*	        next;
		public byte*            buckets;
		public int				capacity;
		public int				bucketCapacityMask; // = bucket capacity - 1
		// Add padding between fields to ensure they are on separate cache-lines
		private fixed byte 		padding1[60];
        public fixed int firstFreeTLS[JobsUtility.MaxJobThreadCount * IntsPerCacheLine];
		public int				allocatedIndexLength;

		// 64 is the cache line size on x86, arm usually has 32 - so it is possible to save some memory there
        public const int IntsPerCacheLine = JobsUtility.CacheLineSize / sizeof(int);
		private const int bucketSizeMultiplier = 2;

		public static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		public static int GrowCapacity(int capacity)
		{
			if (capacity == 0)
				return 1;
			return capacity * 2;
		}


		public unsafe static void AllocateHashMap<TKey, TValue>(int length, int bucketLength, Allocator label, out NativeHashMapData* outBuf)
			where TKey : struct
			where TValue : struct
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!UnsafeUtility.IsBlittable<TKey>())
                throw new ArgumentException(string.Format("{0} used in NativeHashMap<{0},{1}> must be blittable", typeof(TKey), typeof(TValue)));
            if (!UnsafeUtility.IsBlittable<TKey>())
                throw new ArgumentException(string.Format("{1} used in NativeHashMap<{0},{1}> must be blittable", typeof(TKey), typeof(TValue)));
#endif

		    NativeHashMapData* data = (NativeHashMapData*)UnsafeUtility.Malloc (sizeof(NativeHashMapData), UnsafeUtility.AlignOf<NativeHashMapData>(), label);

		    bucketLength = math.ceil_pow2(bucketLength);

			data->capacity = length;
			data->bucketCapacityMask = bucketLength - 1;

			int keyOffset, nextOffset, bucketOffset;
			int totalSize = CalculateDataSize<TKey, TValue>(length, bucketLength, out keyOffset, out nextOffset, out bucketOffset);

			data->values = (byte*)UnsafeUtility.Malloc (totalSize, JobsUtility.CacheLineSize, label);
			data->keys = data->values + keyOffset;
			data->next = data->values + nextOffset;
			data->buckets = data->values + bucketOffset;

		    outBuf = data;
		}
		public unsafe static void ReallocateHashMap<TKey, TValue>(NativeHashMapData* data, int newCapacity, int newBucketCapacity, Allocator label)
			where TKey : struct
			where TValue : struct
		{
		    newBucketCapacity = math.ceil_pow2(newBucketCapacity);

			if (data->capacity == newCapacity && (data->bucketCapacityMask + 1) == newBucketCapacity)
				return;

			if (data->capacity > newCapacity)
				throw new System.Exception("Shrinking a hash map is not supported");

			int keyOffset, nextOffset, bucketOffset;
			int totalSize = CalculateDataSize<TKey, TValue>(newCapacity, newBucketCapacity, out keyOffset, out nextOffset, out bucketOffset);

			byte* newData = (byte*)UnsafeUtility.Malloc (totalSize, JobsUtility.CacheLineSize, label);
			byte* newKeys = newData + keyOffset;
		    byte* newNext = newData + nextOffset;
		    byte* newBuckets = newData + bucketOffset;

			// The items are taken from a free-list and might not be tightly packed, copy all of the old capcity
			UnsafeUtility.MemCpy (newData, data->values, data->capacity * UnsafeUtility.SizeOf<TValue>());
			UnsafeUtility.MemCpy (newKeys, data->keys, data->capacity * UnsafeUtility.SizeOf<TKey>());
			UnsafeUtility.MemCpy (newNext, data->next, data->capacity * UnsafeUtility.SizeOf<int>());
			for (int emptyNext = data->capacity; emptyNext < newCapacity; ++emptyNext)
				((int*)newNext)[emptyNext] = -1;

			// re-hash the buckets, first clear the new bucket list, then insert all values from the old list
			for (int bucket = 0; bucket < newBucketCapacity; ++bucket)
				((int*)newBuckets)[bucket] = -1;
			for (int bucket = 0; bucket <= data->bucketCapacityMask; ++bucket)
			{
				int* buckets = (int*)data->buckets;
				int* nextPtrs = (int*)newNext;
				while (buckets[bucket] >= 0)
				{
					int curEntry = buckets[bucket];
					buckets[bucket] = nextPtrs[curEntry];
					int newBucket = Math.Abs(UnsafeUtility.ReadArrayElement<TKey> (data->keys, curEntry).GetHashCode()) & (newBucketCapacity-1);
					nextPtrs[curEntry] = ((int*)newBuckets)[newBucket];
					((int*)newBuckets)[newBucket] = curEntry;
				}
			}

			UnsafeUtility.Free (data->values, label);
			if (data->allocatedIndexLength > data->capacity)
				data->allocatedIndexLength = data->capacity;
			data->values = newData;
			data->keys = newKeys;
			data->next = newNext;
			data->buckets = newBuckets;
			data->capacity = newCapacity;
			data->bucketCapacityMask = newBucketCapacity - 1;
		}
		public unsafe static void DeallocateHashMap(NativeHashMapData* data, Allocator allocation)
		{
			UnsafeUtility.Free (data->values, allocation);
			data->values = null;
			data->keys = null;
			data->next = null;
			data->buckets = null;
			UnsafeUtility.Free (data, allocation);
		}
        static private int CalculateDataSize<TKey, TValue>(int length, int bucketLength, out int keyOffset, out int nextOffset, out int bucketOffset)
			where TKey : struct
			where TValue : struct
		{
			int elementSize = UnsafeUtility.SizeOf<TValue> ();
			int keySize = UnsafeUtility.SizeOf<TKey> ();

			// Offset is rounded up to be an even cacheLineSize
			keyOffset = (elementSize * length + JobsUtility.CacheLineSize - 1);
			keyOffset -= keyOffset % JobsUtility.CacheLineSize;

			nextOffset = (keyOffset + keySize * length + JobsUtility.CacheLineSize - 1);
			nextOffset -= nextOffset % JobsUtility.CacheLineSize;

			bucketOffset = (nextOffset + UnsafeUtility.SizeOf<int>() * length + JobsUtility.CacheLineSize - 1);
			bucketOffset -= bucketOffset % JobsUtility.CacheLineSize;

			int totalSize = bucketOffset + UnsafeUtility.SizeOf<int>() * bucketLength;
			return totalSize;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	internal struct NativeHashMapBase<TKey, TValue>
		where TKey : struct, System.IEquatable<TKey>
		where TValue : struct
	{
		static unsafe public void Clear(NativeHashMapData* data)
		{
			int* buckets = (int*)data->buckets;
			for (int i = 0; i <= data->bucketCapacityMask; ++i)
				buckets[i] = -1;
			int* nextPtrs = (int*)data->next;
			for (int i = 0; i < data->capacity; ++i)
				nextPtrs[i] = -1;
			for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
				data->firstFreeTLS[tls * NativeHashMapData.IntsPerCacheLine] = -1;
			data->allocatedIndexLength = 0;
		}

		static unsafe private int AllocEntry(NativeHashMapData* data, int threadIndex)
		{
			int idx;
			int* nextPtrs = (int*)data->next;
			do
			{
				idx = data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine];
				if (idx < 0)
				{
					// Try to refill local cache
					Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine], -2);
					// If it failed try to get one from the never-allocated array
					if (data->allocatedIndexLength < data->capacity)
					{
						idx = Interlocked.Add(ref data->allocatedIndexLength, 16)-16;
						if (idx < data->capacity)
						{
							int count = Math.Min(16, data->capacity - idx);
							for (int i = 1; i < count; ++i)
							{
								nextPtrs[idx+i] = idx+i+1;
							}
							nextPtrs[idx+count - 1] = -1;
							nextPtrs[idx] = -1;
							Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine], idx + 1);
							return idx;
						}
					}
					Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine], -1);
					// Failed to get any, try to get one from another free list
					bool again = true;
					while (again)
					{
						again = false;
						for (int other = (threadIndex + 1) % JobsUtility.MaxJobThreadCount; other != threadIndex; other = (other + 1) % JobsUtility.MaxJobThreadCount)
						{
							do
							{
								idx = data->firstFreeTLS[other*NativeHashMapData.IntsPerCacheLine];
								if(idx < 0)
									break;
							}
							while (Interlocked.CompareExchange(ref data->firstFreeTLS[other * NativeHashMapData.IntsPerCacheLine], nextPtrs[idx], idx) != idx);
							if (idx == -2)
								again = true;
							else if (idx >= 0)
							{
								nextPtrs[idx] = -1;
								return idx;
							}
						}
					}
					throw new System.InvalidOperationException("HashMap is full");
				}
			}
			while (Interlocked.CompareExchange(ref data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine], nextPtrs[idx], idx) != idx);
			nextPtrs[idx] = -1;
			return idx;
		}
		static unsafe public bool TryAddAtomic(NativeHashMapData* data, TKey key, TValue item, int threadIndex)
		{
			TValue tempItem;
			NativeMultiHashMapIterator<TKey> tempIt;
			if (TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
				return false;
			// Allocate an entry from the free list
			int idx = AllocEntry(data, threadIndex);

			// Write the new value to the entry
			UnsafeUtility.WriteArrayElement (data->keys, idx, key);
			UnsafeUtility.WriteArrayElement (data->values, idx, item);

			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			// Add the index to the hash-map
			int* buckets = (int*)data->buckets;
			if (Interlocked.CompareExchange(ref buckets[bucket], idx, -1) != -1)
			{
				int* nextPtrs = (int*)data->next;
				do
				{
					nextPtrs[idx] = buckets[bucket];
					if (TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
					{
						// Put back the entry in the free list if someone else added it while trying to add
						do
						{
							nextPtrs[idx] = data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine];
						}
						while (Interlocked.CompareExchange(ref data->firstFreeTLS[threadIndex * NativeHashMapData.IntsPerCacheLine], idx, nextPtrs[idx]) != nextPtrs[idx]);

						return false;
					}
				}
				while (Interlocked.CompareExchange(ref buckets[bucket], idx, nextPtrs[idx]) != nextPtrs[idx]);
			}
			return true;
		}

	    static unsafe public bool TryAddAtomicMulti(NativeHashMapData* data, TKey key, TValue item, int threadIndex)
	    {
	        // Allocate an entry from the free list
	        int idx = AllocEntry(data, threadIndex);

	        // Write the new value to the entry
	        UnsafeUtility.WriteArrayElement (data->keys, idx, key);
	        UnsafeUtility.WriteArrayElement (data->values, idx, item);

	        int bucket = key.GetHashCode() & data->bucketCapacityMask;
	        // Add the index to the hash-map
	        int* buckets = (int*)data->buckets;

	        int nextPtr;
	        int* nextPtrs = (int*)data->next;
	        do
            {
                nextPtr = buckets[bucket];
                nextPtrs[idx] = nextPtr;
            }
            while (Interlocked.CompareExchange(ref buckets[bucket], idx, nextPtr) != nextPtr);


	        return true;
	    }

	    static unsafe public bool TryAdd(NativeHashMapData* data, TKey key, TValue item, bool isMultiHashMap, Allocator allocation)
		{
			TValue tempItem;
			NativeMultiHashMapIterator<TKey> tempIt;
			if (!isMultiHashMap && TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
				return false;
			// Allocate an entry from the free list
			int idx;
			int* nextPtrs;

			if (data->allocatedIndexLength >= data->capacity && data->firstFreeTLS[0] < 0)
			{
				for (int tls = 1; tls < JobsUtility.MaxJobThreadCount; ++tls)
				{
					if (data->firstFreeTLS[tls*NativeHashMapData.IntsPerCacheLine] >= 0)
					{
						idx = data->firstFreeTLS[tls*NativeHashMapData.IntsPerCacheLine];
						nextPtrs = (int*)data->next;
						data->firstFreeTLS[tls*NativeHashMapData.IntsPerCacheLine] = nextPtrs[idx];
						nextPtrs[idx] = -1;
						data->firstFreeTLS[0] = idx;
						break;
					}
				}
				if (data->firstFreeTLS[0] < 0)
				{
					int newCap = NativeHashMapData.GrowCapacity(data->capacity);
					NativeHashMapData.ReallocateHashMap<TKey, TValue>(data, newCap, NativeHashMapData.GetBucketSize(newCap), allocation);
				}
			}
			idx = data->firstFreeTLS[0];
			if (idx >= 0)
			{
				data->firstFreeTLS[0] = ((int*)data->next)[idx];
			}
			else
				idx = data->allocatedIndexLength++;

			if (idx < 0 || idx >= data->capacity)
				throw new System.InvalidOperationException("Internal HashMap error");

			// Write the new value to the entry
			UnsafeUtility.WriteArrayElement (data->keys, idx, key);
			UnsafeUtility.WriteArrayElement (data->values, idx, item);

			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			// Add the index to the hash-map
			int* buckets = (int*)data->buckets;
			nextPtrs = (int*)data->next;

			nextPtrs[idx] = buckets[bucket];
			buckets[bucket] = idx;

			return true;
		}

		static unsafe public void Remove(NativeHashMapData* data, TKey key, bool isMultiHashMap)
		{
			// First find the slot based on the hash
			int* buckets = (int*)data->buckets;
			int* nextPtrs = (int*)data->next;
			int bucket = key.GetHashCode() & data->bucketCapacityMask;

			int prevEntry = -1;
			int entryIdx = buckets[bucket];
			while (entryIdx >= 0 && entryIdx < data->capacity)
			{
				if (UnsafeUtility.ReadArrayElement<TKey> (data->keys, entryIdx).Equals(key))
				{
					// Found matching element, remove it
					if (prevEntry < 0)
						buckets[bucket] = nextPtrs[entryIdx];
					else
						nextPtrs[prevEntry] = nextPtrs[entryIdx];
					// And free the index
					int nextIdx = nextPtrs[entryIdx];
					nextPtrs[entryIdx] = data->firstFreeTLS[0];
					data->firstFreeTLS[0] = entryIdx;
					entryIdx = nextIdx;
					// Can only be one hit in regular hashmaps, so return
					if (!isMultiHashMap)
						return;
				}
				else
				{
					prevEntry = entryIdx;
					entryIdx = nextPtrs[entryIdx];
				}
			}
		}
		static unsafe public void Remove(NativeHashMapData* data, NativeMultiHashMapIterator<TKey> it)
		{
			// First find the slot based on the hash
			int* buckets = (int*)data->buckets;
			int* nextPtrs = (int*)data->next;
			int bucket = it.key.GetHashCode() & data->bucketCapacityMask;

			int entryIdx = buckets[bucket];
			if (entryIdx == it.EntryIndex)
			{
				buckets[bucket] = nextPtrs[entryIdx];
			}
			else
			{
				while (entryIdx >= 0 && nextPtrs[entryIdx] != it.EntryIndex)
					entryIdx = nextPtrs[entryIdx];
				if (entryIdx < 0)
					throw new InvalidOperationException("Invalid iterator passed to HashMap remove");
				nextPtrs[entryIdx] = nextPtrs[it.EntryIndex];
			}
			// And free the index
			nextPtrs[it.EntryIndex] = data->firstFreeTLS[0];
			data->firstFreeTLS[0] = it.EntryIndex;
		}

		static unsafe public bool TryGetFirstValueAtomic(NativeHashMapData* data, TKey key, out TValue item, out NativeMultiHashMapIterator<TKey> it)
		{
			it.key = key;
            if (data->allocatedIndexLength <= 0)
			{
				it.EntryIndex = it.NextEntryIndex = -1;
				item = default(TValue);
				return false;
			}
			// First find the slot based on the hash
			int* buckets = (int*)data->buckets;
			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			it.EntryIndex = it.NextEntryIndex = buckets[bucket];
			return TryGetNextValueAtomic(data, out item, ref it);
		}

		static unsafe public bool TryGetNextValueAtomic(NativeHashMapData* data, out TValue item, ref NativeMultiHashMapIterator<TKey> it)
		{
			int entryIdx = it.NextEntryIndex;
			it.NextEntryIndex = -1;
			it.EntryIndex = -1;
			item = default(TValue);
			if (entryIdx < 0 || entryIdx >= data->capacity)
				return false;
			int* nextPtrs = (int*)data->next;
			while (!UnsafeUtility.ReadArrayElement<TKey> (data->keys, entryIdx).Equals(it.key))
			{
				entryIdx = nextPtrs[entryIdx];
				if (entryIdx < 0 || entryIdx >= data->capacity)
					return false;
			}
			it.NextEntryIndex = nextPtrs[entryIdx];
			it.EntryIndex = entryIdx;

			// Read the value
			item = UnsafeUtility.ReadArrayElement<TValue>(data->values, entryIdx);

			return true;
		}

		static unsafe public bool SetValue(NativeHashMapData* data, ref NativeMultiHashMapIterator<TKey> it, ref TValue item)
		{
			int entryIdx = it.EntryIndex;
			if (entryIdx < 0 || entryIdx >= data->capacity)
				return false;

			UnsafeUtility.WriteArrayElement(data->values, entryIdx, item);
			return true;
		}

	}

	[StructLayout (LayoutKind.Sequential)]
	[NativeContainer]
	public unsafe struct NativeHashMap<TKey, TValue>
		where TKey : struct, System.IEquatable<TKey>
		where TValue : struct
	{
		[NativeDisableUnsafePtrRestriction]
		NativeHashMapData*      m_Buffer;

		#if ENABLE_UNITY_COLLECTIONS_CHECKS
		AtomicSafetyHandle 		m_Safety;
		[NativeSetClassTypeToNullOnSchedule]
		DisposeSentinel			m_DisposeSentinel;
		#endif

		Allocator 				m_AllocatorLabel;

		public unsafe NativeHashMap(int capacity, Allocator label)
		{
			m_AllocatorLabel = label;
			// Bucket size if bigger to reduce collisions
			NativeHashMapData.AllocateHashMap<TKey, TValue> (capacity, capacity*2, label, out m_Buffer);

			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, 0);
			#endif

			Clear();
		}

		unsafe public int Length
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				int* nextPtrs = (int*)data->next;
				int freeListSize = 0;
				for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
					for (int freeIdx = data->firstFreeTLS[tls * NativeHashMapData.IntsPerCacheLine]; freeIdx >= 0; freeIdx = nextPtrs[freeIdx])
						++freeListSize;
				return Math.Min(data->capacity, data->allocatedIndexLength) - freeListSize;
			}
		}
		unsafe public int Capacity
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				return data->capacity;
			}
			set
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				NativeHashMapData.ReallocateHashMap<TKey, TValue>(data, value, NativeHashMapData.GetBucketSize(value), m_AllocatorLabel);
			}
		}

		unsafe public void Clear()
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.Clear((NativeHashMapData*)m_Buffer);
		}

		unsafe public bool TryAdd(TKey key, TValue item)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			return NativeHashMapBase<TKey, TValue>.TryAdd((NativeHashMapData*)m_Buffer, key, item, false, m_AllocatorLabel);
		}

		unsafe public void Remove(TKey key)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.Remove((NativeHashMapData*)m_Buffer, key, false);
		}

		unsafe public bool TryGetValue(TKey key, out TValue item)
		{
			NativeMultiHashMapIterator<TKey> tempIt;
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
			#endif
			return NativeHashMapBase<TKey, TValue>.TryGetFirstValueAtomic((NativeHashMapData*)m_Buffer, key, out item, out tempIt);
		}

		public bool IsCreated
		{
			get { return m_Buffer != null; }
		}

		public void Dispose()
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(m_Safety, ref m_DisposeSentinel);
			#endif

			NativeHashMapData.DeallocateHashMap(m_Buffer, m_AllocatorLabel);
			m_Buffer = null;
		}


		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[NativeContainerNeedsThreadIndex]
		unsafe public struct Concurrent
		{
			[NativeDisableUnsafePtrRestriction]
			NativeHashMapData* 	m_Buffer;

			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle m_Safety;
			#endif

			int m_ThreadIndex;

			unsafe public static implicit operator NativeHashMap<TKey, TValue>.Concurrent (NativeHashMap<TKey, TValue> hashMap)
			{
				NativeHashMap<TKey, TValue>.Concurrent concurrent;
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(hashMap.m_Safety);
				concurrent.m_Safety = hashMap.m_Safety;
				AtomicSafetyHandle.UseSecondaryVersion(ref concurrent.m_Safety);
				#endif
				concurrent.m_ThreadIndex = 0;

				concurrent.m_Buffer = hashMap.m_Buffer;
				return concurrent;
			}

			unsafe public int Capacity
			{
				get
				{
					#if ENABLE_UNITY_COLLECTIONS_CHECKS
					AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
					#endif

					NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
					return data->capacity;
				}
			}


			unsafe public bool TryAdd(TKey key, TValue item)
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
				#endif
				return NativeHashMapBase<TKey, TValue>.TryAddAtomic((NativeHashMapData*)m_Buffer, key, item, m_ThreadIndex);
			}
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	[NativeContainer]
	unsafe public struct NativeMultiHashMap<TKey, TValue>
		where TKey : struct, System.IEquatable<TKey>
		where TValue : struct
	{
		[NativeDisableUnsafePtrRestriction]
		NativeHashMapData*      m_Buffer;

		#if ENABLE_UNITY_COLLECTIONS_CHECKS
		AtomicSafetyHandle 		m_Safety;
		[NativeSetClassTypeToNullOnSchedule]
		DisposeSentinel			m_DisposeSentinel;
		#endif

		Allocator 				m_AllocatorLabel;

		public NativeMultiHashMap(int capacity, Allocator label)
		{
			m_AllocatorLabel = label;
			// Bucket size if bigger to reduce collisions
			NativeHashMapData.AllocateHashMap<TKey, TValue> (capacity, capacity*2, label, out m_Buffer);

			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, 0);
			#endif

			Clear();
		}

		unsafe public int Length
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				int* nextPtrs = (int*)data->next;
				int freeListSize = 0;
				for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
					for (int freeIdx = data->firstFreeTLS[tls * NativeHashMapData.IntsPerCacheLine]; freeIdx >= 0; freeIdx = nextPtrs[freeIdx])
						++freeListSize;
				return Math.Min(data->capacity, data->allocatedIndexLength) - freeListSize;
			}
		}
		unsafe public int Capacity
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				return data->capacity;
			}
			set
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
				#endif

				NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
				NativeHashMapData.ReallocateHashMap<TKey, TValue>(data, value, NativeHashMapData.GetBucketSize(value), m_AllocatorLabel);
			}
		}

		unsafe public void Clear()
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.Clear((NativeHashMapData*)m_Buffer);
		}

		unsafe public void Add(TKey key, TValue item)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.TryAdd((NativeHashMapData*)m_Buffer, key, item, true, m_AllocatorLabel);
		}

		unsafe public void Remove(TKey key)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.Remove((NativeHashMapData*)m_Buffer, key, true);
		}
		unsafe public void Remove(NativeMultiHashMapIterator<TKey> it)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			NativeHashMapBase<TKey, TValue>.Remove((NativeHashMapData*)m_Buffer, it);
		}

		unsafe public bool TryGetFirstValue(TKey key, out TValue item, out NativeMultiHashMapIterator<TKey> it)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
			#endif
			return NativeHashMapBase<TKey, TValue>.TryGetFirstValueAtomic((NativeHashMapData*)m_Buffer, key, out item, out it);
		}

		unsafe public bool TryGetNextValue(out TValue item, ref NativeMultiHashMapIterator<TKey> it)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
			#endif
			return NativeHashMapBase<TKey, TValue>.TryGetNextValueAtomic((NativeHashMapData*)m_Buffer, out item, ref it);
		}

		unsafe public bool SetValue(TValue item, NativeMultiHashMapIterator<TKey> it)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
			#endif
			return NativeHashMapBase<TKey, TValue>.SetValue((NativeHashMapData*)m_Buffer, ref it, ref item);
		}


		public bool IsCreated
		{
			get { return m_Buffer != null; }
		}

		unsafe public void Dispose()
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
            DisposeSentinel.Dispose(m_Safety, ref m_DisposeSentinel);
			#endif

			NativeHashMapData.DeallocateHashMap(m_Buffer, m_AllocatorLabel);
			m_Buffer = null;
		}

		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[NativeContainerNeedsThreadIndex]
		unsafe public struct Concurrent
		{
			[NativeDisableUnsafePtrRestriction]
			NativeHashMapData* 	m_Buffer;

			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle m_Safety;
			#endif

			int m_ThreadIndex;

			unsafe public static implicit operator NativeMultiHashMap<TKey, TValue>.Concurrent (NativeMultiHashMap<TKey, TValue> multiHashMap)
			{
				NativeMultiHashMap<TKey, TValue>.Concurrent concurrent;
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(multiHashMap.m_Safety);
				concurrent.m_Safety = multiHashMap.m_Safety;
				AtomicSafetyHandle.UseSecondaryVersion(ref concurrent.m_Safety);
				#endif
				concurrent.m_ThreadIndex = 0;

				concurrent.m_Buffer = multiHashMap.m_Buffer;
				return concurrent;
			}

			unsafe public int Capacity
			{
				get
				{
					#if ENABLE_UNITY_COLLECTIONS_CHECKS
					AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
					#endif

					NativeHashMapData* data = (NativeHashMapData*)m_Buffer;
					return data->capacity;
				}
			}

			unsafe public void Add(TKey key, TValue item)
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
				#endif
				NativeHashMapBase<TKey, TValue>.TryAddAtomicMulti((NativeHashMapData*)m_Buffer, key, item, m_ThreadIndex);
			}
		}
	}
}
