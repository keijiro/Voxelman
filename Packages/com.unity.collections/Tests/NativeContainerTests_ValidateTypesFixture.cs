using Unity.Jobs;
using NUnit.Framework;
using System;

public class NativeContainerTests_ValidateTypesFixture
{
	protected static void CheckNativeContainerReflectionException<T>(string expected) where T : struct, IJob
	{ 
		var exc = Assert.Catch<InvalidOperationException> (() => { new T ().Schedule(); });
		Assert.AreEqual (expected, exc.Message);
	}

	protected static void CheckNativeContainerReflectionExceptionParallelFor<T>(string expected) where T : struct, IJobParallelFor
	{ 
		var exc = Assert.Catch<InvalidOperationException> (() => { new T ().Schedule (5, 1); });
		Assert.AreEqual (expected, exc.Message);
	}

	protected static void CheckNativeContainerReflectionException<T>() where T : struct, IJob
	{ 
		var exc = Assert.Catch<InvalidOperationException> (() => { new T ().Schedule (); });
		string expected = string.Format ("{0}.value is not a value type. Job structs may not contain any reference types.", typeof(T).Name);
		Assert.AreEqual (expected, exc.Message);
	}
}
