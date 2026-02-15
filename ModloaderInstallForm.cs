using Microsoft.VisualBasic;
using MLInstallerSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace launcherdotnet
{
    public partial class ModloaderInstallForm : Form
    {
        List<MLVersion> versions = new List<MLVersion>();
        GameInfo game = new GameInfo();

        public ModloaderInstallForm(GameInfo userChosenGame)
        {
            InitializeComponent();
            game = userChosenGame;
            this.Icon = SystemIcons.Information;
            ModloaderDropdown.SelectedIndex = 0;
            Initialize();
        }

        public async void Initialize()
        {
            InstallModloaderButton.Enabled = false;
            versions = await MLInstallerSDK.MLManager.FetchAvailableVersionsAsync();
            foreach (MLVersion v in versions)
            {
                VersionDropdown.Items.Add(v.Version);
            }
            if (VersionDropdown.Items.Count > 0)
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

            string? gameDir = Path.GetDirectoryName(game.Path);
            if (string.IsNullOrEmpty(gameDir))
            {
                MessageBox.Show("Game does not have a valid path!", "Invalid operation",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InstallModloaderButton.Enabled = false;
            ModloaderDropdown.Enabled = false;
            VersionDropdown.Enabled = false;

            try
            {
                MLVersion chosen = versions[VersionDropdown.SelectedIndex];
                await InstallSelectedModloaderAsync(chosen, gameDir, progressBar);
            }
            finally
            {
                // restore UI
                InstallModloaderButton.Enabled = true;
                ModloaderDropdown.Enabled = true;
                VersionDropdown.Enabled = true;
            }
        }
        private async Task InstallSelectedModloaderAsync(MLVersion version, string installDir, System.Windows.Forms.ProgressBar progressBar)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;

            bool switchedToPercent = false;
            var progress = new Progress<double>(percent =>
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

            Installer.Log = msg => Console.WriteLine(msg);
            MLManager.Log = msg => Console.WriteLine(msg);
            string tempFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
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