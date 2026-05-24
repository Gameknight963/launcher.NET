using launcherdotnet.Networking;
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

        async Task Install(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps)
        {
            int i = 0;
            foreach (ThunderstoreVersion pkg in pkgs)
            {
                WriteLog($"Installing package {pkg.DownloadUrl}");
                activityHint.Text = $"Installing package {i} out of {pkgs.Count()}";

                using Stream response = await LauncherHttp.Client.GetStreamAsync(pkg.DownloadUrl);
                ZipFile.ExtractToDirectory(response, game.AbsolutePath);
                i++;
            }
            i = 0;
            foreach (ThunderstoreVersion dep in deps)
            {
                WriteLog($"Installing package {dep.DownloadUrl}");
                activityHint.Text = $"Installing package {i} out of {deps.Count()}";
                using Stream response = await LauncherHttp.Client.GetStreamAsync(dep.DownloadUrl);
                ZipFile.ExtractToDirectory(response, game.AbsolutePath);
                i++;
            }
            Close();
        }

        void WriteLog(string text)
        {
            logBox.Text += $"{text}\n";
            LauncherLogger.WriteLine(text);
        }
    }
}
