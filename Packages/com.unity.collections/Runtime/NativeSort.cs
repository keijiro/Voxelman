using System;

using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
    public static class NativeSortExtension
    {
        unsafe public static void Sort<T>(this NativeArray<T> array) where T : struct, IComparable<T>
        {
            IntroSort<T>(array.GetUnsafePtr(), 0, array.Length - 1, (int)(2 * Math.Floor(Math.Log(array.Length, 2))));
        }

        const int k_IntrosortSizeThreshold = 16;
        unsafe static void IntroSort<T>(void* array, int lo, int hi, int depth) where T : struct, IComparable<T>
        {
            while (hi > lo)
            {
                int partitionSize = hi - lo + 1;
                if (partitionSize <= k_IntrosortSizeThreshold)
                {
                    if (partitionSize == 1)
                    {
                        return;
                    }
                    if (partitionSize == 2)
                    {
                        SwapIfGreaterWithItems<T>(array, lo, hi);
                        return;
                    }
                    if (partitionSize == 3)
                    {
                        SwapIfGreaterWithItems<T>(array, lo, hi - 1);
                        SwapIfGreaterWithItems<T>(array, lo, hi);
                        SwapIfGreaterWithItems<T>(array, hi - 1, hi);
                        return;
                    }

                    InsertionSort<T>(array, lo, hi);
                    return;
                }

                if (depth == 0)
                {
                    HeapSort<T>(array, lo, hi);
                    return;
                }
                depth--;

                int p = Partition<T>(array, lo, hi);
                IntroSort<T>(array, p + 1, hi, depth);
                hi = p - 1;
            }
        }

        unsafe static void InsertionSort<T>(void* array, int lo, int hi) where T : struct, IComparable<T>
        {
            int i, j;
            T t;
            for (i = lo; i < hi; i++)
            {
                j = i;
                t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
                while (j >= lo && t.CompareTo(UnsafeUtility.ReadArrayElement<T>(array, j)) < 0)
                {
                    UnsafeUtility.WriteArrayElement<T>(array, j + 1, UnsafeUtility.ReadArrayElement<T>(array, j));
                    j--;
                }
                UnsafeUtility.WriteArrayElement<T>(array, j + 1, t);
            }
        }

        unsafe static int Partition<T>(void* array, int lo, int hi) where T : struct, IComparable<T>
        {
            int mid = lo + ((hi - lo) / 2);
            SwapIfGreaterWithItems<T>(array, lo, mid);
            SwapIfGreaterWithItems<T>(array, lo, hi);
            SwapIfGreaterWithItems<T>(array, mid, hi);

            T pivot = UnsafeUtility.ReadArrayElement<T>(array, mid);
            Swap<T>(array, mid, hi - 1);
            int left = lo, right = hi - 1;

            while (left < right)
            {
                while (pivot.CompareTo(UnsafeUtility.ReadArrayElement<T>(array, ++left)) > 0) ;
                while (pivot.CompareTo(UnsafeUtility.ReadArrayElement<T>(array, --right)) < 0) ;

                if (left >= right)
                    break;

                Swap<T>(array, left, right);
            }

            Swap<T>(array, left, (hi - 1));
            return left;
        }

        unsafe static void HeapSort<T>(void* array, int lo, int hi) where T : struct, IComparable<T>
        {
            int n = hi - lo + 1;

            for (int i = n / 2; i >= 1; i--)
            {
                Heapify<T>(array, i, n, lo);
            }

            for (int i = n; i > 1; i--)
            {
                Swap<T>(array, lo, lo + i - 1);
                Heapify<T>(array, 1, i - 1, lo);
            }
        }

        unsafe static void Heapify<T>(void* array, int i, int n, int lo) where T : struct, IComparable<T>
        {
            T val = UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
            int child;
            while (i <= n / 2)
            {
                child = 2 * i;
                if (child < n && (UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1)).CompareTo(UnsafeUtility.ReadArrayElement<T>(array, (lo + child))) < 0)
                {
                    child++;
                }
                if ((UnsafeUtility.ReadArrayElement<T>(array, (lo + child - 1))).CompareTo(val) < 0)
                    break;

                UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1));
                i = child;
            }
            UnsafeUtility.WriteArrayElement(array, lo + i - 1, val);
        }

        unsafe static void Swap<T>(void* array, int lhs, int rhs) where T : struct, IComparable<T>
        {
            T val = UnsafeUtility.ReadArrayElement<T>(array, lhs);
            UnsafeUtility.WriteArrayElement<T>(array, lhs, UnsafeUtility.ReadArrayElement<T>(array, rhs));
            UnsafeUtility.WriteArrayElement<T>(array, rhs, val);
        }

        unsafe static void SwapIfGreaterWithItems<T>(void* array, int lhs, int rhs) where T : struct, IComparable<T>
        {
            if (lhs != rhs)
            {
                if (UnsafeUtility.ReadArrayElement<T>(array, lhs).CompareTo(UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
                {
                    Swap<T>(array, lhs, rhs);
                }
            }
        }
    }
}

