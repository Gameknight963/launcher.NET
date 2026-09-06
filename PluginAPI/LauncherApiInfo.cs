using Semver;

namespace launcherdotnet.PluginAPI
{
    public class LauncherApiInfo
    {
        public static readonly SemVersion ApiVersion = new(2, 3, 0);
        public static string ApiVersionString => ApiVersion.ToString();
    }
}
