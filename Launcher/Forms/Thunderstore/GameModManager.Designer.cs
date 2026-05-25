namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    partial class GameModManager
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
            modsLv = new ListView();
            nameColumn = new ColumnHeader();
            versionColumn = new ColumnHeader();
            typeColumn = new ColumnHeader();
            uninstallButton = new Button();
            closeButton = new Button();
            installModsButton = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // modsLv
            // 
            modsLv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            modsLv.Columns.AddRange(new ColumnHeader[] { nameColumn, versionColumn, typeColumn });
            modsLv.FullRowSelect = true;
            modsLv.Location = new Point(12, 12);
            modsLv.Name = "modsLv";
            modsLv.Size = new Size(422, 399);
            modsLv.TabIndex = 0;
            modsLv.UseCompatibleStateImageBehavior = false;
            modsLv.View = View.Details;
            modsLv.SelectedIndexChanged += modsLv_SelectedIndexChanged;
            // 
            // nameColumn
            // 
            nameColumn.Text = "Name";
            nameColumn.Width = 220;
            // 
            // versionColumn
            // 
            versionColumn.Text = "Version";
            versionColumn.Width = 70;
            // 
            // typeColumn
            // 
            typeColumn.Text = "Type";
            typeColumn.Width = 100;
            // 
            // uninstallButton
            // 
            uninstallButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            uninstallButton.Location = new Point(278, 417);
            uninstallButton.Name = "uninstallButton";
            uninstallButton.Size = new Size(75, 23);
            uninstallButton.TabIndex = 2;
            uninstallButton.Text = "Uninstall";
            uninstallButton.UseVisualStyleBackColor = true;
            uninstallButton.Click += uninstallButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            closeButton.Location = new Point(359, 417);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 2;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            // 
            // installModsButton
            // 
            installModsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            installModsButton.Location = new Point(12, 417);
            installModsButton.Name = "installModsButton";
            installModsButton.Size = new Size(106, 23);
            installModsButton.TabIndex = 2;
            installModsButton.Text = "Get more mods";
            installModsButton.UseVisualStyleBackColor = true;
            installModsButton.Click += installModsButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(124, 417);
            button1.Name = "button1";
            button1.Size = new Size(134, 23);
            button1.TabIndex = 3;
            button1.Text = "Clean untracked files";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // GameModManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 450);
            Controls.Add(button1);
            Controls.Add(closeButton);
            Controls.Add(installModsButton);
            Controls.Add(uninstallButton);
            Controls.Add(modsLv);
            ForeColor = Color.Black;
            Name = "GameModManager";
            Text = "Manage Mods";
            ResumeLayout(false);
        }

        #endregion

        private ListView modsLv;
        private ColumnHeader nameColumn;
        private ColumnHeader versionColumn;
        private ColumnHeader typeColumn;
        private Button uninstallButton;
        private Button closeButton;
        private Button installModsButton;
        private Button button1;
    }
}