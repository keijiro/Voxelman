using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

class SpinnerSystem : ComponentSystem
{
    struct Data
    {
        public int Length;
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Rotations;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        for (var i = 0; i < m_Data.Length; i++)
        {
            m_Data.Rotations[i] = new Rotation
            {
                Value = math.mul(m_Data.Rotations[i].Value, math.axisAngle(new float3(0, 1, 0), 0.1f))
            };
        }
    }
}
