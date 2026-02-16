using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public class LauncherSettings
    {

        public bool CheckForUpdates { get; set; } = false;
        public bool ConfirmBeforeDeletingInstance { get; set; } = true;
        public bool ConfirmBeforeOverwritingInstance { get; set; } = true;
        public bool RunOnStartup { get; set; } = false;

        // ===== Modloader =====

        public bool ShowBleedingEdgeBuilds { get; set; } = true;
        public bool PreferLatestStableOverCI { get; set; } = true;

        // ===== Advanced =====

        public bool OpenConsoleWhenApplyingSettings { get; set; } = false;
        public bool EnableVerboseLogging { get; set; } = false;
        public bool UseCustomTempDirectory { get; set; } = false;
        public bool UseCustomInstallDirectory { get; set; } = false;
        public string CustomTempDirectory { get; set; } = "temp";
        public string CustomInstallDirectory { get; set; } = "games";

        // ===== Providers (from ListViews) =====

        public List<string> EnabledGameProviders { get; set; } = new();
        public List<string> EnabledModProviders { get; set; } = new();
    }
}
