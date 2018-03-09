using System;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;

namespace UnityEngine.ECS.Tests
{
    public class MoveEntitiesFrom : ECSTestsFixture
    {
        [Test]
        public void MoveEntitiesToSameEntityManagerThrows()
        {
            Assert.Throws<ArgumentException>(() => { m_Manager.MoveEntitiesFrom(m_Manager); });
        }

        [Test]
        public void MoveEntities()
        {
            var creationWorld = new World("CreationWorld");
            var creationManager = creationWorld.GetOrCreateManager<EntityManager>();

            var archetype = creationManager.CreateArchetype(typeof(EcsTestData), typeof(EcsTestData2));
            
            var entities = new NativeArray<Entity>(10000, Allocator.Temp);
            creationManager.CreateEntity(archetype, entities);
            for (int i = 0; i != entities.Length; i++)
                creationManager.SetComponentData(entities[i], new EcsTestData(i));
                

            m_Manager.CheckInternalConsistency();
            creationManager.CheckInternalConsistency();

            m_Manager.MoveEntitiesFrom(creationManager);

            for (int i = 0;i != entities.Length;i++)
                Assert.IsFalse(creationManager.Exists(entities[0]));

            m_Manager.CheckInternalConsistency();
            creationManager.CheckInternalConsistency();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
            Assert.AreEqual(entities.Length, group.CalculateLength());
            Assert.AreEqual(0, creationManager.CreateComponentGroup(typeof(EcsTestData)).CalculateLength());

            // We expect that the order of the crated entities is the same as in the creation scene
            var testDataArray = group.GetComponentDataArray<EcsTestData>();
            for (int i = 0; i != testDataArray.Length; i++)
                Assert.AreEqual(i, testDataArray[i].value);

            entities.Dispose();
            creationWorld.Dispose();
        }

        [Test]
        public void MoveEntitiesWithSharedComponentData()
        {
            var creationWorld = new World("CreationWorld");
            var creationManager = creationWorld.GetOrCreateManager<EntityManager>();

            var archetype = creationManager.CreateArchetype(typeof(EcsTestData), typeof(EcsTestData2), typeof(SharedData1));

            var entities = new NativeArray<Entity>(10000, Allocator.Temp);
            creationManager.CreateEntity(archetype, entities);
            for (int i = 0; i != entities.Length; i++)
            {
                creationManager.SetComponentData(entities[i], new EcsTestData(i));
                creationManager.SetSharedComponentData(entities[i], new SharedData1(i % 5));
            }

            m_Manager.CheckInternalConsistency();
            creationManager.CheckInternalConsistency();

            m_Manager.MoveEntitiesFrom(creationManager);

            m_Manager.CheckInternalConsistency();
            creationManager.CheckInternalConsistency();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData), typeof(SharedData1));
            Assert.AreEqual(entities.Length, group.CalculateLength());
            Assert.AreEqual(0, creationManager.CreateComponentGroup(typeof(EcsTestData)).CalculateLength());

            // We expect that the order of the crated entities is the same as in the creation scene
            var testDataArray = group.GetComponentDataArray<EcsTestData>();
            var testSharedDataArray = group.GetSharedComponentDataArray<SharedData1>();
            for (int i = 0;i != testDataArray.Length;i++)
                Assert.AreEqual(testSharedDataArray[i].value, testDataArray[i].value % 5);

            entities.Dispose();
            creationWorld.Dispose();
        }

        [Test]
        [Ignore("NOT IMPLEMENTED")]
        public void MoveEntitiesPatchesEntityReferences()
        {

        }
        
        [Test]
        [Ignore("NOT IMPLEMENTED")]
        public void UsingComponentGroupOrArchetypeorEntityFromDifferentEntityManagerGivesExceptions()
        {
        }
    }
}
