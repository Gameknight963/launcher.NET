using launcherdotnet.Syling;
using System.Media;

namespace launcherdotnet.Launcher.Forms
{
    public partial class CoolMessageBox : ThemeableForm
    {
        List<Button> formButtons = new List<Button>();

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
            btn.Margin = new Padding(10, 10, 10, 10);
            btn.DialogResult = result;
            btn.AutoSize = true;

            buttonsPanel.Controls.Add(btn);
            return btn;
        }

        public CoolMessageBox(string? text, string? caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            this.Text = caption;
            pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();

            int lineHeight = TextRenderer.MeasureText("A", label.Font).Height;
            if (text != null) label.Text = text;
            if (TextRenderer.MeasureText(label.Text, label.Font).Width > label.Width)
                label.Top -= lineHeight;

            this.ClientSize = new Size(label.Width + label.Left + 15, label.Height +
                label.Top + buttonsPanel.Height + 15);

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    formButtons.Add(CreateButton("OK", DialogResult.OK));
                    break;

                case MessageBoxButtons.OKCancel:
                    formButtons.Add(CreateButton("OK", DialogResult.OK));
                    formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.YesNo:
                    formButtons.Add(CreateButton("Yes", DialogResult.Yes));
                    formButtons.Add(CreateButton("No", DialogResult.No));
                    break;

                case MessageBoxButtons.YesNoCancel:
                    formButtons.Add(CreateButton("Yes", DialogResult.Yes));
                    formButtons.Add(CreateButton("No", DialogResult.No));
                    formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.RetryCancel:
                    formButtons.Add(CreateButton("Retry", DialogResult.Retry));
                    formButtons.Add(CreateButton("Cancel", DialogResult.Cancel));
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    formButtons.Add(CreateButton("Abort", DialogResult.Abort));
                    formButtons.Add(CreateButton("Retry", DialogResult.Retry));
                    formButtons.Add(CreateButton("Ignore", DialogResult.Ignore));
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
    }
}
