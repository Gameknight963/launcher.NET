namespace launcherdotnet.Plugins.SteamGameCopy
{
    partial class SteamCopyForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            okButton = new Button();
            cancelButton = new Button();
            gamesLv = new ListView();
            nameColumn = new ColumnHeader();
            sizeColumn = new ColumnHeader();
            label1 = new Label();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(341, 415);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(260, 415);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // gamesLv
            // 
            gamesLv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamesLv.Columns.AddRange(new ColumnHeader[] { nameColumn, sizeColumn });
            gamesLv.FullRowSelect = true;
            gamesLv.Location = new Point(12, 29);
            gamesLv.MultiSelect = false;
            gamesLv.Name = "gamesLv";
            gamesLv.Size = new Size(404, 380);
            gamesLv.TabIndex = 1;
            gamesLv.UseCompatibleStateImageBehavior = false;
            gamesLv.View = View.Details;
            // 
            // nameColumn
            // 
            nameColumn.Text = "Game name";
            nameColumn.Width = 250;
            // 
            // sizeColumn
            // 
            sizeColumn.Text = "Size on disk";
            sizeColumn.Width = 120;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 8);
            label1.Margin = new Padding(3, 0, 3, 3);
            label1.Name = "label1";
            label1.Size = new Size(254, 15);
            label1.TabIndex = 2;
            label1.Text = "Select a game from your Steam library to copy.";
            // 
            // SteamCopyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 450);
            Controls.Add(label1);
            Controls.Add(gamesLv);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            ForeColor = Color.Black;
            Name = "SteamCopyForm";
            Text = "Copy a Steam Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private ListView gamesLv;
        private Label label1;
        private ColumnHeader nameColumn;
        private ColumnHeader sizeColumn;
    }
}
