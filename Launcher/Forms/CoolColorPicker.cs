using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms
{
    public partial class CoolColorPicker : ThemeableForm
    {
        public Color? ResultColor { get; private set; }
        public CoolColorPicker(Color? startColor = null)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = okButton;
            okButton.DialogResult = DialogResult.OK;
            CancelButton = cancelButton;
            if (startColor != null)
            {
                colorEditor.Color = startColor.Value;

                // this also invalidates it
                // invalidate() doesn't update it here for some reason
                Shown += (s, e) => previewPanel.BackColor = colorEditor.Color;
            }
            InheritGlobalTheme = false;
            if (IsThemeColoredLight())
                ApplyTheme(ThemeManager.Theme.Light);
            else
                ApplyTheme(ThemeManager.Theme.Dark);
        }

        private void colorEditor_ColorChanged(object sender, EventArgs e)
        {
            previewPanel.BackColor = colorEditor.Color;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ResultColor = colorEditor.Color;
        }
    }
}
