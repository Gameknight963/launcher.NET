using Cyotek.Windows.Forms;

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
            AdvancedTab = new TabPage();
            gcCollectBtn = new Button();
            AdvancedCheckbox = new CheckedListBox();
            ThemeTab = new TabPage();
            useVisualStylesCheckBox = new CheckBox();
            colorButton = new Button();
            gradientColorBox = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            ThemeHint = new Label();
            themeButtonsFlowLayoutPanel = new FlowLayoutPanel();
            systemThemeButton = new RadioButton();
            lightThemeButton = new RadioButton();
            darkThemeButton = new RadioButton();
            blurThemeButton = new RadioButton();
            acrylicThemeButton = new RadioButton();
            extendedFrameThemeButton = new RadioButton();
            extendedFrameDarkThemeButton = new RadioButton();
            transparentGradientButton = new RadioButton();
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
            panel1 = new FlowLayoutPanel();
            Hint = new Label();
            openPluginSettingsBtn = new Button();
            DescriptionLabel = new Label();
            SettingsTabControl.SuspendLayout();
            GeneralTab.SuspendLayout();
            PluginsTab.SuspendLayout();
            AdvancedTab.SuspendLayout();
            ThemeTab.SuspendLayout();
            themeButtonsFlowLayoutPanel.SuspendLayout();
            AboutTab.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveButton.Location = new Point(477, 422);
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
            SettingsTabControl.Controls.Add(AdvancedTab);
            SettingsTabControl.Controls.Add(ThemeTab);
            SettingsTabControl.Controls.Add(AboutTab);
            SettingsTabControl.Location = new Point(12, 12);
            SettingsTabControl.Name = "SettingsTabControl";
            SettingsTabControl.SelectedIndex = 0;
            SettingsTabControl.Size = new Size(351, 413);
            SettingsTabControl.TabIndex = 5;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(GeneralCheckbox);
            GeneralTab.Location = new Point(4, 24);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(343, 385);
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
            PluginsTab.Size = new Size(343, 385);
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
            PluginsTabApiVersionLabel.Location = new Point(3, 361);
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
            GamesLabel.Location = new Point(3, 62);
            GamesLabel.Name = "GamesLabel";
            GamesLabel.Size = new Size(96, 15);
            GamesLabel.TabIndex = 2;
            GamesLabel.Text = "Installed plugins:";
            // 
            // MirrorsHint
            // 
            MirrorsHint.Location = new Point(3, 3);
            MirrorsHint.Name = "MirrorsHint";
            MirrorsHint.Size = new Size(337, 55);
            MirrorsHint.TabIndex = 0;
            MirrorsHint.Text = "Plugins are custom code that can add additional functionality or tell launcher.net how to install games.";
            // 
            // AdvancedTab
            // 
            AdvancedTab.Controls.Add(gcCollectBtn);
            AdvancedTab.Controls.Add(AdvancedCheckbox);
            AdvancedTab.Location = new Point(4, 24);
            AdvancedTab.Name = "AdvancedTab";
            AdvancedTab.Size = new Size(343, 385);
            AdvancedTab.TabIndex = 3;
            AdvancedTab.Text = "Advanced";
            AdvancedTab.UseVisualStyleBackColor = true;
            // 
            // gcCollectBtn
            // 
            gcCollectBtn.Location = new Point(3, 85);
            gcCollectBtn.Name = "gcCollectBtn";
            gcCollectBtn.Size = new Size(109, 23);
            gcCollectBtn.TabIndex = 1;
            gcCollectBtn.Text = "Collect garbage";
            gcCollectBtn.UseVisualStyleBackColor = true;
            gcCollectBtn.Click += gcCollectBtn_Click;
            // 
            // AdvancedCheckbox
            // 
            AdvancedCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AdvancedCheckbox.FormattingEnabled = true;
            AdvancedCheckbox.Items.AddRange(new object[] { "Show debug console", "Enable verbose logging", "Disable plugin version check", "Disable IPv6 (requires restart)" });
            AdvancedCheckbox.Location = new Point(3, 3);
            AdvancedCheckbox.Name = "AdvancedCheckbox";
            AdvancedCheckbox.Size = new Size(337, 76);
            AdvancedCheckbox.TabIndex = 0;
            // 
            // ThemeTab
            // 
            ThemeTab.Controls.Add(useVisualStylesCheckBox);
            ThemeTab.Controls.Add(colorButton);
            ThemeTab.Controls.Add(gradientColorBox);
            ThemeTab.Controls.Add(label3);
            ThemeTab.Controls.Add(label4);
            ThemeTab.Controls.Add(label1);
            ThemeTab.Controls.Add(ThemeHint);
            ThemeTab.Controls.Add(themeButtonsFlowLayoutPanel);
            ThemeTab.Location = new Point(4, 24);
            ThemeTab.Name = "ThemeTab";
            ThemeTab.Padding = new Padding(3);
            ThemeTab.Size = new Size(343, 385);
            ThemeTab.TabIndex = 5;
            ThemeTab.Text = "Themes";
            ThemeTab.UseVisualStyleBackColor = true;
            // 
            // useVisualStylesCheckBox
            // 
            useVisualStylesCheckBox.AutoSize = true;
            useVisualStylesCheckBox.Location = new Point(6, 321);
            useVisualStylesCheckBox.Name = "useVisualStylesCheckBox";
            useVisualStylesCheckBox.Size = new Size(126, 19);
            useVisualStylesCheckBox.TabIndex = 7;
            useVisualStylesCheckBox.Text = "Enable visual styles";
            useVisualStylesCheckBox.UseVisualStyleBackColor = true;
            useVisualStylesCheckBox.CheckedChanged += UseVisualStylesCheckBox_CheckedChanged;
            // 
            // colorButton
            // 
            colorButton.Location = new Point(145, 279);
            colorButton.Name = "colorButton";
            colorButton.Size = new Size(86, 23);
            colorButton.TabIndex = 6;
            colorButton.Text = "Pick a color";
            colorButton.UseVisualStyleBackColor = true;
            colorButton.Click += colorButton_Click;
            // 
            // gradientColorBox
            // 
            gradientColorBox.Location = new Point(6, 280);
            gradientColorBox.Name = "gradientColorBox";
            gradientColorBox.PlaceholderText = "0x66000000";
            gradientColorBox.Size = new Size(133, 23);
            gradientColorBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 262);
            label3.Margin = new Padding(3, 3, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 4;
            label3.Text = "Gradient color";
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
            // label1
            // 
            label1.Location = new Point(6, 223);
            label1.Margin = new Padding(3, 3, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(331, 36);
            label1.TabIndex = 1;
            label1.Text = "The background color of transparent themes is determined by the gradient color. Change it here.";
            // 
            // ThemeHint
            // 
            ThemeHint.Location = new Point(6, 6);
            ThemeHint.Margin = new Padding(3, 3, 3, 0);
            ThemeHint.Name = "ThemeHint";
            ThemeHint.Size = new Size(331, 36);
            ThemeHint.TabIndex = 1;
            ThemeHint.Text = "launcher.net has multiple themes you can choose from. Pick the one that best suits you.\r\n";
            // 
            // themeButtonsFlowLayoutPanel
            // 
            themeButtonsFlowLayoutPanel.Controls.Add(systemThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(lightThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(darkThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(blurThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(acrylicThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(extendedFrameThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(extendedFrameDarkThemeButton);
            themeButtonsFlowLayoutPanel.Controls.Add(transparentGradientButton);
            themeButtonsFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            themeButtonsFlowLayoutPanel.Location = new Point(6, 57);
            themeButtonsFlowLayoutPanel.Margin = new Padding(3, 0, 3, 3);
            themeButtonsFlowLayoutPanel.Name = "themeButtonsFlowLayoutPanel";
            themeButtonsFlowLayoutPanel.Size = new Size(186, 162);
            themeButtonsFlowLayoutPanel.TabIndex = 0;
            // 
            // systemThemeButton
            // 
            systemThemeButton.AutoSize = true;
            systemThemeButton.Location = new Point(3, 0);
            systemThemeButton.Margin = new Padding(3, 0, 3, 0);
            systemThemeButton.Name = "systemThemeButton";
            systemThemeButton.Size = new Size(63, 19);
            systemThemeButton.TabIndex = 0;
            systemThemeButton.TabStop = true;
            systemThemeButton.Text = "System";
            systemThemeButton.UseVisualStyleBackColor = true;
            systemThemeButton.CheckedChanged += SystemThemeButton_CheckedChanged;
            // 
            // lightThemeButton
            // 
            lightThemeButton.AutoSize = true;
            lightThemeButton.Location = new Point(3, 19);
            lightThemeButton.Margin = new Padding(3, 0, 3, 0);
            lightThemeButton.Name = "lightThemeButton";
            lightThemeButton.Size = new Size(52, 19);
            lightThemeButton.TabIndex = 0;
            lightThemeButton.TabStop = true;
            lightThemeButton.Text = "Light";
            lightThemeButton.UseVisualStyleBackColor = true;
            lightThemeButton.CheckedChanged += LightThemeButton_CheckedChanged;
            // 
            // darkThemeButton
            // 
            darkThemeButton.AutoSize = true;
            darkThemeButton.Location = new Point(3, 38);
            darkThemeButton.Margin = new Padding(3, 0, 3, 0);
            darkThemeButton.Name = "darkThemeButton";
            darkThemeButton.Size = new Size(49, 19);
            darkThemeButton.TabIndex = 0;
            darkThemeButton.TabStop = true;
            darkThemeButton.Text = "Dark";
            darkThemeButton.UseVisualStyleBackColor = true;
            darkThemeButton.CheckedChanged += DarkThemeButton_CheckedChanged;
            // 
            // blurThemeButton
            // 
            blurThemeButton.AutoSize = true;
            blurThemeButton.Location = new Point(3, 57);
            blurThemeButton.Margin = new Padding(3, 0, 3, 0);
            blurThemeButton.Name = "blurThemeButton";
            blurThemeButton.Size = new Size(130, 19);
            blurThemeButton.TabIndex = 0;
            blurThemeButton.TabStop = true;
            blurThemeButton.Text = "Blurred background";
            blurThemeButton.UseVisualStyleBackColor = true;
            blurThemeButton.CheckedChanged += BlurThemeButton_CheckedChanged;
            // 
            // acrylicThemeButton
            // 
            acrylicThemeButton.AutoSize = true;
            acrylicThemeButton.Location = new Point(3, 76);
            acrylicThemeButton.Margin = new Padding(3, 0, 3, 0);
            acrylicThemeButton.Name = "acrylicThemeButton";
            acrylicThemeButton.Size = new Size(61, 19);
            acrylicThemeButton.TabIndex = 0;
            acrylicThemeButton.TabStop = true;
            acrylicThemeButton.Text = "Acrylic";
            acrylicThemeButton.UseVisualStyleBackColor = true;
            acrylicThemeButton.CheckedChanged += AcrylicThemeButton_CheckedChanged;
            // 
            // extendedFrameThemeButton
            // 
            extendedFrameThemeButton.AutoSize = true;
            extendedFrameThemeButton.Location = new Point(3, 95);
            extendedFrameThemeButton.Margin = new Padding(3, 0, 3, 0);
            extendedFrameThemeButton.Name = "extendedFrameThemeButton";
            extendedFrameThemeButton.Size = new Size(108, 19);
            extendedFrameThemeButton.TabIndex = 0;
            extendedFrameThemeButton.TabStop = true;
            extendedFrameThemeButton.Text = "Extended frame";
            extendedFrameThemeButton.UseVisualStyleBackColor = true;
            extendedFrameThemeButton.CheckedChanged += ExtendedFrameThemeButton_CheckedChanged;
            // 
            // extendedFrameDarkThemeButton
            // 
            extendedFrameDarkThemeButton.AutoSize = true;
            extendedFrameDarkThemeButton.Location = new Point(3, 114);
            extendedFrameDarkThemeButton.Margin = new Padding(3, 0, 3, 0);
            extendedFrameDarkThemeButton.Name = "extendedFrameDarkThemeButton";
            extendedFrameDarkThemeButton.Size = new Size(142, 19);
            extendedFrameDarkThemeButton.TabIndex = 0;
            extendedFrameDarkThemeButton.TabStop = true;
            extendedFrameDarkThemeButton.Text = "Extended frame (dark)";
            extendedFrameDarkThemeButton.UseVisualStyleBackColor = true;
            extendedFrameDarkThemeButton.CheckedChanged += ExtendedFrameDarkThemeButton_CheckedChanged;
            // 
            // transparentGradientButton
            // 
            transparentGradientButton.AutoSize = true;
            transparentGradientButton.Location = new Point(3, 133);
            transparentGradientButton.Margin = new Padding(3, 0, 3, 0);
            transparentGradientButton.Name = "transparentGradientButton";
            transparentGradientButton.Size = new Size(133, 19);
            transparentGradientButton.TabIndex = 1;
            transparentGradientButton.TabStop = true;
            transparentGradientButton.Text = "Transparent gradient";
            transparentGradientButton.UseVisualStyleBackColor = true;
            transparentGradientButton.CheckedChanged += TransparentGradientButton_CheckedChanged;
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
            AboutTab.Size = new Size(343, 385);
            AboutTab.TabIndex = 4;
            AboutTab.Text = "About";
            AboutTab.UseVisualStyleBackColor = true;
            // 
            // license1
            // 
            license1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            license1.Location = new Point(3, 263);
            license1.Name = "license1";
            license1.Size = new Size(251, 60);
            license1.TabIndex = 9;
            license1.Text = "This software is licensed to you under the GNU General Public License v3.0.\r\nIf a copy of this license was not distributed to you alonside this software, visit";
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(3, 323);
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
            GithubLink.Location = new Point(3, 364);
            GithubLink.Name = "GithubLink";
            GithubLink.Size = new Size(268, 15);
            GithubLink.TabIndex = 8;
            GithubLink.TabStop = true;
            GithubLink.Text = "https://github.com/Gameknight963/launcher.net";
            GithubLink.LinkClicked += GithubLink_LinkClicked;
            // 
            // MadeBy
            // 
            MadeBy.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            MadeBy.AutoSize = true;
            MadeBy.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MadeBy.Location = new Point(3, 349);
            MadeBy.Name = "MadeBy";
            MadeBy.Size = new Size(174, 15);
            MadeBy.TabIndex = 6;
            MadeBy.Text = "launcher.net by gameknight963";
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
            LauncherLabel.Size = new Size(72, 15);
            LauncherLabel.TabIndex = 6;
            LauncherLabel.Text = "launcher.net";
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
            LauncherApiLabel.Size = new Size(131, 15);
            LauncherApiLabel.TabIndex = 6;
            LauncherApiLabel.Text = "launcher.net Plugin API";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(Hint);
            panel1.Controls.Add(openPluginSettingsBtn);
            panel1.Location = new Point(365, 82);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 330);
            panel1.TabIndex = 6;
            // 
            // Hint
            // 
            Hint.AutoSize = true;
            Hint.Location = new Point(3, 3);
            Hint.Margin = new Padding(3, 3, 3, 0);
            Hint.Name = "Hint";
            Hint.Size = new Size(157, 30);
            Hint.TabIndex = 0;
            Hint.Text = "Select a setting for more information on what it does.";
            // 
            // openPluginSettingsBtn
            // 
            openPluginSettingsBtn.Location = new Point(3, 36);
            openPluginSettingsBtn.Name = "openPluginSettingsBtn";
            openPluginSettingsBtn.Size = new Size(145, 23);
            openPluginSettingsBtn.TabIndex = 7;
            openPluginSettingsBtn.Text = "Open Plugin Settings";
            openPluginSettingsBtn.UseVisualStyleBackColor = true;
            openPluginSettingsBtn.Click += OpenPluginSettingsBtn_Click;
            // 
            // DescriptionLabel
            // 
            DescriptionLabel.AutoSize = true;
            DescriptionLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DescriptionLabel.Location = new Point(369, 64);
            DescriptionLabel.Name = "DescriptionLabel";
            DescriptionLabel.Size = new Size(71, 15);
            DescriptionLabel.TabIndex = 1;
            DescriptionLabel.Text = "Description:";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 457);
            Controls.Add(panel1);
            Controls.Add(DescriptionLabel);
            Controls.Add(SettingsTabControl);
            Controls.Add(SaveButton);
            ForeColor = Color.Black;
            Name = "SettingsForm";
            Text = "Settings";
            SettingsTabControl.ResumeLayout(false);
            GeneralTab.ResumeLayout(false);
            PluginsTab.ResumeLayout(false);
            PluginsTab.PerformLayout();
            AdvancedTab.ResumeLayout(false);
            ThemeTab.ResumeLayout(false);
            ThemeTab.PerformLayout();
            themeButtonsFlowLayoutPanel.ResumeLayout(false);
            themeButtonsFlowLayoutPanel.PerformLayout();
            AboutTab.ResumeLayout(false);
            AboutTab.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button SaveButton;
        private TabControl SettingsTabControl;
        private TabPage GeneralTab;
        private TabPage PluginsTab;
        private TabPage AdvancedTab;
        private CheckedListBox AdvancedCheckbox;
        private FlowLayoutPanel panel1;
        private Label Hint;
        private CheckedListBox GeneralCheckbox;
        private Label MirrorsHint;
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
        private FlowLayoutPanel themeButtonsFlowLayoutPanel;
        private RadioButton systemThemeButton;
        private RadioButton lightThemeButton;
        private Label ThemeHint;
        private RadioButton darkThemeButton;
        private RadioButton blurThemeButton;
        private RadioButton acrylicThemeButton;
        private RadioButton extendedFrameThemeButton;
        private RadioButton extendedFrameDarkThemeButton;
        private Label label4;
        private RadioButton transparentGradientButton;
        private TextBox gradientColorBox;
        private Label label1;
        private Label label3;
        private Button colorButton;
        private Button openPluginSettingsBtn;
        private Button gcCollectBtn;
        private CheckBox useVisualStylesCheckBox;
    }
}