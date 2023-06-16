using UnityEngine;
using UnityEngine.Profiling;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using MeshCollider = Unity.Physics.MeshCollider;

[BurstCompile(CompileSynchronously = true)]
public class CollisionGenerator : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] SkinnedMeshRenderer _source = null;

    #endregion

    #region Private fields

    Mesh _mesh;
    Entity _entity;

    #endregion

    #region DOTS interop

    [BurstCompile]
    static void CreateCollider
      (in EntityManager manager,
       in Entity entity,
       in NativeArray<float3> vtx,
       in NativeArray<int3> idx,
       int layer)
    {
        var filter = CollisionFilter.Default;
        filter.CollidesWith = (uint)layer;

        var collider = MeshCollider.Create(vtx, idx, filter);
        manager.SetComponentData(entity, new PhysicsCollider{Value = collider});
    }

    #endregion

    #region MonoBehaviour implementation

    void Start()
    {
        _mesh = new Mesh();

        // Entity allocation
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var componentTypes = new ComponentType []
        {
            typeof(LocalTransform),
            typeof(PhysicsCollider),
            typeof(PhysicsWorldIndex)
        };
        _entity = manager.CreateEntity(componentTypes);

        // Physics world index initialization
        var world = new PhysicsWorldIndex{Value = 0};
        manager.AddSharedComponentManaged(_entity, world);
    }

    void Update()
    {
        // Skinned mesh baking
        Profiler.BeginSample("BakeMesh");
        _source.BakeMesh(_mesh);
        Profiler.EndSample();

        // Vertex/index array retrieval
        using var vtx = new NativeArray<Vector3>(_mesh.vertices, Allocator.Temp);
        using var idx = new NativeArray<int>(_mesh.triangles, Allocator.Temp);
        var vtx_re = vtx.Reinterpret<float3>();
        var idx_re = idx.Reinterpret<int3>(sizeof(int));

        // Transform update
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var xform = new LocalTransform
        {
            Position = _source.transform.position,
            Rotation = _source.transform.rotation,
            Scale = _source.transform.localScale.x
        };
        manager.SetComponentData(_entity, xform);

        // Mesh collider update
        Profiler.BeginSample("MeshCollider Update");
        CreateCollider(manager, _entity, vtx_re, idx_re, gameObject.layer);
        Profiler.EndSample();
    }

    #endregion
}
