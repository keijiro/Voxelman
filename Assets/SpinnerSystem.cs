using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

class SpinnerSystem : ComponentSystem
{
    struct Data
    {
        public int Length;
        [ReadOnly] public ComponentDataArray<Position> Positions;
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
    }
}
