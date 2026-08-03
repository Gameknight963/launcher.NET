namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class FillMissingModInfo : Form
    {
        public (string name, string owner, string version)? EditedInfo { get; private set; }
        public FillMissingModInfo(string? labelText = null, (string name, string owner, string version)? modInfo = null)
        {
            InitializeComponent();
            if (labelText != null) messageLabel.Text = labelText;
            okButton.Enabled = false;
            nameTb.TextChanged += AnyTextBox_TextChanged;
            ownerTb.TextChanged += AnyTextBox_TextChanged;
            versionTb.TextChanged += AnyTextBox_TextChanged;
            if (modInfo != null) (nameTb.Text, ownerTb.Text, versionTb.Text) = modInfo.Value;
        }

        private void AnyTextBox_TextChanged(object? sender, EventArgs e)
        {
            okButton.Enabled =
                string.IsNullOrWhiteSpace(nameTb.Text) &&
                string.IsNullOrWhiteSpace(ownerTb.Text) &&
                string.IsNullOrWhiteSpace(versionTb.Text);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            EditedInfo = (nameTb.Text, ownerTb.Text, versionTb.Text);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            EditedInfo = ("", "", "");
            DialogResult = DialogResult.Continue;
            Close();
        }
    }
}
