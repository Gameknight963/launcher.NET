namespace launcherdotnet.PluginAPI
{
    public static class PluginRegistry
    {
        private static readonly List<IGameInstaller> _gameInstallPlugins = new();
        private static readonly List<ILauncherPlugin> _launcherPlugins = new();

        internal static void Register(ILauncherPlugin plugin)
        {
            _launcherPlugins.Add(plugin);
            if (plugin is IGameInstaller installer) _gameInstallPlugins.Add(installer);
        }

        /// <summary>
        /// All registered game install plugins
        /// </summary>
        public static IReadOnlyList<IGameInstaller> GameInstallPlugins => _gameInstallPlugins.AsReadOnly();

        /// <summary>
        /// All registered launcher plugins
        /// </summary>
        public static IReadOnlyList<ILauncherPlugin> LauncherPlugins => _launcherPlugins.AsReadOnly();
    }
}
