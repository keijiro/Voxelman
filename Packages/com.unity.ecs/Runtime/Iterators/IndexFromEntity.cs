using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    /// <summary>
    /// Index of specified Entity in ComponentGroup
    /// </summary>
    [NativeContainer]
    public unsafe struct IndexFromEntity
    {
        private ComponentGroupData m_ComponentGroupData;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        private readonly AtomicSafetyHandle m_Safety;
#endif

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal IndexFromEntity(ComponentGroupData componentGroupData, AtomicSafetyHandle safety)
#else
        internal unsafe IndexFromEntity(ComponentGroupData componentGroupData)
#endif
        {
            m_ComponentGroupData = componentGroupData;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_Safety = safety;
#endif
        }

        public int this[Entity entity]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
#endif
                return m_ComponentGroupData.EntityIndex(entity);
            }
        }
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        void FailOutOfRangeError(Entity entity)
        {
            throw new IndexOutOfRangeException($"Entity {entity.Index} is out of range of ComponentGroup.");
        }
#endif
    }
}
