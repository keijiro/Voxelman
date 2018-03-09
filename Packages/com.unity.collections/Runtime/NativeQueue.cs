using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using System.Threading;

namespace Unity.Collections
{
    unsafe struct NativeQueueBlockHeader
    {
        public byte* nextBlock;
        public int itemsInBlock;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct NativeQueueBlockPoolData
    {
        internal IntPtr firstBlock;
        internal int allocatedBlocks;
        internal int MaxBlocks;
        internal const int BlockSize = 16*1024;

        public byte* AllocateBlock()
        {
            byte* block;
            do
            {
                block = (byte*)firstBlock;
                if (block == null)
                {
                    Interlocked.Increment(ref allocatedBlocks);
                    block = (byte*)UnsafeUtility.Malloc(BlockSize, 16, Allocator.Persistent);
                    return block;
                }
            } while (Interlocked.CompareExchange(ref firstBlock, (IntPtr)((NativeQueueBlockHeader*)block)->nextBlock, (IntPtr)block) != (IntPtr)block);
            return block;
        }
        public void FreeBlock(byte* block)
        {
            if (allocatedBlocks > MaxBlocks)
            {
                if (Interlocked.Decrement(ref allocatedBlocks) + 1 > MaxBlocks)
                {
                    UnsafeUtility.Free(block, Allocator.Persistent);
                    return;
                }
                Interlocked.Increment(ref allocatedBlocks);
            }
            do
            {
                ((NativeQueueBlockHeader*)block)->nextBlock = (byte*)firstBlock;
            } while (Interlocked.CompareExchange(ref firstBlock, (IntPtr)block, (IntPtr)((NativeQueueBlockHeader*)block)->nextBlock) != (IntPtr)((NativeQueueBlockHeader*)block)->nextBlock);
        }
    }
    internal unsafe static class NativeQueueBlockPool
    {
        static NativeQueueBlockPoolData data;
        public static NativeQueueBlockPoolData* QueueBlockPool
        {
            get
            {
                if (data.allocatedBlocks == 0)
                {
                    data.allocatedBlocks = data.MaxBlocks = 256;
                    // Allocate MaxBlocks items
                    byte* prev = null;
                    for (int i = 0; i < data.MaxBlocks; ++i)
                    {
                        NativeQueueBlockHeader* block = (NativeQueueBlockHeader*)UnsafeUtility.Malloc(NativeQueueBlockPoolData.BlockSize, 16, Allocator.Persistent);
                        block->nextBlock = prev;
                        prev = (byte*)block;
                        data.firstBlock = (IntPtr)block;
                    }
                    AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
                }
                return (NativeQueueBlockPoolData*)UnsafeUtility.AddressOf<NativeQueueBlockPoolData>(ref data);
            }
        }

