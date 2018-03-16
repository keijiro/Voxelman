using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

class VoxelScanSystem : JobComponentSystem
{
    // Data entry for injection
    struct Data
    {
        [ReadOnly] public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Voxel> Voxels;
        public int Length;
    }

    [Inject] Data _data;
    
    [ComputeJobOptimization]
    struct TransferJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<RaycastHit> RaycastHits;
        public ComponentDataArray<Voxel> Voxels;

        public void Execute(int i)
        {
            Voxels[i] = new Voxel { Filled = RaycastHits[i].distance < 0.05f };
        }
    }

    [ComputeJobOptimization]
    struct Dispose : IJob
    {
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastCommand> RaycastCommands;
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastHit> RaycastHits;

        public void Execute()
        {
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var commands = new NativeArray<RaycastCommand>(_data.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        var hits = new NativeArray<RaycastHit>(_data.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

        for (var i = 0; i < _data.Length; i++)
            commands[i] = new RaycastCommand(_data.Positions[i].Value, Vector3.left, 0.05f);

        var raycastJob = RaycastCommand.ScheduleBatch(commands, hits, 1, inputDeps);

        var transfer = new TransferJob {
            RaycastHits = hits,
            Voxels = _data.Voxels
        };

        var transferJob = transfer.Schedule(_data.Length, 64, raycastJob);

        var disposeJob = new Dispose {
            RaycastCommands = commands,
            RaycastHits = hits
        };

        return disposeJob.Schedule(transferJob);
    }
}
