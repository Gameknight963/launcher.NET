using Semver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
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
            LauncherLogger.WriteColorLine($"Using version {Config.CurrentVersionString}. " +
                $"{(LauncherSettings.Settings.CheckForUpdates ? "Checking for updates is disabled." : "")}",
                false, ConsoleColor.White, ConsoleColor.Black);

            SemVersion? latest;
            try
            {
                latest = await Updater.GetLatestVersionAsync();
            }
            catch (HttpRequestException)
            {
                LauncherLogger.Warn($"An HttpRequestException occured while checking for updates. Ensure you have an Internet connection " +
                    $"and your DNS is properly configured.");
                if (!LauncherSettings.Settings.WarnOnFailedUpdate) return;
                MessageBox.Show("An error occured while checking for updates. Check your Interent connection.", 
                    "Update check error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                LauncherLogger.Warn($"A {ex.GetType().Name} occured while checking for updates. {ex.Message}");
                if (!LauncherSettings.Settings.WarnOnFailedUpdate) return;
                MessageBox.Show("An unprecedented error occured while checking for updates. Check the console for more details.",
                    "Update check error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (latest == null)
            {
                LauncherLogger.Warn("The launcher.NET Github Releases API sent a response that was either invalid or contained no releases.");
                MessageBox.Show("Github releases sent an invalid response.",
                    "Update check error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
