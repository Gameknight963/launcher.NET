using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Syling;
using Microsoft.VisualBasic;
using MLInstallerSDK;
using Semver;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace launcherdotnet.Launcher.Forms
{
    internal partial class GameInstallForm : ThemeableForm
    {
        public GameInstallForm()
        {
            InitializeComponent();
            this.Icon = SystemIcons.Information;
            this.KeyPreview = true;
            this.KeyDown += GameInstallForm_KeyDown;
            Initialize();
        }

        public bool Success { get; private set; } = false;

        public void Initialize()
        {
            foreach (GameInstallPluginEntry entry in GameInstallerRegistry.GameInstallPlugins)
            {
                int index = GameDropdown.Items.Add(new GamesListItem { Text = entry.Installer.GameName, Tag = entry });
            }
            if (GameDropdown.Items.Count > 0) GameDropdown.SelectedIndex = 0;
            InstallGameButton.Select();
        }

        private void GameInstallForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void GameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
                return;

            GamesListItem selectedItem = (GamesListItem)GameDropdown.SelectedItem;
            GameInstallPluginEntry entry = selectedItem.Tag!;

            VersionDropdown.Items.Clear();

            if (entry.Releases != null)
            {
                foreach (ReleaseInfo r in entry.Releases)
                {
                    VersionDropdown.Items.Add(new VersionDropdownItem { Text = r.Version.ToString(), Release = r });
                }

                if (VersionDropdown.Items.Count > 0) VersionDropdown.SelectedIndex = 0;
            }
        }

        private async void InstallGameButton_Click(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
            {
                CoolMessageBox.Show("Select a game.", "Invalid selection,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string? result = QueryName();
            if (result == null) return;

            GameInfo newGame = new GameInfo { Label = result };

            GamesListItem item = (GamesListItem)GameDropdown.SelectedItem;
            string installDir = Path.Combine(Config.GamesDir, $"{newGame.Label}_{newGame.Id}");
            newGame.RelativeRootDirectory = Path.GetRelativePath(Config.BaseDir, installDir);
            Directory.CreateDirectory(installDir);

            Progress<double> progress = new Progress<double>(percent =>
            {
                progressBar.Value = Math.Min(100, Math.Max(0, (int)percent));
            });
            Progress<string> status = new Progress<string>(text =>
            {
                ActivityHint.Visible = true;
                ActivityHint.Text = text;
            });
            

            LauncherLogger.WriteLine($"Installing {item.Tag!.Installer.GameName} as {newGame.Label}");
            VersionDropdownItem selectedVersion = (VersionDropdownItem)VersionDropdown.SelectedItem!;

            ReleaseInfo release;
            PluginGameInfo installed;
            release = selectedVersion.Release;

            try
            {
                IGameInstaller installer = item.Tag!.Installer;
                installed = await Task.Run(() => installer.Install(installDir, release, progress, status));
                newGame.GameName = installer.GameName;
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error installing game:\n{ex}");
                CoolMessageBox.Show($"Installation failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            newGame.RelativePath = Path.GetRelativePath(Config.BaseDir, installed.ExePath);
            newGame.RunWithCmd = installed.RunWithCmd;

            if (string.IsNullOrWhiteSpace(installed.ExePath))
            {
                LauncherLogger.Error("Installation returned no executable. This can be caused by" +
                    "a bug in the intstaller plugin, or the plugin silently failing.");
                CoolMessageBox.Show("The plugin did not return an executable.",
                    "Installation failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ActivityHint.Text = "Installation failed.";
                return;
            }

            ActivityHint.Text = "Installation complete.";
            GameService.UpsertGame(newGame);
            CoolMessageBox.Show("Installation complete.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;
            this.Close();
        }

        private string? QueryName()
        {
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label",
                "New game");
            if (string.IsNullOrWhiteSpace(result))
                return null;
            if (result != result.Trim())
            {
                CoolMessageBox.Show("Label must not contain trailing or leading whitespace.",
                        "Invalid name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return null;
            }
            return result;
        }

        private class GamesListItem
        {
            public required string Text { get; set; }
            public GameInstallPluginEntry? Tag { get; set; }
            public override string ToString() => Text;
        }
        private class VersionDropdownItem
        {
            public required string Text { get; set; }
            public required ReleaseInfo Release { get; set; }
            public override string ToString() => Text;
        }
    }
}

