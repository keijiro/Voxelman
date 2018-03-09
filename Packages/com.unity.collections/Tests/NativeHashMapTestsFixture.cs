using Unity.Jobs;
using Unity.Collections;

public class NativeHashMapTestsFixture
{
	protected const int hashMapSize = 10*1024;
	
	public struct HashMapWriteJob : IJob
	{
		public NativeHashMap<int, int>.Concurrent 	hashMap;
		public NativeArray<int> 		status;
		public int 						keyMod;

		public void Execute()
		{
			for (int i = 0; i < status.Length; i++)
			{
				status[i] = 0;
				try
				{
					if (!hashMap.TryAdd(i % keyMod, i))
						status[i] = -1;
				}
				catch (System.InvalidOperationException)
				{
					status[i] = -2;
				}
			}
		}
	}

	public struct HashMapReadParallelForJob : IJobParallelFor
	{
		[ReadOnlyAttribute]
		public NativeHashMap<int, int> 		hashMap;
		public NativeArray<int> 			values;
		public int 							keyMod;

		public void Execute(int i)
		{
			int iSquared;
			values[i] = -1;
			if (hashMap.TryGetValue(i % keyMod, out iSquared))
				values[i] = iSquared;
		}
	}
}
