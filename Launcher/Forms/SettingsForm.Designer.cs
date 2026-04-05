namespace launcherdotnet.Launcher.Forms
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
            SaveButton = new Button();
            SettingsTabControl = new TabControl();
            GeneralTab = new TabPage();
            GeneralCheckbox = new CheckedListBox();
            PluginsTab = new TabPage();
            GamePluginsBox = new ListBox();
            PluginsTabApiVersionLabel = new Label();
            button1 = new Button();
            GamesLabel = new Label();
            MirrorsHint = new Label();
            ModloaderTab = new TabPage();
            MLCheckbox = new CheckedListBox();
            AdvancedTab = new TabPage();
            AdvancedCheckbox = new CheckedListBox();
            ThemeTab = new TabPage();
            textModeResolvesTo = new Label();
            textModeResolvesTo2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            TextModeComboBox = new ComboBox();
            ThemeHint = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            SystemThemeButton = new RadioButton();
            LightThemeButton = new RadioButton();
            DarkThemeButton = new RadioButton();
            BlurThemeButton = new RadioButton();
            AcrylicThemeButton = new RadioButton();
            ExtendedFrameThemeButton = new RadioButton();
            ExtendedFrameDarkThemeButton = new RadioButton();
            AboutTab = new TabPage();
            license1 = new Label();
            linkLabel1 = new LinkLabel();
            GithubLink = new LinkLabel();
            MadeBy = new Label();
            LauncherApiVersionLabel = new Label();
            LauncherVersionLabel = new Label();
            LauncherLabel = new Label();
            label2 = new Label();
            LauncherApiLabel = new Label();
            panel1 = new Panel();
            DescriptionLabel = new Label();
            Hint = new Label();
            SettingsTabControl.SuspendLayout();
            GeneralTab.SuspendLayout();
            PluginsTab.SuspendLayout();
            ModloaderTab.SuspendLayout();
            AdvancedTab.SuspendLayout();
            ThemeTab.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            AboutTab.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveButton.Location = new Point(477, 415);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 4;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // SettingsTabControl
            // 
            SettingsTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SettingsTabControl.Controls.Add(GeneralTab);
            SettingsTabControl.Controls.Add(PluginsTab);
            SettingsTabControl.Controls.Add(ModloaderTab);
            SettingsTabControl.Controls.Add(AdvancedTab);
            SettingsTabControl.Controls.Add(ThemeTab);
            SettingsTabControl.Controls.Add(AboutTab);
            SettingsTabControl.Location = new Point(12, 12);
            SettingsTabControl.Name = "SettingsTabControl";
            SettingsTabControl.SelectedIndex = 0;
            SettingsTabControl.Size = new Size(351, 406);
            SettingsTabControl.TabIndex = 5;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(GeneralCheckbox);
            GeneralTab.Location = new Point(4, 24);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(343, 378);
            GeneralTab.TabIndex = 0;
            GeneralTab.Text = "General";
            GeneralTab.UseVisualStyleBackColor = true;
            // 
            // GeneralCheckbox
            // 
            GeneralCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GeneralCheckbox.FormattingEnabled = true;
            GeneralCheckbox.Items.AddRange(new object[] { "Check for launcher.net updates automatically", "Warn if update check failed", "Confirm before deleting instances", "Confirm before overwriting instances", "Run on startup" });
            GeneralCheckbox.Location = new Point(6, 6);
            GeneralCheckbox.Name = "GeneralCheckbox";
            GeneralCheckbox.Size = new Size(331, 364);
            GeneralCheckbox.TabIndex = 0;
            // 
            // PluginsTab
            // 
            PluginsTab.AutoScroll = true;
            PluginsTab.Controls.Add(GamePluginsBox);
            PluginsTab.Controls.Add(PluginsTabApiVersionLabel);
            PluginsTab.Controls.Add(button1);
            PluginsTab.Controls.Add(GamesLabel);
            PluginsTab.Controls.Add(MirrorsHint);
            PluginsTab.Location = new Point(4, 24);
            PluginsTab.Name = "PluginsTab";
            PluginsTab.Size = new Size(343, 378);
            PluginsTab.TabIndex = 2;
            PluginsTab.Text = "Plugins";
            PluginsTab.UseVisualStyleBackColor = true;
            // 
            // GamePluginsBox
            // 
            GamePluginsBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GamePluginsBox.FormattingEnabled = true;
            GamePluginsBox.Location = new Point(0, 83);
            GamePluginsBox.Name = "GamePluginsBox";
            GamePluginsBox.Size = new Size(340, 274);
            GamePluginsBox.TabIndex = 7;
            GamePluginsBox.SelectedIndexChanged += GamePluginsBox_SelectedIndexChanged;
            // 
            // PluginsTabApiVersionLabel
            // 
            PluginsTabApiVersionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            PluginsTabApiVersionLabel.AutoSize = true;
            PluginsTabApiVersionLabel.Location = new Point(3, 360);
            PluginsTabApiVersionLabel.Name = "PluginsTabApiVersionLabel";
            PluginsTabApiVersionLabel.Size = new Size(102, 15);
            PluginsTabApiVersionLabel.TabIndex = 6;
            PluginsTabApiVersionLabel.Text = "API version: v0.0.0";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(197, 58);
            button1.Name = "button1";
            button1.Size = new Size(143, 23);
            button1.TabIndex = 5;
            button1.Text = "Open plugins folder...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            // MirrorsHint
            // 
            MirrorsHint.Location = new Point(3, 3);
            MirrorsHint.Name = "MirrorsHint";
            MirrorsHint.Size = new Size(285, 55);
            MirrorsHint.TabIndex = 0;
            MirrorsHint.Text = "Plugins are custom installation scripts that tell launcher.net how to install games and modloaders. Only game installers are currently supported.";
            // 
            // ModloaderTab
            // 
            ModloaderTab.Controls.Add(MLCheckbox);
            ModloaderTab.Location = new Point(4, 24);
            ModloaderTab.Name = "ModloaderTab";
            ModloaderTab.Padding = new Padding(3);
            ModloaderTab.Size = new Size(343, 378);
            ModloaderTab.TabIndex = 1;
            ModloaderTab.Text = "Melonloader";
            ModloaderTab.UseVisualStyleBackColor = true;
            // 
            // MLCheckbox
            // 
            MLCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MLCheckbox.FormattingEnabled = true;
            MLCheckbox.Items.AddRange(new object[] { "Enable CI releases", "Prefer stable releases" });
            MLCheckbox.Location = new Point(6, 6);
            MLCheckbox.Name = "MLCheckbox";
            MLCheckbox.Size = new Size(331, 364);
            MLCheckbox.TabIndex = 0;
            // 
            // AdvancedTab
            // 
            AdvancedTab.Controls.Add(AdvancedCheckbox);
            AdvancedTab.Location = new Point(4, 24);
            AdvancedTab.Name = "AdvancedTab";
            AdvancedTab.Size = new Size(343, 378);
            AdvancedTab.TabIndex = 3;
            AdvancedTab.Text = "Advanced";
            AdvancedTab.UseVisualStyleBackColor = true;
            // 
            // AdvancedCheckbox
            // 
            AdvancedCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AdvancedCheckbox.FormattingEnabled = true;
            AdvancedCheckbox.Items.AddRange(new object[] { "Show debug console", "Enable verbose logging", "Disable plugin version check" });
            AdvancedCheckbox.Location = new Point(3, 3);
            AdvancedCheckbox.Name = "AdvancedCheckbox";
            AdvancedCheckbox.Size = new Size(337, 58);
            AdvancedCheckbox.TabIndex = 0;
            // 
            // ThemeTab
            // 
            ThemeTab.Controls.Add(textModeResolvesTo);
            ThemeTab.Controls.Add(textModeResolvesTo2);
            ThemeTab.Controls.Add(label4);
            ThemeTab.Controls.Add(label3);
            ThemeTab.Controls.Add(label1);
            ThemeTab.Controls.Add(TextModeComboBox);
            ThemeTab.Controls.Add(ThemeHint);
            ThemeTab.Controls.Add(flowLayoutPanel1);
            ThemeTab.Location = new Point(4, 24);
            ThemeTab.Name = "ThemeTab";
            ThemeTab.Padding = new Padding(3);
            ThemeTab.Size = new Size(343, 378);
            ThemeTab.TabIndex = 5;
            ThemeTab.Text = "Themes";
            ThemeTab.UseVisualStyleBackColor = true;
            // 
            // textModeResolvesTo
            // 
            textModeResolvesTo.AutoSize = true;
            textModeResolvesTo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textModeResolvesTo.Location = new Point(9, 329);
            textModeResolvesTo.Name = "textModeResolvesTo";
            textModeResolvesTo.Size = new Size(39, 15);
            textModeResolvesTo.TabIndex = 6;
            textModeResolvesTo.Text = "label5";
            // 
            // textModeResolvesTo2
            // 
            textModeResolvesTo2.Location = new Point(9, 296);
            textModeResolvesTo2.Name = "textModeResolvesTo2";
            textModeResolvesTo2.Size = new Size(328, 33);
            textModeResolvesTo2.TabIndex = 5;
            textModeResolvesTo2.Text = "This text mode resolves to:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 42);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 4;
            label4.Text = "Theme";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 252);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 4;
            label3.Text = "Text render mode";
            // 
            // label1
            // 
            label1.Location = new Point(6, 201);
            label1.Name = "label1";
            label1.Size = new Size(331, 51);
            label1.TabIndex = 3;
            label1.Text = "Transparent background can cause text anti-aliasing issues. To fix this, launcher.NET has a custom text drawing implementation you can enable.";
            // 
            // TextModeComboBox
            // 
            TextModeComboBox.FormattingEnabled = true;
            TextModeComboBox.Items.AddRange(new object[] { "Auto", "Auto (strict)", "TextRenderer (default Winforms)", "Shadow Text (launcher.NET)" });
            TextModeComboBox.Location = new Point(9, 270);
            TextModeComboBox.Name = "TextModeComboBox";
            TextModeComboBox.Size = new Size(203, 23);
            TextModeComboBox.TabIndex = 2;
            TextModeComboBox.SelectedIndexChanged += TextModeComboBox_SelectedIndexChanged;
            // 
            // ThemeHint
            // 
            ThemeHint.Location = new Point(6, 6);
            ThemeHint.Margin = new Padding(3, 3, 3, 0);
            ThemeHint.Name = "ThemeHint";
            ThemeHint.Size = new Size(331, 36);
            ThemeHint.TabIndex = 1;
            ThemeHint.Text = "launcher.NET has multiple themes you can choose from. Pick the one that best suits you.\r\n";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(SystemThemeButton);
            flowLayoutPanel1.Controls.Add(LightThemeButton);
            flowLayoutPanel1.Controls.Add(DarkThemeButton);
            flowLayoutPanel1.Controls.Add(BlurThemeButton);
            flowLayoutPanel1.Controls.Add(AcrylicThemeButton);
            flowLayoutPanel1.Controls.Add(ExtendedFrameThemeButton);
            flowLayoutPanel1.Controls.Add(ExtendedFrameDarkThemeButton);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(6, 57);
            flowLayoutPanel1.Margin = new Padding(3, 0, 3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(186, 141);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // SystemThemeButton
            // 
            SystemThemeButton.AutoSize = true;
            SystemThemeButton.Location = new Point(3, 0);
            SystemThemeButton.Margin = new Padding(3, 0, 3, 0);
            SystemThemeButton.Name = "SystemThemeButton";
            SystemThemeButton.Size = new Size(63, 19);
            SystemThemeButton.TabIndex = 0;
            SystemThemeButton.TabStop = true;
            SystemThemeButton.Text = "System";
            SystemThemeButton.UseVisualStyleBackColor = true;
            SystemThemeButton.CheckedChanged += SystemThemeButton_CheckedChanged;
            // 
            // LightThemeButton
            // 
            LightThemeButton.AutoSize = true;
            LightThemeButton.Location = new Point(3, 19);
            LightThemeButton.Margin = new Padding(3, 0, 3, 0);
            LightThemeButton.Name = "LightThemeButton";
            LightThemeButton.Size = new Size(52, 19);
            LightThemeButton.TabIndex = 0;
            LightThemeButton.TabStop = true;
            LightThemeButton.Text = "Light";
            LightThemeButton.UseVisualStyleBackColor = true;
            LightThemeButton.CheckedChanged += LightThemeButton_CheckedChanged;
            // 
            // DarkThemeButton
            // 
            DarkThemeButton.AutoSize = true;
            DarkThemeButton.Location = new Point(3, 38);
            DarkThemeButton.Margin = new Padding(3, 0, 3, 0);
            DarkThemeButton.Name = "DarkThemeButton";
            DarkThemeButton.Size = new Size(49, 19);
            DarkThemeButton.TabIndex = 0;
            DarkThemeButton.TabStop = true;
            DarkThemeButton.Text = "Dark";
            DarkThemeButton.UseVisualStyleBackColor = true;
            DarkThemeButton.CheckedChanged += DarkThemeButton_CheckedChanged;
            // 
            // BlurThemeButton
            // 
            BlurThemeButton.AutoSize = true;
            BlurThemeButton.Location = new Point(3, 57);
            BlurThemeButton.Margin = new Padding(3, 0, 3, 0);
            BlurThemeButton.Name = "BlurThemeButton";
            BlurThemeButton.Size = new Size(130, 19);
            BlurThemeButton.TabIndex = 0;
            BlurThemeButton.TabStop = true;
            BlurThemeButton.Text = "Blurred background";
            BlurThemeButton.UseVisualStyleBackColor = true;
            BlurThemeButton.CheckedChanged += BlurThemeButton_CheckedChanged;
            // 
            // AcrylicThemeButton
            // 
            AcrylicThemeButton.AutoSize = true;
            AcrylicThemeButton.Location = new Point(3, 76);
            AcrylicThemeButton.Margin = new Padding(3, 0, 3, 0);
            AcrylicThemeButton.Name = "AcrylicThemeButton";
            AcrylicThemeButton.Size = new Size(61, 19);
            AcrylicThemeButton.TabIndex = 0;
            AcrylicThemeButton.TabStop = true;
            AcrylicThemeButton.Text = "Acrylic";
            AcrylicThemeButton.UseVisualStyleBackColor = true;
            AcrylicThemeButton.CheckedChanged += AcrylicThemeButton_CheckedChanged;
            // 
            // ExtendedFrameThemeButton
            // 
            ExtendedFrameThemeButton.AutoSize = true;
            ExtendedFrameThemeButton.Location = new Point(3, 95);
            ExtendedFrameThemeButton.Margin = new Padding(3, 0, 3, 0);
            ExtendedFrameThemeButton.Name = "ExtendedFrameThemeButton";
            ExtendedFrameThemeButton.Size = new Size(108, 19);
            ExtendedFrameThemeButton.TabIndex = 0;
            ExtendedFrameThemeButton.TabStop = true;
            ExtendedFrameThemeButton.Text = "Extended frame";
            ExtendedFrameThemeButton.UseVisualStyleBackColor = true;
            ExtendedFrameThemeButton.CheckedChanged += ExtendedFrameThemeButton_CheckedChanged;
            // 
            // ExtendedFrameDarkThemeButton
            // 
            ExtendedFrameDarkThemeButton.AutoSize = true;
            ExtendedFrameDarkThemeButton.Location = new Point(3, 114);
            ExtendedFrameDarkThemeButton.Margin = new Padding(3, 0, 3, 0);
            ExtendedFrameDarkThemeButton.Name = "ExtendedFrameDarkThemeButton";
            ExtendedFrameDarkThemeButton.Size = new Size(142, 19);
            ExtendedFrameDarkThemeButton.TabIndex = 0;
            ExtendedFrameDarkThemeButton.TabStop = true;
            ExtendedFrameDarkThemeButton.Text = "Extended frame (dark)";
            ExtendedFrameDarkThemeButton.UseVisualStyleBackColor = true;
            ExtendedFrameDarkThemeButton.CheckedChanged += ExtendedFrameDarkThemeButton_CheckedChanged;
            // 
            // AboutTab
            // 
            AboutTab.Controls.Add(license1);
            AboutTab.Controls.Add(linkLabel1);
            AboutTab.Controls.Add(GithubLink);
            AboutTab.Controls.Add(MadeBy);
            AboutTab.Controls.Add(LauncherApiVersionLabel);
            AboutTab.Controls.Add(LauncherVersionLabel);
            AboutTab.Controls.Add(LauncherLabel);
            AboutTab.Controls.Add(label2);
            AboutTab.Controls.Add(LauncherApiLabel);
            AboutTab.Location = new Point(4, 24);
            AboutTab.Name = "AboutTab";
            AboutTab.Padding = new Padding(3);
            AboutTab.Size = new Size(343, 378);
            AboutTab.TabIndex = 4;
            AboutTab.Text = "About";
            AboutTab.UseVisualStyleBackColor = true;
            // 
            // license1
            // 
            license1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            license1.Location = new Point(3, 253);
            license1.Name = "license1";
            license1.Size = new Size(251, 60);
            license1.TabIndex = 9;
            license1.Text = "This software is licensed to you under the GNU General Public License v3.0.\r\nIf a copy of this license was not distributed to you alonside this software, visit";
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(3, 313);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(251, 15);
            linkLabel1.TabIndex = 8;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://www.gnu.org/licenses/gpl-3.0.en.html";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // GithubLink
            // 
            GithubLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            GithubLink.AutoSize = true;
            GithubLink.Location = new Point(3, 354);
            GithubLink.Name = "GithubLink";
            GithubLink.Size = new Size(272, 15);
            GithubLink.TabIndex = 8;
            GithubLink.TabStop = true;
            GithubLink.Text = "https://github.com/Gameknight963/launcher.NET";
            GithubLink.LinkClicked += GithubLink_LinkClicked;
            // 
            // MadeBy
            // 
            MadeBy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MadeBy.AutoSize = true;
            MadeBy.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MadeBy.Location = new Point(3, 339);
            MadeBy.Name = "MadeBy";
            MadeBy.Size = new Size(178, 15);
            MadeBy.TabIndex = 6;
            MadeBy.Text = "launcher.NET by gameknight963";
            // 
            // LauncherApiVersionLabel
            // 
            LauncherApiVersionLabel.AutoSize = true;
            LauncherApiVersionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LauncherApiVersionLabel.Location = new Point(3, 75);
            LauncherApiVersionLabel.Name = "LauncherApiVersionLabel";
            LauncherApiVersionLabel.Size = new Size(37, 15);
            LauncherApiVersionLabel.TabIndex = 6;
            LauncherApiVersionLabel.Text = "v0.0.0";
            // 
            // LauncherVersionLabel
            // 
            LauncherVersionLabel.AutoSize = true;
            LauncherVersionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LauncherVersionLabel.Location = new Point(3, 34);
            LauncherVersionLabel.Name = "LauncherVersionLabel";
            LauncherVersionLabel.Size = new Size(37, 15);
            LauncherVersionLabel.TabIndex = 6;
            LauncherVersionLabel.Text = "v0.0.0";
            // 
            // LauncherLabel
            // 
            LauncherLabel.AutoSize = true;
            LauncherLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LauncherLabel.Location = new Point(3, 19);
            LauncherLabel.Name = "LauncherLabel";
            LauncherLabel.Size = new Size(77, 15);
            LauncherLabel.TabIndex = 6;
            LauncherLabel.Text = "launcher.NET";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 45);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 6;
            // 
            // LauncherApiLabel
            // 
            LauncherApiLabel.AutoSize = true;
            LauncherApiLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LauncherApiLabel.Location = new Point(3, 60);
            LauncherApiLabel.Name = "LauncherApiLabel";
            LauncherApiLabel.Size = new Size(136, 15);
            LauncherApiLabel.TabIndex = 6;
            LauncherApiLabel.Text = "launcher.NET Plugin API";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(DescriptionLabel);
            panel1.Controls.Add(Hint);
            panel1.Location = new Point(365, 36);
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
            // Hint
            // 
            Hint.Location = new Point(0, 45);
            Hint.Name = "Hint";
            Hint.Size = new Size(180, 319);
            Hint.TabIndex = 0;
            Hint.Text = "Select a setting for more information on what it does.";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 450);
            Controls.Add(panel1);
            Controls.Add(SettingsTabControl);
            Controls.Add(SaveButton);
            ForeColor = Color.Black;
            Name = "SettingsForm";
            Text = "Settings";
            SettingsTabControl.ResumeLayout(false);
            GeneralTab.ResumeLayout(false);
            PluginsTab.ResumeLayout(false);
            PluginsTab.PerformLayout();
            ModloaderTab.ResumeLayout(false);
            AdvancedTab.ResumeLayout(false);
            ThemeTab.ResumeLayout(false);
            ThemeTab.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            AboutTab.ResumeLayout(false);
            AboutTab.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button SaveButton;
        private TabControl SettingsTabControl;
        private TabPage GeneralTab;
        private TabPage PluginsTab;
        private TabPage ModloaderTab;
        private TabPage AdvancedTab;
        private CheckedListBox AdvancedCheckbox;
        private Panel panel1;
        private Label Hint;
        private CheckedListBox GeneralCheckbox;
        private Label MirrorsHint;
        private CheckedListBox MLCheckbox;
        private Button button1;
        private Label GamesLabel;
        private Label DescriptionLabel;
        private Label LauncherApiLabel;
        private TabPage AboutTab;
        private Label LauncherVersionLabel;
        private Label LauncherLabel;
        private Label label2;
        private Label LauncherApiVersionLabel;
        private Label PluginsTabApiVersionLabel;
        private ListBox GamePluginsBox;
        private LinkLabel GithubLink;
        private Label MadeBy;
        private Label license1;
        private LinkLabel linkLabel1;
        private TabPage ThemeTab;
        private FlowLayoutPanel flowLayoutPanel1;
        private RadioButton SystemThemeButton;
        private RadioButton LightThemeButton;
        private Label ThemeHint;
        private RadioButton DarkThemeButton;
        private RadioButton BlurThemeButton;
        private RadioButton AcrylicThemeButton;
        private RadioButton ExtendedFrameThemeButton;
        private RadioButton ExtendedFrameDarkThemeButton;
        private ComboBox TextModeComboBox;
        private Label label4;
        private Label label3;
        private Label label1;
        private Label textModeResolvesTo2;
        private Label textModeResolvesTo;
    }
}