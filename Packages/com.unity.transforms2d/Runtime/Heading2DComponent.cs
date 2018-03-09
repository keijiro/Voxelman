using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.Transforms2D
{
    /// <summary>
    /// Direction vector representing heading. 
    /// If present, used to build TransformMatrix (if also present), where Y is set to zero.
    /// </summary>
    [Serializable]
    public struct Heading2D : IComponentData
    {
        public float2 Value;
    }

    public class Heading2DComponent : ComponentDataWrapper<Heading2D> { }
}
