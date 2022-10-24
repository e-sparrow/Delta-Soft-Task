using UnityEngine;

namespace Utils
{
    public static class EmulatorValidator
    {
        public static bool IsEmulator()
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getApplicationContext");
            var cls = new AndroidJavaClass("com.nekolaboratory.EmulatorDetector");
            
            var result = cls.CallStatic<bool>("isEmulator", context);
            return result;
        }
    }
}