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
            button1 = new Button();
            status = new Label();
            label1 = new Label();
            gamesView = new ListView();
            LabelColumn = new ColumnHeader();
            PathColumn = new ColumnHeader();
            instanceOptions = new TableLayoutPanel();
            button3 = new Button();
            InstallHint = new Label();
            instanceOptions.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(172, 97);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Install";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            label1.Location = new Point(12, 9);
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
            instanceOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            instanceOptions.ColumnCount = 1;
            instanceOptions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            instanceOptions.Controls.Add(button1, 0, 0);
            instanceOptions.Controls.Add(InstallHint, 0, 1);
            instanceOptions.Location = new Point(522, 27);
            instanceOptions.Name = "instanceOptions";
            instanceOptions.RowCount = 3;
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            instanceOptions.RowStyles.Add(new RowStyle(SizeType.Absolute, 142F));
            instanceOptions.Size = new Size(250, 389);
            instanceOptions.TabIndex = 5;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(387, 422);
            button3.Name = "button3";
            button3.Size = new Size(129, 23);
            button3.TabIndex = 7;
            button3.Text = "+ Add new instance";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // InstallHint
            // 
            InstallHint.AutoSize = true;
            InstallHint.Location = new Point(3, 125);
            InstallHint.Margin = new Padding(3, 2, 3, 0);
            InstallHint.Name = "InstallHint";
            InstallHint.Size = new Size(210, 15);
            InstallHint.TabIndex = 1;
            InstallHint.Text = "If you see this text something is wrong";
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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

        private Button button1;
        private Label status;
        private Label label1;
        private ListView gamesView;
        private ColumnHeader LabelColumn;
        private ColumnHeader PathColumn;
        private TableLayoutPanel instanceOptions;
        private Button button3;
        private Label InstallHint;
    }
}
