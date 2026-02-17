using System;
using Semver;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public interface ILauncherPlugin
    {
        string Name { get; }
        SemVersion TargetApiVersion { get; }

        void Initialize();
    }
}
