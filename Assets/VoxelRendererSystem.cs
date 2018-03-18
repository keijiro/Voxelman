using System.Collections.Generic;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

[UpdateAfter(typeof(VoxelScanSystem))]
class VoxelRendererSystem : ComponentSystem
{
    [Inject] VoxelScanSystem _scanner;

    List<ComputeBuffer> _buffers = new List<ComputeBuffer>();

    protected override void OnUpdate()
    {
        for (var i = 0; i < _scanner.BufferCount; i++)
        {
            var source = _scanner.GetBuffer(i);

            if (_buffers.Count > i)
            {
                if (_buffers[i].count != source.Length)
                {
                    _buffers[i].Release();
                    _buffers[i] = new ComputeBuffer(source.Length, 16);
                }
            }
            else
            {
                _buffers.Add(new ComputeBuffer(source.Length, 16));
            }

            _buffers[i].SetData(source);
        }
    }

    protected override void OnDestroyManager()
    {
        for (var i = 0; i < _buffers.Count; i++)
            _buffers[i].Release();

        _buffers.Clear();
    }
}
