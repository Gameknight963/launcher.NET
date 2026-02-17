using Semver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json.Nodes;

namespace launcherdotnet.Launcher
{
    internal class Updater
    {
        public static async Task<SemVersion?> GetLatestVersionAsync()
        {
            using HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Add("User-Agent", "launcher.net");
            string response = await http.GetStringAsync(Config.ReleasesAPIUrl);

            JsonNode? json = JsonNode.Parse(response);
            if (json is null || json.AsArray().Count == 0)
                return null;

            JsonNode latest = json[0]!;
            if (latest?["tag_name"] is null)
                return null;

            if (SemVersion.TryParse(latest["tag_name"]!.ToString(), SemVersionStyles.Any, out SemVersion? version))
                return version;

            return null;
        }

        public static async void CheckForUpdates()
        {
            LauncherLogger.Write($"Using version {Config.CurrentVersionString}. ");
            if (LauncherSettings.Settings.CheckForUpdates == false)
            {
                LauncherLogger.WriteLine("Checking for updates is disabled.");
                return;
            }
            SemVersion? latest = await Updater.GetLatestVersionAsync();
            if (latest == null) return;
            if (Config.CurrentVersion != latest)
            {
                LauncherLogger.WriteLine($" Update available: {latest}");
                DialogResult result = MessageBox.Show($"New version is available: {latest}. You are currently on version {Config.CurrentVersionString}. Update?",
                    "Update available",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = Config.RelesesPage,
                    UseShellExecute = true
                });
                return;
            }
            LauncherLogger.WriteLine($" No updates availabe.");
        }

    }
}
