namespace launcherdotnet
{
    partial class SettingsForm
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
            ApplyButton = new Button();
            SettingsTabControl = new TabControl();
            GeneralTab = new TabPage();
            GeneralCheckbox = new CheckedListBox();
            MirrorsTab = new TabPage();
            AddLoaderButton = new Button();
            button1 = new Button();
            listView1 = new ListView();
            listView2 = new ListView();
            GamesLabel = new Label();
            ModloaderLabel = new Label();
            MirrorsHint = new Label();
            ModloaderTab = new TabPage();
            MLCheckbox = new CheckedListBox();
            AdvancedTab = new TabPage();
            textBox1 = new TextBox();
            AdvancedCheckbox = new CheckedListBox();
            panel1 = new Panel();
            SelectedHint = new Label();
            panel2 = new Panel();
            TempDirLabel = new Label();
            button2 = new Button();
            panel3 = new Panel();
            button3 = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            AdvancedWarning = new Label();
            SettingsTabControl.SuspendLayout();
            GeneralTab.SuspendLayout();
            MirrorsTab.SuspendLayout();
            ModloaderTab.SuspendLayout();
            AdvancedTab.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // ApplyButton
            // 
            ApplyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ApplyButton.Location = new Point(425, 415);
            ApplyButton.Name = "ApplyButton";
            ApplyButton.Size = new Size(75, 23);
            ApplyButton.TabIndex = 4;
            ApplyButton.Text = "Apply";
            ApplyButton.UseVisualStyleBackColor = true;
            // 
            // SettingsTabControl
            // 
            SettingsTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            SettingsTabControl.Controls.Add(GeneralTab);
            SettingsTabControl.Controls.Add(MirrorsTab);
            SettingsTabControl.Controls.Add(ModloaderTab);
            SettingsTabControl.Controls.Add(AdvancedTab);
            SettingsTabControl.Location = new Point(12, 12);
            SettingsTabControl.Name = "SettingsTabControl";
            SettingsTabControl.SelectedIndex = 0;
            SettingsTabControl.Size = new Size(299, 397);
            SettingsTabControl.TabIndex = 5;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(GeneralCheckbox);
            GeneralTab.Location = new Point(4, 24);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(291, 369);
            GeneralTab.TabIndex = 0;
            GeneralTab.Text = "General";
            GeneralTab.UseVisualStyleBackColor = true;
            // 
            // GeneralCheckbox
            // 
            GeneralCheckbox.FormattingEnabled = true;
            GeneralCheckbox.Items.AddRange(new object[] { "Check for launcher updates automatically", "Check for game updates automatically", "Confirm before deleting instances", "Confirm before overwriting instances", "Run on startup" });
            GeneralCheckbox.Location = new Point(6, 6);
            GeneralCheckbox.Name = "GeneralCheckbox";
            GeneralCheckbox.Size = new Size(257, 346);
            GeneralCheckbox.TabIndex = 0;
            // 
            // MirrorsTab
            // 
            MirrorsTab.Controls.Add(AddLoaderButton);
            MirrorsTab.Controls.Add(button1);
            MirrorsTab.Controls.Add(listView1);
            MirrorsTab.Controls.Add(listView2);
            MirrorsTab.Controls.Add(GamesLabel);
            MirrorsTab.Controls.Add(ModloaderLabel);
            MirrorsTab.Controls.Add(MirrorsHint);
            MirrorsTab.Location = new Point(4, 24);
            MirrorsTab.Name = "MirrorsTab";
            MirrorsTab.Size = new Size(291, 369);
            MirrorsTab.TabIndex = 2;
            MirrorsTab.Text = "Mirrors";
            MirrorsTab.UseVisualStyleBackColor = true;
            // 
            // AddLoaderButton
            // 
            AddLoaderButton.Location = new Point(224, 214);
            AddLoaderButton.Name = "AddLoaderButton";
            AddLoaderButton.Size = new Size(64, 23);
            AddLoaderButton.TabIndex = 5;
            AddLoaderButton.Text = "+ Add";
            AddLoaderButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(224, 56);
            button1.Name = "button1";
            button1.Size = new Size(64, 23);
            button1.TabIndex = 5;
            button1.Text = "+ Add";
            button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Location = new Point(3, 241);
            listView1.Name = "listView1";
            listView1.Size = new Size(285, 123);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            listView2.Location = new Point(3, 85);
            listView2.Name = "listView2";
            listView2.Size = new Size(285, 123);
            listView2.TabIndex = 4;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // GamesLabel
            // 
            GamesLabel.AutoSize = true;
            GamesLabel.Location = new Point(3, 60);
            GamesLabel.Name = "GamesLabel";
            GamesLabel.Size = new Size(46, 15);
            GamesLabel.TabIndex = 2;
            GamesLabel.Text = "Games:";
            // 
            // ModloaderLabel
            // 
            ModloaderLabel.AutoSize = true;
            ModloaderLabel.Location = new Point(3, 219);
            ModloaderLabel.Name = "ModloaderLabel";
            ModloaderLabel.Size = new Size(73, 15);
            ModloaderLabel.TabIndex = 2;
            ModloaderLabel.Text = "Modloaders:";
            // 
            // MirrorsHint
            // 
            MirrorsHint.Location = new Point(3, 3);
            MirrorsHint.Name = "MirrorsHint";
            MirrorsHint.Size = new Size(285, 55);
            MirrorsHint.TabIndex = 0;
            MirrorsHint.Text = "You can change what Github repositories launcher.net downloads games and modloaders from.";
            // 
            // ModloaderTab
            // 
            ModloaderTab.Controls.Add(MLCheckbox);
            ModloaderTab.Location = new Point(4, 24);
            ModloaderTab.Name = "ModloaderTab";
            ModloaderTab.Padding = new Padding(3);
            ModloaderTab.Size = new Size(291, 369);
            ModloaderTab.TabIndex = 1;
            ModloaderTab.Text = "Melonloader";
            ModloaderTab.UseVisualStyleBackColor = true;
            // 
            // MLCheckbox
            // 
            MLCheckbox.FormattingEnabled = true;
            MLCheckbox.Items.AddRange(new object[] { "Enable CI releases", "Prefer stable releases" });
            MLCheckbox.Location = new Point(6, 6);
            MLCheckbox.Name = "MLCheckbox";
            MLCheckbox.Size = new Size(279, 94);
            MLCheckbox.TabIndex = 0;
            // 
            // AdvancedTab
            // 
            AdvancedTab.Controls.Add(AdvancedWarning);
            AdvancedTab.Controls.Add(panel3);
            AdvancedTab.Controls.Add(panel2);
            AdvancedTab.Controls.Add(AdvancedCheckbox);
            AdvancedTab.Location = new Point(4, 24);
            AdvancedTab.Name = "AdvancedTab";
            AdvancedTab.Size = new Size(291, 369);
            AdvancedTab.TabIndex = 3;
            AdvancedTab.Text = "Advanced";
            AdvancedTab.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(3, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(285, 23);
            textBox1.TabIndex = 1;
            // 
            // AdvancedCheckbox
            // 
            AdvancedCheckbox.FormattingEnabled = true;
            AdvancedCheckbox.Items.AddRange(new object[] { "Show debug console", "Enable verbose logging", "Use custom temporary directory", "Use custom install directory" });
            AdvancedCheckbox.Location = new Point(3, 3);
            AdvancedCheckbox.Name = "AdvancedCheckbox";
            AdvancedCheckbox.Size = new Size(285, 364);
            AdvancedCheckbox.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(SelectedHint);
            panel1.Location = new Point(317, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(183, 369);
            panel1.TabIndex = 6;
            // 
            // SelectedHint
            // 
            SelectedHint.Location = new Point(3, 45);
            SelectedHint.Name = "SelectedHint";
            SelectedHint.Size = new Size(177, 148);
            SelectedHint.TabIndex = 0;
            SelectedHint.Text = "Select a setting for more information on what it does.";
            // 
            // panel2
            // 
            panel2.Controls.Add(button2);
            panel2.Controls.Add(TempDirLabel);
            panel2.Controls.Add(textBox1);
            panel2.Location = new Point(3, 79);
            panel2.Name = "panel2";
            panel2.Size = new Size(285, 77);
            panel2.TabIndex = 7;
            // 
            // TempDirLabel
            // 
            TempDirLabel.AutoSize = true;
            TempDirLabel.Location = new Point(3, 3);
            TempDirLabel.Name = "TempDirLabel";
            TempDirLabel.Size = new Size(160, 15);
            TempDirLabel.TabIndex = 2;
            TempDirLabel.Text = "Custom temporary directory:";
            // 
            // button2
            // 
            button2.Location = new Point(3, 50);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(button3);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(textBox2);
            panel3.Location = new Point(3, 162);
            panel3.Name = "panel3";
            panel3.Size = new Size(285, 77);
            panel3.TabIndex = 7;
            // 
            // button3
            // 
            button3.Location = new Point(3, 50);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "Browse";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(136, 15);
            label1.TabIndex = 2;
            label1.Text = "Custom install directory:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(3, 21);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(285, 23);
            textBox2.TabIndex = 1;
            // 
            // AdvancedWarning
            // 
            AdvancedWarning.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AdvancedWarning.Location = new Point(6, 304);
            AdvancedWarning.Name = "AdvancedWarning";
            AdvancedWarning.Size = new Size(282, 63);
            AdvancedWarning.TabIndex = 8;
            AdvancedWarning.Text = "WARNING: Custom directory is expirmental, stability is not guarnteed! Launcher.net may not function properly if you enable the custom directory settings.";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 450);
            Controls.Add(panel1);
            Controls.Add(SettingsTabControl);
            Controls.Add(ApplyButton);
            Name = "SettingsForm";
            Text = "Settings";
            SettingsTabControl.ResumeLayout(false);
            GeneralTab.ResumeLayout(false);
            MirrorsTab.ResumeLayout(false);
            MirrorsTab.PerformLayout();
            ModloaderTab.ResumeLayout(false);
            AdvancedTab.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button ApplyButton;
        private TabControl SettingsTabControl;
        private TabPage GeneralTab;
        private TabPage MirrorsTab;
        private TabPage ModloaderTab;
        private TabPage AdvancedTab;
        private CheckedListBox AdvancedCheckbox;
        private Panel panel1;
        private Label SelectedHint;
        private CheckedListBox GeneralCheckbox;
        private Label MirrorsHint;
        private CheckedListBox MLCheckbox;
        private Button button1;
        private ListView listView1;
        private ListView listView2;
        private Label GamesLabel;
        private Label ModloaderLabel;
        private Button AddLoaderButton;
        private TextBox textBox1;
        private Panel panel2;
        private Button button2;
        private Label TempDirLabel;
        private Label AdvancedWarning;
        private Panel panel3;
        private Button button3;
        private Label label1;
        private TextBox textBox2;
    }
}