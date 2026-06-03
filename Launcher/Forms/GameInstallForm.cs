using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Styling;
using Semver;

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
            IGameInstaller installer = selectedItem.Tag!;

            VersionDropdown.Items.Clear();
            IEnumerable<string>? releases = installer.GetReleases();
            bool versionless = releases == null;
            whichVersionYouWantLabel.Visible = !versionless;
            VersionDropdown.Visible = !versionless;
            InstallGameButton.Text = installer.PromptForLabel == LabelQueryTime.BeforeInstall ? "Install" : "Continue";
            Size = new(Size.Width, versionless ? 200 : 252);
            if (releases != null)
            {
                foreach (string r in releases)
                    VersionDropdown.Items.Add(r);
            }
            if (VersionDropdown.Items.Count > 0) VersionDropdown.SelectedIndex = 0;
        }

        private async void InstallGameButton_Click(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
            {
                CoolMessageBox.Show("Select a game.", "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GamesListItem item = (GamesListItem)GameDropdown.SelectedItem;
            IGameInstaller installer = item.Tag!;

            string? label = null;
            if (installer.PromptForLabel == LabelQueryTime.BeforeInstall)
            {
                label = QueryLabel(installer.GameName);
                if (label == null) return;
            }

            string? version = null;
            if (VersionDropdown.Visible && VersionDropdown.SelectedItem != null)
                version = (string)VersionDropdown.SelectedItem;

            Progress<double> progress = new(percent =>
                progressBar.Value = Math.Min(100, Math.Max(0, (int)percent)));
            Progress<string> status = new(text =>
            {
                ActivityHint.Visible = true;
                ActivityHint.Text = text;
            });

            GameInfo? newGame;
            try
            {
                newGame = await GameInstallService.InstallAsync(installer, version, progress, status, label);
                if (newGame == null) return;
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error installing {installer}:\n{ex}");
                CoolMessageBox.Show(
                    $"Installation failed since a {ex.GetType().Name} occurred: {ex.Message}. See console for full exception.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (installer.PromptForLabel == LabelQueryTime.AfterInstall)
            {
                label = LauncherDialogs.QueryLabel(installer.GameName);
                if (label == null) return;
                newGame.Label = label;
                GameService.UpsertGame(newGame); // re-save with updated label
            }

            ActivityHint.Text = "Installation complete.";
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
    }
}

