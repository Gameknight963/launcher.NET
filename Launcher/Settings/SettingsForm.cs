using launcherdotnet.PluginAPI;
using MLInstallerSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace launcherdotnet
{
    public partial class SettingsForm : Form
    {
        private string _defaultSelectedHint = "";
        public SettingsForm()
        {
            InitializeComponent();
            ShowSettings();
            GamePluginView.MouseDown += GamesListView_MouseDown;
            GeneralCheckbox.MouseDown += GeneralCheckbox_MouseDown;
            AdvancedCheckbox.MouseDown += AdvancedCheckbox_MouseDown;
            //CustomTempDirPanel.MouseDown += CustomTempDirPanel_MouseDown;
            //CustomInstallDirectoryPanel.MouseDown += CustomInstallDirectoryPanel_MouseDown;
            MLCheckbox.MouseDown += MLCheckbox_MouseDown;
            HookMouseDown(this);
            _defaultSelectedHint = SelectedHint.Text;
        }

        private void HookMouseDown(Control parent)
        {
            parent.MouseDown += Form_MouseDown;
            foreach (Control child in parent.Controls)
                HookMouseDown(child);
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (SettingsTabControl.SelectedIndex != 3) return;
            Point pt = CustomTempDirPanel.PointToClient((sender as Control)!.PointToScreen(e.Location));
            if (CustomTempDirPanel.ClientRectangle.Contains(pt))
                SetSelectedHint("Specifies the directory use if the \"Use custom temporary directory\" option is on. " +
                "This directory is used to store .zip files before they are extracted. " +
                "Can be an absolute path or a relative path.",
                "temp/");

            pt = CustomInstallDirectoryPanel.PointToClient((sender as Control)!.PointToScreen(e.Location));
            if (CustomInstallDirectoryPanel.ClientRectangle.Contains(pt))
                SetSelectedHint("Specifies the directory use if the \"Use custom install directory\" option is on. " +
                    "This directory is used to store extracted game files. " +
                    "Can be an absolute path or a relative path.",
                    "games/");
        }

        private void ApplySettings()
        {
            LauncherSettings s = LauncherSettings.Settings;

            // --- General ---
            s.CheckForUpdates = GeneralCheckbox.GetItemChecked(0);
            s.ConfirmDelete = GeneralCheckbox.GetItemChecked(1);
            s.ConfirmOverwrite = GeneralCheckbox.GetItemChecked(2);
            s.RunOnStartup = GeneralCheckbox.GetItemChecked(3);

            // --- Melonloader ---
            s.MLShowCI = MLCheckbox.GetItemChecked(0);
            s.MLSelectStableByDefault = MLCheckbox.GetItemChecked(1);

            // --- Advanced ---
            s.OpenDebugConsole = AdvancedCheckbox.GetItemChecked(0);
            s.VerboseLogging = AdvancedCheckbox.GetItemChecked(1);
            s.DisablePluginVersionCheck = AdvancedCheckbox.GetItemChecked(2);
            s.UseCustomTempDirectory = AdvancedCheckbox.GetItemChecked(3);
            s.UseCustomInstallDirectory = AdvancedCheckbox.GetItemChecked(4);
            s.CustomTempDirectory = CustomTempDirTextbox.Text;
            s.CustomInstallDirectory = CustomInstallDirTextbox.Text;

            CustomTempDirPanel.Enabled = s.UseCustomTempDirectory;
            CustomInstallDirectoryPanel.Enabled = s.UseCustomInstallDirectory;

            string json = JsonConvert.SerializeObject(s, Formatting.Indented);
            LauncherLogger.WriteLine("New settings saved:");
            LauncherLogger.WriteLine(json);

            LauncherSettings.Save();
        }

        private void ShowSettings()
        {
            LauncherSettings s = LauncherSettings.Settings;

            // --- General ---
            GeneralCheckbox.SetItemChecked(0, s.CheckForUpdates);
            GeneralCheckbox.SetItemChecked(1, s.ConfirmDelete);
            GeneralCheckbox.SetItemChecked(2, s.ConfirmOverwrite);
            GeneralCheckbox.SetItemChecked(3, s.RunOnStartup);

            // --- Plugin List ---
            GamePluginView.Items.Clear();
            foreach (GameInstallPluginEntry entry in PluginApi.GameInstallPlugins)
            {
                ListViewItem item = new ListViewItem(entry.Installer.Name);
                item.Tag = (entry);
                GamePluginView.Items.Add(item);
                LauncherLogger.WriteLine(item.Name);
            }
            PluginsTabApiVersionLabel.Text = $"launcher.NET plugin API v{PluginAPI.LauncherApiInfo.ApiVersion}";

            // --- MelonLoader ---
            MLCheckbox.SetItemChecked(0, s.MLShowCI);
            MLCheckbox.SetItemChecked(1, s.MLSelectStableByDefault);

            // --- Advanced ---
            AdvancedCheckbox.SetItemChecked(0, s.OpenDebugConsole);
            AdvancedCheckbox.SetItemChecked(1, s.VerboseLogging);
            AdvancedCheckbox.SetItemChecked(2, s.DisablePluginVersionCheck);
            AdvancedCheckbox.SetItemChecked(3, s.UseCustomTempDirectory);
            AdvancedCheckbox.SetItemChecked(4, s.UseCustomInstallDirectory);

            // --- Custom Directories ---
            CustomTempDirTextbox.Text = s.CustomTempDirectory;
            CustomInstallDirTextbox.Text = s.CustomInstallDirectory;

            CustomTempDirPanel.Enabled = s.UseCustomTempDirectory;
            CustomInstallDirectoryPanel.Enabled = s.UseCustomInstallDirectory;

            // --- About ---
            LauncherVersionLabel.Text = $"v{Config.CurrentVersionString}";
            LauncherApiVersionLabel.Text = $"v{PluginAPI.LauncherApiInfo.ApiVersion.ToString()}";
        }


        private void SetSelectedHint(string? description, string? defaultSetting = null)
        {
            if (description == null)
            {
                SelectedHint.Text = _defaultSelectedHint;
                return;
            }
            SelectedHint.Text = $"{description}\n\nDefault: {defaultSetting ?? "Not specified"}";
        }

        private void SetPluginHint(string? description, string? apiVersion = null)
        {
            if (description == null)
            {
                SelectedHint.Text = _defaultSelectedHint;
                return;
            }
            SelectedHint.Text = $"{description}\n\nTarget API version: {apiVersion ?? "Not specified"}";
        }

        private void GeneralCheckbox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (GeneralCheckbox.SelectedIndex == -1) return;
            switch (GeneralCheckbox.SelectedIndex)
            {
                case 0:
                    SetSelectedHint("If enabled, launcher.net will notify you if an update is available.",
                        "Disabled");
                    break;
                case 1:
                    SetSelectedHint("If enabled, launcher.net will ask you for confirmation before it tries to delete an instance.",
                        "Enabled");
                    break;
                case 2:
                    SetSelectedHint("If enabled, launcher.net will ask you for confirmation when it tries to overwrite an instance.",
                        "Enabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will run when your computer turns on.",
                        "Disabled");
                    break;
            }
        }

        private const string _pluginHint = "Specifies which games launcher.net can download from. " +
            "launcher.net can currently only download from Github Releases.\n" +
            "launcher.net expects a .zip file containing the game folder. If this is not the case, the installation will fail.";

        private void GamesListView_MouseDown(object? sender, MouseEventArgs e)
        {
            SetSelectedHint(_pluginHint);
        }

        private void GamePluginView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GamePluginView.SelectedItems.Count == 0)
            {
                SetSelectedHint(_pluginHint);
                return;
            }
            GameInstallPluginEntry entry = (GameInstallPluginEntry)GamePluginView.SelectedItems[0].Tag!;
            SetPluginHint(entry.Installer.Description, entry.Installer.TargetApiVersion.ToString());
        }

        private void MLCheckbox_MouseDown(object? sender, MouseEventArgs e)
        {
            switch (MLCheckbox.SelectedIndex)
            {
                case 0:
                    SetSelectedHint("If enabled, bleeding edge builds will appear in the download list.\n" +
                        "These builds may be unstable, but contain the newest features and bugfixes.",
                        "Enabled");
                    break;
                case 1:
                    SetSelectedHint("If enabled, the latest stable version will be automatically selected, rather than the latest CI build.",
                        "Enabled");
                    break;
            }
        }

        private void AdvancedCheckbox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (AdvancedCheckbox.SelectedIndex == -1) return;
            switch (AdvancedCheckbox.SelectedIndex)
            {
                case 0:
                    SetSelectedHint("If enabled, a debug console will open when settings are applied.",
                        "Disabled");
                    break;
                case 1:
                    SetSelectedHint("If enabled, debug logging will be more verbose.",
                        "Disabled");
                    break;
                case 2:
                    SetSelectedHint("If enabled, launcher.net will download games to custom temporary directory.",
                        "Disabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will install games to a custom directory.",
                        "Disabled");
                    break;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplySettings();
                MessageBox.Show(
                    "Settings applied succesfully.",
                    "Notice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error applying settings: {ex}");
                MessageBox.Show(
                    $"Error applying settings: {ex.GetType().Name}. The console may include additional " +
                    $"information to help resolve this error.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
