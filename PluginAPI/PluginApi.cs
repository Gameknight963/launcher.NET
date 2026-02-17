using Semver;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class PluginApi
    {
        private static readonly List<GameInstallPluginEntry> _gameInstallPlugins = new();

        /// <summary>
        /// Called by a plugin to register itself as a game installer
        /// </summary>
        public static void RegisterGameInstallPlugin(
            IGameInstaller Installer, 
            IEnumerable<SemVersion>? Versions = null)
        {
            _gameInstallPlugins.Add(new GameInstallPluginEntry(Installer, Versions));
        }

        /// <summary>
        /// All registered game install plugins
        /// </summary>
        public static IReadOnlyList<GameInstallPluginEntry> GameInstallPlugins => _gameInstallPlugins.AsReadOnly();
    }

    public record GameInstallPluginEntry(
        IGameInstaller Installer,
        IEnumerable<SemVersion>? Versions);
}
