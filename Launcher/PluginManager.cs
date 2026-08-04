using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using System.Reflection;

namespace launcherdotnet.Launcher
{
    internal static class PluginManager
    {
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
            int loadedPluginsCount = 0;

            List<(LauncherPluginAttribute Meta, ILauncherPlugin Plugin)> plugins = new();

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

                    if (!LauncherSettings.Settings.DisablePluginVersionCheck &&
                        !(meta.TargetApiVersion.ComparePrecedenceTo(LauncherApiInfo.ApiVersion) <= 0 &&
                          meta.TargetApiVersion.Major == LauncherApiInfo.ApiVersion.Major))
                    {
                        LauncherLogger.Error(
                            $"Plugin '{Path.GetFileName(file)}' incompatible. \n" +
                            $"Expected any version between {LauncherApiInfo.ApiVersion.Major}.0.0-{LauncherApiInfo.ApiVersion}, got {meta.TargetApiVersion}");
                        continue;
                    }

                    Type entryType = meta.EntryType;

                    if (!typeof(ILauncherPlugin).IsAssignableFrom(entryType))
                    {
                        throw new InvalidOperationException(
                            $"Plugin entry type '{entryType.FullName}' does not implement ILauncherPlugin.");
                    }

                    ILauncherPlugin plugin = (ILauncherPlugin)Activator.CreateInstance(entryType)!;

                    plugins.Add((meta, plugin));
                    plugin.InitializeMainThread();
                }
                catch (Exception ex)
                {
                    LauncherLogger.Error(
                        $"Failed to load plugin {Path.GetFileName(file)}: {ex.GetType().Name} - {ex.Message}");
                }
            }

            await Task.WhenAll(plugins.Select(async x =>
            {
                try
                {
                    await x.Plugin.Initialize();

                    PluginRegistry.PluginDescriptor descriptor = new()
                    {
                        Name = x.Meta.Name,
                        Description = x.Meta.Description,
                        TargetApiVersion = x.Meta.TargetApiVersion,
                        Instance = x.Plugin
                    };

                    PluginRegistry.Register(descriptor);
                    Interlocked.Increment(ref loadedPluginsCount);

                    LauncherLogger.WriteLine($"Loaded plugin: {descriptor.Name}", true);
                }
                catch (Exception ex)
                {
                    LauncherLogger.Error(
                        $"Failed to initialize plugin '{x.Meta.Name}': {ex.GetType().Name} - {ex.Message}");
                }
            }));
            if (loadedPluginsCount > 0)
                LauncherLogger.Success($"Loaded {loadedPluginsCount} plugins successfully!" , true);
        }
    }
}
