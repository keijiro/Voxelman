using UnityEditor;
using Unity.Collections;
using Unity.Jobs.LowLevel.Unsafe;

class JobsMenu
{
    const string kDebuggerMenu = "Jobs/JobsDebugger";

    [MenuItem(kDebuggerMenu, false)]
    static void SwitchJobsDebugger()
    {
        JobsUtility.JobDebuggerEnabled = !JobsUtility.JobDebuggerEnabled;
    }

    [MenuItem(kDebuggerMenu, true)]
    static bool SwitchJobsDebuggerValidate()
    {
        Menu.SetChecked(kDebuggerMenu, JobsUtility.JobDebuggerEnabled);

        return true;
    }


    const string kLeakDetection = "Jobs/Leak Detection (Native Containers)";
    [MenuItem(kLeakDetection, false)]
    static void SwitchLeaks()
    {
        NativeLeakDetection.Mode = NativeLeakDetection.Mode == NativeLeakDetectionMode.Enabled ? NativeLeakDetectionMode.Disabled : NativeLeakDetectionMode.Enabled;
    }

    [MenuItem(kLeakDetection, true)]
    static bool SwitchLeaksValidate()
    {
        Menu.SetChecked(kLeakDetection, NativeLeakDetection.Mode == NativeLeakDetectionMode.Enabled);
        return true;
    }
}
