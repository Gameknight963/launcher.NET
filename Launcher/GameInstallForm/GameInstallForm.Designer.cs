namespace launcherdotnet
{
    partial class GameInstallForm
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
            GameDropdown = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            VersionDropdown = new ComboBox();
            InstallGameButton = new Button();
            progressBar = new ProgressBar();
            ActivityHint = new Label();
            SuspendLayout();
            // 
            // GameDropdown
            // 
            GameDropdown.FormattingEnabled = true;
            GameDropdown.Location = new Point(41, 30);
            GameDropdown.Name = "GameDropdown";
            GameDropdown.Size = new Size(121, 23);
            GameDropdown.TabIndex = 0;
            GameDropdown.SelectedIndexChanged += GameDropdown_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 12);
            label1.Name = "label1";
            label1.Size = new Size(201, 15);
            label1.TabIndex = 1;
            label1.Text = "What game would you like to install?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 66);
            label2.Name = "label2";
            label2.Size = new Size(215, 15);
            label2.TabIndex = 1;
            label2.Text = "Which version would you like to install?";
            // 
            // VersionDropdown
            // 
            VersionDropdown.FormattingEnabled = true;
            VersionDropdown.Location = new Point(41, 84);
            VersionDropdown.Name = "VersionDropdown";
            VersionDropdown.Size = new Size(121, 23);
            VersionDropdown.TabIndex = 0;
            // 
            // InstallGameButton
            // 
            InstallGameButton.Location = new Point(41, 127);
            InstallGameButton.Name = "InstallGameButton";
            InstallGameButton.Size = new Size(75, 23);
            InstallGameButton.TabIndex = 2;
            InstallGameButton.Text = "Install";
            InstallGameButton.UseVisualStyleBackColor = true;
            InstallGameButton.Click += InstallGameButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(41, 161);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(439, 23);
            progressBar.TabIndex = 4;
            // 
            // ActivityHint
            // 
            ActivityHint.AutoSize = true;
            ActivityHint.Location = new Point(41, 187);
            ActivityHint.Name = "ActivityHint";
            ActivityHint.Size = new Size(167, 15);
            ActivityHint.TabIndex = 6;
            ActivityHint.Text = "Installation hint will show here";
            ActivityHint.Visible = false;
            // 
            // GameInstallForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 214);
            Controls.Add(ActivityHint);
            Controls.Add(progressBar);
            Controls.Add(InstallGameButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(VersionDropdown);
            Controls.Add(GameDropdown);
            Name = "GameInstallForm";
            Text = "Installation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox GameDropdown;
        private Label label1;
        private Label label2;
        private ComboBox VersionDropdown;
        private Button InstallGameButton;
        private ProgressBar progressBar;
        private Label ActivityHint;
    }
}