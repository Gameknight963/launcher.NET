using launcherdotnet.Launcher.Settings;


namespace launcherdotnet.PluginAPI
{
    public class PluginConfig
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// Temporary directory where plugins can put things.
        /// </summary>
        public static string TempDir => LauncherConstants.TempDir;
        /// <summary>
        /// The games directory
        /// </summary>
        public static string GamesDir => LauncherConstants.GamesDir;
    }
}
