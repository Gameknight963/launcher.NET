using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Styling;
using Newtonsoft.Json;
using System.Diagnostics;

namespace launcherdotnet.Launcher.Forms
{
    public partial class SettingsForm : ThemeableForm
    {
        private readonly string _defaultSelectedHint = "";
        private readonly Dictionary<string, RadioButton> _themeButtons;

        public SettingsForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            Icon = LauncherConstants.AppIcon;


            _themeButtons = new Dictionary<string, RadioButton>
                {
                    { Theme.System.Name, systemThemeButton },
                    { Theme.Light.Name, lightThemeButton },
                    { Theme.Dark.Name, darkThemeButton },
                    { Theme.ExtendFrame.Name, extendedFrameThemeButton },
                    { Theme.ExtendFrameDark.Name, extendedFrameDarkThemeButton },
                    { Theme.Blur.Name, blurThemeButton },
                    { Theme.Acrylic.Name, acrylicThemeButton },
                    { Theme.TransparentGradient.Name, transparentGradientButton }
                };

            _defaultSelectedHint = Hint.Text;
            ShowSettings();
            openPluginSettingsBtn.Visible = false;

            GeneralCheckbox.MouseDown += ShowGeneralHint;
            GeneralCheckbox.SelectedIndexChanged += ShowGeneralHint;

            AdvancedCheckbox.MouseDown += ShowAdvancedHint;
            AdvancedCheckbox.SelectedIndexChanged += ShowAdvancedHint;

            GamePluginsBox.MouseDown += GamePluginsBox_MouseDown; ;

            this.KeyPreview = true;
            this.KeyDown += SettingsForm_KeyDown;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public class PluginsListboxItem
        {
            public required PluginRegistry.PluginDescriptor Plugin;
            public override string ToString() => Plugin.Name;
        }

        private bool ApplySettings()
        {
            LauncherSettings s = LauncherSettings.Settings;

            // --- General ---
            s.CheckForUpdates = GeneralCheckbox.GetItemChecked(0);
            s.WarnOnFailedUpdate = GeneralCheckbox.GetItemChecked(1);
            s.ConfirmDelete = GeneralCheckbox.GetItemChecked(2);
            s.ConfirmOverwrite = GeneralCheckbox.GetItemChecked(3);
            s.RunOnStartup = GeneralCheckbox.GetItemChecked(4);

            // --- Theme ---
            s.ActiveTheme = _themeButtons.First(kvp => kvp.Value.Checked).Key;
            if (!DwmColor.TryParse(gradientColorBox.Text, out DwmColor? color))
            {
                CoolMessageBox.Show($"Ivalid integer: {gradientColorBox.Text}", "Invalid input");
                return false;
            }

            s.GradientColor = color;

            // --- Advanced ---
            s.OpenDebugConsole = AdvancedCheckbox.GetItemChecked(0);
            s.VerboseLogging = AdvancedCheckbox.GetItemChecked(1);
            s.DisablePluginVersionCheck = AdvancedCheckbox.GetItemChecked(2);
            s.DisableIPv6 = AdvancedCheckbox.GetItemChecked(3);

            string json = JsonConvert.SerializeObject(s, Formatting.Indented);
            LauncherLogger.WriteLine("New settings saved:");
            LauncherLogger.WriteLine(json);

            LauncherSettings.Save();
            return true;
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
            foreach (PluginRegistry.PluginDescriptor plugin in PluginRegistry.PluginDescriptors)
            {
                PluginsListboxItem item = new() { Plugin = plugin };
                GamePluginsBox.Items.Add(item);
            }

            PluginsTabApiVersionLabel.Text = $"launcher.net plugin API v{LauncherApiInfo.ApiVersion}";

            // --- Advanced ---
            AdvancedCheckbox.SetItemChecked(0, s.OpenDebugConsole);
            AdvancedCheckbox.SetItemChecked(1, s.VerboseLogging);
            AdvancedCheckbox.SetItemChecked(2, s.DisablePluginVersionCheck);
            AdvancedCheckbox.SetItemChecked(3, s.DisableIPv6);

            // --- Theme ---
            _themeButtons[s.ActiveTheme].Checked = true;
            gradientColorBox.Text = s.GradientColor.ToString();

            // --- About ---
            LauncherVersionLabel.Text = $"v{LauncherConstants.CurrentVersionString}";
            LauncherApiVersionLabel.Text = $"v{LauncherApiInfo.ApiVersion}";

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
                    SetSelectedHint("If enabled, launcher.net will notify you if an update is available.",
                        "Disabled");
                    break;
                case 1:
                    SetSelectedHint("If enabled, launcher.net will not display a dialog if an update check has failed. " +
                        "There will still be a warning in the console, if the console is enabled.", "Enabled");
                    break;
                case 2:
                    SetSelectedHint("If enabled, launcher.net will ask you for confirmation before it tries to delete an instance.",
                        "Enabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will ask you for confirmation when it tries to overwrite an instance.",
                        "Enabled");
                    break;
                case 4:
                    SetSelectedHint("If enabled, launcher.net will run when your computer turns on.",
                        "Disabled");
                    break;
            }
        }

