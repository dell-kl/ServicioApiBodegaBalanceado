using System.Runtime.InteropServices;

namespace Utility.DetectSO
{
    public static class DetectSystemOperation
    {

        public static bool IsLinux()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return true;

            return false;
        }

        public static bool IsWindows()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return true;

            return false;
        }

    }
}
