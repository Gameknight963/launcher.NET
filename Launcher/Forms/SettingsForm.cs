using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Syling;
using Newtonsoft.Json;
using System.Diagnostics;

namespace launcherdotnet.Launcher.Forms
{
    public partial class SettingsForm : ThemeableForm
    {
        private readonly string _defaultSelectedHint = "";
        private readonly RadioButton[] themeButtons;

        public SettingsForm()
        {
            InitializeComponent();

            themeButtons = [
                SystemThemeButton,
                LightThemeButton,
                DarkThemeButton,
                ExtendedFrameThemeButton,
                ExtendedFrameDarkThemeButton,
                BlurThemeButton,
                AcrylicThemeButton
            ];

            _defaultSelectedHint = Hint.Text;
            ShowSettings();

            GeneralCheckbox.MouseDown += ShowGeneralHint;
            GeneralCheckbox.SelectedIndexChanged += ShowGeneralHint;

            AdvancedCheckbox.MouseDown += ShowAdvancedHint;
            AdvancedCheckbox.SelectedIndexChanged += ShowAdvancedHint;

            GamePluginsBox.MouseDown += ShowGamePluginsBoxHint;
            GamePluginsBox.SelectedIndexChanged += ShowGamePluginsBoxHint;

            MLCheckbox.MouseDown += ShowMLCheckboxHint;
            MLCheckbox.SelectedIndexChanged += ShowMLCheckboxHint;

            this.KeyPreview = true;
            this.KeyDown += SettingsForm_KeyDown;
        }

        public class TaggedListBoxItem
        {
            public required string Text { get; set; }
            public required object Tag { get; set; }
            public override string ToString() => Text;
        }

        private void ApplySettings()
        {
            LauncherSettings s = LauncherSettings.Settings;

            // --- General ---
            s.CheckForUpdates = GeneralCheckbox.GetItemChecked(0);
            s.WarnOnFailedUpdate = GeneralCheckbox.GetItemChecked(1);
            s.ConfirmDelete = GeneralCheckbox.GetItemChecked(2);
            s.ConfirmOverwrite = GeneralCheckbox.GetItemChecked(3);
            s.RunOnStartup = GeneralCheckbox.GetItemChecked(4);

            // --- Melonloader ---
            s.MLShowCI = MLCheckbox.GetItemChecked(0);
            s.MLSelectStableByDefault = MLCheckbox.GetItemChecked(1);

            // --- Theme ---
            s.Theme = (ThemeManager.Theme)Array.FindIndex(themeButtons, b => b.Checked);
            s.TextRenderMode = (ThemeManager.TextRenderMode)TextModeComboBox.SelectedIndex;

            // --- Advanced ---
            s.OpenDebugConsole = AdvancedCheckbox.GetItemChecked(0);
            s.VerboseLogging = AdvancedCheckbox.GetItemChecked(1);
            s.DisablePluginVersionCheck = AdvancedCheckbox.GetItemChecked(2);

            string json = JsonConvert.SerializeObject(s, Formatting.Indented);
            LauncherLogger.WriteLine("New settings saved:");
            LauncherLogger.WriteLine(json);

            textModeResolvesTo2.Text = $"With the active theme {ThemeManager.ActiveTheme}, this text mode resolves to:";
            textModeResolvesTo.Text = $"{(ThemeManager.ResolveTextRenderMode((ThemeManager.TextRenderMode)TextModeComboBox.SelectedIndex) ?
                "TextRenderer" : "Shadow Text")}";

