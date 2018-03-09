using System;
using Microsoft.Msagl.Core.Layout;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;

namespace UnityEngine.ECS.Tests
{
    public class IJobProcessComponentDataTests :ECSTestsFixture
    {
        struct Process1 : IJobProcessComponentData<EcsTestData>
        {
            public void Execute(ref EcsTestData value)
            {
                value.value++;
            }
        }

        struct Process2 : IJobProcessComponentData<EcsTestData, EcsTestData2>
        {
            public void Execute([ReadOnly]ref EcsTestData src, ref EcsTestData2 dst)
            {
                dst.value1 = src.value;
            }
        }

        struct Process3 : IJobProcessComponentData<EcsTestData, EcsTestData2, EcsTestData3>
        {
            public void Execute([ReadOnly]ref EcsTestData src, ref EcsTestData2 dst1, ref EcsTestData3 dst2)
            {
                dst1.value1 = dst2.value2 = src.value;
            }
        }
        
        [Test]
        public void JobProcessSimple()
        {
            var entity = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));
            m_Manager.SetComponentData(entity, new EcsTestData(42));

            new Process2().Run(EmptySystem);
            
            Assert.AreEqual(42, m_Manager.GetComponentData<EcsTestData2>(entity).value1);
        }
                
        [Test]
        public void JobProcessComponentGroupCorrect()
        {
            m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));

            ComponentType[] expectedTypes = { ComponentType.ReadOnly<EcsTestData>(), ComponentType.Create<EcsTestData2>() };

            new Process2().Run(EmptySystem);
            new Process2().Run(EmptySystem);
            var group = EmptySystem.GetComponentGroup(expectedTypes);
                        
            Assert.AreEqual(1, EmptySystem.ComponentGroups.Length);
            Assert.IsTrue(EmptySystem.ComponentGroups[0].CompareComponents(expectedTypes));
            Assert.AreEqual(group, EmptySystem.ComponentGroups[0]);
        }

        public enum ProcessMode
        {
            Single,
            Parallel,
            Run
        }

        void Schedule<T>(ProcessMode mode) where T : struct, IBaseJobProcessComponentData
        {
            if (mode == ProcessMode.Parallel)
                new T().Schedule(EmptySystem, 13).Complete();
            else if (mode == ProcessMode.Run)
                new T().Run(EmptySystem);
            else 
                new T().Schedule(EmptySystem).Complete();
        }

        [Theory]
        public void JobProcessStress_1(ProcessMode mode)
        {
            var archetype = m_Manager.CreateArchetype(typeof(EcsTestData));

            var entities = new NativeArray<Entity>(StressTestEntityCount, Allocator.Temp);
            m_Manager.CreateEntity(archetype, entities);

            Schedule<Process1>(mode);

            for (int i = 0; i < entities.Length; i++)
                Assert.AreEqual(1, m_Manager.GetComponentData<EcsTestData>(entities[i]).value);

            entities.Dispose();
        }
        
        [Theory]
        public void JobProcessStress_2(ProcessMode mode)
        {
            var archetype = m_Manager.CreateArchetype(typeof(EcsTestData), typeof(EcsTestData2));

            var entities = new NativeArray<Entity>(StressTestEntityCount, Allocator.Temp);
            m_Manager.CreateEntity(archetype, entities);
            
            for (int i = 0;i<entities.Length;i++)
                m_Manager.SetComponentData(entities[i], new EcsTestData(i));

            Schedule<Process2>(mode);

            for (int i = 0; i < entities.Length; i++)
                Assert.AreEqual(i, m_Manager.GetComponentData<EcsTestData2>(entities[i]).value1);

            entities.Dispose();
        }

        [Theory]
        public void JobProcessStress_3(ProcessMode mode)
        {
            var archetype = m_Manager.CreateArchetype(typeof(EcsTestData), typeof(EcsTestData2), typeof(EcsTestData3));

            var entities = new NativeArray<Entity>(StressTestEntityCount, Allocator.Temp);
            m_Manager.CreateEntity(archetype, entities);
            for (int i = 0;i<entities.Length;i++)
                m_Manager.SetComponentData(entities[i], new EcsTestData(i));

            Schedule<Process3>(mode);

            for (int i = 0; i < entities.Length; i++)
            {
                Assert.AreEqual(0, m_Manager.GetComponentData<EcsTestData2>(entities[i]).value0);
                Assert.AreEqual(0, m_Manager.GetComponentData<EcsTestData3>(entities[i]).value0);
                Assert.AreEqual(0, m_Manager.GetComponentData<EcsTestData3>(entities[i]).value1);

                Assert.AreEqual(i, m_Manager.GetComponentData<EcsTestData2>(entities[i]).value1);
                Assert.AreEqual(i, m_Manager.GetComponentData<EcsTestData3>(entities[i]).value2);
            }

            entities.Dispose();
        }
        
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        [Test]
        public void JobWithMissingDependency()
        {
            Assert.IsTrue(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobDebuggerEnabled, "JobDebugger must be enabled for these tests");

            m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));

            var job = new Process2().Schedule(EmptySystem, 64);
            Assert.Throws<InvalidOperationException>(() => { new Process2().Schedule(EmptySystem, 64); });
            
            job.Complete();
        }
#endif
        
        [RequireSubtractiveComponent(typeof(EcsTestData3))]
        [RequireComponentTag(typeof(EcsTestData4))]
        struct ProcessTagged : IJobProcessComponentData<EcsTestData, EcsTestData2>
        {
            public void Execute(ref EcsTestData src, ref EcsTestData2 dst)
            {
                dst.value1 = dst.value0 = src.value;
            }
        }
        
        void Test(bool didProcess, Entity entity)
        {
            m_Manager.SetComponentData(entity, new EcsTestData(42));

            new ProcessTagged().Schedule(EmptySystem, 64).Complete();

            if (didProcess)
                Assert.AreEqual(42, m_Manager.GetComponentData<EcsTestData2>(entity).value0);
            else
                Assert.AreEqual(0, m_Manager.GetComponentData<EcsTestData2>(entity).value0);
        }

        [Test]
        public void JobProcessAdditionalRequirements()
        {
            var entityIgnore0 = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2), typeof(EcsTestData3));
            Test(false, entityIgnore0);
            
            var entityIgnore1 = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2));
            Test(false, entityIgnore1);

            var entityProcess = m_Manager.CreateEntity(typeof(EcsTestData), typeof(EcsTestData2), typeof(EcsTestData4));
            Test(true, entityProcess);
        }


        [Test]
        [Ignore("TODO")]
        public void TestCoverageFor_ComponentSystemBase_InjectNestedIJobProcessComponentDataJobs()
        {
        }
        
        [Test]
        [Ignore("TODO")]
        public void DuplicateComponentTypeParametersThrows()
        {
        }
    }
}