using Unity.Entities;

namespace Unity.Transforms
{
    /// <summary>
    /// User assigned parent Entity.
    /// Local transformations (e.g. LocalPosition, LocalRotation) will be relative
    /// to the object to world matrix associated with the parent entity.
    /// The parent's object to world matrix may only be temporary if it's not explicitly
    /// stored in a TransformMatrix.
    /// </summary>
    public struct TransformParent : IComponentData
    {
        public Entity Value { get; set; }
    }

    public class TransformParentComponent : ComponentDataWrapper<TransformParent> { }
}