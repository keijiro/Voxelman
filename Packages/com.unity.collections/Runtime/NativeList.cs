using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using System.Diagnostics;

namespace Unity.Collections
{
	unsafe struct NativeListData
	{
		public void*                            list;
		public int								length;
		public int								capacity;

		public unsafe static void DeallocateList(void* buffer, Allocator allocation)
		{
			NativeListData* data = (NativeListData*)buffer;
			UnsafeUtility.Free (data->list, allocation);
			data->list = null;
			UnsafeUtility.Free (buffer, allocation);
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(NativeListDebugView < >))]
	unsafe public struct NativeList<T> where T : struct
	{
		[NativeDisableUnsafePtrRestriction]
		internal NativeListData*        m_Buffer;
		Allocator 						m_AllocatorLabel;
		#if ENABLE_UNITY_COLLECTIONS_CHECKS
		internal AtomicSafetyHandle 	m_Safety;
		[NativeSetClassTypeToNullOnSchedule]
		DisposeSentinel					m_DisposeSentinel;
		#endif

		unsafe public T this [int index]
		{
			get
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
                if ((uint)index >= (uint)m_Buffer->length)
                    throw new System.IndexOutOfRangeException(string.Format("Index {0} is out of range in NativeList of '{1}' Length.", index, m_Buffer->length));
#endif

                return UnsafeUtility.ReadArrayElement<T>(m_Buffer->list, index);
			}

			set
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckWriteAndThrow(m_Safety);
                if ((uint)index >= (uint)m_Buffer->length)
                    throw new System.IndexOutOfRangeException(string.Format("Index {0} is out of range in NativeList of '{1}' Length.", index, m_Buffer->length));
#endif

                UnsafeUtility.WriteArrayElement<T>(m_Buffer->list, index, value);
			}
		}

		unsafe public int Length
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				return m_Buffer->length;
			}
		}

		unsafe public int Capacity
		{
			get
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				#endif

				return m_Buffer->capacity;
			}

			set
			{
				#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckWriteAndBumpSecondaryVersion(m_Safety);

			    if (value < m_Buffer->length)
			        throw new System.ArgumentException("Capacity must be larger than the length of the NativeList.");
				#endif

				if (m_Buffer->capacity == value)
					return;

				void* newData = UnsafeUtility.Malloc (value * UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), m_AllocatorLabel);
				UnsafeUtility.MemCpy (newData, m_Buffer->list, m_Buffer->length * UnsafeUtility.SizeOf<T>());
				UnsafeUtility.Free (m_Buffer->list, m_AllocatorLabel);
			    m_Buffer->list = newData;
			    m_Buffer->capacity = value;
			}
		}

		unsafe public NativeList(Allocator i_label) : this (1, i_label, 1) { }
		unsafe public NativeList(int capacity, Allocator i_label) : this (capacity, i_label, 1) { }

		unsafe private NativeList(int capacity, Allocator i_label, int stackDepth)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (!UnsafeUtility.IsBlittable<T>())
                throw new ArgumentException(string.Format("{0} used in NativeList<{0}> must be blittable", typeof(T)));
#endif

            NativeListData* data  = (NativeListData*)UnsafeUtility.Malloc (sizeof(NativeListData), UnsafeUtility.AlignOf<NativeListData>(), i_label);

			int elementSize = UnsafeUtility.SizeOf<T> ();

            //@TODO: Find out why this is needed?
            capacity = Math.Max(1, capacity);
			data->list = UnsafeUtility.Malloc (capacity * elementSize, UnsafeUtility.AlignOf<T>(), i_label);

			data->length = 0;
			data->capacity = capacity;

		    m_Buffer = data;
			m_AllocatorLabel = i_label;

#if ENABLE_UNITY_COLLECTIONS_CHECKS

            DisposeSentinel.Create(out m_Safety, out m_DisposeSentinel, stackDepth);
#endif
		}

		unsafe public void Add(T element)
		{
			NativeListData* data = m_Buffer;
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndBumpSecondaryVersion(m_Safety);
			#endif

			if (data->length >= data->capacity)
				Capacity = data->length + data->capacity * 2;

			int length = data->length;
			data->length = length + 1;
			this[length] = element;
		}

        //@TODO: Test for AddRange
        unsafe public void AddRange(NativeArray<T> elements)
        {
            NativeListData* data = m_Buffer;
            #if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndBumpSecondaryVersion(m_Safety);
            #endif

            if (data->length + elements.Length > data->capacity)
                Capacity = data->length + elements.Length * 2;

            int sizeOf = UnsafeUtility.SizeOf<T> ();
            UnsafeUtility.MemCpy((byte*)data->list + data->length * sizeOf, elements.GetUnsafePtr(), sizeOf * elements.Length);

            data->length += elements.Length;
        }

		unsafe public void RemoveAtSwapBack(int index)
		{
			NativeListData* data = m_Buffer;
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndBumpSecondaryVersion(m_Safety);
			#endif

			int newLength = Length - 1;
			this[index] = this[newLength];
			data->length = newLength;
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

			NativeListData.DeallocateList(m_Buffer, m_AllocatorLabel);
			m_Buffer = null;
		}

		public void Clear()
		{
			ResizeUninitialized (0);
		}

		unsafe public static implicit operator NativeArray<T> (NativeList<T> nativeList)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle arraySafety = new AtomicSafetyHandle();
			AtomicSafetyHandle.CheckGetSecondaryDataPointerAndThrow(nativeList.m_Safety);
			arraySafety = nativeList.m_Safety;
			AtomicSafetyHandle.UseSecondaryVersion(ref arraySafety);
#endif

			NativeListData* data = (NativeListData*)nativeList.m_Buffer;
			var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T> (data->list, data->length, Allocator.Invalid);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, arraySafety);
#endif

            return array;
		}

		unsafe public T[] ToArray()
		{
			NativeArray<T> nativeArray = this;
			return nativeArray.ToArray();
		}

		public void CopyFrom(T[] array)
		{
			//@TODO: Thats not right... This doesn't perform a resize
			Capacity = array.Length;
			NativeArray<T> nativeArray = this;
			nativeArray.CopyFrom(array);
		}

		public unsafe void ResizeUninitialized(int length)
		{
			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			AtomicSafetyHandle.CheckWriteAndThrow (m_Safety);
			#endif

			Capacity = math.max(length, Capacity);
			NativeListData* data = (NativeListData*)m_Buffer;
			data->length = length;
		}
	}
    
    internal sealed class NativeListDebugView<T> where T : struct
    {
        private NativeList<T> m_Array;

        public NativeListDebugView(NativeList<T> array)
        {
            m_Array = array;
        }

        public T[] Items
        {
            get { return m_Array.ToArray(); }
        }
    }
}
namespace Unity.Collections.LowLevel.Unsafe
{
	public static class NativeListUnsafeUtility
	{
        public static unsafe void* GetUnsafePtr<T>(this NativeList<T> nativeList) where T : struct
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckWriteAndThrow(nativeList.m_Safety);
#endif
			NativeListData* data = (NativeListData*)nativeList.m_Buffer;
			return data->list;
        }
	}
}
