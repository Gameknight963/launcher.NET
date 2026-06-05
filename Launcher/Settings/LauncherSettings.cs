using launcherdotnet.Helpers;
using launcherdotnet.Styling;
using Newtonsoft.Json;
using launcherdotnet.Launcher.Forms;

namespace launcherdotnet.Launcher.Settings
{
    internal class LauncherSettings
    {
        private static readonly string _settingsPath = Path.Combine(LauncherConstants.BaseDir, "settings.json");

        public static string ToAbsolutePath(string path) => 
            Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

        public static void Load()
        {
            if (!File.Exists(_settingsPath))
            {
                Settings = new LauncherSettings();
                Save();
                return;
            }

            string json = File.ReadAllText(_settingsPath);
            Settings = JsonConvert.DeserializeObject<LauncherSettings>(json) ?? new LauncherSettings();

            if (Settings.OpenDebugConsole) ConsoleHelper.Show();
            ThemeManager.SetGlobalTheme(Theme.FromName(Settings.ActiveTheme), Settings.GradientColor.ToAbgr());
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
            File.WriteAllText(_settingsPath, json);
            ThemeManager.SetGlobalTheme(Theme.FromName(Settings.ActiveTheme), Settings.GradientColor.ToAbgr());
            if (Settings.RunOnStartup) StartupHelper.EnableRunOnStartup();
            else StartupHelper.DisableRunOnStartup();
            if (ConsoleHelper.ConsoleShown == Settings.OpenDebugConsole) return;
            if (Settings.OpenDebugConsole) ConsoleHelper.Show();
            else ConsoleHelper.Hide();
        }

        public static LauncherSettings Settings { get; private set; } = new();

        // ======================
        // Settings
        // ======================

        // ===== General =====

        public bool CheckForUpdates { get; set; } = false;
        public bool WarnOnFailedUpdate { get; set; } = true; 
        public bool ConfirmDelete { get; set; } = true;
        public bool ConfirmOverwrite { get; set; } = true;
        public bool RunOnStartup { get; set; } = false;

        // ===== Theme =====
        public string ActiveTheme { get; set; } = Theme.Light.Name;
        public DwmColor GradientColor { get; set; } = DwmColor.FromAbgr(0x66000000);

        // ===== Advanced =====

        public bool OpenDebugConsole { get; set; } = false;
        public bool VerboseLogging { get; set; } = false;
        public bool DisablePluginVersionCheck { get; set; } = false;
        public bool DisablePathChecks { get; set; } = false;
        public bool DisableIPv6 { get; set; } = false;
    }
}