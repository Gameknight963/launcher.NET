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
            Icon = Config.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = okButton;
            okButton.DialogResult = DialogResult.OK;
            CancelButton = cancelButton;
            if (startColor != null)
            {
                colorEditor.Color = startColor.Value;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ResultColor = colorEditor.Color;
        }
    }
}
