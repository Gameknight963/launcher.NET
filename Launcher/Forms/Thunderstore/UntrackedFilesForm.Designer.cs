namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    partial class UntrackedFilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UntrackedFilesForm));
            filesClb = new CheckedListBox();
            okButton = new Button();
            selectAllButton = new Button();
            deselectAllButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // filesClb
            // 
            filesClb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            filesClb.FormattingEnabled = true;
            filesClb.Location = new Point(12, 76);
            filesClb.Name = "filesClb";
            filesClb.Size = new Size(324, 382);
            filesClb.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(261, 467);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "Accept";
            okButton.UseVisualStyleBackColor = true;
            // 
            // selectAllButton
            // 
            selectAllButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            selectAllButton.Location = new Point(12, 467);
            selectAllButton.Name = "selectAllButton";
            selectAllButton.Size = new Size(75, 23);
            selectAllButton.TabIndex = 1;
            selectAllButton.Text = "Select all";
            selectAllButton.UseVisualStyleBackColor = true;
            selectAllButton.Click += selectAllButton_Click;
            // 
            // deselectAllButton
            // 
            deselectAllButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            deselectAllButton.Location = new Point(93, 467);
            deselectAllButton.Name = "deselectAllButton";
            deselectAllButton.Size = new Size(84, 23);
            deselectAllButton.TabIndex = 1;
            deselectAllButton.Text = "Deselect all";
            deselectAllButton.UseVisualStyleBackColor = true;
            deselectAllButton.Click += deselectAllButton_Click;
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(324, 67);
            label1.TabIndex = 2;
            label1.Text = resources.GetString("label1.Text");
            // 
            // UntrackedFilesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(348, 502);
            Controls.Add(label1);
            Controls.Add(deselectAllButton);
            Controls.Add(selectAllButton);
            Controls.Add(okButton);
            Controls.Add(filesClb);
            ForeColor = Color.Black;
            Name = "UntrackedFilesForm";
            Text = "Manage untracked files";
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox filesClb;
        private Button okButton;
        private Button selectAllButton;
        private Button deselectAllButton;
        private Label label1;
    }
}