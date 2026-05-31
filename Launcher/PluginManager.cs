using launcherdotnet.PluginAPI;
using System;
using System.Reflection;

namespace launcherdotnet.Launcher
{

    internal static class PluginManager
    {
        private static readonly List<ILauncherPlugin> _plugins = new();

        public static async void LoadPlugins(string folder)
        {
            if (!Directory.Exists(folder))
            {
                LauncherLogger.WriteLine("No plugins found.", true);
                Directory.CreateDirectory(folder);
                return;
            }

            string[] paths = Directory.GetFiles(folder, "*.dll");
            if (paths.Length == 0)
            {
                LauncherLogger.WriteLine("No plugins found.", true);
                return;
            }

            LauncherLogger.WriteColor($"Found {paths.Length} plugins", true, ConsoleColor.White, ConsoleColor.Black);
            LauncherLogger.WriteLine(" in plugins folder:", true);
            foreach (string p in paths)
                LauncherLogger.WriteLine(Path.GetFileName(p), true);

            foreach (string file in paths)
            {
                try
                {
                    Assembly asm = Assembly.LoadFrom(file);

                    LauncherPluginAttribute? meta = asm.GetCustomAttribute<LauncherPluginAttribute>();

                    if (meta == null)
                    {
                        LauncherLogger.Warn($"No plugin metadata in {Path.GetFileName(file)}");
                        continue;
                    }

                    if (meta.TargetApiVersion.Major != LauncherApiInfo.ApiVersion.Major)
                    {
                        LauncherLogger.Error(
                            $"Plugin '{Path.GetFileName(file)}' incompatible. " +
                            $"Expected {LauncherApiInfo.ApiVersion.Major}, got {meta.TargetApiVersion.Major}");

                        continue;
                    }

                    Type entryType = meta.EntryType;

                    if (!typeof(ILauncherPlugin).IsAssignableFrom(entryType))
                    {
                        throw new InvalidOperationException(
                            $"Plugin entry type '{entryType.FullName}' does not implement ILauncherPlugin.");
                    }

                    ILauncherPlugin plugin = (ILauncherPlugin)Activator.CreateInstance(entryType)!;

                    await plugin.Initialize();

                    PluginRegistry.Register(plugin);
                    _plugins.Add(plugin);

                    LauncherLogger.WriteLine($"Loaded plugin: {plugin.Name}", true);
                }
                catch (Exception ex)
                {
                    LauncherLogger.Error(
                        $"Failed to load plugin {Path.GetFileName(file)}: {ex.GetType().Name} - {ex.Message}");
                }
            }
            if (_plugins.Count > 0)
                LauncherLogger.Success($"Loaded {_plugins.Count} plugins successfully!" , true);
        }

        public static IReadOnlyList<ILauncherPlugin> Plugins => _plugins;
    }
}
