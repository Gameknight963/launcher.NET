using launcherdotnet.PluginAPI;
using launcherdotnet.Plugins.HelloWorld;
using Semver;

[assembly: LauncherPlugin(typeof(HelloWorldInstaller), 
    "Hello World Installer", 
    "Generates an exe that prints Hello World to the console.", 
    "1.0.0")]

namespace launcherdotnet.Plugins.HelloWorld
{
    public class HelloWorldInstaller : IGameInstaller
    {
        private const string _hex = 
            "4D5A6B65726E656C3332000050450000" +
            "4C010300000000000000000000000000" +
            "78000F030B0100000000000000000000" +
            "0000000014310000000000000C000000" +
            "00004000001000000002000004000000" +
            "010000000400000000000000A6310000" +
            "14010000000000000300000000001000" +
            "00100000000000000000000000000000" +
            "0200000000000000000000007E310000" +
            "4B000000000047657453746448616E64" +
            "6C650000010000000010000000000000" +
            "00000000000000000000000000000000" +
            "400030C0787878787878780001000000" +
            "00200000000000000000000000000000" +
            "0000000000000000400030C078787878" +
            "78787800A60100000030000090010000" +
            "02000000000000000000000000000000" +
            "200030E06AF5FF155B3140005089E16A" +
            "00516A0F683831400050FF155F314000" +
            "6A00FF155731400048656C6C6F2C2057" +
            "6F726C64210D0A653100009430000072" +
            "31000000000000653100009430000072" +
            "310000000000004578697450726F6365" +
            "73730000577269746546696C65004731" +
            "00000000000000000000023000005731" +
            "0000";

        public string GameName => "Hello World";

        public LabelQueryTime PromptForLabel => LabelQueryTime.BeforeInstall;

        public IEnumerable<ReleaseInfo>? GetReleases()
        {
            return null;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task<PluginGameInfo?> Install(string installDir, IProgress<double> progress, IProgress<string> status, ReleaseInfo? release)
        {
            // we don't care about selected release since there's only one
            status.Report("Starting installation...");
            progress.Report(0);
            string finalpath = Path.Combine(installDir, "dummygame.exe");
            MakeDummyExe(finalpath, progress, status);
            return new PluginGameInfo
            {
                ExePath = finalpath,
                RunWithCmd = true,
                ModManageable = false
            };
        }

        static void MakeDummyExe(string outputPath, IProgress<double> progress, IProgress<string> status)
        {
            status.Report("Converting...");
            byte[] bytes = Convert.FromHexString(_hex);
            progress.Report(100);
            status.Report("Installing...");
            File.WriteAllBytes(outputPath, bytes);
        }
    }
}
