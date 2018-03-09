using System.Collections.Generic;
using RequireComponent = UnityEngine.RequireComponent;
using SerializeField = UnityEngine.SerializeField;
using MonoBehaviour = UnityEngine.MonoBehaviour;
using DisallowMultipleComponent = UnityEngine.DisallowMultipleComponent;
using GameObject = UnityEngine.GameObject;
using Component = UnityEngine.Component;

namespace Unity.Entities
{
    //@TODO: This should be fully implemented in C++ for efficiency
    [RequireComponent(typeof(GameObjectEntity))]
    public abstract class ComponentDataWrapperBase : MonoBehaviour
    {
        internal abstract ComponentType GetComponentType(EntityManager manager);
        internal abstract void UpdateComponentData(EntityManager manager, Entity entity);
    }

    //@TODO: This should be fully implemented in C++ for efficiency
    public class ComponentDataWrapper<T> : ComponentDataWrapperBase where T : struct, IComponentData
    {
        [SerializeField]
        T m_SerializedData;

        public T Value
        {
            get
            {
                return m_SerializedData;
            }
            set
            {
                m_SerializedData = value;
            }
        }


        internal override ComponentType GetComponentType(EntityManager manager)
        {
            return ComponentType.Create<T>();
        }

        internal override void UpdateComponentData(EntityManager manager, Entity entity)
        {
            manager.SetComponentData(entity, m_SerializedData);
        }
    }

    //@TODO: This should be fully implemented in C++ for efficiency
    public class SharedComponentDataWrapper<T> : ComponentDataWrapperBase where T : struct, ISharedComponentData
    {
        [SerializeField]
        T m_SerializedData;

        public T Value
        {
            get
            {
                return m_SerializedData;
            }
            set
            {
                m_SerializedData = value;
            }
        }


        internal override ComponentType GetComponentType(EntityManager manager)
        {
            return ComponentType.Create<T>();
        }

        internal override void UpdateComponentData(EntityManager manager, Entity entity)
        {
            manager.SetSharedComponentData(entity, m_SerializedData);
        }
    }

    [DisallowMultipleComponent]
    public class GameObjectEntity : MonoBehaviour
    {
        EntityManager m_EntityManager;

        public Entity Entity { get; private set; }

        //@TODO: Very wrong error messages when creating entity with empty ComponentType array?

        public static Entity AddToEntityManager(EntityManager entityManager, GameObject gameObject)
        {
            ComponentType[] types;
            Component[] components;
            GetComponents(entityManager, gameObject, true, out types, out components);

            var archetype = entityManager.CreateArchetype(types);
            var entity = CreateEntity(entityManager, archetype, components, types);

            return entity;
        }

        static void GetComponents(EntityManager entityManager, GameObject gameObject, bool includeGameObjectComponents, out ComponentType[] types, out Component[] components)
        {
            components = gameObject.GetComponents<Component>();

            var componentCount = 0;
            if (includeGameObjectComponents)
            {
                var gameObjectEntityComponent = gameObject.GetComponent<GameObjectEntity>();
                componentCount = gameObjectEntityComponent == null ? components.Length : components.Length - 1;
            }
            else
            {
                for (var i = 0; i != components.Length; i++)
                {
                    if (components[i] is ComponentDataWrapperBase)
                        componentCount++;
                }
            }

            types = new ComponentType[componentCount];

            var t = 0;
            for (var i = 0; i != components.Length; i++)
            {
                var com = components[i];
                var componentData = com as ComponentDataWrapperBase;

                if (componentData != null)
                    types[t++] = componentData.GetComponentType(entityManager);
                else if (includeGameObjectComponents && !(com is GameObjectEntity))
                    types[t++] = com.GetType();
            }
        }

        static Entity CreateEntity(EntityManager entityManager, EntityArchetype archetype, IReadOnlyList<Component> components, IReadOnlyList<ComponentType> types)
        {
            var entity = entityManager.CreateEntity(archetype);
            var t = 0;
            for (var i = 0; i != components.Count; i++)
            {
                var com = components[i];
                var componentDataWrapper = com as ComponentDataWrapperBase;

                if (componentDataWrapper != null)
                {
                    componentDataWrapper.UpdateComponentData(entityManager, entity);
                    t++;
                }
                else if (!(com is GameObjectEntity))
                {
                    entityManager.SetComponentObject(entity, types[t], com);
                    t++;
                }
            }
            return entity;
        }

        public void OnEnable()
        {
            m_EntityManager = World.Active.GetOrCreateManager<EntityManager>();
            Entity = AddToEntityManager(m_EntityManager, gameObject);
        }

        public void OnDisable()
        {
            if (m_EntityManager != null && m_EntityManager.IsCreated && m_EntityManager.Exists(Entity))
                m_EntityManager.DestroyEntity(Entity);

            m_EntityManager = null;
            Entity = new Entity();
        }
    }
}
