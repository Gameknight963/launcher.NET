using launcherdotnet.Networking;
using launcherdotnet.PluginAPI;
using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using System.IO.Compression;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class ThunderstoreModInstaller : ThemeableForm
    {
        public ThunderstoreModInstaller(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps)
        {
            InitializeComponent();
            logBox.DeselectAll();
            IEnumerable<ThunderstoreVersion> dedupedDeps = deps.Where(d => !pkgs.Any(p => p == d));
            _ = Install(game, pkgs, dedupedDeps);
        }

        void DeleteIgnoreExt(string path)
        {
            string[] filesToDelete = Directory.GetFiles(Path.GetDirectoryName(path)!, $"{Path.GetFileNameWithoutExtension(path)}.*");

            foreach (string f in filesToDelete)
            {
                File.Delete(f);
                WriteLog($"Deleted {Path.GetFileName(f)}");
            }
        }

        async Task<InstalledMod> InstallPackage(ThunderstoreVersion pkg, GameInfo game, bool isDependency)
        {
            downloadProgressBar.Value = 0;
            WriteLog($"Downloading {pkg.DownloadUrl}...");
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "mod.zip");
            await DownloadWithProgressAsync(pkg.DownloadUrl, zipPath);
            WriteLog($"Download complete, extracting...");

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

            extractedFiles.RemoveAll(file => Path.GetFileNameWithoutExtension(file) == "manifest");
            extractedFiles.RemoveAll(file => Path.GetFileNameWithoutExtension(file) == "icon");
            extractedFiles.RemoveAll(file => Path.GetFileNameWithoutExtension(file) == "README");

            WriteLog($"Extracted {extractedFiles.Count} files.");
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

        async Task DownloadWithProgressAsync(string url, string destination)
        {
            using HttpResponseMessage response =
                await LauncherHttp.Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            long total = response.Content.Headers.ContentLength ?? -1;
            using Stream input = await response.Content.ReadAsStreamAsync();

            using FileStream output = new(
                destination,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                81920,
                true);

            byte[] buffer = new byte[81920];
            long downloaded = 0;
            int read;
            while ((read = await input.ReadAsync(buffer)) > 0)
            {
                await output.WriteAsync(buffer.AsMemory(0, read));
                downloaded += read;
                if (total > 0)
                {
                    downloadProgressBar.Value = downloadProgressBar.Maximum;
                    downloadProgressBar.Value = (int)(downloaded * 100 / total);
                }
            }
        }

        async Task Install(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps)
        {
            progressBar.Maximum = pkgs.Count() + deps.Count();
            List<InstalledMod> installed = [];
            int i = 0;
            int j = 0;
            foreach (ThunderstoreVersion pkg in pkgs)
            {
                activityHint.Text = $"Installing package {i + 1} out of {pkgs.Count()}";
                installed.Add(await InstallPackage(pkg, game, false));
                i++;
                j++;
                progressBar.Value = j;
            }
            i = 0;
            foreach (ThunderstoreVersion dep in deps)
            {
                activityHint.Text = $"Installing dependency {i + 1} out of {deps.Count()}";
                installed.Add(await InstallPackage(dep, game, true));
                i++;
                j++;
                progressBar.Value = j;
            }
            WriteLog("Removing leftover package metadata");
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "manifest"));
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "icon"));
            DeleteIgnoreExt(Path.Combine(game.AbsoluteRootDirectory, "README"));

            WriteLog("Updating manifest...");
            ModManifest manifest = ModManifest.Load(game.AbsoluteRootDirectory);
            foreach (InstalledMod mod in installed)
            {
                // replace existing entry if already installed
                manifest.InstalledMods.RemoveAll(m => m.Name == mod.Name && m.Owner == mod.Owner);
                manifest.InstalledMods.Add(mod);
            }
            manifest.Save(game.AbsoluteRootDirectory);

            WriteLog("All done.");
            Close();
        }

        void WriteLog(string text)
        {
            logBox.Text += $"{text}\r\n";
            logBox.SelectionStart = logBox.TextLength;
            logBox.ScrollToCaret();
            LauncherLogger.WriteLine(text);
        }
    }
}
