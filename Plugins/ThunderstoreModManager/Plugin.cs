using ThunderstoreModManager;
using launcherdotnet.PluginAPI;
using launcherdotnet.Launcher;
using launcherdotnet.Launcher.Forms;
using launcherdotnet;

[assembly: LauncherPlugin(typeof(Plugin),
    "Thunderstore Mod Manager",
    "Browse and install mods on Thunderstore",
    "2.0.0")]

namespace ThunderstoreModManager
{
    public class Plugin : IModSource
    {
        internal const string SourceId = "modstate";

        public string DisplayName => "Thunderstore Mod Manager";

        public string Id => "launcherdotnet.thunderstore";

        public IEnumerable<InstalledMod> GetInstalledMods(GameInfo game)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(game, SourceId);
            return config.InstalledMods;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task OpenModBrowser(GameInfo game)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(game, SourceId);
            using ThunderstoreModBrowser browser = new(game, config);
            browser.ShowDialog();
            config.Save(game, SourceId);
        }

        public bool UninstallMod(GameInfo game, InstalledMod mod)
        {
            throw new NotImplementedException();
        }
    }
}
