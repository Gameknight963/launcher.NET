using launcherdotnet.Styling;
using System.Media;

namespace launcherdotnet.Launcher.Forms
{
    public partial class CoolMessageBox : ThemeableForm
    {
        readonly List<Button> _formButtons = new List<Button>();
        string _clipboardText;

        public static DialogResult Show(
            string? text = null,
            string caption = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Asterisk)
        {
            using CoolMessageBox box = new CoolMessageBox(text, caption, buttons, icon);
            return box.ShowDialog();
        }

        private Button CreateButton(string text, DialogResult result)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Margin = new Padding(5, 10, 5, 10);
            btn.DialogResult = result;
            btn.AutoSize = true;

            buttonsPanel.Controls.Add(btn);
            return btn;
        }

        public CoolMessageBox(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            _clipboardText =
                "---------------------------\r\n" +
                caption + "\r\n" +
                "---------------------------\r\n" +
                text + "\r\n" +
                "---------------------------\r\n" +
                (buttons == MessageBoxButtons.OK ? "OK" :
                 buttons == MessageBoxButtons.OKCancel ? "OK    Cancel" :
                 buttons == MessageBoxButtons.YesNo ? "Yes    No" :
                 buttons == MessageBoxButtons.YesNoCancel ? "Yes    No    Cancel" :
                 buttons == MessageBoxButtons.RetryCancel ? "Retry    Cancel" :
                 buttons == MessageBoxButtons.AbortRetryIgnore ? "Abort    Retry    Ignore" :
                 "OK") +
                "\r\n---------------------------";

            this.Text = caption;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();

            int lineHeight = TextRenderer.MeasureText("A", label.Font).Height;
            if (text != null) label.Text = text;
            if (TextRenderer.MeasureText(label.Text, label.Font).Width > label.Width)
                label.Top -= lineHeight;

            int captionWidth = TextRenderer.MeasureText(this.Text, SystemFonts.CaptionFont).Width;
            captionWidth = captionWidth == 0 ? 15 : captionWidth;

            this.StartPosition = FormStartPosition.CenterParent;

            this.ClientSize = new Size(label.Width + label.Left + 15 + captionWidth, label.Height +
                label.Top + buttonsPanel.Height + 30);

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    _formButtons.Add(CreateButton("OK", DialogResult.OK));
                    break;

                case MessageBoxButtons.OKCancel:
                    _formButtons.Add(CreateButton("OK", DialogResult.OK));
                    _formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.YesNo:
                    _formButtons.Add(CreateButton("Yes", DialogResult.Yes));
                    _formButtons.Add(CreateButton("No", DialogResult.No));
                    break;

                case MessageBoxButtons.YesNoCancel:
                    _formButtons.Add(CreateButton("Yes", DialogResult.Yes));
                    _formButtons.Add(CreateButton("No", DialogResult.No));
                    _formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.RetryCancel:
                    _formButtons.Add(CreateButton("Retry", DialogResult.Retry));
                    _formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    _formButtons.Add(CreateButton("Abort", DialogResult.Abort));
                    _formButtons.Add(CreateButton("Retry", DialogResult.Retry));
                    _formButtons.Add(CreateButton("Ignore", DialogResult.Ignore));
                    break;
            }
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    this.Icon = SystemIcons.Information;
                    pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();
                    break;

                case MessageBoxIcon.Warning:
                    this.Icon = SystemIcons.Warning;
                    pictureBoxIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;

                case MessageBoxIcon.Error:
                    this.Icon = SystemIcons.Error;
                    pictureBoxIcon.Image = SystemIcons.Error.ToBitmap();
                    break;

                case MessageBoxIcon.Question:
                    this.Icon = SystemIcons.Question;
                    pictureBoxIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
            }
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    SystemSounds.Asterisk.Play();
                    break;

                case MessageBoxIcon.Warning:
                    SystemSounds.Exclamation.Play();
                    break;

                case MessageBoxIcon.Error:
                    SystemSounds.Hand.Play();
                    break;

                case MessageBoxIcon.Question:
                    SystemSounds.Question.Play();
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                Clipboard.SetText(_clipboardText);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
