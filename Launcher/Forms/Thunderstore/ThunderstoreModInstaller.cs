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
            _ = Install(game, pkgs, deps);
        }

        async Task InstallPackage(ThunderstoreVersion pkg, GameInfo game)
        {
            WriteLog($"Downloading {pkg.DownloadUrl}...");
            byte[] data = await DownloadWithProgressAsync(pkg.DownloadUrl);
            WriteLog($"Download complete, extracting...");
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "pkg.zip");
            await File.WriteAllBytesAsync(zipPath, data);
            ZipFile.ExtractToDirectory(zipPath, game.AbsoluteRootDirectory, overwriteFiles: true);
            WriteLog($"Extracted.");
        }

        async Task<byte[]> DownloadWithProgressAsync(string url)
        {
            using HttpResponseMessage response = await LauncherHttp.Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            long total = response.Content.Headers.ContentLength ?? -1;
            using Stream stream = await response.Content.ReadAsStreamAsync();
            using MemoryStream ms = new();
            byte[] buffer = new byte[81920];
            long downloaded = 0;
            int read;
            while ((read = await stream.ReadAsync(buffer)) > 0)
            {
                await ms.WriteAsync(buffer.AsMemory(0, read));
                downloaded += read;
                if (total > 0)
                {
                    // going backwards doesnt do the smoothing
                    downloadProgressBar.Value = downloadProgressBar.Maximum;
                    downloadProgressBar.Value = (int)(downloaded * 100 / total);
                }
            }
            return ms.ToArray();
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
