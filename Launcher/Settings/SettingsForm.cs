using launcherdotnet.Launcher;
using launcherdotnet.PluginAPI;
using MLInstallerSDK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace launcherdotnet
{
    public partial class SettingsForm : Form
    {
        public class TaggedListBoxItem
        {
            public required string Text { get; set; }
            public required object Tag { get; set; }
            public override string ToString() => Text;
        }

        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private string _defaultSelectedHint = "";
        public SettingsForm()
        {
            InitializeComponent();
            ShowSettings();
            GeneralCheckbox.MouseDown += GeneralCheckbox_MouseDown;
            AdvancedCheckbox.MouseDown += AdvancedCheckbox_MouseDown;
            GamePluginsBox.MouseDown += GamePluginsBox_MouseDown;
            //CustomTempDirPanel.MouseDown += CustomTempDirPanel_MouseDown;
            //CustomInstallDirectoryPanel.MouseDown += CustomInstallDirectoryPanel_MouseDown;
            MLCheckbox.MouseDown += MLCheckbox_MouseDown;
            _defaultSelectedHint = SelectedHint.Text;
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

            GamePluginsBox.Items.Clear();
            foreach (GameInstallPluginEntry entry in PluginApi.GameInstallPlugins)
            {
                TaggedListBoxItem item = new TaggedListBoxItem { Text = entry.Installer.Name, Tag = entry };
                GamePluginsBox.Items.Add(item);
            }

            PluginsTabApiVersionLabel.Text = $"launcher.NET plugin API v{PluginAPI.LauncherApiInfo.ApiVersion}";

            // --- MelonLoader ---
            MLCheckbox.SetItemChecked(0, s.MLShowCI);
            MLCheckbox.SetItemChecked(1, s.MLSelectStableByDefault);

            // --- Advanced ---
            AdvancedCheckbox.SetItemChecked(0, s.OpenDebugConsole);
            AdvancedCheckbox.SetItemChecked(1, s.VerboseLogging);
            AdvancedCheckbox.SetItemChecked(2, s.DisablePluginVersionCheck);

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

        private void GamePluginsBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (GamePluginsBox.SelectedItems.Count == 0) return;
            TaggedListBoxItem item = (TaggedListBoxItem)GamePluginsBox.SelectedItems[0]!;
            GameInstallPluginEntry entry = (GameInstallPluginEntry)item.Tag!;
            SetPluginHint(entry.Installer.Description, entry.Installer.TargetApiVersion.ToString());
        }

        private void GamePluginsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GamePluginsBox.SelectedItems.Count == 0) return;
            TaggedListBoxItem item = (TaggedListBoxItem)GamePluginsBox.SelectedItems[0]!;
            GameInstallPluginEntry entry = (GameInstallPluginEntry)item.Tag!;
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
                    SetSelectedHint("If enabled, launcher.net will load plugins even if the major " +
                        "versions of the target API don't match.",
                        "Disabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will download games to custom temporary directory.",
                        "Disabled");
                    break;
                case 4:
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenInBrowser(linkLabel1.Text);
        }

        private void GithubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenInBrowser(GithubLink.Text);
        }

        private void OpenInBrowser(string url)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Config.PluginsDir,
                    UseShellExecute = true
                };

                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open folder: {ex.Message}");
            }
        }
    }
}
