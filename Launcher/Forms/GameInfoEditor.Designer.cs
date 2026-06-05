namespace launcherdotnet.Launcher.Forms
{
    partial class GameInfoEditor
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
            labelOfTheGameLabel = new Label();
            labelBox = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            nameBox = new TextBox();
            gameNameLabel = new Label();
            thunderstoreSlugLabel = new Label();
            thunderstoreSlugBox = new TextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            exeLabel = new Label();
            gameExeBox = new TextBox();
            gameRootDirBox = new TextBox();
            rootDirLabel = new Label();
            label2 = new Label();
            guidLabel = new Label();
            copyGUIDButton = new Button();
            okButton = new Button();
            cancelButton = new Button();
            runsWithCmdCheck = new CheckBox();
            baselineButton = new Button();
            baselineFilesAmountLabel = new Label();
            modManageableBox = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // labelOfTheGameLabel
            // 
            labelOfTheGameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            labelOfTheGameLabel.AutoSize = true;
            labelOfTheGameLabel.Location = new Point(6, 6);
            labelOfTheGameLabel.Margin = new Padding(6);
            labelOfTheGameLabel.Name = "labelOfTheGameLabel";
            labelOfTheGameLabel.Size = new Size(35, 16);
            labelOfTheGameLabel.TabIndex = 0;
            labelOfTheGameLabel.Text = "Label";
            labelOfTheGameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelBox
            // 
            labelBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelBox.Location = new Point(123, 3);
            labelBox.Name = "labelBox";
            labelBox.Size = new Size(218, 23);
            labelBox.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(labelOfTheGameLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(labelBox, 1, 0);
            tableLayoutPanel1.Controls.Add(nameBox, 1, 1);
            tableLayoutPanel1.Controls.Add(gameNameLabel, 0, 1);
            tableLayoutPanel1.Controls.Add(thunderstoreSlugLabel, 0, 2);
            tableLayoutPanel1.Controls.Add(thunderstoreSlugBox, 1, 2);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(344, 85);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // nameBox
            // 
            nameBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            nameBox.Location = new Point(123, 31);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(218, 23);
            nameBox.TabIndex = 1;
            // 
            // gameNameLabel
            // 
            gameNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            gameNameLabel.AutoSize = true;
            gameNameLabel.Location = new Point(6, 34);
            gameNameLabel.Margin = new Padding(6);
            gameNameLabel.Name = "gameNameLabel";
            gameNameLabel.Size = new Size(73, 16);
            gameNameLabel.TabIndex = 0;
            gameNameLabel.Text = "Game Name";
            gameNameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // thunderstoreSlugLabel
            // 
            thunderstoreSlugLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            thunderstoreSlugLabel.AutoSize = true;
            thunderstoreSlugLabel.Location = new Point(6, 62);
            thunderstoreSlugLabel.Margin = new Padding(6);
            thunderstoreSlugLabel.Name = "thunderstoreSlugLabel";
            thunderstoreSlugLabel.Size = new Size(103, 17);
            thunderstoreSlugLabel.TabIndex = 0;
            thunderstoreSlugLabel.Text = "Thunderstore Slug";
            thunderstoreSlugLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // thunderstoreSlugBox
            // 
            thunderstoreSlugBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            thunderstoreSlugBox.Location = new Point(123, 59);
            thunderstoreSlugBox.Name = "thunderstoreSlugBox";
            thunderstoreSlugBox.Size = new Size(218, 23);
            thunderstoreSlugBox.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(exeLabel, 0, 0);
            tableLayoutPanel2.Controls.Add(gameExeBox, 1, 0);
            tableLayoutPanel2.Controls.Add(gameRootDirBox, 1, 1);
            tableLayoutPanel2.Controls.Add(rootDirLabel, 0, 1);
            tableLayoutPanel2.Location = new Point(12, 101);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(344, 57);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // exeLabel
            // 
            exeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            exeLabel.AutoSize = true;
            exeLabel.Location = new Point(6, 6);
            exeLabel.Margin = new Padding(6);
            exeLabel.Name = "exeLabel";
            exeLabel.Size = new Size(64, 16);
            exeLabel.TabIndex = 0;
            exeLabel.Text = "Executable";
            exeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // gameExeBox
            // 
            gameExeBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            gameExeBox.Location = new Point(123, 3);
            gameExeBox.Name = "gameExeBox";
            gameExeBox.Size = new Size(218, 23);
            gameExeBox.TabIndex = 1;
            // 
            // gameRootDirBox
            // 
            gameRootDirBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            gameRootDirBox.Location = new Point(123, 31);
            gameRootDirBox.Name = "gameRootDirBox";
            gameRootDirBox.Size = new Size(218, 23);
            gameRootDirBox.TabIndex = 1;
            // 
            // rootDirLabel
            // 
            rootDirLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            rootDirLabel.AutoSize = true;
            rootDirLabel.Location = new Point(6, 34);
            rootDirLabel.Margin = new Padding(6);
            rootDirLabel.Name = "rootDirLabel";
            rootDirLabel.Size = new Size(83, 17);
            rootDirLabel.TabIndex = 0;
            rootDirLabel.Text = "Root Directory";
            rootDirLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 193);
            label2.Name = "label2";
            label2.Size = new Size(37, 15);
            label2.TabIndex = 4;
            label2.Text = "GUID:";
            // 
            // guidLabel
            // 
            guidLabel.AutoSize = true;
            guidLabel.Location = new Point(61, 193);
            guidLabel.Name = "guidLabel";
            guidLabel.Size = new Size(117, 15);
            guidLabel.TabIndex = 4;
            guidLabel.Text = "guid will appear here";
            // 
            // copyGUIDButton
            // 
            copyGUIDButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            copyGUIDButton.Location = new Point(278, 189);
            copyGUIDButton.Name = "copyGUIDButton";
            copyGUIDButton.Size = new Size(75, 23);
            copyGUIDButton.TabIndex = 5;
            copyGUIDButton.Text = "Copy GUID";
            copyGUIDButton.UseVisualStyleBackColor = true;
            copyGUIDButton.Click += copyGUIDButton_Click;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(278, 295);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 6;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(197, 295);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // runsWithCmdCheck
            // 
            runsWithCmdCheck.AutoSize = true;
            runsWithCmdCheck.Location = new Point(19, 164);
            runsWithCmdCheck.Name = "runsWithCmdCheck";
            runsWithCmdCheck.Size = new Size(108, 19);
            runsWithCmdCheck.TabIndex = 7;
            runsWithCmdCheck.Text = "Runs with CMD";
            runsWithCmdCheck.UseVisualStyleBackColor = true;
            // 
            // baselineButton
            // 
            baselineButton.Location = new Point(215, 218);
            baselineButton.Name = "baselineButton";
            baselineButton.Size = new Size(138, 23);
            baselineButton.TabIndex = 8;
            baselineButton.Text = "Recalculate baseline";
            baselineButton.UseVisualStyleBackColor = true;
            baselineButton.Click += baselineButton_Click;
            // 
            // baselineFilesAmountLabel
            // 
            baselineFilesAmountLabel.AutoSize = true;
            baselineFilesAmountLabel.Location = new Point(16, 222);
            baselineFilesAmountLabel.Name = "baselineFilesAmountLabel";
            baselineFilesAmountLabel.Size = new Size(110, 15);
            baselineFilesAmountLabel.TabIndex = 9;
            baselineFilesAmountLabel.Text = "72 file(s) in baseline";
            // 
            // modManageableBox
            // 
            modManageableBox.AutoSize = true;
            modManageableBox.Location = new Point(135, 164);
            modManageableBox.Name = "modManageableBox";
            modManageableBox.Size = new Size(139, 19);
            modManageableBox.TabIndex = 7;
            modManageableBox.Text = "Enable mod manager";
            modManageableBox.UseVisualStyleBackColor = true;
            // 
            // GameInfoEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 330);
            Controls.Add(baselineFilesAmountLabel);
            Controls.Add(baselineButton);
            Controls.Add(modManageableBox);
            Controls.Add(runsWithCmdCheck);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(copyGUIDButton);
            Controls.Add(guidLabel);
            Controls.Add(label2);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.Black;
            Name = "GameInfoEditor";
            Text = "Game Info Editor";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelOfTheGameLabel;
        private TextBox labelBox;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox nameBox;
        private Label gameNameLabel;
        private Label thunderstoreSlugLabel;
        private TextBox thunderstoreSlugBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label exeLabel;
        private TextBox gameExeBox;
        private TextBox gameRootDirBox;
        private Label rootDirLabel;
        private Label label2;
        private Label guidLabel;
        private Button copyGUIDButton;
        private Button okButton;
        private Button cancelButton;
        private CheckBox runsWithCmdCheck;
        private Button baselineButton;
        private Label baselineFilesAmountLabel;
        private CheckBox modManageableBox;
    }
}