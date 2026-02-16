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
            ProvidersTab = new TabPage();
            AddLoaderButton = new Button();
            button1 = new Button();
            LoadersListView = new ListView();
            GamesListView = new ListView();
            GamesLabel = new Label();
            ModloaderLabel = new Label();
            MirrorsHint = new Label();
            ModloaderTab = new TabPage();
            MLCheckbox = new CheckedListBox();
            AdvancedTab = new TabPage();
            AdvancedWarning = new Label();
            CustomInstallDirectoryPanel = new Panel();
            button3 = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            CustomTempDirPanel = new Panel();
            button2 = new Button();
            TempDirLabel = new Label();
            textBox1 = new TextBox();
            AdvancedCheckbox = new CheckedListBox();
            panel1 = new Panel();
            DescriptionLabel = new Label();
            SelectedHint = new Label();
            SettingsTabControl.SuspendLayout();
            GeneralTab.SuspendLayout();
            ProvidersTab.SuspendLayout();
            ModloaderTab.SuspendLayout();
            AdvancedTab.SuspendLayout();
            CustomInstallDirectoryPanel.SuspendLayout();
            CustomTempDirPanel.SuspendLayout();
            panel1.SuspendLayout();
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
            SettingsTabControl.Controls.Add(ProvidersTab);
            SettingsTabControl.Controls.Add(ModloaderTab);
            SettingsTabControl.Controls.Add(AdvancedTab);
            SettingsTabControl.Location = new Point(12, 12);
            SettingsTabControl.Name = "SettingsTabControl";
            SettingsTabControl.SelectedIndex = 0;
            SettingsTabControl.Size = new Size(299, 406);
            SettingsTabControl.TabIndex = 5;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(GeneralCheckbox);
            GeneralTab.Location = new Point(4, 24);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(291, 378);
            GeneralTab.TabIndex = 0;
            GeneralTab.Text = "General";
            GeneralTab.UseVisualStyleBackColor = true;
            // 
            // GeneralCheckbox
            // 
            GeneralCheckbox.FormattingEnabled = true;
            GeneralCheckbox.Items.AddRange(new object[] { "Check for launcher.net updates automatically", "Confirm before deleting instances", "Confirm before overwriting instances", "Run on startup" });
            GeneralCheckbox.Location = new Point(6, 6);
            GeneralCheckbox.Name = "GeneralCheckbox";
            GeneralCheckbox.Size = new Size(279, 364);
            GeneralCheckbox.TabIndex = 0;
            // 
            // ProvidersTab
            // 
            ProvidersTab.Controls.Add(AddLoaderButton);
            ProvidersTab.Controls.Add(button1);
            ProvidersTab.Controls.Add(LoadersListView);
            ProvidersTab.Controls.Add(GamesListView);
            ProvidersTab.Controls.Add(GamesLabel);
            ProvidersTab.Controls.Add(ModloaderLabel);
            ProvidersTab.Controls.Add(MirrorsHint);
            ProvidersTab.Location = new Point(4, 24);
            ProvidersTab.Name = "ProvidersTab";
            ProvidersTab.Size = new Size(291, 378);
            ProvidersTab.TabIndex = 2;
            ProvidersTab.Text = "Providers";
            ProvidersTab.UseVisualStyleBackColor = true;
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
            // LoadersListView
            // 
            LoadersListView.Location = new Point(3, 241);
            LoadersListView.Name = "LoadersListView";
            LoadersListView.Size = new Size(285, 123);
            LoadersListView.TabIndex = 4;
            LoadersListView.UseCompatibleStateImageBehavior = false;
            LoadersListView.View = View.Details;
            // 
            // GamesListView
            // 
            GamesListView.Location = new Point(3, 85);
            GamesListView.Name = "GamesListView";
            GamesListView.Size = new Size(285, 123);
            GamesListView.TabIndex = 4;
            GamesListView.UseCompatibleStateImageBehavior = false;
            GamesListView.View = View.Details;
            // 
            // GamesLabel
            // 
            GamesLabel.AutoSize = true;
            GamesLabel.Location = new Point(3, 58);
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
            ModloaderTab.Size = new Size(291, 378);
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
            MLCheckbox.Size = new Size(279, 364);
            MLCheckbox.TabIndex = 0;
            // 
            // AdvancedTab
            // 
            AdvancedTab.Controls.Add(AdvancedWarning);
            AdvancedTab.Controls.Add(CustomInstallDirectoryPanel);
            AdvancedTab.Controls.Add(CustomTempDirPanel);
            AdvancedTab.Controls.Add(AdvancedCheckbox);
            AdvancedTab.Location = new Point(4, 24);
            AdvancedTab.Name = "AdvancedTab";
            AdvancedTab.Size = new Size(291, 378);
            AdvancedTab.TabIndex = 3;
            AdvancedTab.Text = "Advanced";
            AdvancedTab.UseVisualStyleBackColor = true;
            // 
            // AdvancedWarning
            // 
            AdvancedWarning.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AdvancedWarning.Location = new Point(9, 306);
            AdvancedWarning.Name = "AdvancedWarning";
            AdvancedWarning.Size = new Size(269, 63);
            AdvancedWarning.TabIndex = 8;
            AdvancedWarning.Text = "WARNING: Custom directory is expirmental, stability is not guarnteed! launcher.net may not function properly if you enable the custom directory settings.";
            // 
            // CustomInstallDirectoryPanel
            // 
            CustomInstallDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            CustomInstallDirectoryPanel.Controls.Add(button3);
            CustomInstallDirectoryPanel.Controls.Add(label1);
            CustomInstallDirectoryPanel.Controls.Add(textBox2);
            CustomInstallDirectoryPanel.Location = new Point(3, 162);
            CustomInstallDirectoryPanel.Name = "CustomInstallDirectoryPanel";
            CustomInstallDirectoryPanel.Size = new Size(285, 75);
            CustomInstallDirectoryPanel.TabIndex = 7;
            // 
            // button3
            // 
            button3.Location = new Point(3, 47);
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
            textBox2.Size = new Size(277, 23);
            textBox2.TabIndex = 1;
            // 
            // CustomTempDirPanel
            // 
            CustomTempDirPanel.BorderStyle = BorderStyle.FixedSingle;
            CustomTempDirPanel.Controls.Add(button2);
            CustomTempDirPanel.Controls.Add(TempDirLabel);
            CustomTempDirPanel.Controls.Add(textBox1);
            CustomTempDirPanel.Location = new Point(3, 82);
            CustomTempDirPanel.Name = "CustomTempDirPanel";
            CustomTempDirPanel.Size = new Size(285, 75);
            CustomTempDirPanel.TabIndex = 7;
            // 
            // button2
            // 
            button2.Location = new Point(3, 47);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
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
            // textBox1
            // 
            textBox1.Location = new Point(3, 21);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(277, 23);
            textBox1.TabIndex = 1;
            // 
            // AdvancedCheckbox
            // 
            AdvancedCheckbox.FormattingEnabled = true;
            AdvancedCheckbox.Items.AddRange(new object[] { "Show debug console", "Enable verbose logging", "Use custom temporary directory", "Use custom install directory" });
            AdvancedCheckbox.Location = new Point(3, 3);
            AdvancedCheckbox.Name = "AdvancedCheckbox";
            AdvancedCheckbox.Size = new Size(285, 76);
            AdvancedCheckbox.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(DescriptionLabel);
            panel1.Controls.Add(SelectedHint);
            panel1.Location = new Point(313, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 369);
            panel1.TabIndex = 6;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DescriptionLabel.Location = new Point(0, 30);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(71, 15);
            DescriptionLabel.TabIndex = 1;
            DescriptionLabel.Text = "Description:";
            // 
            // SelectedHint
            // 
            SelectedHint.Location = new Point(0, 45);
            SelectedHint.Name = "SelectedHint";
            SelectedHint.Size = new Size(180, 319);
            SelectedHint.TabIndex = 0;
            SelectedHint.Text = "Select a setting for more information on what it does.";
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
            ProvidersTab.ResumeLayout(false);
            ProvidersTab.PerformLayout();
            ModloaderTab.ResumeLayout(false);
            AdvancedTab.ResumeLayout(false);
            CustomInstallDirectoryPanel.ResumeLayout(false);
            CustomInstallDirectoryPanel.PerformLayout();
            CustomTempDirPanel.ResumeLayout(false);
            CustomTempDirPanel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button ApplyButton;
        private TabControl SettingsTabControl;
        private TabPage GeneralTab;
        private TabPage ProvidersTab;
        private TabPage ModloaderTab;
        private TabPage AdvancedTab;
        private CheckedListBox AdvancedCheckbox;
        private Panel panel1;
        private Label SelectedHint;
        private CheckedListBox GeneralCheckbox;
        private Label MirrorsHint;
        private CheckedListBox MLCheckbox;
        private Button button1;
        private ListView LoadersListView;
        private ListView GamesListView;
        private Label GamesLabel;
        private Label ModloaderLabel;
        private Button AddLoaderButton;
        private TextBox textBox1;
        private Panel CustomTempDirPanel;
        private Button button2;
        private Label TempDirLabel;
        private Label AdvancedWarning;
        private Panel CustomInstallDirectoryPanel;
        private Button button3;
        private Label label1;
        private TextBox textBox2;
        private Label DescriptionLabel;
    }
}