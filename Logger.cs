using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public static class LauncherLogger
    {
        public static void Write(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.Write(message);
            }
        }
        public static void WriteLine(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.WriteLine(message);
            }
        }
    }
}
