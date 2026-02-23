using Microsoft.VisualBasic;
using MLInstallerSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace launcherdotnet
{
    internal partial class ModloaderInstallForm : Form
    {
        List<MLVersion> versions = new List<MLVersion>();
        GameInfo game = new GameInfo();

        public ModloaderInstallForm(GameInfo userChosenGame)
        {
            InitializeComponent();
            game = userChosenGame;
            this.Icon = SystemIcons.Information;
            ModloaderDropdown.Items.Add("Melonloader");
            ModloaderDropdown.SelectedIndex = 0;
            this.FormClosing += ModloaderInstallForm_FormClosing;
            Initialize();
        }

        public async void Initialize()
        {
            InstallModloaderButton.Enabled = false;
            versions = await MLInstallerSDK.MLManager.FetchAvailableVersionsAsync();
            List<MLVersion> stableVersions = versions
                .Where(v => string.IsNullOrEmpty(v.Version.Prerelease))
                .ToList();
            if (LauncherSettings.Settings.MLShowCI)
            {
                foreach (MLVersion v in versions)
                    VersionDropdown.Items.Add(v.Version);
            }
            else
            {
                foreach (MLVersion v in stableVersions)
                    VersionDropdown.Items.Add(v.Version);
            }

            if (VersionDropdown.Items.Count == 0)
            {
                LauncherLogger.Error("Error: No Melonloader versions were sent by the Github API.");
                MessageBox.Show("No Melonloader versions were sent by the Github API.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LauncherSettings.Settings.MLSelectStableByDefault || LauncherSettings.Settings.MLShowCI)
                VersionDropdown.SelectedIndex = versions.Count - stableVersions.Count;
            else
                VersionDropdown.SelectedIndex = 0;

            InstallModloaderButton.Enabled = true;
        }

        private async void InstallModloader_Click(object sender, EventArgs e)
        {
            // validate selection
            if (ModloaderDropdown.SelectedItem == null || VersionDropdown.SelectedItem == null)
            {
                MessageBox.Show("Select a modloader and version.", "Invalid selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string? gameDir = Path.GetDirectoryName(game.AbsolutePath);
            try
            {
                game.EnsurePathsValid();
                if (gameDir == null) throw new NullReferenceException("Game path is null.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Game does not have a valid path! {ex.Message}", "Invalid operation",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InstallModloaderButton.Enabled = false;
            ModloaderDropdown.Enabled = false;
            VersionDropdown.Enabled = false;
            _installInProgress = true;
            try
            {
                MLVersion chosen = versions[VersionDropdown.SelectedIndex];
                await InstallSelectedModloaderAsync(chosen, gameDir, progressBar);
            }
            finally
            {
                _installInProgress = false;
                InstallModloaderButton.Enabled = true;
                ModloaderDropdown.Enabled = true;
                VersionDropdown.Enabled = true;
            }
        }

        private void ModloaderInstallForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_installInProgress)
            {
                MessageBox.Show("Please wait for the installation to finish.", "Installation in progress",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private bool _installInProgress = false;

        private async Task InstallSelectedModloaderAsync(MLVersion version, string installDir, System.Windows.Forms.ProgressBar progressBar)
        {

            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            bool switchedToPercent = false;
            Progress<double> progress = new Progress<double>(percent =>
            {
                if (!switchedToPercent)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = 0;
                    switchedToPercent = true;
                    ActivityHint.Text = "Downloading...";
                }

                int value = Math.Min(100, Math.Max(0, (int)percent));
                progressBar.Value = value;
                ActivityHint.Text = $"Downloading... {percent:0.0}%";
            });

            Installer.Log = msg => LauncherLogger.WriteLine(msg);
            MLManager.Log = msg => LauncherLogger.WriteLine(msg);
            string tempFolder = Config.TempDir;
            Directory.CreateDirectory(tempFolder);
            string tempZip = Path.Combine(Path.Combine(tempFolder, $"melon_{Guid.NewGuid():N}.zip"));

            try
            {
                ActivityHint.Visible = true;
                ActivityHint.Text = "Preparing download... This may take a while.";
                progressBar.Style = ProgressBarStyle.Marquee;

                bool downloadOk = await Installer.DownloadVersionAsync(
                    version,
                    tempZip,
                    Installer.Architecture.x64,
                    progress);

                if (!downloadOk)
                {
                    MessageBox.Show("Download failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                ActivityHint.Text = "Extracting...";
                Directory.CreateDirectory(installDir);
                ZipFile.ExtractToDirectory(tempZip, installDir, true);

                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 100;
                ActivityHint.Text = "Installation complete.";
                MessageBox.Show("Installation complete.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                MessageBox.Show($"Installation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try { if (File.Exists(tempZip)) File.Delete(tempZip); } catch { /* ignore */ }
                try { if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder); } catch { /* ignore again */ }
            }
        }
    }
}