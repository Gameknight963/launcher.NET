namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    partial class ThunderstoreModInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            activityHint = new Label();
            progressBar = new ProgressBar();
            logBox = new TextBox();
            downloadProgressBar = new ProgressBar();
            SuspendLayout();
            // 
            // activityHint
            // 
            activityHint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            activityHint.AutoSize = true;
            activityHint.Location = new Point(41, 190);
            activityHint.Name = "activityHint";
            activityHint.Size = new Size(167, 15);
            activityHint.TabIndex = 7;
            activityHint.Text = "Installation hint will show here";
            activityHint.Visible = true;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(41, 135);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(439, 23);
            progressBar.TabIndex = 8;
            // 
            // logBox
            // 
            logBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logBox.Location = new Point(41, 12);
            logBox.Multiline = true;
            logBox.Name = "logBox";
            logBox.ReadOnly = true;
            logBox.Size = new Size(439, 117);
            logBox.TabIndex = 10;
            // 
            // downloadProgressBar
            // 
            downloadProgressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            downloadProgressBar.Location = new Point(41, 164);
            downloadProgressBar.Name = "downloadProgressBar";
            downloadProgressBar.Size = new Size(439, 23);
            downloadProgressBar.TabIndex = 8;
            // 
            // ThunderstoreModInstaller
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 217);
            Controls.Add(logBox);
            Controls.Add(downloadProgressBar);
            Controls.Add(progressBar);
            Controls.Add(activityHint);
            ForeColor = Color.Black;
            Name = "ThunderstoreModInstaller";
            Text = "Mod installation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label activityHint;
        private ProgressBar progressBar;
        private TextBox logBox;
        private ProgressBar downloadProgressBar;
    }
}