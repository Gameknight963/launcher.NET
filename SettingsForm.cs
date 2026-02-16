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
        private string DefaultSelectedHint = "";
        public SettingsForm()
        {
            InitializeComponent();
            GamesListView.MouseDown += GamesListView_MouseDown;
            LoadersListView.MouseDown += LoadersListView_MouseDown;
            GeneralCheckbox.MouseDown += GeneralCheckbox_MouseDown;
            AdvancedCheckbox.MouseDown += AdvancedCheckbox_MouseDown;
            CustomTempDirPanel.MouseDown += CustomTempDirPanel_MouseDown;
            CustomInstallDirectoryPanel.MouseDown += CustomInstallDirectoryPanel_MouseDown;
            MLCheckbox.MouseDown += MLCheckbox_MouseDown;
            DefaultSelectedHint = SelectedHint.Text;
        }

        private void SetSelectedHint(string? description, string? defaultSetting = null)
        {
            if (description == null)
            {
                SelectedHint.Text = DefaultSelectedHint;
                return;
            }
            SelectedHint.Text = $"{description}\n\nDefault: {defaultSetting ?? "Not specified"}";
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

        private void GamesListView_MouseDown(object? sender, MouseEventArgs e)
        {
            SetSelectedHint("Specifies which games launcher.net can download from. " +
                "launcher.net can currently only download from Github Releases.\n" +
                "launcher.net expects a .zip file containing a folder, which further contains the game folder. If this is not the case, the installation will fail.");
        }

        private void LoadersListView_MouseDown(object? sender, MouseEventArgs e)
        {
            SetSelectedHint("Specifies which mods launcher.net can download from. " +
                "launcher.net can currently only download from Github Releases.\n" +
                "launcher.net expects a .zip that can be extracted directly to the game folder. " +
                "If this is not the case, the modloader will probably not work.");
        }

        private void MLCheckbox_MouseDown(object? sender, MouseEventArgs e)
        {
            switch (MLCheckbox.SelectedIndex)
            {
                case 0:
                    SetSelectedHint("If enabled bleeding edge builds will appear in the download list.\n" +
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
        private void CustomTempDirPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            SetSelectedHint("Specifies the directory use if the \"Use custom temporary directory\" option is on. " +
                "This directory is used to store .zip files before they are extracted. " +
                "Can be an absolute path or a relative path.",
                "/temp");
        }
        private void CustomInstallDirectoryPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            SetSelectedHint("Specifies the directory use if the \"Use custom install directory\" option is on. " +
                "This directory is used to store extracted game files. " +
                "Can be an absolute path or a relative path.",
                "/games");
        }
    }
}
