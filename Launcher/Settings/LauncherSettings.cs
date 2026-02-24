using Newtonsoft.Json;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Versioning;

namespace launcherdotnet
{
    internal class LauncherSettings
    {
        private static readonly string _baseDir = Config.BaseDir;
        private static readonly string _settingsPath = Path.Combine(_baseDir, "settings.json");
        private const string KeyName = "Launcher.NET";

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
            try
            {
                if (Settings.RunOnStartup)
                    StartupHelper.EnableRunOnStartup();
                else
                    StartupHelper.DisableRunOnStartup();
            }
            catch (Exception ex)
            {
                LauncherLogger.WriteLine($"Failed to set Run on startup to {Settings.RunOnStartup}: {ex.Message}", true);
                MessageBox.Show($"Failed to set Run on startup to {Settings.RunOnStartup}. Check the console for more details.", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            if (ConsoleHelper.ConsoleShown == Settings.OpenDebugConsole) return;
            if (Settings.OpenDebugConsole)
                ConsoleHelper.Show();
            else
                ConsoleHelper.Hide();
        }

        public static LauncherSettings Settings { get; private set; } = new();

        // ======================
        // Settings
        // ======================

        // ===== General =====

        public bool CheckForUpdates { get; set; } = false;
        public bool WarnOnFailedUpdate { get; set; } = false; 
        public bool ConfirmDelete { get; set; } = true;
        public bool ConfirmOverwrite { get; set; } = true;
        public bool RunOnStartup { get; set; } = false;

        // ===== Melonloader =====

        public bool MLShowCI { get; set; } = true;
        public bool MLSelectStableByDefault { get; set; } = true;

        // ===== Advanced =====

        public bool OpenDebugConsole { get; set; } = false;
        public bool VerboseLogging { get; set; } = false;
        public bool DisablePluginVersionCheck { get; set; } = false;
        public bool DisablePathChecks { get; set; } = false;
    }
}