using launcherdotnet.Styling;
using System.Windows.Forms;

namespace launcherdotnet.Plugins.GameFromUrl
{
    public partial class UrlInputBox : ThemeableForm
    {
        public string? ResultText { get; private set; }

        public static string? Prompt()
        {
            using UrlInputBox box = new();
            box.ShowDialog();
            return box.ResultText;
        }

        public UrlInputBox()
        {
            InitializeComponent();
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            textBox.Select();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ResultText = textBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ResultText = null;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new();
            dialog.Title = "Select a ZIP";
            dialog.Filter = "ZIP files (*.zip)|*.zip|All files (*.*)|*.*";
            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBox.Text = $"file:///{dialog.FileName.Replace('\\', '/')}";
        }

        private void folderBrowseBtn_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            textBox.Text = $"file:///{dialog.SelectedPath.Replace('\\', '/')}";
        }
    }
}
