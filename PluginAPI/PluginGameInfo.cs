using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class PluginGameInfo
    {
        public required string ExePath;
        public bool RunWithCmd = false;
        public string? ThunderstoreCommunitySlug;
    }
}
