using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    class SharedComponentDataManager
    {
        NativeMultiHashMap<int, int> m_HashLookup = new NativeMultiHashMap<int, int>(128, Allocator.Persistent);

        List<object>    m_SharedComponentData = new List<object>();
        NativeList<int> m_SharedComponentRefCount = new NativeList<int>(0, Allocator.Persistent);
        NativeList<int> m_SharedComponentVersion = new NativeList<int>(0, Allocator.Persistent);
        NativeList<int> m_SharedComponentType = new NativeList<int>(0, Allocator.Persistent);

        public SharedComponentDataManager()
        {
            m_SharedComponentData.Add(null);
            m_SharedComponentRefCount.Add(1);
            m_SharedComponentVersion.Add(1);
            m_SharedComponentType.Add(-1);
        }

        public void Dispose()
        {
            m_SharedComponentType.Dispose();
            m_SharedComponentRefCount.Dispose();
            m_SharedComponentVersion.Dispose();
            m_SharedComponentData.Clear();
            m_SharedComponentData = null;
            m_HashLookup.Dispose();
        }

        public void GetAllUniqueSharedComponents<T>(List<T> sharedComponentValues) where T : struct, ISharedComponentData
        {
            sharedComponentValues.Add(default(T));
            for (var i = 1; i != m_SharedComponentData.Count; i++)
            {
                var data = m_SharedComponentData[i];
                if (data != null && data.GetType() == typeof(T))
                    sharedComponentValues.Add((T)m_SharedComponentData[i]);
            }
        }

        public int InsertSharedComponent<T>(T newData) where T : struct
        {
            int typeIndex = TypeManager.GetTypeIndex<T>();
            int index = FindSharedComponentIndex(TypeManager.GetTypeIndex<T>(), newData);

            if (index == 0)
            {
                return 0;
            }
            else if (index != -1)
            {
                m_SharedComponentRefCount[index]++;
                return index;
            }
            else
            {
                var fastLayout = TypeManager.GetComponentType(typeIndex).FastEqualityLayout;
                var hashcode = FastEquality.GetHashCode(ref newData, fastLayout);
                return Add(typeIndex, hashcode, newData);
            }
        }

        unsafe int FindSharedComponentIndex<T>(int typeIndex, T newData) where T : struct
        {
            var defaultVal = default(T);
            var fastLayout = TypeManager.GetComponentType(typeIndex).FastEqualityLayout;
            if (FastEquality.Equals(ref defaultVal, ref newData, fastLayout))
                return 0;
            else
                return FindNonDefaultSharedComponentIndex(typeIndex, FastEquality.GetHashCode(ref newData, fastLayout),
                    UnsafeUtility.AddressOf(ref newData), fastLayout);
        }

        unsafe int FindNonDefaultSharedComponentIndex(int typeIndex, int hashCode, void* newData, FastEquality.Layout[] layout)
        {
            int itemIndex;
            NativeMultiHashMapIterator<int> iter;

            if (!m_HashLookup.TryGetFirstValue(hashCode, out itemIndex, out iter))
                return -1;

            do
            {
                var data = m_SharedComponentData[itemIndex];
                if (data != null && m_SharedComponentType[itemIndex] == typeIndex)
                {
                    ulong handle;
                    void* value = PinGCObjectAndGetAddress(data, out handle);
                    bool res = FastEquality.Equals(newData, value, layout);
                    UnsafeUtility.ReleaseGCObject(handle);

                    if (res)
                        return itemIndex;
                }
            } 
            while (m_HashLookup.TryGetNextValue(out itemIndex, ref iter));

            return -1;
        }

        unsafe int InsertSharedComponentAssumeNonDefault(int typeIndex, int hashCode, object newData, FastEquality.Layout[] layout)
        {
            ulong handle;
            void* newDataPtr = PinGCObjectAndGetAddress(newData, out handle);
            
            int index = FindNonDefaultSharedComponentIndex(typeIndex, hashCode, newDataPtr, layout);

            UnsafeUtility.ReleaseGCObject(handle);

            if (-1 == index)
                index = Add(typeIndex, hashCode, newData);
            else
                m_SharedComponentRefCount[index] += 1;

            return index;
        }

        int Add(int typeIndex, int hashCode, object newData)
        {
            int index = m_SharedComponentData.Count;
            m_HashLookup.Add(hashCode, index);
            m_SharedComponentData.Add(newData);
            m_SharedComponentRefCount.Add(1);
            m_SharedComponentVersion.Add(1);
            m_SharedComponentType.Add(typeIndex);
            return index;
        }
        

        public void IncrementSharedComponentVersion(int index)
        {
            m_SharedComponentVersion[index]++;
        }

        public int GetSharedComponentVersion<T>(T sharedData) where T : struct
        {
            var index = FindSharedComponentIndex(TypeManager.GetTypeIndex<T>(), sharedData);
            return index == -1 ? 0 : m_SharedComponentVersion[index];
        }

        public T GetSharedComponentData<T>(int index) where T : struct
        {
            if (index == 0)
                return default(T);

            return (T) m_SharedComponentData[index];
        }

        public void AddReference(int index)
        {
            if (index != 0)
                ++m_SharedComponentRefCount[index];
        }

        unsafe static int GetHashCodeFast(object target, FastEquality.Layout[] fastLayout)
        {
            ulong handle;
            void* ptr = PinGCObjectAndGetAddress(target, out handle);
            var hashCode = FastEquality.GetHashCode(ptr, fastLayout);
            UnsafeUtility.ReleaseGCObject(handle);

            return hashCode;
        }
    
        unsafe static void* PinGCObjectAndGetAddress(object target, out ulong handle)
        {
            void* ptr = UnsafeUtility.PinGCObjectAndGetAddress(target, out handle);
            return (byte*)ptr + TypeManager.ObjectOffset;
        }
        
        
        public void RemoveReference(int index)
        {
            if (index == 0)
                return;

            var newCount = --m_SharedComponentRefCount[index];

            if (newCount != 0)
                return;

            var typeIndex = m_SharedComponentType[index];
            
            var fastLayout = TypeManager.GetComponentType(typeIndex).FastEqualityLayout;
            var hashCode = GetHashCodeFast(m_SharedComponentData[index], fastLayout);
            
            m_SharedComponentData[index] = null;
            m_SharedComponentType[index] = -1;

            int itemIndex;
            NativeMultiHashMapIterator<int> iter;
            if (!m_HashLookup.TryGetFirstValue(hashCode, out itemIndex, out iter))
                return;

            do
            {
                if (itemIndex != index)
                    continue;

                m_HashLookup.Remove(iter);
                break;
            }
            while (m_HashLookup.TryGetNextValue(out itemIndex, ref iter));
        }

        
        unsafe public void MoveSharedComponents(SharedComponentDataManager srcSharedComponents, int* sharedComponentIndices, int sharedComponentIndicesCount)
        {
            for (int i = 0;i != sharedComponentIndicesCount;i++)
            {
                int srcIndex = sharedComponentIndices[i];
                if (srcIndex == 0)
                    continue;

                var srcData = srcSharedComponents.m_SharedComponentData[srcIndex];
                int typeIndex = srcSharedComponents.m_SharedComponentType[srcIndex];
                
                var fastLayout = TypeManager.GetComponentType(typeIndex).FastEqualityLayout;
                var hashCode = GetHashCodeFast(srcData, fastLayout);
                
                int dstIndex = InsertSharedComponentAssumeNonDefault(typeIndex, hashCode, srcData, fastLayout);
                srcSharedComponents.RemoveReference(srcIndex);

                sharedComponentIndices[i] = dstIndex;
            }
        }
    }
}
