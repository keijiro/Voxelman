using NUnit.Framework;
using Unity.Collections;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
public class NativeHashMapTests_NativeChecks
{
	[Test]
	public void Double_Deallocate_Throws()
	{
		var hashMap = new NativeMultiHashMap<int, int> (16, Allocator.Temp);
		hashMap.Dispose ();
		Assert.Throws<System.InvalidOperationException> (() => { hashMap.Dispose (); });
	}
}
#endif
