using launcherdotnet;
using launcherdotnet.Launcher;
using launcherdotnet.Launcher.Forms;
using launcherdotnet.Networking;
using launcherdotnet.PluginAPI;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Reflection;
using System.Xml.Linq;
using ThunderstoreModManager.Extensions;
using static System.Windows.Forms.AxHost;

namespace ThunderstoreModManager.ThunderstoreAPI
{
    public static class ModInstaller
    {
        public static async Task InstallAsync(
            GameInfo game,
            IEnumerable<ThunderstoreVersion> pkgs,
            IEnumerable<ThunderstoreVersion> deps,
            ThunderstoreConfig config,
            Action<string>? onLog = null,
            Action<int, int>? onProgress = null,
            Action<int>? onDownloadProgress = null)
        {
            HashSet<string> installedVersions = config.InstalledMods
                .Select(m => $"{m.Owner}-{m.Name}-{m.Version}")
                .ToHashSet();

            bool IsAlreadyInstalled(ThunderstoreVersion v) =>
                installedVersions.Contains($"{v.Namespace}-{v.Name}-{v.VersionNumber}");

            List<ThunderstoreVersion> pkgsList = pkgs.Where(p => !IsAlreadyInstalled(p)).ToList();
            List<ThunderstoreVersion> depsList = deps.Where(d => !IsAlreadyInstalled(d)).ToList();

            foreach (ThunderstoreVersion skipped in pkgs.Concat(deps).Where(IsAlreadyInstalled))
                onLog?.Invoke($"Skipping {skipped.Name} v{skipped.VersionNumber} (already installed)");

            int total = pkgsList.Count + depsList.Count;
            int completed = 0;

            List<InstalledMod> installed = [];

            foreach (ThunderstoreVersion pkg in pkgsList)
            {
                onLog?.Invoke($"Installing {pkg.Name} v{pkg.VersionNumber}...");
                installed.Add(await InstallPackageAsync(pkg, game, false, onLog, onDownloadProgress));
                completed++;
                onProgress?.Invoke(completed, total);
            }

            foreach (ThunderstoreVersion dep in depsList)
            {
                onLog?.Invoke($"Installing dependency {dep.Name} v{dep.VersionNumber}...");
                installed.Add(await InstallPackageAsync(dep, game, true, onLog, onDownloadProgress));
                completed++;
                onProgress?.Invoke(completed, total);
            }

            onLog?.Invoke("Removing leftover package metadata...");
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "manifest"), onLog);
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "icon"), onLog);
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "README"), onLog);

            onLog?.Invoke("Updating manifest...");
            foreach (InstalledMod mod in installed)
            {
                config.InstalledMods.RemoveAll(m => m.Name == mod.Name && m.Owner == mod.Owner);
                config.InstalledMods.Add(mod);
            }
            config.Save(game, ThunderstoreConfig.SourceId);
            onLog?.Invoke("All done.");
        }

        public static void UninstallMod(GameInfo game, InstalledMod mod, ThunderstoreConfig config)
        {
            foreach (string file in mod.Files)
            {
                string absolute = Path.Combine(game.AbsoluteRootDirectory, file);
                File.Delete(absolute);
                PluginLogger.WriteLine($"Deleted '{absolute}'", true);
            }
            
            int removed = config.InstalledMods.RemoveAll(x => x.DependencyStringEquals(mod));
            if (removed == 0)
                PluginLogger.Error($"unable to remove mod '{mod.DependencyString()}' from config", true);
            else if (removed > 1)
                PluginLogger.Warn($"Somehow managed to find two matches for $'{mod.DependencyString()}' in config. " +
                    $"This is most definitely a bug");
            else
                PluginLogger.Success($"successfully uninstalled mod '{mod.Name}'");

        }

        public static bool UninstallManyMods(GameInfo game, List<InstalledMod> mods, ThunderstoreConfig config, bool gui = true)
        {
            if (gui)
            {
                string names = string.Join(Environment.NewLine, mods.Select(m => m.Name));

                // warn if other mods depend on what we're removing
                List<InstalledMod> dependents = config.InstalledMods
                    .Except(mods)
                    .Where(m => m.Dependencies.Any(d => mods.Any(s => d.StartsWith($"{s.Owner}-{s.Name}-"))))
                    .ToList();

                if (dependents.Count > 0)
                {
                    string dependentNames = string.Join(Environment.NewLine, dependents.Select(m => m.Name));
                    PluginLogger.Warn($"Uninstall requested for mods that have dependents: {string.Join(", ", mods.Select(m => m.Name))}");
                    if (CoolMessageBox.Show(
                        $"The following mods depend on one or more mods you are trying to uninstall:\n" +
                        $"{dependentNames}\n\nUninstalling may cause them to break. Continue anyway?",
                        "Warning",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) != DialogResult.OK) return false;
                }

                if (CoolMessageBox.Show(
                    $"Are you sure you would like to uninstall the following mods?\n{names}",
                    "Confirm Uninstall",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) != DialogResult.OK) return false;

                // find orphaned dependencies after removal
                HashSet<string> stillRequired = config.InstalledMods
                    .Except(mods)
                    .SelectMany(m => m.Dependencies)
                    .ToHashSet();

                List<InstalledMod> orphans = config.InstalledMods
                    .Except(mods)
                    .Where(m => m.IsDependency && !stillRequired.Any(d => d.StartsWith($"{m.Owner}-{m.Name}-")))
                    .ToList();

                if (orphans.Count > 0)
                {
                    string orphanNames = string.Join(Environment.NewLine, orphans.Select(m => m.Name));
                    PluginLogger.WriteLine($"Found {orphans.Count} orphaned dependencies: {string.Join(", ", orphans.Select(m => m.Name))}");
                    if (CoolMessageBox.Show(
                        $"The following dependencies are no longer needed by any installed mod:\n{orphanNames}" +
                        $"\n\nWould you like to remove them too?",
                        "Remove Unused Dependencies",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                        mods.AddRange(orphans);
                }
            }

            foreach (InstalledMod mod in mods)
            {
                UninstallMod(game, mod, config);
            }

            return true;

            // soon
            //if (config.InstalledMods.Count == 0 && config.HasBaseline)
            //    CleanUpUntrackedFiles();

        }

        private static async Task<InstalledMod> InstallPackageAsync(
            ThunderstoreVersion pkg,
            GameInfo game,
            bool isDependency,
            Action<string>? onLog,
            Action<int>? onDownloadProgress)
        {
            onLog?.Invoke($"Downloading {pkg.DownloadUrl}...");
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "mod.zip");
            await DownloadWithProgressAsync(pkg.DownloadUrl, zipPath, onDownloadProgress);
            onLog?.Invoke("Download complete, extracting...");

            List<string> extractedFiles = [];
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name)) continue;
                    string destPath = Path.Combine(game.AbsoluteRootDirectory, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath)!);
                    entry.ExtractToFile(destPath, overwrite: true);
                    extractedFiles.Add(entry.FullName);
                }
            }

            onLog?.Invoke($"Extracted {extractedFiles.Count} files.");
            return new InstalledMod
            {
                Name = pkg.Name ?? "",
                Owner = pkg.Namespace ?? "",
                Version = pkg.VersionNumber,
                Files = extractedFiles,
                IsDependency = isDependency,
                Dependencies = pkg.Dependencies
            };
        }

        private static async Task DownloadWithProgressAsync(string url, string destination, Action<int>? onProgress)
        {
            using HttpResponseMessage response =
                await LauncherHttp.Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            long total = response.Content.Headers.ContentLength ?? -1;
            using Stream input = await response.Content.ReadAsStreamAsync();
            using FileStream output = new(destination, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true);
            byte[] buffer = new byte[81920];
            long downloaded = 0;
            int read;
            while ((read = await input.ReadAsync(buffer)) > 0)
            {
                await output.WriteAsync(buffer.AsMemory(0, read));
                downloaded += read;
                if (total > 0)
                    onProgress?.Invoke((int)(downloaded * 100 / total));
            }
        }

        private static void DeleteIgnoreExt(string path, Action<string>? onLog)
        {
            string[] filesToDelete = Directory.GetFiles(
                Path.GetDirectoryName(path)!,
                $"{Path.GetFileNameWithoutExtension(path)}.*");
            foreach (string f in filesToDelete)
            {
                File.Delete(f);
                onLog?.Invoke($"Deleted {Path.GetFileName(f)}");
            }
        }

        internal static async Task InstallZipAsync(
            string zipPath,
            GameInfo game,
            Func<(string name, string owner, string version)?> onMissingInfo,
            ThunderstoreConfig config)
        {

            string? modName = null, modOwner = null, modVersion = null;

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                ZipArchiveEntry? manifestEntry = archive.GetEntry("manifest.json");
                if (manifestEntry != null)
                {
                    using StreamReader reader = new(manifestEntry.Open());
                    JObject manifest = JObject.Parse(await reader.ReadToEndAsync());
                    modName = manifest["name"]?.ToString();
                    modOwner = manifest["author"]?.ToString();
                    modVersion = manifest["version_number"]?.ToString();
                }
            }

            if (modName == null || modOwner == null || modVersion == null)
            {
                (string name, string owner, string version)? result = onMissingInfo();
                if (result == null) return;
                (modName, modOwner, modVersion) = result.Value;
            }

            List<string> extractedFiles = [];
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name)) continue;
                    string destPath = Path.Combine(game.AbsoluteRootDirectory, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath)!);
                    entry.ExtractToFile(destPath, overwrite: true);
                    extractedFiles.Add(entry.FullName);
                }
            }

            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "manifest"), null);
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "icon"), null);
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "README"), null);

            config.InstalledMods.RemoveAll(m => m.Name == modName && m.Owner == modOwner);
            config.InstalledMods.Add(new InstalledMod
            {
                Name = modName,
                Owner = modOwner,
                Version = modVersion,
                Files = extractedFiles,
            });
        }

        internal static bool TryInstallDllAsync(
            string dllPath,
            GameInfo game,
            string modName,
            string modOwner,
            string modVersion,
            ThunderstoreConfig config,
            bool isDependency = false)
        {
            string modsDir = Path.Combine(game.AbsoluteRootDirectory, "Mods");
            string pluginsDir = Path.Combine(game.AbsoluteRootDirectory, "BepInEx", "plugins");

            if (!Directory.Exists(modsDir) && !Directory.Exists(pluginsDir)) return false;
            string targetDir = Directory.Exists(modsDir) ? modsDir : pluginsDir;

            string destPath = Path.Combine(targetDir, Path.GetFileName(dllPath));
            File.Copy(dllPath, destPath, overwrite: true);

            string relativeDest = Path.GetRelativePath(game.AbsoluteRootDirectory, destPath);

            config.InstalledMods.RemoveAll(m => m.Name == modName && m.Owner == modOwner);
            config.InstalledMods.Add(new InstalledMod
            {
                Name = modName,
                Owner = modOwner,
                Version = modVersion,
                Files = [relativeDest],
                IsDependency = isDependency
            });
            return true;
        }
    }
}