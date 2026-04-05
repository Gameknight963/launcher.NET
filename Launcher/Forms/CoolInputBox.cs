using launcherdotnet.Helpers;
using launcherdotnet.Syling;

namespace launcherdotnet.Launcher.Forms
{
    public partial class CoolInputBox : ThemeableForm
    {
        public string? ResultText { get; private set; }

        public static string? Prompt(string prompt,
            string caption = "",
            string defaultResponse = "",
            string placeholder = "")
        {
            using CoolInputBox box = new(prompt, caption, defaultResponse, placeholder);
            box.ShowDialog();
            return box.ResultText;
        }

        public CoolInputBox(string prompt,
            string caption = "",
            string defaultResponse = "",
            string placeholder = "")
        {
            InitializeComponent();
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;

            label.Text = prompt;
            this.Text = caption;
            textBox.Text = defaultResponse;
            TextBoxHelpers.SetPlaceholder(textBox, placeholder);
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
    }
}
