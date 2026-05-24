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
            SuspendLayout();
            // 
            // activityHint
            // 
            activityHint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            activityHint.AutoSize = true;
            activityHint.Location = new Point(41, 155);
            activityHint.Name = "activityHint";
            activityHint.Size = new Size(167, 15);
            activityHint.TabIndex = 7;
            activityHint.Text = "Installation hint will show here";
            activityHint.Visible = false;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(41, 129);
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
            logBox.Size = new Size(439, 111);
            logBox.TabIndex = 10;
            // 
            // ThunderstoreModInstaller
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 182);
            Controls.Add(logBox);
            Controls.Add(progressBar);
            Controls.Add(activityHint);
            Name = "ThunderstoreModInstaller";
            Text = "Mod installation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label activityHint;
        private ProgressBar progressBar;
        private TextBox logBox;
    }
}