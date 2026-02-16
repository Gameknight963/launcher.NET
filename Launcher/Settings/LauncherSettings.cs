using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public class LauncherSettings
    {

        private static readonly string _baseDir = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string _settingsPath = Path.Combine(_baseDir, "settings.json");
        public static LauncherSettings Settings { get; private set; } = new();

        public static void Load()
        {
            if (!File.Exists(_settingsPath))
            {
                Settings = new LauncherSettings();
                Save();
                return;
            }
            try
            {
                string json = File.ReadAllText(_settingsPath);
                Settings = JsonConvert.DeserializeObject<LauncherSettings>(json) ?? new LauncherSettings();
            }
            catch
            {
                Settings = new LauncherSettings();
                Save();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
            File.WriteAllText(_settingsPath, json);
        }

        // ======================
        // Settings
        // ======================

        // ===== General =====

        public bool CheckForUpdates { get; set; } = false;
        public bool ConfirmBeforeDeletingInstance { get; set; } = true;
        public bool ConfirmBeforeOverwritingInstance { get; set; } = true;
        public bool RunOnStartup { get; set; } = false;

        // ===== Melonloader =====

        public bool ShowBleedingEdgeBuilds { get; set; } = true;
        public bool PreferLatestStableOverCI { get; set; } = true;

        // ===== Advanced =====

        public bool OpenConsoleWhenApplyingSettings { get; set; } = false;
        public bool EnableVerboseLogging { get; set; } = false;
        public bool UseCustomTempDirectory { get; set; } = false;
        public bool UseCustomInstallDirectory { get; set; } = false;
        public string CustomTempDirectory { get; set; } = "temp";
        public string CustomInstallDirectory { get; set; } = "games";

        // ===== Providers =====

        public List<string> EnabledGameProviders { get; set; } = new();
        public List<string> EnabledModloaderProviders { get; set; } = new();
    }
}
