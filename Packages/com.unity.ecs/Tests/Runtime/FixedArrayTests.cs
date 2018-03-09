using NUnit.Framework;
using Unity.Collections;
using System;
using Unity.Entities;

namespace UnityEngine.ECS.Tests
{
	public class FixedArrayTests : ECSTestsFixture
	{
		[Test]
		public void CreateEntityWithTwoSameTypeFixedArraysThrows()
		{
			var array11Type = ComponentType.FixedArray(typeof(int), 11);
			var array12Type = ComponentType.FixedArray(typeof(int), 12);
			Assert.Throws<ArgumentException>(() => { m_Manager.CreateEntity(array11Type, array12Type); });
		}
		
		[Test]
		public void GetComponentFixedArrayAgainstIComponentDataThrows()
		{
			var entity = m_Manager.CreateEntity(typeof(EcsTestData));
			Assert.Throws<ArgumentException>(() => { m_Manager.GetFixedArray<EcsTestData>(entity); });
		}

		[Test]
		public void CreatingFixedArrayOfIComponentDataThrows()
		{
			Assert.Throws<ArgumentException>(() => { m_Manager.CreateEntity(ComponentType.FixedArray(typeof(EcsTestData), 2)); });
		}

		[Test]
		public void CreateEntityWithIntThrows()
		{
			Assert.Throws<System.ArgumentException>(() => { m_Manager.CreateEntity(typeof(int));});
		}

		[Test]
		public void AddComponentWithIntThrows()
		{
			var entity = m_Manager.CreateEntity();
			Assert.Throws<System.ArgumentException>(() => { m_Manager.AddComponent(entity, ComponentType.Create<int>()); });
		}
		
		[Test]
		public void CreateEntityArrayWithValidLengths([Values(0, 1, 2, 100)]int length)
		{
			var entity = m_Manager.CreateEntity(ComponentType.FixedArray(typeof(int), length));
			
			var array = m_Manager.GetFixedArray<int>(entity);
			Assert.AreEqual(length, array.Length);
		}
		
		[Test]
		[TestCase(-1)]
		// Invalid because chunk size is too small to hold a single entity
		[TestCase(1024 * 1024)]
		[Ignore("Crashes")]
		public void CreateEntityWithInvalidFixedArraySize(int length)
		{
			var arrayType = ComponentType.FixedArray(typeof(int), length);
			Assert.Throws<ArgumentException>(() => m_Manager.CreateEntity(arrayType));
		}
		
		[Test]
		public void HasComponent()
		{
			var array11Type = ComponentType.FixedArray(typeof(int), 11);
			var array12Type = ComponentType.FixedArray(typeof(int), 12);
			var entity = m_Manager.CreateEntity(array11Type);
			
			Assert.IsTrue(m_Manager.HasComponent(entity, typeof(int)));			
			Assert.IsTrue(m_Manager.HasComponent(entity, array11Type));
			
			Assert.IsFalse(m_Manager.HasComponent(entity, array12Type));
		}
		
		[Test]
		public void RemoveComponentWithUnspecifiedLength()
		{
			var entity = m_Manager.CreateEntity(ComponentType.FixedArray(typeof(int), 11));
			m_Manager.RemoveComponent(entity, typeof(int));
			Assert.IsFalse(m_Manager.HasComponent(entity, typeof(int)));
		}
		
		[Test]
		public void RemoveComponentWithExactLength()
		{
			var fixed11 = ComponentType.FixedArray(typeof(int), 11);
			var entity = m_Manager.CreateEntity(fixed11 );
			m_Manager.RemoveComponent(entity, fixed11 );
			Assert.IsFalse(m_Manager.HasComponent(entity, typeof(int)));
		}

		[Test]
		public void RemoveComponentWithIncorrectLength()
		{
			var fixed11 = ComponentType.FixedArray(typeof(int), 11);
			var fixed1 = ComponentType.FixedArray(typeof(int), 1);
			var entity = m_Manager.CreateEntity(fixed11 );
			Assert.Throws<ArgumentException>(() => { m_Manager.RemoveComponent(entity, fixed1); });
		}

		[Test]
		public void MutateFixedArrayData()
		{
			var entity = m_Manager.CreateEntity();
			m_Manager.AddComponent(entity, ComponentType.FixedArray(typeof(int), 11));

			var array = m_Manager.GetFixedArray<int>(entity);
			
			Assert.AreEqual(11, array.Length);
			array[7] = 5;
			Assert.AreEqual(5, array[7]);
		}
		
		//@TODO: Should really test additional constraint against exact size as well...

		[Test]
        public void FixedArrayComponentGroupIteration()
        {
            /*var entity64 =*/ m_Manager.CreateEntity(ComponentType.FixedArray(typeof(int), 64));
	        /*var entity10 =*/ m_Manager.CreateEntity(ComponentType.FixedArray(typeof(int), 10));

            var group = m_Manager.CreateComponentGroup(typeof(int));

            var fixedArray = group.GetFixedArrayArray<int>();

	        Assert.AreEqual(2, fixedArray.Length);
	        Assert.AreEqual(64, fixedArray[0].Length);
	        Assert.AreEqual(10, fixedArray[1].Length);

	        Assert.AreEqual(0, fixedArray[0][3]);
	        Assert.AreEqual(0, fixedArray[1][3]);
	        
			NativeArray<int > array;
		        
	        array = fixedArray[0];
	        array[3] = 0;

	        array = fixedArray[1];
	        array[3] = 1;

            for (int i = 0; i < fixedArray.Length; i++)
            {
                Assert.AreEqual(i, fixedArray[i][3]);
            }
        }
		
		[Test]
		public void FixedArrayFromEntityWorks()
		{
			var entityInt = m_Manager.CreateEntity(ComponentType.FixedArray(typeof(int), 3));
			m_Manager.GetFixedArray<int>(entityInt).CopyFrom(new int[] { 1, 2, 3});
						
			var intLookup = EmptySystem.GetFixedArrayFromEntity<int>();
			Assert.IsTrue(intLookup.Exists(entityInt));
			Assert.IsFalse(intLookup.Exists(new Entity()));
			
			Assert.AreEqual(2, intLookup[entityInt][1]);
		}
	    

	    [Test]
	    [Ignore("Should work, need to write test")]
	    public void FixedArrayReadingFromTwoJobsInParallel()
	    {
	    }

	    [Test]
	    [Ignore("Should work, need to write test")]
	    public void FixedArrayWritingInJob()
	    {
	    }
	    
	    [Test]
	    [Ignore("Should work, need to write test")]
	    public void FixedArrayCantBeWrittenFromTwoJobsInParallel()
	    {
	    }
	}
}
