using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class GameInstallerRegistry
    {
        private static readonly List<GameInstallPluginEntry> _gameInstallPlugins = new();

        /// <summary>
        /// Called by a plugin to register itself as a game installer
        /// </summary>
        public static void RegisterGameInstallPlugin(
            IGameInstaller Installer, 
            IEnumerable<ReleaseInfo>? Releases = null)
        {
            _gameInstallPlugins.Add(new GameInstallPluginEntry(Installer, Releases));
        }

        /// <summary>
        /// All registered game install plugins
        /// </summary>
        public static IReadOnlyList<GameInstallPluginEntry> GameInstallPlugins => _gameInstallPlugins.AsReadOnly();
    }

    public record GameInstallPluginEntry(
        IGameInstaller Installer,
        IEnumerable<ReleaseInfo>? Releases);
}
