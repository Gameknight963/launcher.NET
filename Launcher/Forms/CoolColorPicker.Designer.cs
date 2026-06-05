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
            okButton = new Button();
            cancelButton = new Button();
            previewPanel = new CheckerboardPanel();
            hslaColorEditor = new HslaColorEditor();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(188, 187);
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
            cancelButton.Location = new Point(107, 187);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // previewPanel
            // 
            previewPanel.Location = new Point(12, 137);
            previewPanel.Name = "previewPanel";
            previewPanel.Size = new Size(246, 33);
            previewPanel.TabIndex = 11;
            // 
            // hslaColorEditor
            // 
            hslaColorEditor.Color = Color.Red;
            hslaColorEditor.Location = new Point(12, 12);
            hslaColorEditor.Name = "hslaColorEditor";
            hslaColorEditor.Size = new Size(246, 119);
            hslaColorEditor.TabIndex = 11;
            hslaColorEditor.ColorChanged += hslaColorEditor_ColorChanged;
            // 
            // CoolColorPicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(275, 222);
            Controls.Add(hslaColorEditor);
            Controls.Add(previewPanel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            ForeColor = Color.Black;
            Name = "CoolColorPicker";
            Text = "Pick a Color";
            ResumeLayout(false);
        }

        #endregion
        private Button okButton;
        private Button cancelButton;
        private CheckerboardPanel previewPanel;
        private HslaColorEditor hslaColorEditor;
    }
}