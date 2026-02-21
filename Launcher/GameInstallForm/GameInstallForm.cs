using launcherdotnet.PluginAPI;
using Microsoft.VisualBasic;
using Semver;

namespace launcherdotnet
{
    internal partial class GameInstallForm : Form
    {
        public GameInstallForm()
        {
            InitializeComponent();
            this.Icon = SystemIcons.Information;
            Initialize();
        }

        public void Initialize()
        {
            foreach (GameInstallPluginEntry entry in PluginApi.GameInstallPlugins)
            {
                int index = GameDropdown.Items.Add(new GamesListItem { Text = entry.Installer.GameName, Tag = entry });
            }
            if (GameDropdown.Items.Count > 0) GameDropdown.SelectedIndex = 0;
        }

        private async void InstallGameButton_Click(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
            {
                MessageBox.Show("Select a game.", "Invalid selection,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string? result = QueryName();
            if (result == null) return;

            GameInfo newGame = new GameInfo { Label = result };

            GamesListItem item = (GamesListItem)GameDropdown.SelectedItem;
            string installDir = Path.Combine(LauncherSettings.GamesDir, $"{newGame.Label}_{newGame.Id}");
            newGame.RootDirectory = installDir;
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

            try
            {
                LauncherLogger.WriteLine($"Installing {item.Tag!.Installer.Name} as {newGame.Label}");
                string exePath;
                VersionDropdownItem selectedVersion = (VersionDropdownItem)VersionDropdown.SelectedItem!;
                SemVersion ver = selectedVersion.Version;
                try
                {
                    exePath = await Task.Run(() => item.Tag!.Installer.Install(installDir, ver, progress, status));
                }
                catch (Exception ex)
                {
                    LauncherLogger.Error($"Eror installing plugin: {ex}\nSTACK TRACK\n{ex.StackTrace}");
                    MessageBox.Show($"Installation failed: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(exePath))
                {
                    MessageBox.Show("Installation failed or returned no executable.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                newGame.Path = exePath;
                ActivityHint.Text = "Installation complete.";
                GameService.UpsertGame(newGame);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error installing game: {ex}");
            }
        }

        private void GameDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
                return;

            GamesListItem selectedItem = (GamesListItem)GameDropdown.SelectedItem;
            GameInstallPluginEntry entry = selectedItem.Tag!;

            VersionDropdown.Items.Clear();

            if (entry.Versions != null)
            {
                foreach (SemVersion ver in entry.Versions)
                {
                    VersionDropdown.Items.Add(new VersionDropdownItem { Text = ver.ToString(), Version = ver });
                }

                if (VersionDropdown.Items.Count > 0) VersionDropdown.SelectedIndex = 0;
            }
        }

        private string? QueryName()
        {
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            if (string.IsNullOrWhiteSpace(result))
                return null;
            if (result != result.Trim())
            {
                MessageBox.Show("Label must not contain trailing or leading whitespace.",
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
            public required SemVersion Version { get; set; }
            public override string ToString() => Text;
        }
    }
}

