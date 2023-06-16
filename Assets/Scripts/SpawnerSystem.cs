using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

[BurstCompile(CompileSynchronously = true)]
public partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;

        foreach (var (spawner, xform) in
                 SystemAPI.Query<RefRW<Spawner>,
                                 RefRO<LocalTransform>>())
        {
            var world = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld;

            // Timer update
            var nt = spawner.ValueRO.Timer + spawner.ValueRO.Frequency * dt;
            var count = (int)nt;
            spawner.ValueRW.Timer = nt - count;

            // Raycast base
            var p0 = xform.ValueRO.Position;
            var ext = spawner.ValueRO.Extent;

            // Collision filter
            var filter = CollisionFilter.Default;
            filter.CollidesWith = (uint)spawner.ValueRO.Mask;

            // Racast loop
            for (var i = 0; i < count; i++)
            {
                var disp = spawner.ValueRW.Random.NextFloat2(-ext.xy, ext.xy);

                var vox = SystemAPI.GetSingleton<Voxelizer>();
                disp = math.floor(disp / vox.VoxelSize) * vox.VoxelSize;

                var ray = new RaycastInput()
                  { Start = p0 + math.float3(disp, -ext.z),
                    End   = p0 + math.float3(disp, +ext.z),
                    Filter = filter };

                var hit = new RaycastHit();
                if (!world.CastRay(ray, out hit)) continue;

                // Prefab instantiation
                var spawned = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                var cx = SystemAPI.GetComponentRW<LocalTransform>(spawned);
                cx.ValueRW.Position = hit.Position;
            }
        }
    }
}
