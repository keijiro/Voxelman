using Unity.Collections;

namespace Unity.Entities
{
    [UpdateBefore(typeof(UnityEngine.Experimental.PlayerLoop.Initialization))]
    public class EndFrameBarrier : BarrierSystem
    {
    }
}
