using UnityEngine;

namespace Utils
{
    public static class EmulatorValidator
    {
        public static bool IsEmulator()
        {
            var osBuild = new AndroidJavaClass("android.os.Build");
            var fingerPrint = osBuild.GetStatic<string>("FINGERPRINT");

            var result = fingerPrint.Contains("generic");
            return result;
        }

    }
}