        static void OnDomainUnload(object sender, EventArgs e)
        {
            while (data.firstBlock != IntPtr.Zero)
            {
                NativeQueueBlockHeader* block = (NativeQueueBlockHeader*)data.firstBlock;
                data.firstBlock = (IntPtr)(block->nextBlock);
                UnsafeUtility.Free(block, Allocator.Persistent);
                --data.allocatedBlocks;
            }
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct NativeQueueData
    {
        public byte* m_FirstBlock;
        public IntPtr m_LastBlock;
        public byte* m_FreeBlocks;

        public int m_ItemsPerBlock;

        public int m_CurrentReadIndexInBlock;

        public const int IntsPerCacheLine = JobsUtility.CacheLineSize / sizeof(int);

        public byte** m_CurrentWriteBlockTLS;

        public static byte* AllocateWriteBlockMT<T>(NativeQueueData* data, NativeQueueBlockPoolData* pool, int threadIndex) where T : struct
        {
            int tlsIdx = threadIndex;

            byte* currentWriteBlock = data->m_CurrentWriteBlockTLS[tlsIdx * IntsPerCacheLine];
            if (currentWriteBlock != null && ((NativeQueueBlockHeader*)currentWriteBlock)->itemsInBlock == data->m_ItemsPerBlock)
                currentWriteBlock = null;
            if (currentWriteBlock == null)
            {
                currentWriteBlock = pool->AllocateBlock();
                ((NativeQueueBlockHeader*)currentWriteBlock)->nextBlock = null;
                ((NativeQueueBlockHeader*)currentWriteBlock)->itemsInBlock = 0;
                if (data->m_LastBlock == IntPtr.Zero && Interlocked.CompareExchange(ref data->m_LastBlock, (IntPtr)currentWriteBlock, IntPtr.Zero) == IntPtr.Zero)
                    data->m_FirstBlock = currentWriteBlock;
                else
                {
                    NativeQueueBlockHeader* prevLast = (NativeQueueBlockHeader*)Interlocked.Exchange(ref data->m_LastBlock, (IntPtr)currentWriteBlock);
                    prevLast->nextBlock = currentWriteBlock;
                }
                data->m_CurrentWriteBlockTLS[tlsIdx * IntsPerCacheLine] = currentWriteBlock;
            }
            return currentWriteBlock;
        }

        public unsafe static void AllocateQueue<T>(Allocator label, out NativeQueueData* outBuf) where T : struct
        {
            var data = (NativeQueueData*)UnsafeUtility.Malloc(UnsafeUtility.SizeOf<NativeQueueData>() + UnsafeUtility.SizeOf<IntPtr>()*JobsUtility.MaxJobThreadCount * IntsPerCacheLine, UnsafeUtility.AlignOf<NativeQueueData>(), label);

            data->m_CurrentWriteBlockTLS = (byte**)(((byte*)data) + UnsafeUtility.SizeOf<NativeQueueData>());

            data->m_FirstBlock = null;
            data->m_LastBlock = IntPtr.Zero;
            data->m_FreeBlocks = null;
            data->m_ItemsPerBlock = (NativeQueueBlockPoolData.BlockSize - UnsafeUtility.SizeOf<NativeQueueBlockHeader>()) / UnsafeUtility.SizeOf<T>();

            data->m_CurrentReadIndexInBlock = 0;
            for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
                data->m_CurrentWriteBlockTLS[tls * IntsPerCacheLine] = null;

            outBuf = data;
        }

        public unsafe static void DeallocateQueue(NativeQueueData* data, NativeQueueBlockPoolData* pool, Allocator allocation)
        {
            NativeQueueBlockHeader* firstBlock = (NativeQueueBlockHeader*)data->m_FirstBlock;
            while (firstBlock != null)
            {
                NativeQueueBlockHeader* next = (NativeQueueBlockHeader*)firstBlock->nextBlock;
                pool->FreeBlock((byte*)firstBlock);
                firstBlock = next;
            }
            UnsafeUtility.Free(data, allocation);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    [NativeContainer]
    unsafe public struct NativeQueue<T> where T : struct
    {
	    [NativeDisableUnsafePtrRestriction]
        NativeQueueData* m_Buffer;

        [NativeDisableUnsafePtrRestriction]
        NativeQueueBlockPoolData* m_QueuePool;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        AtomicSafetyHandle m_Safety;
	    [NativeSetClassTypeToNullOnSchedule]
        DisposeSentinel m_DisposeSentinel;
#endif

        Allocator m_AllocatorLabel;

        public unsafe NativeQueue(Allocator label)
        {
            m_QueuePool = NativeQueueBlockPool.QueueBlockPool;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!UnsafeUtility.IsBlittable<T>())
                throw new ArgumentException(string.Format("{0} used in NativeQueue<{0}> must be blittable", typeof(T)));
#endif
            m_AllocatorLabel = label;

			NativeQueueData.AllocateQueue<T>(label, out m_Buffer);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
			DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, 0);
#endif
		}

		unsafe public int Count
		{
			get
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
#endif
                int count = 0;
                for (NativeQueueBlockHeader* block = (NativeQueueBlockHeader*)m_Buffer->m_FirstBlock; block != null; block = (NativeQueueBlockHeader*)block->nextBlock)
                    count += block->itemsInBlock;
                return count - m_Buffer->m_CurrentReadIndexInBlock;
			}
		}

        static public int PersistentMemoryBlockCount
        {
            get {return NativeQueueBlockPool.QueueBlockPool->MaxBlocks;}
            set {Interlocked.Exchange(ref NativeQueueBlockPool.QueueBlockPool->MaxBlocks, value);}
        }
        static public int MemoryBlockSize
        {
            get {return NativeQueueBlockPoolData.BlockSize;}
        }

		unsafe public T Peek()
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
#endif

            byte* firstBlock = (byte*)m_Buffer->m_FirstBlock;
            if (firstBlock == null)
                throw new InvalidOperationException("Trying to peek from an empty queue");
			return UnsafeUtility.ReadArrayElement<T>(firstBlock + UnsafeUtility.SizeOf<NativeQueueBlockHeader>(), m_Buffer->m_CurrentReadIndexInBlock);
		}
		unsafe public void Enqueue(T entry)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif

