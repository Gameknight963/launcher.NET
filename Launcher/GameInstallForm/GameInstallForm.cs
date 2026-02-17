using launcherdotnet.PluginAPI;
using Microsoft.VisualBasic;
using MLInstallerSDK;
using Semver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace launcherdotnet
{
    internal partial class GameInstallForm : Form
    {
        List<MLVersion> versions = new List<MLVersion>();
        GameInfo game = new GameInfo();

        public GameInstallForm()
        {
            InitializeComponent();
            this.Icon = SystemIcons.Information;
            Initialize();
        }

        public async void Initialize()
        {
            foreach (GameInstallPluginEntry entry in PluginApi.GameInstallPlugins)
            {
                int index = GameDropdown.Items.Add(new GamesListItem { Text = entry.Plugin.Name, Tag = entry });
            }
        }

        private async void InstallGameButton_Click(object sender, EventArgs e)
        {
            if (GameDropdown.SelectedItem == null)
            {
                MessageBox.Show("Select a game.", "Invalid selection,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GamesListItem item = (GamesListItem)GameDropdown.SelectedItem;
            try
            {
                item.Tag!.OnInstallGameClicked?.Invoke(LauncherSettings.GamesDir);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error: Game intall menu: {ex}");
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
                    VersionDropdown.Items.Add(ver);
                }

                if (VersionDropdown.Items.Count > 0) VersionDropdown.SelectedIndex = 0;
            }
        }
    }
}

internal class GamesListItem
{
    public required string Text { get; set; }
    public GameInstallPluginEntry? Tag { get; set; }
    public override string ToString() => Text;
}

/* for reference:
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            if (string.IsNullOrWhiteSpace(result))
                return;
            if (result != result.Trim())
            {
                MessageBox.Show("Label must not contain trailing or leading whitespace.",
                        "Invalid name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            GameInfo newGame = new GameInfo { Label = result };
            await Install.DownloadAndInstallGameAsync(
                "",
                LauncherSettings.GamesDir,
                newGame,
                SetStatus);
            LauncherLogger.WriteLine($"Installing {result} to {LauncherSettings.GamesDir}");
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            GameService.UpsertGame(newGame);
*/