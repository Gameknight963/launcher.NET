using launcherdotnet.Launcher;
using launcherdotnet.Launcher.Settings;

namespace launcherdotnet.PluginAPI
{
    public class PluginGameInfo
    {
        public required string ExePath;
        public bool RunWithCmd = false;
        public string? ThunderstoreCommunitySlug;
        public bool ModManageable = false;
    }
}