			byte* writeBlock = NativeQueueData.AllocateWriteBlockMT<T>(m_Buffer, m_QueuePool, 0);
			UnsafeUtility.WriteArrayElement(writeBlock + UnsafeUtility.SizeOf<NativeQueueBlockHeader>(), ((NativeQueueBlockHeader*)writeBlock)->itemsInBlock, entry);
			++((NativeQueueBlockHeader*)writeBlock)->itemsInBlock;
		}

		unsafe public T Dequeue()
		{
			T item;
			if (!TryDequeue(out item))
				throw new InvalidOperationException("Trying to dequeue from an empty queue");
			return item;
		}
		unsafe public bool TryDequeue(out T item)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif

            byte* firstBlock = (byte*)m_Buffer->m_FirstBlock;
            if (firstBlock == null)
            {
                item = default(T);
                return false;
            }
			item = UnsafeUtility.ReadArrayElement<T>(firstBlock + UnsafeUtility.SizeOf<NativeQueueBlockHeader>(), m_Buffer->m_CurrentReadIndexInBlock);
            ++m_Buffer->m_CurrentReadIndexInBlock;
            if (m_Buffer->m_CurrentReadIndexInBlock >= ((NativeQueueBlockHeader*)firstBlock)->itemsInBlock)
            {
                m_Buffer->m_FirstBlock = ((NativeQueueBlockHeader*)firstBlock)->nextBlock;
                m_QueuePool->FreeBlock(firstBlock);
                if (m_Buffer->m_FirstBlock == null)
                    m_Buffer->m_LastBlock = IntPtr.Zero;
                for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
                {
                    if (m_Buffer->m_CurrentWriteBlockTLS[tls * NativeQueueData.IntsPerCacheLine] == firstBlock)
                        m_Buffer->m_CurrentWriteBlockTLS[tls * NativeQueueData.IntsPerCacheLine] = null;
                }
                m_Buffer->m_CurrentReadIndexInBlock = 0;
            }
            return true;
		}

		unsafe public void Clear()
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif
            NativeQueueBlockHeader* firstBlock = (NativeQueueBlockHeader*)m_Buffer->m_FirstBlock;
            while (firstBlock != null)
            {
                NativeQueueBlockHeader* next = (NativeQueueBlockHeader*)firstBlock->nextBlock;
                m_QueuePool->FreeBlock((byte*)firstBlock);
                firstBlock = next;
            }
            m_Buffer->m_FirstBlock = null;
            m_Buffer->m_LastBlock = IntPtr.Zero;
            m_Buffer->m_CurrentReadIndexInBlock = 0;
            for (int tls = 0; tls < JobsUtility.MaxJobThreadCount; ++tls)
                m_Buffer->m_CurrentWriteBlockTLS[tls * NativeQueueData.IntsPerCacheLine] = null;
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

			NativeQueueData.DeallocateQueue(m_Buffer, m_QueuePool, m_AllocatorLabel);
			m_Buffer = null;
		}
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[NativeContainerNeedsThreadIndex]
		unsafe public struct Concurrent
		{
			[NativeDisableUnsafePtrRestriction]
			NativeQueueData* 	m_Buffer;
			[NativeDisableUnsafePtrRestriction]
			NativeQueueBlockPoolData* 	m_QueuePool;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle m_Safety;
#endif

			int m_ThreadIndex;

			unsafe public static implicit operator NativeQueue<T>.Concurrent (NativeQueue<T> queue)
			{
				NativeQueue<T>.Concurrent concurrent;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(queue.m_Safety);
				concurrent.m_Safety = queue.m_Safety;
				AtomicSafetyHandle.UseSecondaryVersion(ref concurrent.m_Safety);
#endif

				concurrent.m_Buffer = queue.m_Buffer;
                concurrent.m_QueuePool = queue.m_QueuePool;
				concurrent.m_ThreadIndex = 0;
				return concurrent;
			}

			unsafe public void Enqueue(T entry)
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
#endif
                byte* writeBlock = NativeQueueData.AllocateWriteBlockMT<T>(m_Buffer, m_QueuePool, m_ThreadIndex);
                UnsafeUtility.WriteArrayElement(writeBlock + UnsafeUtility.SizeOf<NativeQueueBlockHeader>(), ((NativeQueueBlockHeader*)writeBlock)->itemsInBlock, entry);
                ++((NativeQueueBlockHeader*)writeBlock)->itemsInBlock;
			}
		}
	}
}
