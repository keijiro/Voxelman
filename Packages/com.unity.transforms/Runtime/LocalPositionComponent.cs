using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Transforms
{
    /// <summary>
    /// User specified position in local space.
    /// 1. If a TransformParent exists, the object to world matrix will be determined
    /// by the parent's transform. Otherwise, it will be a Unit matrix.
    /// 2. If a TransformMatrix exists, the calculated world position will be stored
    /// as the translation in that matrix.
    /// 3. If a Position exists, the calculated world position will be stored in that
    /// component data.
    /// 4. If TransformParent refers to the entity this component is associated with,
    /// the calculated world position will be used as the translation in the object
    /// to world matrix for the entity associated with this component, regardless of
    /// if it is stored in a TransformMatrix.
    /// </summary>
    [Serializable]
    public struct LocalPosition : IComponentData
    {
        public float3 Value { get; set; }
    }

    public class LocalPositionComponent : ComponentDataWrapper<LocalPosition> { } 
}
