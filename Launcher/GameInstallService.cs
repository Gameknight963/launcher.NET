using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using Semver;

namespace launcherdotnet.Launcher
{
    public static class GameInstallService
    {
        public static async Task<GameInfo?> InstallAsync(
            IGameInstaller installer,
            string? version,
            IProgress<double> progress,
            IProgress<string> status,
            string? label = null)
        {
            GameInfo newGame = new();
            if (label != null) newGame.Label = label;
            string installDir = Path.Combine(LauncherConstants.GamesDir, $"{newGame.Label}_{newGame.Id}");
            newGame.RelativeRootDirectory = Path.GetRelativePath(LauncherConstants.BaseDir, installDir);
            Directory.CreateDirectory(installDir);

            PluginGameInfo? installed = await Task.Run(() => installer.Install(installDir, progress, status, version));
            if (installed == null) return null;

            newGame.GameName = installer.GameName;
            newGame.RelativePath = Path.GetRelativePath(LauncherConstants.BaseDir, installed.ExePath);
            newGame.RunWithCmd = installed.RunWithCmd;
            newGame.ModManagable = installed.ModManageable;
            newGame.ThunderstoreCommunitySlug = installed.ThunderstoreCommunitySlug;

            GameService.UpsertGame(newGame);
            GameModState state = new();
            state.TakeBaseline(installDir);
            state.Save(installDir);

            return newGame;
        }
    }
}
