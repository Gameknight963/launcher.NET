using System.Windows.Forms;
using System.Drawing;

namespace launcherdotnet.Plugins.GameFromUrl
{
    partial class UrlInputBox
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
            label = new Label();
            cancelButton = new Button();
            okButton = new Button();
            textBox = new TextBox();
            browseBtn = new Button();
            folderBrowseBtn = new Button();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(12, 12);
            label.Margin = new Padding(3);
            label.Name = "label";
            label.Size = new Size(306, 15);
            label.TabIndex = 0;
            label.Text = "Enter a URL. HTTP/HTTPS and file:/// URL are supported";
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(365, 41);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Location = new Point(365, 12);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // textBox
            // 
            textBox.Location = new Point(12, 94);
            textBox.Name = "textBox";
            textBox.Size = new Size(428, 23);
            textBox.TabIndex = 2;
            // 
            // browseBtn
            // 
            browseBtn.Location = new Point(12, 41);
            browseBtn.Name = "browseBtn";
            browseBtn.Size = new Size(75, 23);
            browseBtn.TabIndex = 3;
            browseBtn.Text = "Browse...";
            browseBtn.UseVisualStyleBackColor = true;
            browseBtn.Click += browseBtn_Click;
            // 
            // folderBrowseBtn
            // 
            folderBrowseBtn.Location = new Point(93, 41);
            folderBrowseBtn.Name = "folderBrowseBtn";
            folderBrowseBtn.Size = new Size(136, 23);
            folderBrowseBtn.TabIndex = 3;
            folderBrowseBtn.Text = "Browse for folders...";
            folderBrowseBtn.UseVisualStyleBackColor = true;
            folderBrowseBtn.Click += folderBrowseBtn_Click;
            // 
            // UrlInputBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(452, 129);
            Controls.Add(folderBrowseBtn);
            Controls.Add(browseBtn);
            Controls.Add(textBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(label);
            Name = "UrlInputBox";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private Button cancelButton;
        private Button okButton;
        private TextBox textBox;
        private Button browseBtn;
        private Button folderBrowseBtn;
    }
}