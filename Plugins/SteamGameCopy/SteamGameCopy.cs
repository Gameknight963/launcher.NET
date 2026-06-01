using launcherdotnet.Launcher.Forms;
using launcherdotnet.PluginAPI;
using launcherdotnet.Plugins.SteamGameCopy;

[assembly: LauncherPlugin(typeof(SteamGameCopy),
    "Steam Game Copier",
    "Copies any installed Steam game to launcher.net",
    "1.0.0")]

namespace launcherdotnet.Plugins.SteamGameCopy
{
    public class SteamGameCopy : IGameInstaller
    {
        public string GameName => "Steam Game";

        public LabelQueryTime PromptForLabel => LabelQueryTime.AfterInstall;

        public IEnumerable<ReleaseInfo>? GetReleases()
        {
            return null;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task<PluginGameInfo?> Install(string installDir, IProgress<double> progress, IProgress<string> status, ReleaseInfo? release = null)
        {
            List<SteamGame> games = SteamHelper.GetInstalledGames(SteamHelper.GetSteamPath() ?? throw new InvalidOperationException());
            SteamCopyForm form = new(games);
            form.ShowDialog();
            if (form.SelectedGame == null) return null;
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(form.SelectedGame.RootDirectory, installDir);
            if (!PluginTools.FindGameExe(installDir, out string? path))
            {
                if (CoolMessageBox.Show("The executable of the game could not be found.\n" +
                    "Would you like to select it manually?",
                    "User Input Required",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return null;
                }
                using OpenFileDialog dialog = new();
                dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                dialog.Title = "Select the game executable";
                if (dialog.ShowDialog() != DialogResult.OK) return null;
                path = dialog.FileName;
            }
            return new PluginGameInfo
            {
                ExePath = path,
            };
        }
    }
}