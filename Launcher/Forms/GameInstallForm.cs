using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Styling;

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
            this.StartPosition = FormStartPosition.CenterParent;
            Initialize();
        }

        public bool Success { get; private set; } = false;

        public void Initialize()
        {
            foreach (IGameInstaller installer in PluginRegistry.GameInstallPlugins)
            {
                GameDropdown.Items.Add(new GamesListItem { Text = installer.GameName, Tag = installer });
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
            IGameInstaller entry = selectedItem.Tag!;

            VersionDropdown.Items.Clear();

            foreach (ReleaseInfo r in entry.GetReleases())
            {
                VersionDropdown.Items.Add(new VersionDropdownItem { Text = r.Version.ToString(), Release = r });
            }

            if (VersionDropdown.Items.Count > 0) VersionDropdown.SelectedIndex = 0;
        }

        private async void InstallGameButton_Click(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
            {
                CoolMessageBox.Show("Select a game.", "Invalid selection,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? result = CoolInputBox.Prompt(
                "Enter a label for this instance:",
                "Set Game Label",
                "New game");
            if (result == null) return;
            if (result != result.Trim())
            {
                CoolMessageBox.Show("Label must not contain trailing or leading whitespace.",
                        "Invalid name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }

            GameInfo newGame = new GameInfo { Label = result };

            GamesListItem item = (GamesListItem)GameDropdown.SelectedItem;
            string installDir = Path.Combine(LauncherConstants.GamesDir, $"{newGame.Label}_{newGame.Id}");
            newGame.RelativeRootDirectory = Path.GetRelativePath(LauncherConstants.BaseDir, installDir);
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

            IGameInstaller installer = item.Tag!;

            LauncherLogger.WriteLine($"Installing {installer.GameName} as {newGame.Label}");
            VersionDropdownItem selectedVersion = (VersionDropdownItem)VersionDropdown.SelectedItem!;

            ReleaseInfo release = selectedVersion.Release;
            PluginGameInfo installed;

            try
            {
                installed = await Task.Run(() => installer.Install(installDir, release, progress, status));
                newGame.GameName = installer.GameName;
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error installing {installer}:\n{ex}");
                CoolMessageBox.Show($"Installation failed since a {ex.GetType().Name} occured: {ex.Message}. See console for full exception.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            newGame.RelativePath = Path.GetRelativePath(LauncherConstants.BaseDir, installed.ExePath);
            newGame.RunWithCmd = installed.RunWithCmd;

            ActivityHint.Text = "Installation complete.";
            GameService.UpsertGame(newGame);
            GameModState state = new();
            state.TakeBaseline(installDir);
            state.Save(installDir);
            CoolMessageBox.Show("Installation complete.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Success = true;
            Close();
        }

        private class GamesListItem
        {
            public required string Text { get; set; }
            public required IGameInstaller Tag { get; set; }
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

