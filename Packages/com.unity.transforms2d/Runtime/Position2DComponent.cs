using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Transforms2D
{
    /// <summary>
    /// World position in 2D.
    /// If present, used to build TransformMatrix (if also present), where Y is set to zero.
    /// </summary>
    [Serializable]
    public struct Position2D : IComponentData
    {
        public float2 Value;
    }

    public class Position2DComponent : ComponentDataWrapper<Position2D> { } 
}
