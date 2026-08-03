namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    partial class ReviewAndConfirm
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
            modsLb = new ListBox();
            okButton = new Button();
            cancelButton = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(245, 15);
            label1.TabIndex = 0;
            label1.Text = "The following mods are about to be installed:";
            // 
            // modsLb
            // 
            modsLb.FormattingEnabled = true;
            modsLb.Location = new Point(12, 30);
            modsLb.Name = "modsLb";
            modsLb.Size = new Size(333, 379);
            modsLb.TabIndex = 1;
            // 
            // okButton
            // 
            okButton.Location = new Point(270, 415);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(189, 415);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 419);
            label2.Name = "label2";
            label2.Size = new Size(103, 15);
            label2.TabIndex = 3;
            label2.Text = "(69 dependencies)";
            // 
            // ReviewAndConfirm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(357, 444);
            Controls.Add(label2);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(modsLb);
            Controls.Add(label1);
            ForeColor = Color.Black;
            Name = "ReviewAndConfirm";
            Text = "Review";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox modsLb;
        private Button okButton;
        private Button cancelButton;
        private Label label2;
    }
}