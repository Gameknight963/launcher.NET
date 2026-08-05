using launcherdotnet.Launcher.Forms;
using launcherdotnet.PluginAPI;
using launcherdotnet.Plugins.SteamGameCopy;
using System.Reflection;

[assembly: LauncherPlugin(typeof(Plugin),
    "Steam Game Copier",
    "Copies any installed Steam game to launcher.net",
    "2.1.0")]

namespace launcherdotnet.Plugins.SteamGameCopy
{
    public class Plugin : IGameInstaller
    {
        public string GameName => "Steam Game";

        public LabelQueryTime PromptForLabel => LabelQueryTime.Never;

        public IEnumerable<string>? GetReleases() => null;

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task<PluginGameInfo?> Install(string installDir, IProgress<double> progress, IProgress<string> status, string? release = null)
        {
            List<SteamGame> games = SteamHelper.GetInstalledGames(SteamHelper.GetSteamPath() ?? throw new InvalidOperationException());
            using SteamCopyForm form = new(games);
            form.ShowDialog();
            if (form.SelectedGame == null) return null;
            string? label = Launcher.LauncherDialogs.QueryLabel(form.SelectedGame.Name);
            if (label == null) return null;
            PluginTools.CopyDirectoryWithProgress(form.SelectedGame.RootDirectory, installDir, progress, status);
            if (!PluginTools.FindGameExe(installDir, out string? path, PluginTools.GameSearchOptions.SearchExcludeHelpers))
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

            PluginGameInfo info = new PluginGameInfo
            {
                ExePath = path,
                Label = label,
                GameName = form.SelectedGame.Name,
            };

            Assembly? assembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "ThunderstoreModManager");
            if (assembly != null)
            {
                string? slug = PluginTools.ToThunderstoreSlug(form.SelectedGame.Name);
                slug = await OptionalThunderstoreWrapper.DoesSlugExist(slug) ? slug : null;
                OptionalThunderstoreWrapper.SetSlug(slug, info);
                info.ModManagerId = "launcherdotnet.thunderstore";
            }

            return info;
        }
    }
}