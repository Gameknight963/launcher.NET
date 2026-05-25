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
            IEnumerable<ThunderstoreVersion> dedupedDeps = deps.Where(d => !pkgs.Any(p => p == d));
            _ = Install(game, pkgs, dedupedDeps);
        }

        async Task InstallPackage(ThunderstoreVersion pkg, GameInfo game)
        {
            WriteLog($"Downloading {pkg.DownloadUrl}...");
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "pkg.zip");
            await DownloadWithProgressAsync(pkg.DownloadUrl, zipPath);
            WriteLog("Download complete, extracting...");
            ZipFile.ExtractToDirectory(zipPath, game.AbsoluteRootDirectory, overwriteFiles: true);
            WriteLog("Extracted.");
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
                    downloadProgressBar.Value = (int)(downloaded * 100 / total);
            }
        }

        async Task Install(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps)
        {
            progressBar.Maximum = pkgs.Count() + deps.Count();
            int i = 0;
            int j = 0;
            foreach (ThunderstoreVersion pkg in pkgs)
            {
                activityHint.Text = $"Installing package {i+1} out of {pkgs.Count()}";
                await InstallPackage(pkg, game);
                i++;
                j++;
                progressBar.Value = j;
            }
            i = 0;
            foreach (ThunderstoreVersion dep in deps)
            {
                activityHint.Text = $"Installing dependency {i+1} out of {deps.Count()}";
                await InstallPackage(dep, game);
                i++;
                j++;
                progressBar.Value = j;
            }
            WriteLog("All done.");
            Close();
        }

        void WriteLog(string text)
        {
            logBox.Text += $"{text}\r\n";
            LauncherLogger.WriteLine(text);
        }
    }
}
