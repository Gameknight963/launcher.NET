namespace launcherdotnet.Launcher.Forms
{
    partial class CoolMessageBox
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
            pictureBoxIcon = new PictureBox();
            label = new Label();
            buttonsPanel = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Location = new Point(21, 21);
            pictureBoxIcon.Margin = new Padding(12);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(32, 32);
            pictureBoxIcon.TabIndex = 0;
            pictureBoxIcon.TabStop = false;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(65, 29);
            label.Margin = new Padding(3);
            label.MaximumSize = new Size(400, 0);
            label.Name = "label";
            label.Size = new Size(145, 15);
            label.TabIndex = 1;
            label.Text = "This is a CoolMessageBox.";
            // 
            // buttonsPanel
            // 
            buttonsPanel.BackColor = SystemColors.Control;
            buttonsPanel.Dock = DockStyle.Bottom;
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Location = new Point(0, 73);
            buttonsPanel.Margin = new Padding(0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new Size(240, 43);
            buttonsPanel.TabIndex = 2;
            // 
            // CoolMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(240, 116);
            Controls.Add(buttonsPanel);
            Controls.Add(label);
            Controls.Add(pictureBoxIcon);
            Name = "CoolMessageBox";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxIcon;
        private Label label;
        private FlowLayoutPanel buttonsPanel;
    }
}