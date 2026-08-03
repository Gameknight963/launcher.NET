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
        private const string _sourceId = "modstate";

        public string DisplayName => "Thunderstore Mod Manager";

        public string Id => "launcherdotnet.thunderstore";

        public IEnumerable<InstalledMod> GetInstalledMods(GameInfo game)
        {
            ThunderstoreData config = ModSourceConfig<ThunderstoreData>.Load(game, _sourceId);
            return config.InstalledMods;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task OpenModBrowser(GameInfo game)
        {
            ThunderstoreData config = ModSourceConfig<ThunderstoreData>.Load(game, _sourceId);
            using ThunderstoreModBrowser browser = new(game, config);
            browser.ShowDialog();
        }

        public bool UninstallMod(GameInfo game, InstalledMod mod)
        {
            throw new NotImplementedException();
        }
    }
}
