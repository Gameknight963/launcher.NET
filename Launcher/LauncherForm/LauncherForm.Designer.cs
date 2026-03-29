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
            Panel = new Panel();
            InstallSometingButton = new Button();
            OpenFolderButton = new Button();
            RenameButton = new Button();
            SettingsButton = new Button();
            SearchBox = new TextBox();
            Panel.SuspendLayout();
            SuspendLayout();
            // 
            // status
            // 
            status.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            status.AutoSize = true;
            status.Location = new Point(14, 568);
            status.Name = "status";
            status.Size = new Size(160, 20);
            status.TabIndex = 2;
            status.Text = "Status will appear here";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 7);
            label1.Name = "label1";
            label1.Size = new Size(129, 20);
            label1.TabIndex = 3;
            label1.Text = "Installed instances";
            // 
            // gamesView
            // 
            gamesView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gamesView.Columns.AddRange(new ColumnHeader[] { LabelColumn, GameColumn });
            gamesView.FullRowSelect = true;
            gamesView.Location = new Point(14, 36);
            gamesView.Margin = new Padding(3, 4, 3, 4);
            gamesView.MultiSelect = false;
            gamesView.Name = "gamesView";
            gamesView.Size = new Size(578, 517);
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
            button3.Location = new Point(445, 563);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(147, 31);
            button3.TabIndex = 7;
            button3.Text = "+ Add new instance";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // RefreshList
            // 
            RefreshList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            RefreshList.Location = new Point(138, 1);
            RefreshList.Margin = new Padding(3, 4, 3, 4);
            RefreshList.Name = "RefreshList";
            RefreshList.Size = new Size(86, 31);
            RefreshList.TabIndex = 8;
            RefreshList.Text = "Refresh";
            RefreshList.UseVisualStyleBackColor = true;
            RefreshList.Click += RefreshList_Click;
            // 
            // LaunchButton
            // 
            LaunchButton.Location = new Point(6, 127);
            LaunchButton.Margin = new Padding(3, 4, 3, 4);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new Size(86, 31);
            LaunchButton.TabIndex = 2;
            LaunchButton.Text = "Launch";
            LaunchButton.UseVisualStyleBackColor = true;
            LaunchButton.Click += LaunchButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(98, 204);
            DeleteButton.Margin = new Padding(3, 4, 3, 4);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(86, 31);
            DeleteButton.TabIndex = 6;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // InstallHint
            // 
            InstallHint.Location = new Point(6, 64);
            InstallHint.Margin = new Padding(3, 3, 3, 0);
            InstallHint.Name = "InstallHint";
            InstallHint.Size = new Size(178, 44);
            InstallHint.TabIndex = 1;
            InstallHint.Text = "Select an instance to see more options";
            // 
            // Panel
            // 
            Panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Panel.Controls.Add(InstallSometingButton);
            Panel.Controls.Add(InstallHint);
            Panel.Controls.Add(OpenFolderButton);
            Panel.Controls.Add(LaunchButton);
            Panel.Controls.Add(RenameButton);
            Panel.Controls.Add(DeleteButton);
            Panel.Location = new Point(599, 36);
            Panel.Margin = new Padding(3, 4, 3, 4);
            Panel.Name = "Panel";
            Panel.Size = new Size(198, 519);
            Panel.TabIndex = 3;
            // 
            // InstallSometingButton
            // 
            InstallSometingButton.Location = new Point(98, 127);
            InstallSometingButton.Margin = new Padding(3, 4, 3, 4);
            InstallSometingButton.Name = "InstallSometingButton";
            InstallSometingButton.Size = new Size(86, 31);
            InstallSometingButton.TabIndex = 3;
            InstallSometingButton.Text = "Install...";
            InstallSometingButton.UseVisualStyleBackColor = true;
            InstallSometingButton.Click += InstallSometingButton_Click;
            // 
            // OpenFolderButton
            // 
            OpenFolderButton.Location = new Point(6, 165);
            OpenFolderButton.Margin = new Padding(3, 4, 3, 4);
            OpenFolderButton.Name = "OpenFolderButton";
            OpenFolderButton.Size = new Size(178, 31);
            OpenFolderButton.TabIndex = 4;
            OpenFolderButton.Text = "Open game folder";
            OpenFolderButton.UseVisualStyleBackColor = true;
            OpenFolderButton.Click += OpenFolderButton_Click;
            // 
            // RenameButton
            // 
            RenameButton.Location = new Point(6, 204);
            RenameButton.Margin = new Padding(3, 4, 3, 4);
            RenameButton.Name = "RenameButton";
            RenameButton.Size = new Size(86, 31);
            RenameButton.TabIndex = 5;
            RenameButton.Text = "Rename";
            RenameButton.UseVisualStyleBackColor = true;
            RenameButton.Click += RenameButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SettingsButton.Location = new Point(711, 563);
            SettingsButton.Margin = new Padding(3, 4, 3, 4);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(86, 31);
            SettingsButton.TabIndex = 9;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // SearchBox
            // 
            SearchBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SearchBox.Location = new Point(231, 3);
            SearchBox.Margin = new Padding(3, 4, 3, 4);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(361, 27);
            SearchBox.TabIndex = 10;
            SearchBox.TextChanged += SearchBox_TextChanged;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 600);
            Controls.Add(SearchBox);
            Controls.Add(SettingsButton);
            Controls.Add(RefreshList);
            Controls.Add(button3);
            Controls.Add(gamesView);
            Controls.Add(Panel);
            Controls.Add(label1);
            Controls.Add(status);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LauncherForm";
            Text = "launcher.net";
            Panel.ResumeLayout(false);
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
        private Panel Panel;
        private Button InstallSometingButton;
        private Button SettingsButton;
        private Button OpenFolderButton;
        private Button RenameButton;
        private TextBox SearchBox;
    }
}
