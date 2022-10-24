using UnityEngine;

public static class CheckSim
{
    public static bool HasSim()
    {
        using (var plugin = new AndroidJavaClass("com.esparrow.checksim.Plugin"))
        {
            var result = plugin.CallStatic<bool>("HasSim");
            return result;
        }
    }
}
