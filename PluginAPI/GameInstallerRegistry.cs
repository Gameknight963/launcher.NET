using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class GameInstallerRegistry
    {
        private static readonly List<IGameInstaller> _gameInstallPlugins = new();

        /// <summary>
        /// Called by a plugin to register itself as a game installer
        /// </summary>
        internal static void RegisterGameInstallPlugin(IGameInstaller Installer)
        {
            _gameInstallPlugins.Add(Installer);
        }

        /// <summary>
        /// All registered game install plugins
        /// </summary>
        public static IReadOnlyList<IGameInstaller> GameInstallPlugins => _gameInstallPlugins.AsReadOnly();
    }
}
