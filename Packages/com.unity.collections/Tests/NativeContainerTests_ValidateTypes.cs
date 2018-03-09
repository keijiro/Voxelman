using UnityEngine;
using Unity.Jobs;
using NUnit.Framework;
using Unity.Collections;

#pragma warning disable 0649

public class NativeContainerTests_ValidateTypes : NativeContainerTests_ValidateTypesFixture
{
	struct GenericStruct<T>
	{
		public T value;
	}
	
	class GenericClass<T>
	{
		public T value;
	}
	
	enum MyTestEnum
	{
		HellWorld
	}
		
	struct StructWithVariousStructsAndValueTypesJob : IJob
	{
		[ReadOnly]
		public NativeArray<float> 	nativeArrayRO;
		
		public NativeArray<float> 	nativeArrayRW;

		GenericStruct<float>	value;	
		Vector3 				vec3;
		float 					floatVal;
		Matrix4x4 				matrix;
		int 					intValue;
		byte 					byteValue;
		short 					shortValue;
		char 					charValue;
		MyTestEnum 				myEnum;

		public void Execute() {}
	}
	
	struct StructEmptyJob : IJob
	{
		public void Execute() {}
	}

	[Test]
	public void Scheduling_With_Supported_Types()
	{
		var temp1 = new NativeArray<float> (1, Allocator.Temp);
		var temp2 = new NativeArray<float> (1, Allocator.Temp);

		var types = new StructWithVariousStructsAndValueTypesJob();
		types.nativeArrayRO = temp1;
		types.nativeArrayRW = temp2;
		types.Schedule ().Complete();
		new StructEmptyJob ().Schedule ().Complete();

		temp1.Dispose ();
		temp2.Dispose ();
	}
}
