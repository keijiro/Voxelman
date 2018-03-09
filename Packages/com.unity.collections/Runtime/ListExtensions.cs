using System;
using System.Collections.Generic;

namespace Unity.Collections
{
    public static class ListExtensions
    {
		public static void RemoveAtSwapBack<T>(this List<T> list, int index)
        {
            int lastIndex = list.Count - 1;
            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }
    }

	public static class ArrayExtensions
	{
		public static int IndexOf<T>(this NativeArray<T> array, T value) where T : struct, IComparable<T>
		{
			for (int i = 0; i != array.Length; i++)
			{
				if (array[i].CompareTo(value) == 0)
					return i;
			}
			return -1;
		}
	}

}
