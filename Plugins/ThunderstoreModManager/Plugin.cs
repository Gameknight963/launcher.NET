using ThunderstoreModManager;
using launcherdotnet.PluginAPI;
using launcherdotnet.Launcher;
using launcherdotnet.Launcher.Forms;
using launcherdotnet;
using ThunderstoreModManager.ThunderstoreAPI;

[assembly: LauncherPlugin(typeof(Plugin),
    "Thunderstore Mod Manager",
    "Browse and install mods on Thunderstore",
    "2.0.0")]

namespace ThunderstoreModManager
{
    public class Plugin : IModSource
    {
        public string DisplayName => "Thunderstore Mod Manager";

        public string Id => "launcherdotnet.thunderstore";

        public IEnumerable<InstalledMod> GetInstalledMods(GameInfo game)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(game, ThunderstoreConfig.SourceId);
            return config.InstalledMods;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task OpenModBrowser(GameInfo game)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(game, ThunderstoreConfig.SourceId);
            using ThunderstoreModBrowser browser = new(game, config);
            browser.ShowDialog();
            config.Save(game, ThunderstoreConfig.SourceId);
        }

        public bool UninstallMods(GameInfo game, List<InstalledMod> mods)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(game, ThunderstoreConfig.SourceId);
            bool success = ModInstaller.UninstallManyMods(game, mods, config);
            config.Save(game, ThunderstoreConfig.SourceId);
            return success;
        }
    }
}
