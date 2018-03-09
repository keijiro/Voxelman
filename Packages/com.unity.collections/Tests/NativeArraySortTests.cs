using NUnit.Framework;
using Unity.Collections;

public class NativeArraySortTests
{
    [Test]
    public void SortNativeArray_RandomInts_ReturnSorted([Values(1, 10, 1000, 10000)] int size)
    {
        var random = new System.Random();
        NativeArray<int> array = new NativeArray<int>(size, Allocator.Persistent);
        Assert.IsTrue(array.IsCreated);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(int.MinValue, int.MaxValue);
        }

        array.Sort();

        int min = array[0];
        foreach (var i in array)
        {
            Assert.LessOrEqual(min, i);
            min = i;
        }
        array.Dispose();
    }

    [Test]
    public void SortNativeArray_SortedInts_ReturnSorted([Values(1, 10, 1000, 10000)] int size)
    {
        NativeArray<int> array = new NativeArray<int>(size, Allocator.Persistent);
        Assert.IsTrue(array.IsCreated);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        array.Sort();

        int min = array[0];
        foreach (var i in array)
        {
            Assert.LessOrEqual(min, i);
            min = i;
        }
        array.Dispose();
    }

    [Test]
    public void SortNativeArray_RandomBytes_ReturnSorted([Values(1, 10, 1000, 10000, 100000)] int size)
    {
        var random = new System.Random();
        NativeArray<byte> array = new NativeArray<byte>(size, Allocator.Persistent);
        Assert.IsTrue(array.IsCreated);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (byte)random.Next(byte.MinValue, byte.MinValue);
        }

        array.Sort();

        int min = array[0];
        foreach (var i in array)
        {
            Assert.LessOrEqual(min, i);
            min = i;
        }
        array.Dispose();
    }

    [Test]
    public void SortNativeArray_RandomShorts_ReturnSorted([Values(1, 10, 1000, 10000)] int size)
    {
        var random = new System.Random();
        NativeArray<short> array = new NativeArray<short>(size, Allocator.Persistent);
        Assert.IsTrue(array.IsCreated);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (short)random.Next(short.MinValue, short.MaxValue);
        }

        array.Sort();

        int min = array[0];
        foreach (var i in array)
        {
            Assert.LessOrEqual(min, i);
            min = i;
        }
        array.Dispose();
    }

    [Test]
    public void SortNativeArray_RandomFloats_ReturnSorted([Values(1, 10, 1000, 10000)] int size)
    {
        var random = new System.Random();
        NativeArray<float> array = new NativeArray<float>(size, Allocator.Persistent);
        Assert.IsTrue(array.IsCreated);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = (float)random.NextDouble();
        }

        array.Sort();

        float min = array[0];
        foreach (var i in array)
        {
            Assert.LessOrEqual(min, i);
            min = i;
        }
        array.Dispose();
    }
}
