using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace UnityEngine.ECS.Tests
{
	public class InjectComponentGroupTests : ECSTestsFixture
	{
		[DisableAutoCreation]
		[AlwaysUpdateSystem]
		public class PureEcsTestSystem : ComponentSystem
		{
			public struct DataAndEntites
			{
				public ComponentDataArray<EcsTestData> Data;
				public EntityArray                     Entities;
				public int                             Length;
			}

			[Inject]
			public DataAndEntites Group;

			protected override void OnUpdate()
			{
			}
		}

		[DisableAutoCreation]
		[AlwaysUpdateSystem]
		public class PureReadOnlySystem : ComponentSystem
		{
			public struct Datas
			{
				[ReadOnly]
				public ComponentDataArray<EcsTestData> Data;
			}

			[Inject]
			public Datas Group;

			protected override void OnUpdate()
			{
			}
		}

		[DisableAutoCreation]
		[AlwaysUpdateSystem]
		public class SubtractiveSystem : ComponentSystem
		{
			public struct Datas
			{
				public ComponentDataArray<EcsTestData> Data;
				public SubtractiveComponent<EcsTestData2> Data2;
			}

			[Inject]
			public Datas Group;

			protected override void OnUpdate()
			{
			}
		}

	    public struct SharedData : ISharedComponentData
	    {
	        public int value;

	        public SharedData(int val) { value = val; }
	    }

	    [DisableAutoCreation]
	    [AlwaysUpdateSystem]
	    public class SharedComponentSystem : ComponentSystem
	    {
	        public struct Datas
	        {
	            public ComponentDataArray<EcsTestData> Data;
	            [ReadOnly] public SharedComponentDataArray<SharedData> SharedData;
	        }

	        [Inject]
	        public Datas Group;

	        protected override void OnUpdate()
	        {
	        }
	    }

		[Test]
        public void ReadOnlyComponentDataArray()
        {
            var readOnlySystem = World.GetOrCreateManager<PureReadOnlySystem> ();

            var go = m_Manager.CreateEntity (new ComponentType[0]);
            m_Manager.AddComponentData (go, new EcsTestData(2));

            readOnlySystem.Update ();
            Assert.AreEqual (2, readOnlySystem.Group.Data[0].value);
            Assert.Throws<System.InvalidOperationException>(()=> { readOnlySystem.Group.Data[0] = new EcsTestData(); });
        }

		[Test]
        public void SubtractiveComponent()
        {
            var subtractiveSystem = World.GetOrCreateManager<SubtractiveSystem> ();

            var go = m_Manager.CreateEntity (new ComponentType[0]);
            m_Manager.AddComponentData (go, new EcsTestData(2));

            subtractiveSystem.Update ();
            Assert.AreEqual (1, subtractiveSystem.Group.Data.Length);
            Assert.AreEqual (2, subtractiveSystem.Group.Data[0].value);
            m_Manager.AddComponentData (go, new EcsTestData2());
            subtractiveSystem.Update ();
            Assert.AreEqual (0, subtractiveSystem.Group.Data.Length);
        }

	    [Test]
	    public void SharedComponentDataArray()
	    {
	        var sharedComponentSystem = World.GetOrCreateManager<SharedComponentSystem> ();

	        var go = m_Manager.CreateEntity(new ComponentType[0]);
	        m_Manager.AddComponentData (go, new EcsTestData(2));
	        m_Manager.AddSharedComponentData(go, new SharedData(3));

	        sharedComponentSystem.Update ();
	        Assert.AreEqual (1, sharedComponentSystem.Group.Data.Length);
	        Assert.AreEqual (2, sharedComponentSystem.Group.Data[0].value);
	        Assert.AreEqual (3, sharedComponentSystem.Group.SharedData[0].value);
	    }


        [Test]
        public void RemoveComponentGroupTracking()
        {
            var pureSystem = World.GetOrCreateManager<PureEcsTestSystem> ();

            var go0 = m_Manager.CreateEntity (new ComponentType[0]);
            m_Manager.AddComponentData (go0, new EcsTestData(10));

            var go1 = m_Manager.CreateEntity ();
            m_Manager.AddComponentData (go1, new EcsTestData(20));

            pureSystem.Update ();
            Assert.AreEqual (2, pureSystem.Group.Length);
            Assert.AreEqual (10, pureSystem.Group.Data[0].value);
            Assert.AreEqual (20, pureSystem.Group.Data[1].value);

            m_Manager.RemoveComponent<EcsTestData> (go0);

            pureSystem.Update ();
            Assert.AreEqual (1, pureSystem.Group.Length);
            Assert.AreEqual (20, pureSystem.Group.Data[0].value);

            m_Manager.RemoveComponent<EcsTestData> (go1);
            pureSystem.Update ();
            Assert.AreEqual (0, pureSystem.Group.Length);
        }

        [Test]
        public void EntityGroupTracking()
        {
            var pureSystem = World.GetOrCreateManager<PureEcsTestSystem> ();

            var go = m_Manager.CreateEntity (new ComponentType[0]);
            m_Manager.AddComponentData (go, new EcsTestData(2));

            pureSystem.Update ();
            Assert.AreEqual (1, pureSystem.Group.Length);
            Assert.AreEqual (1, pureSystem.Group.Data.Length);
            Assert.AreEqual (1, pureSystem.Group.Entities.Length);
            Assert.AreEqual (2, pureSystem.Group.Data[0].value);
            Assert.AreEqual (go, pureSystem.Group.Entities[0]);
        }

		[DisableAutoCreation]
		public class FromEntitySystemIncrementInJob : JobComponentSystem
		{
			public struct IncrementValueJob : IJob
			{
				public Entity entity;

				public ComponentDataFromEntity<EcsTestData> ecsTestDataFromEntity;
				public FixedArrayFromEntity<int> intArrayFromEntity;

				public void Execute()
				{
					var array = intArrayFromEntity[entity];
					for (int i = 0;i<array.Length;i++)
						array[i]++;

					var value = ecsTestDataFromEntity[entity];
					value.value++;
					ecsTestDataFromEntity[entity] = value;
				}
			}

			[Inject]
			FixedArrayFromEntity<int> intArrayFromEntity;

		    [Inject]
			ComponentDataFromEntity<EcsTestData> ecsTestDataFromEntity;

			public Entity entity;

			protected override JobHandle OnUpdate(JobHandle inputDeps)
			{
				var job = new IncrementValueJob();
				job.entity = entity;
				job.ecsTestDataFromEntity = ecsTestDataFromEntity;
				job.intArrayFromEntity = intArrayFromEntity;

				return job.Schedule(inputDeps);
			}
		}

		[Test]
		public void FromEntitySystemIncrementInJobWorks()
		{
			var system = World.GetOrCreateManager<FromEntitySystemIncrementInJob> ();

			var entity = m_Manager.CreateEntity (typeof(EcsTestData), ComponentType.FixedArray(typeof(int), 5));
			system.entity = entity;
			system.Update();
			system.Update();

			Assert.AreEqual(2, m_Manager.GetComponentData<EcsTestData>(entity).value);
			Assert.AreEqual(2, m_Manager.GetFixedArray<int>(entity)[0]);
		}

		[DisableAutoCreation]
		public class OnCreateManagerComponentGroupInjectionSystem : JobComponentSystem
		{
			public struct Group
			{
				public ComponentDataArray<EcsTestData> Data;
			}

			[Inject]
			public Group group;

			protected override void OnCreateManager(int capacity)
			{
				Assert.AreEqual(1, group.Data.Length);
				Assert.AreEqual(42, group.Data[0].value);
			}
		}

		[Test]
		public void OnCreateManagerComponentGroupInjectionWorks()
		{
			var entity = m_Manager.CreateEntity (typeof(EcsTestData));
			m_Manager.SetComponentData(entity, new EcsTestData(42));
			World.GetOrCreateManager<OnCreateManagerComponentGroupInjectionSystem>();
		}

	    [DisableAutoCreation]
	    public class OnDestroyManagerComponentGroupInjectionSystem : JobComponentSystem
	    {
	        public struct Group
	        {
	            public ComponentDataArray<EcsTestData> Data;
	        }

	        [Inject]
	        public Group group;

	        protected override void OnDestroyManager()
	        {
	            Assert.AreEqual(1, group.Data.Length);
	            Assert.AreEqual(42, group.Data[0].value);
	        }
	    }

	    [Test]
	    public void OnDestroyManagerComponentGroupInjectionWorks()
	    {
	        var system = World.GetOrCreateManager<OnDestroyManagerComponentGroupInjectionSystem>();
	        var entity = m_Manager.CreateEntity (typeof(EcsTestData));
	        m_Manager.SetComponentData(entity, new EcsTestData(42));
	        World.DestroyManager(system);
	    }

	    [DisableAutoCreation]
	    public class IndexFromEntityMultipleArchetypesSytem : ComponentSystem
	    {
	        public struct Group
	        {
	            public ComponentDataArray<EcsTestData> Data;
	            [ReadOnly] public IndexFromEntity indexFromEntity;
	            [ReadOnly] public EntityArray entities;
	            public int Length;
	        }

	        [Inject]
	        public Group group;

	        struct CompareEntityIndex : IJobParallelFor
	        {
	            [ReadOnly] public IndexFromEntity indexFromEntity;
	            [ReadOnly] public EntityArray entities;

	            public void Execute(int index)
	            {
	                var entity = entities[index];
	                var entityIndex = indexFromEntity[entity];
	                Assert.AreEqual(index,entityIndex);
	            }
	        }

			protected override void OnUpdate()
			{
			    var compareEntityIndexJob = new CompareEntityIndex
			    {
			        indexFromEntity = group.indexFromEntity,
			        entities = group.entities
			    };
			    var compareEntityIndexJobHandle = compareEntityIndexJob.Schedule(group.Length, 64);
			    compareEntityIndexJobHandle.Complete();
			}
	    }

	    [Test]
	    public void IndexFromEntityMultipleArchetypes()
	    {
	        for (int i = 0; i < 512; i++)
	        {
	          var entity = m_Manager.CreateEntity (typeof(EcsTestData));
	          m_Manager.SetComponentData(entity, new EcsTestData(i));
	        }
	        for (int i = 0; i < 512; i++)
	        {
	          var entity = m_Manager.CreateEntity (typeof(EcsTestData),typeof(EcsTestData2));
	          m_Manager.SetComponentData(entity, new EcsTestData(i));
	          m_Manager.SetComponentData(entity, new EcsTestData2(i));
	        }
	        var system = World.GetOrCreateManager<IndexFromEntityMultipleArchetypesSytem>();
	        system.Update();
	    }
	}
}
