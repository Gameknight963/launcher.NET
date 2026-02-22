using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class PluginConfig
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// Temporary directory where plugins can put things.
        /// </summary>
        public static string TempDir => LauncherSettings.TempDir;
        /// <summary>
        /// The games directory
        /// </summary>
        public static string GamesDir => LauncherSettings.GamesDir;
        /// <summary>
        /// User agent for plugins to use when making requests to APIs
        /// </summary>
        public static readonly string UserAgent = "launcher.NET";
    }
}
