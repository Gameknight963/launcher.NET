using Semver;

namespace launcherdotnet.PluginAPI
{
    public class LauncherApiInfo
    {
        public static readonly SemVersion ApiVersion = new(0, 8, 0);
        public static string ApiVersionString => ApiVersion.ToString();
    }
}
