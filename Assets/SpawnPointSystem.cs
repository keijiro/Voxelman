using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;

// System implementation for processing SpawnPoints

class SpawnSystem : ComponentSystem
{
    // Spawn point data entry for temporary use
    struct TempData
    {
        public int SharedDataIndex;
        public Entity SourceEntity;
        public float3 Position;
    }

    // Spawn point data group used for querying
    private ComponentGroup _mainGroup;

    // Voxel archetype used for instantiation
    private EntityArchetype _voxelArchetype;

    protected override void OnCreateManager(int capacity)
    {
        // Spawn point data group: Only requires SpawnPoint and Position.
        _mainGroup = GetComponentGroup(typeof(SpawnPoint), typeof(Position));

        // Voxel archetype: Voxel data, transform and instanced renderer.
        _voxelArchetype = EntityManager.CreateArchetype(
            typeof(Voxel),
            typeof(Position), typeof(Rotation), typeof(TransformMatrix),
            typeof(MeshInstanceRenderer)
        );
    }

    protected override void OnUpdate()
    {
        // Spawn point data can be shared between spawn point instances.
        // Enumerate all the unique data first.
        var sharedDatas = new System.Collections.Generic.List<SpawnPoint>(10);
        EntityManager.GetAllUniqueSharedComponentDatas(sharedDatas);
        
        // Calculate the total count of the spawn points.
        var spawnCount = 0;
        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            _mainGroup.SetFilter(sharedDatas[sharedIndex]);
            spawnCount += _mainGroup.CalculateLength();
        }

        // Do nothing if no one is there.
        if (spawnCount == 0) return;

        // Build a simple plain array of spawn points.
        var tempDatas = new NativeArray<TempData>(spawnCount, Allocator.Temp);

        spawnCount = 0;

        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            _mainGroup.SetFilter(sharedDatas[sharedIndex]);
            
            var entities = _mainGroup.GetEntityArray();
            var positions = _mainGroup.GetComponentDataArray<Position>();

            for (var i = 0; i < entities.Length; i++)
            {
                tempDatas[spawnCount++] = new TempData
                {
                    SharedDataIndex = sharedIndex,
                    SourceEntity = entities[i],
                    Position = positions[i].Value
                };
            }
        }

        // Spawn voxels at the each spawn point.
        for (var i = 0; i < spawnCount; i++)
        {
            var sharedIndex = tempDatas[i].SharedDataIndex;
            var ext = sharedDatas[sharedIndex].Extent;
            var reso = sharedDatas[sharedIndex].Resolution;

            // Instantiation
            var instances = new NativeArray<Entity>(reso.x * reso.y * reso.z, Allocator.Temp);
            EntityManager.CreateEntity(_voxelArchetype, instances);

            // Initialization
            var offs = 0;
            for (var ix = 0; ix < reso.x; ix++)
            {
                var x = math.lerp(-ext.x, ext.x, (float)ix / reso.x);
                for (var iy = 0; iy < reso.y; iy++)
                {
                    var y = math.lerp(-ext.y, ext.y, (float)iy / reso.y);
                    for (var iz = 0; iz < reso.z; iz++)
                    {
                        var z = math.lerp(-ext.z, ext.z, (float)iz / reso.z);
                        EntityManager.SetComponentData(instances[offs], new Position { Value = new float3(x, y, z) });
                        EntityManager.SetSharedComponentData(instances[offs], sharedDatas[sharedIndex].RendererSettings);
                        offs++;
                    }
                }
            }

            instances.Dispose();

            // Remove the spawn point data from the source spawner entity.
            EntityManager.RemoveComponent(tempDatas[i].SourceEntity, typeof(SpawnPoint));
        }
        
        tempDatas.Dispose();
    }
}
