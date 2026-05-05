namespace launcherdotnet.Launcher.Forms
{
    partial class ThunderstoreModBrowser
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
            tableLayoutPanel1 = new TableLayoutPanel();
            modsLv = new ListView();
            nameColumn = new ColumnHeader();
            descriptionRtb = new RichTextBox();
            okButton = new Button();
            cancelButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(modsLv, 0, 0);
            tableLayoutPanel1.Controls.Add(descriptionRtb, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(572, 366);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // modsLv
            // 
            modsLv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            modsLv.Columns.AddRange(new ColumnHeader[] { nameColumn });
            modsLv.FullRowSelect = true;
            modsLv.HeaderStyle = ColumnHeaderStyle.None;
            modsLv.Location = new Point(3, 3);
            modsLv.Name = "modsLv";
            modsLv.Size = new Size(280, 360);
            modsLv.TabIndex = 0;
            modsLv.UseCompatibleStateImageBehavior = false;
            modsLv.View = View.Details;
            // 
            // nameColumn
            // 
            nameColumn.Text = "Name";
            nameColumn.Width = 276;
            // 
            // descriptionRtb
            // 
            descriptionRtb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            descriptionRtb.Location = new Point(289, 3);
            descriptionRtb.Name = "descriptionRtb";
            descriptionRtb.ReadOnly = true;
            descriptionRtb.Size = new Size(280, 360);
            descriptionRtb.TabIndex = 1;
            descriptionRtb.Text = "";
            // 
            // okButton
            // 
            okButton.Location = new Point(506, 415);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(425, 415);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // ThunderstoreModBrowser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(596, 450);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(tableLayoutPanel1);
            ForeColor = Color.Black;
            Name = "ThunderstoreModBrowser";
            Text = "ThunderstoreModBrowser";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ListView modsLv;
        private RichTextBox descriptionRtb;
        private Button okButton;
        private Button cancelButton;
        private ColumnHeader nameColumn;
    }
}