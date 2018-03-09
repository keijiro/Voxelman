using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Transforms
{
    /// <summary>
    /// User specified (or calculated) world rotation
    /// 1. If a TransformParent exists and no LocalRotation exists, the value
    /// will be used as the object to world translation irrespective of the
    /// parent object to world matrix.
    /// 2. If a TransformParent exists and a LocalRotation exists, the calculated
    /// world rotation will be stored in this value by the TransformSystem.
    /// 3. If a TrasformMatrix exists, the value will be stored as the rotation 
    /// part of the matrix.
    /// </summary>
    [System.Serializable]
    public struct Rotation : IComponentData
    {
        public quaternion Value { get; set; }
    }

    public class RotationComponent : ComponentDataWrapper<Rotation> { }
}