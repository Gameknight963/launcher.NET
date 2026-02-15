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
            DeleteButton = new Button();
            status = new Label();
            label1 = new Label();
            gamesView = new ListView();
            LabelColumn = new ColumnHeader();
            PathColumn = new ColumnHeader();
            instanceOptions = new TableLayoutPanel();
            InstallHint = new Label();
            button3 = new Button();
            RefreshList = new Button();
            instanceOptions.SuspendLayout();
            SuspendLayout();
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(3, 85);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 0;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
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
            gamesView.Columns.AddRange(new ColumnHeader[] { LabelColumn, PathColumn });
            gamesView.Location = new Point(12, 27);
            gamesView.MultiSelect = false;
            gamesView.Name = "gamesView";
            gamesView.Size = new Size(504, 389);
            gamesView.TabIndex = 4;
            gamesView.UseCompatibleStateImageBehavior = false;
            gamesView.View = View.Details;
            gamesView.SelectedIndexChanged += gamesView_SelectedIndexChanged;
            // 
            // LabelColumn
            // 
            LabelColumn.Text = "Name";
            LabelColumn.Width = 300;
            // 
            // PathColumn
            // 
            PathColumn.Text = "Path";
            PathColumn.Width = 500;
            // 
            // instanceOptions
            // 
            instanceOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            instanceOptions.ColumnCount = 1;
            instanceOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            instanceOptions.Controls.Add(InstallHint, 0, 1);
            instanceOptions.Controls.Add(DeleteButton, 0, 2);
            instanceOptions.Location = new Point(522, 27);
            instanceOptions.Name = "instanceOptions";
            instanceOptions.RowCount = 3;
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 71.95122F));
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 28.04878F));
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Absolute, 306F));
            instanceOptions.Size = new Size(250, 389);
            instanceOptions.TabIndex = 5;
            // 
            // InstallHint
            // 
            InstallHint.AutoSize = true;
            InstallHint.Location = new Point(3, 61);
            InstallHint.Margin = new Padding(3, 2, 3, 0);
            InstallHint.Name = "InstallHint";
            InstallHint.Size = new Size(202, 15);
            InstallHint.TabIndex = 1;
            InstallHint.Text = "Select an instance to see it's filename";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Location = new Point(387, 422);
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
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RefreshList);
            Controls.Add(button3);
            Controls.Add(instanceOptions);
            Controls.Add(gamesView);
            Controls.Add(label1);
            Controls.Add(status);
            Name = "LauncherForm";
            Text = "Form1";
            instanceOptions.ResumeLayout(false);
            instanceOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DeleteButton;
        private Label status;
        private Label label1;
        private ListView gamesView;
        private ColumnHeader LabelColumn;
        private ColumnHeader PathColumn;
        private TableLayoutPanel instanceOptions;
        private Button button3;
        private Label InstallHint;
        private Button RefreshList;
    }
}
