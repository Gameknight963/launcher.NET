using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Tasks;
using MLInstallerSDK;
using Microsoft.VisualBasic;

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
            if (ModloaderDropdown.SelectedItem == null)
            {
                MessageBox.Show("You must select something to be installed.",
                    "Invalid selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
                if (VersionDropdown.SelectedItem == null)
            {
                MessageBox.Show("You must select a version.",
                    "Invalid selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            string? path = Path.GetDirectoryName(game.Path);
            if (path == null)
            {
                MessageBox.Show("Game does not have a path!",
                    "Invalid operation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Installing...",
                "Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            var progress = new Progress<double>(percent =>
            {
                if (progressBar.Style != ProgressBarStyle.Continuous)
                    progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = (int)percent;
            });
            Installer.Log = msg => Console.WriteLine(msg);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            bool success = await Installer.DownloadVersionAsync(versions[VersionDropdown.SelectedIndex], Path.Combine(path, "installed.zip"), Installer.Architecture.x64, progress);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 100;
            if (success == true)
            {
                MessageBox.Show($"Installation complete", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show($"Installer failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
