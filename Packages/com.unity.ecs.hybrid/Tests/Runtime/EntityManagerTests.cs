using NUnit.Framework;
using UnityEngine;
using UnityEngine.ECS.Tests;

namespace Unity.Entities.Tests
{
    public class EntityManagerTests : ECSTestsFixture
    {
        [Test]
        public void GetComponentObjectReturnsTheCorrectType()
        {
            var go = new GameObject();
            go.AddComponent<EcsTestComponent>();
            // Execute in edit mode is not enabled so this has to be called manually right now
            go.GetComponent<GameObjectEntity>().OnEnable();

            var component = m_Manager.GetComponentObject<Transform>(go.GetComponent<GameObjectEntity>().Entity);

            Assert.NotNull(component, "EntityManager.GetComponentObject returned a null object");
            Assert.AreEqual(typeof(Transform), component.GetType(), "EntityManager.GetComponentObject returned the wrong component type.");
            Assert.AreEqual(go.transform, component, "EntityManager.GetComponentObject returned a different copy of the component.");
        }

        [Test]
        public void GetComponentObjectThrowsIfComponentDoesNotExist()
        {
            var go = new GameObject();
            go.AddComponent<EcsTestComponent>();
            // Execute in edit mode is not enabled so this has to be called manually right now
            go.GetComponent<GameObjectEntity>().OnEnable();

            Assert.Throws<System.ArgumentException>(() => m_Manager.GetComponentObject<Rigidbody>(go.GetComponent<GameObjectEntity>().Entity));
        }
    }
}
