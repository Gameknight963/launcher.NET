using launcherdotnet.Launcher.Controls;
namespace launcherdotnet.Launcher.Forms
{
    partial class CoolColorPicker
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
            colorEditor = new Cyotek.Windows.Forms.ColorEditor();
            okButton = new Button();
            cancelButton = new Button();
            panel1 = new Panel();
            previewPanel = new CheckerboardPanel();
            SuspendLayout();
            // 
            // colorEditor
            // 
            colorEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            colorEditor.Location = new Point(13, -85);
            colorEditor.Margin = new Padding(4, 3, 4, 3);
            colorEditor.Name = "colorEditor";
            colorEditor.Size = new Size(246, 262);
            colorEditor.TabIndex = 8;
            colorEditor.ColorChanged += colorEditor_ColorChanged;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(188, 246);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 9;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(107, 246);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Location = new Point(12, -5);
            panel1.Name = "panel1";
            panel1.Size = new Size(251, 21);
            panel1.TabIndex = 10;
            // 
            // previewPanel
            // 
            previewPanel.Location = new Point(13, 183);
            previewPanel.Name = "previewPanel";
            previewPanel.Size = new Size(246, 33);
            previewPanel.TabIndex = 11;
            // 
            // CoolColorPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 281);
            Controls.Add(previewPanel);
            Controls.Add(panel1);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(colorEditor);
            ForeColor = Color.Black;
            Name = "CoolColorPicker";
            Text = "Pick a Color";
            ResumeLayout(false);
        }

        #endregion

        private Cyotek.Windows.Forms.ColorEditor colorEditor;
        private Button okButton;
        private Button cancelButton;
        private Panel panel1;
        private CheckerboardPanel previewPanel;
    }
}