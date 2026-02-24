using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.Launcher
{
    using System.Reflection;
    using launcherdotnet.PluginAPI;

    internal static class PluginManager
    {
        private static readonly List<ILauncherPlugin> _plugins = new();

        public static async void LoadPlugins(string folder)
        {
            if (!Directory.Exists(folder))
            {
                LauncherLogger.WriteLine("No plugins found.");
                Directory.CreateDirectory(folder);
                return;
            }

            string[] paths = Directory.GetFiles(folder, "*.dll");
            if (paths.Length == 0)
            {
                LauncherLogger.WriteLine("No plugins found.");
                return;
            }

            LauncherLogger.WriteColor($"Found {paths.Length} plugins", false, ConsoleColor.White, ConsoleColor.Black);
            LauncherLogger.WriteLine(" in plugins folder:");
            foreach (string p in paths)
                LauncherLogger.WriteLine(Path.GetFileName(p));

            foreach (string file in paths)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(file);

                    foreach (Type type in asm.GetTypes())
                    {
                        if (!typeof(ILauncherPlugin).IsAssignableFrom(type) || type.IsAbstract)
                            continue;

                        ILauncherPlugin plugin =
                            (ILauncherPlugin)Activator.CreateInstance(type)!;

                        // API version check
                        if (plugin.TargetApiVersion.Major != LauncherApiInfo.ApiVersion.Major)
                            continue;

                        await plugin.Initialize();
                        LauncherLogger.WriteLine($"Loaded plugin: {plugin.Name}");
                        _plugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    LauncherLogger.Error($"Failed to load plugin {Path.GetFileName(file)} due to a {ex.GetType().Name}: {ex.Message}");
                }
            }
            if (_plugins.Count > 0)
                LauncherLogger.Success($"Loaded {_plugins.Count} plugins successfully!");
        }

        public static IReadOnlyList<ILauncherPlugin> Plugins => _plugins;
    }
}
