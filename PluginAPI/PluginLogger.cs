using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class PluginLogger
    {
        public static void Log(string msg, bool force = false) => LauncherLogger.WriteLine(msg, force);
        public static void Error(string msg, bool force = false) => LauncherLogger.Error(msg, force);
        public static void Success(string msg, bool force = false) => LauncherLogger.Success(msg, force);
    }
}
