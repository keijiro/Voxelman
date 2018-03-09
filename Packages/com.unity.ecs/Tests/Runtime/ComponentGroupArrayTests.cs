using NUnit.Framework;
using Unity.Jobs;
using Unity.Collections;
using Unity.Entities;

namespace UnityEngine.ECS.Tests
{
    public class ComponentGroupArrayTests : ECSTestsFixture
	{
        public ComponentGroupArrayTests()
        {
            Assert.IsTrue(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobDebuggerEnabled, "JobDebugger must be enabled for these tests");
        }

		struct TestCopy1To2Job : IJob
		{
			public ComponentGroupArray<TestEntity> entities;
			unsafe public void Execute()
			{
				foreach (var e in entities)
					e.testData2->value0 = e.testData->value; 
			}
		}
	    
		struct TestCopy1To2ProcessEntityJob : IJobProcessEntities<TestEntity>
		{
			unsafe public void Execute(TestEntity e)
			{
				e.testData2->value0 = e.testData->value; 
			}
		}
		
		struct TestReadOnlyJob : IJob
		{
			public ComponentGroupArray<TestEntityReadOnly> entities;
			public void Execute()
			{
				foreach (var e in entities)
					;
			}
		}
		
		
	    //@TODO: Test for Entity setup with same component twice...
	    //@TODO: Test for subtractive components
	    //@TODO: Test for process ComponentGroupArray in job
	    
	    unsafe struct TestEntity
	    {
	        [ReadOnly]
	        public EcsTestData* testData;
	        public EcsTestData2* testData2;
	    }

		unsafe struct TestEntityReadOnly
		{
			[ReadOnly]
			public EcsTestData* testData;
			[ReadOnly]
			public EcsTestData2* testData2;
		}
		
	    [Test]
	    public void ComponentAccessAfterScheduledJobThrowsEntityArray()
	    {
	        var entityArrayCache = new ComponentGroupArrayStaticCache(typeof(TestEntity), m_Manager);
	        m_Manager.CreateComponentGroup(typeof(EcsTestData));
	        m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));

	        var job = new TestCopy1To2Job();
		    job.entities = new ComponentGroupArray<TestEntity>(entityArrayCache);

	        var fence = job.Schedule();
            
	        var entityArray = new ComponentGroupArray<TestEntity>(entityArrayCache);
	        Assert.Throws<System.InvalidOperationException>(() => { var temp = entityArray[0]; });

	        fence.Complete();
	    }

		[Test]
		public void IJobProcessEntitiesWorks()
		{
			var entityArrayCache = new ComponentGroupArrayStaticCache(typeof(TestEntity), m_Manager);

			var entities = new NativeArray<Entity>(100, Allocator.Persistent);
			var arch = m_Manager.CreateArchetype(typeof(EcsTestData), typeof(EcsTestData2));
			m_Manager.CreateEntity(arch, entities);
			for (int i = 0; i < entities.Length; i++)
				m_Manager.SetComponentData(entities[i], new EcsTestData(i));

			var job = new TestCopy1To2ProcessEntityJob();
			job.Schedule(new ComponentGroupArray<TestEntity>(entityArrayCache), 1).Complete();

			// not sure why this works. Shouldn't schedule throw an exception because parallelFor restriction
			for (int i = 0; i < entities.Length; i++)
				Assert.AreEqual(i, m_Manager.GetComponentData<EcsTestData2>(entities[i]).value0);;
			
			entities.Dispose();
			entityArrayCache.Dispose();
		}
			
	    [Test]
	    public void ComponentGroupArrayJobScheduleDetectsWriteDependency()
	    {
	        var entityArrayCache = new ComponentGroupArrayStaticCache(typeof(TestEntity), m_Manager);
	        var entity = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));
	        m_Manager.SetComponentData(entity, new EcsTestData(42));

	        var job = new TestCopy1To2Job();
	        job.entities = new ComponentGroupArray<TestEntity>(entityArrayCache);

	        var fence = job.Schedule();
			Assert.Throws<System.InvalidOperationException>(() => { job.Schedule(); });
			
	        fence.Complete();

		    entityArrayCache.Dispose();
	    }
		
		[Test]
		public void ComponentGroupArrayJobScheduleReadOnlyParallelIsAllowed()
		{
			var entityArrayCache = new ComponentGroupArrayStaticCache(typeof(TestEntityReadOnly), m_Manager);
			var entity = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));
			m_Manager.SetComponentData(entity, new EcsTestData(42));

			var job = new TestReadOnlyJob();
			job.entities = new ComponentGroupArray<TestEntityReadOnly>(entityArrayCache);

			var fence = job.Schedule();
			var fence2 = job.Schedule();
			
			JobHandle.CompleteAll(ref fence, ref fence2);
			entityArrayCache.Dispose();
		}

		unsafe struct TestEntitySub2
		{
			public EcsTestData* testData;
			public SubtractiveComponent<EcsTestData2> testData2;
		}
		
		[Test]
		public void ComponentGroupArraySubtractive()
		{
			var entityArrayCache = new ComponentGroupArrayStaticCache(typeof(TestEntitySub2), m_Manager);
			m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));
			m_Manager.CreateEntity(typeof(EcsTestData));

			var entities = new ComponentGroupArray<TestEntitySub2>(entityArrayCache);
			Assert.AreEqual(1, entities.Length);
		}
    }
}