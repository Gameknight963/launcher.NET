namespace launcherdotnet
{
    partial class ModloaderInstallForm
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
            ModloaderDropdown = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            VersionDropdown = new ComboBox();
            InstallModloaderButton = new Button();
            progressBar = new ProgressBar();
            SuspendLayout();
            // 
            // ModloaderDropdown
            // 
            ModloaderDropdown.FormattingEnabled = true;
            ModloaderDropdown.Items.AddRange(new object[] { "Melonloader" });
            ModloaderDropdown.Location = new Point(41, 30);
            ModloaderDropdown.Name = "ModloaderDropdown";
            ModloaderDropdown.Size = new Size(121, 23);
            ModloaderDropdown.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 12);
            label1.Name = "label1";
            label1.Size = new Size(168, 15);
            label1.TabIndex = 1;
            label1.Text = "What would you like to install?";
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
            // InstallModloaderButton
            // 
            InstallModloaderButton.Location = new Point(41, 127);
            InstallModloaderButton.Name = "InstallModloaderButton";
            InstallModloaderButton.Size = new Size(75, 23);
            InstallModloaderButton.TabIndex = 2;
            InstallModloaderButton.Text = "Install";
            InstallModloaderButton.UseVisualStyleBackColor = true;
            InstallModloaderButton.Click += InstallModloader_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(41, 161);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(430, 23);
            progressBar.TabIndex = 4;
            // 
            // ModloaderInstallForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 196);
            Controls.Add(progressBar);
            Controls.Add(InstallModloaderButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(VersionDropdown);
            Controls.Add(ModloaderDropdown);
            Name = "ModloaderInstallForm";
            Text = "Installation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ModloaderDropdown;
        private Label label1;
        private Label label2;
        private ComboBox VersionDropdown;
        private Button InstallModloaderButton;
        private ProgressBar progressBar;
    }
}