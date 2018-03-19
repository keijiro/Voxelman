using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

unsafe class ScannerSystem : JobComponentSystem
{
    // Groups used for querying components.
    ComponentGroup _scannerGroup;
    ComponentGroup _voxelGroup;

    // Used for sharing a counter between transfer jobs.
    // Static member field isn't allowed in Burst, so use a pointer.
    int* _pTransformCount;

    // Setup job: Set up raycast commands in a jobified way.
    [ComputeJobOptimization]
    struct SetupJob : IJobParallelFor
    {
        // Output
        public NativeArray<RaycastCommand> Commands;

        // Common parameters
        public float3 Extent;
        public int3 Resolution;

        public void Execute(int i)
        {
            var ix = i % Resolution.x;
            var iy = i / Resolution.x;

            var x = math.lerp(-Extent.x, Extent.x, (float)ix / Resolution.x);
            var y = math.lerp(-Extent.y, Extent.y, (float)iy / Resolution.y);

            var p = new float3(x, y, -Extent.z);
            Commands[i] = new RaycastCommand(p, new float3(0, 0, 1), Extent.z * 2);
        }
    }

    // Transfer job: Transfers raycast results to voxels in a jobified way.
    [ComputeJobOptimization]
    struct TransferJob : IJobParallelFor
    {
        // Input arrays; Will be automatically deallocated.
        [DeallocateOnJobCompletion] [ReadOnly]
        public NativeArray<RaycastCommand> RaycastCommands;

        [DeallocateOnJobCompletion] [ReadOnly]
        public NativeArray<RaycastHit> RaycastHits;

        // Output array; Allowing random access.
        [NativeDisableParallelForRestriction]
        public ComponentDataArray<TransformMatrix> Transforms;

        // Output counter; Shared between jobs via pointer.
        [NativeDisableUnsafePtrRestriction] public int* pCounter;

        // Common parameters
        public float Scale;

        public void Execute(int index)
        {
            // Hit test
            if (RaycastHits[index].distance <= 0) return;

            // Retrieve the result.
            var from = (float3)RaycastCommands[index].from;
            var dist = RaycastHits[index].distance;

            var p = from + new float3(0, 0, dist);
            var matrix = new float4x4(
                new float4(Scale, 0, 0, 0),
                new float4(0, Scale, 0, 0),
                new float4(0, 0, Scale, 0),
                new float4(p.x, p.y, p.z, 1));

            // Increment the output counter in a thread safe way.
            var count = System.Threading.Interlocked.Increment(ref *pCounter) - 1;

            // Output
            Transforms[count % Transforms.Length] = new TransformMatrix { Value = matrix };
        }
    }

    JobHandle BuildJobChain(float3 origin, Scanner scanner, JobHandle deps)
    {
        // Transform output destination
        var transforms = _voxelGroup.GetComponentDataArray<TransformMatrix>();
        if (transforms.Length == 0) return deps;

        // Shared counter initialization/update
        if (_pTransformCount == null)
        {
            // Initialization
            _pTransformCount = (int*)UnsafeUtility.Malloc(
                sizeof(int), sizeof(int), Allocator.Persistent);
            *_pTransformCount = 0;
        }
        else
        {
            // Wrapping around for avoiding overflow
            *_pTransformCount %= transforms.Length;
        }

        // Total count of rays
        var total = scanner.Resolution.x * scanner.Resolution.y;

        // Ray cast command/result array
        var commands = new NativeArray<RaycastCommand>(total, Allocator.TempJob);
        var hits = new NativeArray<RaycastHit>(total, Allocator.TempJob);

        // 1: Setup job
        var setupJob = new SetupJob {
            Commands = commands,
            Extent = scanner.Extent,
            Resolution = scanner.Resolution
        };
        deps = setupJob.Schedule(total, 16, deps);

        // 2: Raycast job
        deps = RaycastCommand.ScheduleBatch(commands, hits, 16, deps);

        // 3: Transfer job
        var transferJob = new TransferJob {
            RaycastCommands = commands,
            RaycastHits = hits,
            Scale = scanner.Extent.x * 2 / scanner.Resolution.x,
            Transforms = transforms,
            pCounter = _pTransformCount
        };
        deps = transferJob.Schedule(total, 16, deps);

        return deps;
    }

    protected override void OnCreateManager(int capacity)
    {
        _scannerGroup = GetComponentGroup(typeof(Scanner), typeof(Position));
        _voxelGroup = GetComponentGroup(typeof(Voxel), typeof(TransformMatrix));
    }

    protected override void OnDestroyManager()
    {
        if (_pTransformCount != null)
        {
            UnsafeUtility.Free(_pTransformCount, Allocator.Persistent);
            _pTransformCount = null;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var origins = _scannerGroup.GetComponentDataArray<Position>();
        var scanners = _scannerGroup.GetComponentDataArray<Scanner>();

        for (var i = 0; i < scanners.Length; i++)
            inputDeps = BuildJobChain(origins[i].Value, scanners[i], inputDeps);

        return inputDeps;
    }
}
