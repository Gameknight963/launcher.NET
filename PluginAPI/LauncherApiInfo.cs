using Semver;

namespace launcherdotnet.PluginAPI
{
    public class LauncherApiInfo
    {
        public static readonly SemVersion ApiVersion = new(1, 0, 0);
        public static string ApiVersionString => ApiVersion.ToString();
    }
}
