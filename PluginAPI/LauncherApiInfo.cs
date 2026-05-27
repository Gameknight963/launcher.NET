using Semver;

namespace launcherdotnet.PluginAPI
{
    public class LauncherApiInfo
    {
        public static readonly SemVersion ApiVersion = new(0, 8, 1);
        public static string ApiVersionString => ApiVersion.ToString();
    }
}
