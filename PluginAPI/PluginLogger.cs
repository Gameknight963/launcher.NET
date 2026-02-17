using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class PluginLogger
    {
        public static void Log(string msg) => LauncherLogger.WriteLine(msg);
        public static void Error(string msg) => LauncherLogger.Error(msg);
        public static void Success(string msg) => LauncherLogger.Success(msg);
    }
}
