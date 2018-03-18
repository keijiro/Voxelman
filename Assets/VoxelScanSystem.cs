using System.Collections.Generic;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

class VoxelScanSystem : JobComponentSystem
{
    // Used for collecting shared component data
    List<VoxelScan> _uniqueTypes = new List<VoxelScan>(10);

    // Group for querying (Position + VoxelScan)
    ComponentGroup _group;

    // Buffrs for storing scan results
    List<NativeArray<float4>> _resultBuffers = new List<NativeArray<float4>>();

    public int BufferCount {
        get { return _resultBuffers.Count; }
    }

    public NativeArray<float4> GetBuffer(int index)
    {
        return _resultBuffers[index];
    }

    int _hashSeed;

    // A job that transfers raycast results to a native array.
    [ComputeJobOptimization]
    struct TransferJob : IJobParallelFor
    {
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastCommand> RaycastCommands;
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastHit> RaycastHits;

        public NativeArray<float4> Destination;

        public void Execute(int i)
        {
            var p = RaycastCommands[i].from;
            var w = RaycastHits[i].distance > 0 ? 1.0f : 0.0f;
            Destination[i] = new float4(p.x, p.y, p.z, w);
        }
    }

    protected override void OnCreateManager(int capacity)
    {
        _group = GetComponentGroup(typeof(Position), typeof(VoxelScan));
    }

    JobHandle BuildJob(VoxelScan scan, float3 origin, JobHandle deps)
    {
        var ext = scan.Extent;
        var reso = scan.Resolution;
        var total = reso.x * reso.y * reso.z;

        var commands = new NativeArray<RaycastCommand>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        var hits = new NativeArray<RaycastHit>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

        var hash = new XXHash(_hashSeed++);

        var scale = ext / reso;
        var dist = math.max(math.max(scale.x, scale.y), scale.z);

        var i = 0;
        var seed = 0;
        for (var ix = 0; ix < reso.x; ix++)
        {
            var x = math.lerp(-ext.x, ext.x, (float)ix / reso.x);
            for (var iy = 0; iy < reso.y; iy++)
            {
                var y = math.lerp(-ext.y, ext.y, (float)iy / reso.y);
                for (var iz = 0; iz < reso.z; iz++)
                {
                    var z = math.lerp(-ext.z, ext.z, (float)iz / reso.z);
                    var p = new float3(x, y, z);

                    var d = math.normalize(new float3(
                        hash.Range(-1.0f, 1.0f, seed++),
                        hash.Range(-1.0f, 1.0f, seed++),
                        hash.Range(-1.0f, 1.0f, seed++)
                    ));

                    commands[i++] = new RaycastCommand(p - d * dist, d, dist);
                }
            }
        }

        // Raycast job
        deps = RaycastCommand.ScheduleBatch(commands, hits, 1, deps);

        // Transfer results to a native array.
        var results = new NativeArray<float4>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        _resultBuffers.Add(results);

        var transferJob = new TransferJob {
            RaycastCommands = commands,
            RaycastHits = hits,
            Destination = results
        };

        return transferJob.Schedule(total, 1, deps);
    }

    void DisposeResultBuffers()
    {
        for (var i = 0; i < _resultBuffers.Count; i++)
            _resultBuffers[i].Dispose();

        _resultBuffers.Clear();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        DisposeResultBuffers();

        EntityManager.GetAllUniqueSharedComponentDatas(_uniqueTypes);

        for (var i = 0; i < _uniqueTypes.Count; i++)
        {
            var voxelScan = _uniqueTypes[i];

            _group.SetFilter(voxelScan);

            var positions = _group.GetComponentDataArray<Position>();

            for (var j = 0; j < positions.Length; j++)
                inputDeps = BuildJob(voxelScan, positions[j].Value, inputDeps);
        }

        _uniqueTypes.Clear();
        inputDeps.Complete();

        return inputDeps;
    }

    protected override void OnDestroyManager()
    {
        DisposeResultBuffers();
    }
}
