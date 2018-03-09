using System;

namespace Unity
{
    // TODO: provide an implementation of Unity.Debug that does not rely on UnityEngine.
    static class Debug
    {
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public static void LogException(Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }
    }
}
