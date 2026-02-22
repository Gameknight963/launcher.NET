namespace launcherdotnet
{
    partial class LauncherForm
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
            status = new Label();
            label1 = new Label();
            gamesView = new ListView();
            LabelColumn = new ColumnHeader();
            GameColumn = new ColumnHeader();
            button3 = new Button();
            RefreshList = new Button();
            LaunchButton = new Button();
            DeleteButton = new Button();
            InstallHint = new Label();
            panel1 = new Panel();
            InstallSometingButton = new Button();
            OpenFolderButton = new Button();
            RenameButton = new Button();
            SettingsButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // status
            // 
            status.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            status.AutoSize = true;
            status.Location = new Point(12, 426);
            status.Name = "status";
            status.Size = new Size(125, 15);
            status.TabIndex = 2;
            status.Text = "Status will appear here";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 5);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 3;
            label1.Text = "Installed instances";
            // 
            // gamesView
            // 
            gamesView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamesView.Columns.AddRange(new ColumnHeader[] { LabelColumn, GameColumn });
            gamesView.FullRowSelect = true;
            gamesView.Location = new Point(12, 27);
            gamesView.MultiSelect = false;
            gamesView.Name = "gamesView";
            gamesView.Size = new Size(506, 389);
            gamesView.TabIndex = 4;
            gamesView.UseCompatibleStateImageBehavior = false;
            gamesView.View = View.Details;
            gamesView.SelectedIndexChanged += gamesView_SelectedIndexChanged;
            // 
            // LabelColumn
            // 
            LabelColumn.Text = "Name";
            LabelColumn.Width = 370;
            // 
            // GameColumn
            // 
            GameColumn.Text = "Game";
            GameColumn.Width = 132;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Location = new Point(389, 422);
            button3.Name = "button3";
            button3.Size = new Size(129, 23);
            button3.TabIndex = 7;
            button3.Text = "+ Add new instance";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // RefreshList
            // 
            RefreshList.Location = new Point(121, 1);
            RefreshList.Name = "RefreshList";
            RefreshList.Size = new Size(75, 23);
            RefreshList.TabIndex = 8;
            RefreshList.Text = "Refresh";
            RefreshList.UseVisualStyleBackColor = true;
            RefreshList.Click += RefreshList_Click;
            // 
            // LaunchButton
            // 
            LaunchButton.Location = new Point(5, 95);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new Size(75, 23);
            LaunchButton.TabIndex = 2;
            LaunchButton.Text = "Launch";
            LaunchButton.UseVisualStyleBackColor = true;
            LaunchButton.Click += LaunchButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(86, 153);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 0;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // InstallHint
            // 
            InstallHint.Location = new Point(5, 48);
            InstallHint.Margin = new Padding(3, 2, 3, 0);
            InstallHint.Name = "InstallHint";
            InstallHint.Size = new Size(156, 33);
            InstallHint.TabIndex = 1;
            InstallHint.Text = "Select an instance to see more options";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(InstallSometingButton);
            panel1.Controls.Add(InstallHint);
            panel1.Controls.Add(OpenFolderButton);
            panel1.Controls.Add(LaunchButton);
            panel1.Controls.Add(RenameButton);
            panel1.Controls.Add(DeleteButton);
            panel1.Location = new Point(524, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(173, 389);
            panel1.TabIndex = 3;
            // 
            // InstallSometingButton
            // 
            InstallSometingButton.Location = new Point(86, 95);
            InstallSometingButton.Name = "InstallSometingButton";
            InstallSometingButton.Size = new Size(75, 23);
            InstallSometingButton.TabIndex = 3;
            InstallSometingButton.Text = "Install...";
            InstallSometingButton.UseVisualStyleBackColor = true;
            InstallSometingButton.Click += InstallSometingButton_Click;
            // 
            // OpenFolderButton
            // 
            OpenFolderButton.Location = new Point(5, 124);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(156, 23);
            OpenFolderButton.TabIndex = 2;
            OpenFolderButton.Text = "Open game folder";
            OpenFolderButton.UseVisualStyleBackColor = true;
            OpenFolderButton.Click += OpenFolderButton_Click;
            // 
            // RenameButton
            // 
            RenameButton.Location = new Point(5, 153);
            RenameButton.Name = "RenameButton";
            RenameButton.Size = new Size(75, 23);
            RenameButton.TabIndex = 0;
            RenameButton.Text = "Rename";
            RenameButton.UseVisualStyleBackColor = true;
            RenameButton.Click += RenameButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SettingsButton.Location = new Point(622, 422);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(75, 23);
            SettingsButton.TabIndex = 9;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(709, 450);
            Controls.Add(SettingsButton);
            Controls.Add(RefreshList);
            Controls.Add(button3);
            Controls.Add(gamesView);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(status);
            Name = "LauncherForm";
            Text = "launcher.net";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label status;
        private Label label1;
        private ListView gamesView;
        private ColumnHeader LabelColumn;
        private Button button3;
        private Button RefreshList;
        private ColumnHeader GameColumn;
        private Button LaunchButton;
        private Button DeleteButton;
        private Label InstallHint;
        private Panel panel1;
        private Button InstallSometingButton;
        private Button SettingsButton;
        private Button OpenFolderButton;
        private Button RenameButton;
    }
}
