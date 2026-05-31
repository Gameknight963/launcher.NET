using Semver;

namespace launcherdotnet.PluginAPI
{
    public static class PluginRegistry
    {
        private static readonly List<IGameInstaller> _gameInstallPlugins = new();
        private static readonly List<ILauncherPlugin> _launcherPlugins = new();
        private static readonly List<PluginDescriptor> _pluginDescriptors = new();

        internal static void Register(PluginDescriptor descriptor)
        {
            _launcherPlugins.Add(descriptor.Instance);
            if (descriptor.Instance is IGameInstaller installer) _gameInstallPlugins.Add(installer);
            _pluginDescriptors.Add(descriptor);
        }

        /// <summary>
        /// All registered game install plugins
        /// </summary>
        public static IReadOnlyList<IGameInstaller> GameInstallPlugins => _gameInstallPlugins;

        /// <summary>
        /// All registered launcher plugins
        /// </summary>
        public static IReadOnlyList<ILauncherPlugin> LauncherPlugins => _launcherPlugins;

        /// <summary>
        /// All registered plugin descriptors. Used for UI purposes
        /// </summary>
        public static IReadOnlyList<PluginDescriptor> PluginDescriptors => _pluginDescriptors;

        public class PluginDescriptor
        {
            public required string Name { get; init; }
            public required string Description { get; init; }
            public required SemVersion TargetApiVersion { get; init; }
            public required ILauncherPlugin Instance { get; set; }
        }
    }
}
