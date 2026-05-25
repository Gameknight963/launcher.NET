using launcherdotnet.Launcher;
using launcherdotnet.Networking;
using launcherdotnet.PluginAPI;
using System.IO.Compression;

namespace launcherdotnet.Thunderstore
{
    public static class ModInstaller
    {
        public static async Task InstallAsync(
            GameInfo game,
            IEnumerable<ThunderstoreVersion> pkgs,
            IEnumerable<ThunderstoreVersion> deps,
            Action<string>? onLog = null,
            Action<int, int>? onProgress = null,
            Action<int>? onDownloadProgress = null)
        {
            GameModState state = GameModState.Load(game.AbsoluteRootDirectory);
            HashSet<string> installedVersions = state.InstalledMods
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
                state.InstalledMods.RemoveAll(m => m.Name == mod.Name && m.Owner == mod.Owner);
                state.InstalledMods.Add(mod);
            }
            state.Save(game.AbsoluteRootDirectory);
            onLog?.Invoke("All done.");
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
    }
}