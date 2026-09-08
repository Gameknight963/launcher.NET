using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using System.Reflection;
using System.Runtime.InteropServices;

namespace launcherdotnet.Launcher
{
    internal static class PluginManager
    {
        public static async Task LoadPlugins()
        {
            if (!Directory.Exists(LauncherConstants.PluginsDir))
            {
                LauncherLogger.WriteLine("No plugins directory. Creating it now", true);
                Directory.CreateDirectory(LauncherConstants.PluginsDir);
                return;
            }

            string[] paths = Directory.GetDirectories(LauncherConstants.PluginsDir);
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

            foreach (string path in Directory.GetFiles(LauncherConstants.PluginsDir, "*.dll"))
            {
                LauncherLogger.Error($"{Path.GetFileName(path)}: loading assemblies directly from the plugins directory " +
                    $"is no longer supported. Move the plugin to a subfolder.");
            }
            foreach (string directory in paths)
            {
                try
                {
                    string? pluginPath = FindPlugin(directory);
                    if (pluginPath == null) return;

                    Assembly asm = Assembly.LoadFrom(pluginPath);
                    LauncherPluginAttribute? meta = asm.GetCustomAttribute<LauncherPluginAttribute>();

                    if (meta == null)
                    {
                        throw new Exception($"Could not load plugin metadata from {Path.GetFileName(directory)}.\n" +
                            $"EVEN THOUGH the previous check found it before. Something is very wrong");
                    }

                    if (!LauncherSettings.Settings.DisablePluginVersionCheck &&
                        !(meta.TargetApiVersion.ComparePrecedenceTo(LauncherApiInfo.ApiVersion) <= 0 &&
                          meta.TargetApiVersion.Major == LauncherApiInfo.ApiVersion.Major))
                    {
                        LauncherLogger.Error(
                            $"Plugin '{Path.GetFileName(directory)}' incompatible. \n" +
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
                        $"Failed to load plugin {Path.GetFileName(directory)}: {ex.GetType().Name} - {ex.Message}");
                    if (LauncherSettings.Settings.DisableExceptionHandling) throw;
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
                    if (LauncherSettings.Settings.DisableExceptionHandling) throw;
                }
            }));
            if (loadedPluginsCount > 0)
                LauncherLogger.Success($"Loaded {loadedPluginsCount} plugins successfully!" , true);
        }

        private static string? FindPlugin(string directory)
        {
            string folderName = Path.GetFileName(directory);
            string[] allDlls = Directory.GetFiles(directory, "*.dll");
            string[] exactMatches = allDlls
                .Where(dll => Path.GetFileNameWithoutExtension(dll)
                    .Equals(folderName, StringComparison.OrdinalIgnoreCase))
                .ToArray();
            string[] fuzzyMatches = allDlls
                .Where(dll => folderName.Contains(
                    Path.GetFileNameWithoutExtension(dll),
                    StringComparison.OrdinalIgnoreCase))
                .Except(exactMatches)
                .ToArray();
            string[] runtimeDlls = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
            string[] hostDlls = Directory.GetFiles(AppContext.BaseDirectory, "*.dll");
            PathAssemblyResolver resolver = new(runtimeDlls.Concat(allDlls).Concat(hostDlls));
            using MetadataLoadContext mlc = new(resolver);
            string? result = ScanDlls(exactMatches, mlc);
            if (result != null) return result;
            result = ScanDlls(fuzzyMatches, mlc);
            if (result != null)
            {
                LauncherLogger.Warn($"Plugin found in '{Path.GetRelativePath(LauncherConstants.PluginsDir, result)}' " +
                    $"which only fuzzy matched folder '{folderName}'. " +
                    $"Consider renaming the plugin DLL to match the folder name");
                return result;
            }
            string[] remainingDlls = allDlls.Except(exactMatches).Except(fuzzyMatches).ToArray();
            string? remainingResult = ScanDlls(remainingDlls, mlc);
            if (remainingResult == null)
                LauncherLogger.Error($"Could not find any assemblies with " +
                    $"plugin metadata in {folderName}.\n" +
                    $"Is it missing the LauncherPluginAttribute?");
            else
                LauncherLogger.Warn($"Plugin found in " +
                    $"'{Path.GetRelativePath(LauncherConstants.PluginsDir, remainingResult)}' " +
                    $"which was only found by scanning all DLLs in {folderName}. Consider renaming the plugin DLL " +
                    $"to match the folder name.");
            return remainingResult;
        }

        private static string? ScanDlls(string[] dlls, MetadataLoadContext mlc)
        {
            foreach (string dll in dlls)
            {
                Assembly assembly;
                try { assembly = mlc.LoadFromAssemblyPath(dll); }
                catch { continue; }

                if (assembly.CustomAttributes.Any(a => a.AttributeType.Name == nameof(LauncherPluginAttribute)))
                    return dll;
            }

            return null;
        }
    }
}