        private void ShowPluginsBoxHint()
        {
            openPluginSettingsBtn.Visible = false;
            if (GamePluginsBox.SelectedItems.Count == 0) return;
            PluginsListboxItem item = (PluginsListboxItem)GamePluginsBox.SelectedItems[0]!;
            openPluginSettingsBtn.Visible = item.Plugin.Instance is IPluginWithSettings;
            SetPluginHint(item.Plugin.Description, item.Plugin.TargetApiVersion.ToString());
        }

        private void GamePluginsBox_MouseDown(object? sender, MouseEventArgs e) => ShowPluginsBoxHint();
        private void GamePluginsBox_SelectedIndexChanged(object sender, EventArgs e) => ShowPluginsBoxHint();

        private void OpenPluginSettingsBtn_Click(object sender, EventArgs e)
        {
            // due to ShowPluginsBoxHint(), if this button is visible, the selected item's plugin
            // is an IPluginWithSettings
            // obviously it's not clickable if it's invisible so this won't throw
            PluginsListboxItem item = (PluginsListboxItem)GamePluginsBox.SelectedItems[0]!;
            IPluginWithSettings pluginWithSettings = (IPluginWithSettings)item.Plugin.Instance;
            using Form form = pluginWithSettings.CreateSettingsForm();
            form.ShowDialog();
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
                    SetSelectedHint("If enabled, launcher.net will load plugins even if the major " +
                        "versions of the target API don't match.",
                        "Disabled");
                    break;
                case 3:
                    SetSelectedHint("If enabled, launcher.net will not allow IPv6 connections.",
                        "Disabled");
                    break;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ApplySettings()) return;
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
            catch (Exception ex)
            {
                CoolMessageBox.Show(ex.Message, "Error opening browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = LauncherConstants.PluginsDir,
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
            "DWMBlurGlass that blurs titlebars, launcher.net will become transparent.");

        private void ExtendedFrameDarkThemeButton_CheckedChanged(object sender, EventArgs e) => Hint.Text = ("Extends the titlebar into the app, " +
            "but uses dark mode titlebar");

        private void TransparentGradientButton_CheckedChanged(object sender, EventArgs e) => Hint.Text =
            ("Clear background with no blur.");

        private void colorButton_Click(object sender, EventArgs e)
        {
            Color? result = DwmColor.TryParse(gradientColorBox.Text, out DwmColor? value) ? value : null;
            using CoolColorPicker dialog = new(result);
            if (dialog.ShowDialog() == DialogResult.OK)
                gradientColorBox.Text = DwmColor.ToDwmString(dialog.ResultColor!.Value);
        }

        private void gcCollectBtn_Click(object sender, EventArgs e)
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
