using System;
using NUnit.Framework;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace UnityEngine.ECS.Tests
{
    public class EntityCommandBufferTests : ECSTestsFixture
    {
        [Test]
        public void EmptyOK()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.Playback(m_Manager);
            cmds.Dispose();
        }

        struct TestJob : IJob
        {
            public EntityCommandBuffer Buffer;

            public void Execute()
            {
                Buffer.CreateEntity();
                Buffer.AddComponent(new EcsTestData { value = 1 });
            }
        }

        [Test]
        public void SingleWriterEnforced()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            var job = new TestJob {Buffer = cmds};

            cmds.CreateEntity();
            cmds.AddComponent(new EcsTestData { value = 42 });

            var handle = job.Schedule();

            Assert.Throws<InvalidOperationException>(() => { cmds.CreateEntity(); });
            Assert.Throws<InvalidOperationException>(() => { job.Buffer.CreateEntity(); });

            handle.Complete();

            cmds.Playback(m_Manager);
            cmds.Dispose();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
            var arr = group.GetComponentDataArray<EcsTestData>();
            Assert.AreEqual(2, arr.Length);
            Assert.AreEqual(42, arr[0].value);
            Assert.AreEqual(1, arr[1].value);
            group.Dispose();
        }

        [Test]
        public void DisposeWhileJobRunningThrows()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            var job = new TestJob {Buffer = cmds};

            var handle = job.Schedule();

            Assert.Throws<InvalidOperationException>(() => { cmds.Dispose(); });

            handle.Complete();

            cmds.Dispose();
        }

        [Test]
        public void ModifiesWhileJobRunningThrows()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            var job = new TestJob {Buffer = cmds};

            var handle = job.Schedule();

            Assert.Throws<InvalidOperationException>(() => { cmds.CreateEntity(); });

            handle.Complete();

            cmds.Dispose();
        }

        [Test]
        public void PlaybackWhileJobRunningThrows()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            var job = new TestJob {Buffer = cmds};

            var handle = job.Schedule();

            Assert.Throws<InvalidOperationException>(() => { cmds.Playback(m_Manager); });

            handle.Complete();

            cmds.Dispose();
        }

        [Test]
        public void ImplicitCreateEntity()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.CreateEntity();
            cmds.AddComponent(new EcsTestData { value = 12 });
            cmds.Playback(m_Manager);
            cmds.Dispose();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
            var arr = group.GetComponentDataArray<EcsTestData>();
            Assert.AreEqual(1, arr.Length);
            Assert.AreEqual(12, arr[0].value);
            group.Dispose();
        }

        [Test]
        public void ImplicitCreateEntityWithArchetype()
        {
            var a = m_Manager.CreateArchetype(typeof(EcsTestData));

            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.CreateEntity(a);
            cmds.SetComponent(new EcsTestData { value = 12 });
            cmds.Playback(m_Manager);
            cmds.Dispose();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
            var arr = group.GetComponentDataArray<EcsTestData>();
            Assert.AreEqual(1, arr.Length);
            Assert.AreEqual(12, arr[0].value);
            group.Dispose();
        }

        [Test]
        public void ImplicitCreateEntityTwice()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.CreateEntity();
            cmds.AddComponent(new EcsTestData { value = 12 });
            cmds.CreateEntity();
            cmds.AddComponent(new EcsTestData { value = 13 });
            cmds.Playback(m_Manager);
            cmds.Dispose();

            var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
            var arr = group.GetComponentDataArray<EcsTestData>();
            Assert.AreEqual(2, arr.Length);
            Assert.AreEqual(12, arr[0].value);
            Assert.AreEqual(13, arr[1].value);
            group.Dispose();
        }

        [Test]
        public void ImplicitCreateTwoComponents()
        {
            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.CreateEntity();
            cmds.AddComponent(new EcsTestData { value = 12 });
            cmds.AddComponent(new EcsTestData2 { value0 = 1, value1 = 2 });
            cmds.Playback(m_Manager);
            cmds.Dispose();

            {
                var group = m_Manager.CreateComponentGroup(typeof(EcsTestData));
                var arr = group.GetComponentDataArray<EcsTestData>();
                Assert.AreEqual(1, arr.Length);
                Assert.AreEqual(12, arr[0].value);
                group.Dispose();
            }

            {
                var group = m_Manager.CreateComponentGroup(typeof(EcsTestData2));
                var arr = group.GetComponentDataArray<EcsTestData2>();
                Assert.AreEqual(1, arr.Length);
                Assert.AreEqual(1, arr[0].value0);
                Assert.AreEqual(2, arr[0].value1);
                group.Dispose();
            }
        }

        [Test]
        public void TestMultiChunks()
        {
            const int count = 65536;

            var cmds = new EntityCommandBuffer(Allocator.TempJob);
            cmds.MinimumChunkSize = 512;

            for (int i = 0; i < count; ++i)
            {
                cmds.CreateEntity();
                cmds.AddComponent(new EcsTestData { value = i });
                cmds.AddComponent(new EcsTestData2 { value0 = i, value1 = i });
            }

            cmds.Playback(m_Manager);
            cmds.Dispose();

            {
                var group = m_Manager.CreateComponentGroup(typeof(EcsTestData), typeof(EcsTestData2));
                var arr = group.GetComponentDataArray<EcsTestData>();
                var arr2 = group.GetComponentDataArray<EcsTestData2>();
                Assert.AreEqual(count, arr.Length);
                for (int i = 0; i < count; ++i)
                {
                    Assert.AreEqual(i, arr[i].value);
                    Assert.AreEqual(i, arr2[i].value0);
                    Assert.AreEqual(i, arr2[i].value1);
                }
                group.Dispose();
            }
        }
    }
}