            LauncherSettings.Save();
        }

        private void ShowSettings()
        {
            LauncherSettings s = LauncherSettings.Settings;

            // --- General ---
            GeneralCheckbox.SetItemChecked(0, s.CheckForUpdates);
            GeneralCheckbox.SetItemChecked(1, s.WarnOnFailedUpdate);
            GeneralCheckbox.SetItemChecked(2, s.ConfirmDelete);
            GeneralCheckbox.SetItemChecked(3, s.ConfirmOverwrite);
            GeneralCheckbox.SetItemChecked(4, s.RunOnStartup);

            // --- Plugin List ---

            GamePluginsBox.Items.Clear();
            foreach (GameInstallPluginEntry entry in GameInstallerRegistry.GameInstallPlugins)
            {
                TaggedListBoxItem item = new TaggedListBoxItem { Text = entry.Installer.Name, Tag = entry };
                GamePluginsBox.Items.Add(item);
            }

            PluginsTabApiVersionLabel.Text = $"launcher.NET plugin API v{LauncherApiInfo.ApiVersion}";

            // --- MelonLoader ---
            MLCheckbox.SetItemChecked(0, s.MLShowCI);
            MLCheckbox.SetItemChecked(1, s.MLSelectStableByDefault);

            // --- Advanced ---
            AdvancedCheckbox.SetItemChecked(0, s.OpenDebugConsole);
            AdvancedCheckbox.SetItemChecked(1, s.VerboseLogging);
            AdvancedCheckbox.SetItemChecked(2, s.DisablePluginVersionCheck);

            // --- Theme ---
            themeButtons[(int)s.Theme].Checked = true;
            TextModeComboBox.SelectedIndex = (int)s.TextRenderMode;

            // --- About ---
            LauncherVersionLabel.Text = $"v{Config.CurrentVersionString}";
            LauncherApiVersionLabel.Text = $"v{PluginAPI.LauncherApiInfo.ApiVersion.ToString()}";

            SetSelectedHint(null);
        }


        private void SetSelectedHint(string? description, string? defaultSetting = null)
        {
            if (description == null)
            {
                Hint.Text = _defaultSelectedHint;
                return;
            }
            Hint.Text = $"{description}\n\nDefault: {defaultSetting ?? "Not specified"}";
        }

        private void SetPluginHint(string? description, string? apiVersion = null)
        {
            if (description == null)
            {
                Hint.Text = _defaultSelectedHint;
                return;
            }
            Hint.Text = $"{description}\n\nTarget API version: {apiVersion ?? "Not specified"}";
        }

        private void SettingsForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }
        private void ShowGeneralHint(object? sender, EventArgs e)
        {
            if (GeneralCheckbox.SelectedIndex == -1) return;
            switch (GeneralCheckbox.SelectedIndex)
            {
                case 0:
                    SetSelectedHint("If enabled, launcher.NET will notify you if an update is available.",
                        "Disabled");
                    break;
                case 1:
                    SetSelectedHint("If enabled, launcher.NET will not display a dialog if an update check has failed. " +
                        "There will still be a warning in the console, if the console is enabled.", "Enabled");
                    break;
                case 2:
                    SetSelectedHint("If enabled, launcher.NET will ask you for confirmation before it tries to delete an instance.",
                        "Enabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.NET will ask you for confirmation when it tries to overwrite an instance.",
                        "Enabled");
                    break;
                case 4:
                    SetSelectedHint("If enabled, launcher.NET will run when your computer turns on.",
                        "Disabled");
                    break;
            }
        }

        private void ShowGamePluginsBoxHint(object? sender, EventArgs e)
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

        private void ShowMLCheckboxHint(object? sender, EventArgs e)
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

        private void ShowAdvancedHint(object? sender, EventArgs e)
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
                    SetSelectedHint("If enabled, launcher.NET will load plugins even if the major " +
                        "versions of the target API don't match.",
                        "Disabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will not prevent plugins from installing outside of " +
                        "launcher.NET's's root folder.",
                        "Disabled");
                    break;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                ApplySettings();
                CoolMessageBox.Show(
                    "Settings applied succesfully.",
                    "Notice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error applying settings: {ex}");
                CoolMessageBox.Show(
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
                CoolMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CoolMessageBox.Show($"Failed to open folder: {ex.Message}");
            }
        }

        private void SystemThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = "Use the theme Windows is set to.";

        private void LightThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Use light theme.");

        private void DarkThemeButton_CheckedChanged(object sender, EventArgs e) =>
            Hint.Text = ("Use dark theme. Some controls, such as comboboxes, are drawn at the UxTheme layer, so they cannot be themed");

        private void BlurThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Use a blurred background.");

        private void AcrylicThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Use acrylic background.");

        private void ExtendedFrameThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Extends the titlebar into the app. If you use something like" +
            "DWMBlurGlass that blurs titlebars, launcher.NET will become transparent.");

        private void ExtendedFrameDarkThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Extends the titlebar into the app, " +
            "but uses dark mode titlebar");

        private void TextModeComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (TextModeComboBox.SelectedIndex)
            {
                case 0:
                    Hint.Text = "Normal TextRenderer rendering except on Blur and Acrylic themes.";
                    break;
                case 1:
                    Hint.Text = "Normal TextRenderer rendering only on Light, Dark, and System themes. This prevents " +
                        "aliasing issues if you use DWMBlurGlass or other software that makes titlebars transparent.";
                    break;
                case 2:
                    Hint.Text = "Always use TextRenderer rendering. This is the way Winforms normally looks.";
                    break;
                case 3:
                    Hint.Text = "Always use shadow text custom rendering.";
                    break;
            }
            textModeResolvesTo2.Text = $"With the active theme {ThemeManager.ActiveTheme}, this text mode resolves to:";
            textModeResolvesTo.Text = $"{(ThemeManager.ResolveTextRenderMode((ThemeManager.TextRenderMode)TextModeComboBox.SelectedIndex) ?
                "TextRenderer" : "Shadow Text")}";
        }
    }
}
