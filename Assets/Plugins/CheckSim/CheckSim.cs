using UnityEngine;

public static class CheckSim
{
    public static bool HasSim()
    {
        #if UNITY_ANDROID
        using (var plugin = new AndroidJavaClass("com.esparrow.check-sim-card.Plugin"))
        {
            var result = plugin.CallStatic<bool>("HasSim");
            return result;
        }
        #elif UNITY_EDITOR
        return false;
        #else
        return !(Application.internetReachability != NetworkReachability.NotReachable);
        #endif
    }
}
