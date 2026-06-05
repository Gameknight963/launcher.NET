
namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    partial class FillMissingModInfo
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
            label1 = new Label();
            okButton = new Button();
            skipButton = new Button();
            cancelButton = new Button();
            nameTb = new TextBox();
            label2 = new Label();
            label3 = new Label();
            ownerTb = new TextBox();
            label4 = new Label();
            versionTb = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(389, 23);
            label1.TabIndex = 0;
            label1.Text = "The package you have selected is missing the following information:";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(326, 190);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // skipButton
            // 
            skipButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            skipButton.Location = new Point(12, 190);
            skipButton.Name = "skipButton";
            skipButton.Size = new Size(75, 23);
            skipButton.TabIndex = 1;
            skipButton.Text = "Skip";
            skipButton.UseVisualStyleBackColor = true;
            skipButton.Click += SkipButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(245, 190);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // nameTb
            // 
            nameTb.Location = new Point(12, 50);
            nameTb.Name = "nameTb";
            nameTb.Size = new Size(332, 23);
            nameTb.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 32);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 76);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 3;
            label3.Text = "Owner:";
            // 
            // ownerTb
            // 
            ownerTb.Location = new Point(12, 94);
            ownerTb.Name = "ownerTb";
            ownerTb.Size = new Size(332, 23);
            ownerTb.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 120);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 3;
            label4.Text = "Version:";
            // 
            // versionTb
            // 
            versionTb.Location = new Point(12, 138);
            versionTb.Name = "versionTb";
            versionTb.Size = new Size(332, 23);
            versionTb.TabIndex = 2;
            // 
            // FillMissingModInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 225);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(versionTb);
            Controls.Add(ownerTb);
            Controls.Add(nameTb);
            Controls.Add(cancelButton);
            Controls.Add(skipButton);
            Controls.Add(okButton);
            Controls.Add(label1);
            Name = "FillMissingModInfo";
            Text = "Missing Package Manifest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button okButton;
        private Button skipButton;
        private Button cancelButton;
        private TextBox nameTb;
        private Label label2;
        private Label label3;
        private TextBox ownerTb;
        private Label label4;
        private TextBox versionTb;
    }
}