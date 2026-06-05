namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class FillMissingModInfo : Form
    {
        public (string Name, string Owner, string Version)? ModInfo { get; private set; }
        public Result FormResult { get; private set; } = Result.Cancel;
        public enum Result
        {
            OK,
            Skip,
            Cancel,
        }
        public FillMissingModInfo()
        {
            InitializeComponent();
            okButton.Enabled = false;
            nameTb.TextChanged += AnyTextBox_TextChanged;
            ownerTb.TextChanged += AnyTextBox_TextChanged;
            versionTb.TextChanged += AnyTextBox_TextChanged;
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
            ModInfo = (nameTb.Text, ownerTb.Text, versionTb.Text);
            FormResult = Result.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            FormResult = Result.Cancel;
            Close();
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            FormResult = Result.Skip;
            Close();
        }
    }
}
