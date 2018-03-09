using System;
using Unity.Entities;
using UnityEngine.Experimental.LowLevel;

namespace UnityEditor.ECS
{
    [InitializeOnLoad]
    public class PlayerLoopHelper
    {

        public static event Action<PlayerLoopSystem> OnUpdatePlayerLoop;

        public static PlayerLoopSystem currentPlayerLoop { get; private set; }

        static PlayerLoopHelper()
        {
            ScriptBehaviourUpdateOrder.OnSetPlayerLoop += UpdatePlayerLoop;
        }

        static void UpdatePlayerLoop(PlayerLoopSystem newPlayerLoop)
        {
            currentPlayerLoop = newPlayerLoop;
		    OnUpdatePlayerLoop?.Invoke(newPlayerLoop);
        }
    }

}
