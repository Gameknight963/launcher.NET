using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;
using ThunderstoreModManager;
using ThunderstoreModManager.ThunderstoreAPI;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class ThunderstoreModInstaller : ThemeableForm
    {
        public ThunderstoreModInstaller(GameInfo game, IEnumerable<ThunderstoreVersion> pkgs, 
            IEnumerable<ThunderstoreVersion> deps, ThunderstoreConfig config)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            logBox.DeselectAll();
            IEnumerable<ThunderstoreVersion> dedupedDeps = deps.Where(d => !pkgs.Any(p => p == d));
            _ = Install(game, pkgs, dedupedDeps, config);
        }

        async Task Install(GameInfo game, 
            IEnumerable<ThunderstoreVersion> pkgs, IEnumerable<ThunderstoreVersion> deps,
            ThunderstoreConfig config)
        {
            progressBar.Maximum = pkgs.Count() + deps.Count();
            await ModInstaller.InstallAsync(
                game, pkgs, deps,
                config,
                onLog: WriteLog,
                onProgress: (completed, total) =>
                {
                    progressBar.Maximum = total;
                    progressBar.Value = completed;
                },
                onDownloadProgress: value =>
                {
                    downloadProgressBar.Maximum = downloadProgressBar.Maximum;
                    downloadProgressBar.Value = value;
                });
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
