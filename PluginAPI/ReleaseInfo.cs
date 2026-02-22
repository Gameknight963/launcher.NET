using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class ReleaseInfo
    {
        public required SemVersion Version { get; set; }
        public string? Url { get; set; }
    }
}
