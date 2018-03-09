using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Transforms
{
    /// <summary>
    /// User specified (or calculated) world position
    /// 1. If a TransformParent exists and no LocalPosition exists, the value
    /// will be used as the object to world translation irrespective of the
    /// parent object to world matrix.
    /// 2. If a TransformParent exists and a LocalPosition exists, the calculated
    /// world position will be stored in this value by the TransformSystem.
    /// 3. If a TrasformMatrix exists, the value will be stored as the translation
    /// part of the matrix.
    /// </summary>
    [Serializable]
    public struct Position : IComponentData, ISingleValue<float3>
    {
        public float3 Value { get; set; }
    }
}

namespace Unity.Transforms
{
    public class PositionComponent : ComponentDataWrapper<Position> { } 
}
