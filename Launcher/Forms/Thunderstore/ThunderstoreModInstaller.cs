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
            byte[] data = await LauncherHttp.Client.GetByteArrayAsync(pkg.DownloadUrl);
            WriteLog($"Download complete, extracting...");
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "pkg.zip");
            await File.WriteAllBytesAsync(zipPath, data);
            ZipFile.ExtractToDirectory(zipPath, game.AbsoluteRootDirectory, overwriteFiles: true);
            WriteLog($"Extracted.");
        }

        async Task Install(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps)
        {
            int i = 0;
            foreach (ThunderstoreVersion pkg in pkgs)
            {
                activityHint.Text = $"Installing package {i} out of {pkgs.Count()}";
                await InstallPackage(pkg, game);
                i++;
            }
            i = 0;
            foreach (ThunderstoreVersion dep in deps)
            {
                activityHint.Text = $"Installing dependency {i} out of {deps.Count()}";
                await InstallPackage(dep, game);
                i++;
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
