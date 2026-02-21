using System;
using Semver;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public interface ILauncherPlugin
    {
        /// <summary>
        /// The name of your plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of your plugin to show
        /// in UI. Optional, set to null if you don't
        /// want it, but highly recommended.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The version of the launcher.NET
        /// plugin API your plugin was built for.
        /// </summary>
        SemVersion TargetApiVersion { get; }

        /// <summary>
        /// Called when the plugin is loaded.
        /// </summary>
        void Initialize();
    }
}
