using Cyotek.Windows.Forms;

namespace launcherdotnet.Launcher.Controls
{
    partial class HslaColorEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            hueColorSlider = new HueColorSlider();
            saturationColorSlider = new SaturationColorSlider();
            lightnessColorSlider = new LightnessColorSlider();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lightnessNumericUpDown = new NumericUpDown();
            saturationNumericUpDown = new NumericUpDown();
            hueNumericUpDown = new NumericUpDown();
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            alphaColorSlider = new RgbaColorSlider();
            alphaNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)lightnessNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)saturationNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hueNumericUpDown).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)alphaNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // hueColorSlider
            // 
            hueColorSlider.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            hueColorSlider.Location = new Point(33, 4);
            hueColorSlider.Margin = new Padding(3, 4, 3, 4);
            hueColorSlider.Name = "hueColorSlider";
            hueColorSlider.Size = new Size(332, 22);
            hueColorSlider.TabIndex = 0;
            hueColorSlider.Value = 52F;
            hueColorSlider.ValueChanged += HueColorSlider_ValueChanged;
            // 
            // saturationColorSlider
            // 
            saturationColorSlider.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            saturationColorSlider.Color = Color.Peru;
            saturationColorSlider.Location = new Point(33, 34);
            saturationColorSlider.Margin = new Padding(3, 4, 3, 4);
            saturationColorSlider.Name = "saturationColorSlider";
            saturationColorSlider.Size = new Size(332, 22);
            saturationColorSlider.TabIndex = 0;
            saturationColorSlider.ValueChanged += SaturationColorSlider_ValueChanged;
            // 
            // lightnessColorSlider
            // 
            lightnessColorSlider.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lightnessColorSlider.Color = Color.Peru;
            lightnessColorSlider.Location = new Point(33, 64);
            lightnessColorSlider.Margin = new Padding(3, 4, 3, 4);
            lightnessColorSlider.Name = "lightnessColorSlider";
            lightnessColorSlider.Size = new Size(332, 22);
            lightnessColorSlider.TabIndex = 0;
            lightnessColorSlider.Value = 52F;
            lightnessColorSlider.ValueChanged += LightnessColorSlider_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 1;
            label1.Text = "H:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 30);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 1;
            label2.Text = "S:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 60);
            label3.Name = "label3";
            label3.Size = new Size(16, 15);
            label3.TabIndex = 1;
            label3.Text = "L:";
            // 
            // lightnessNumericUpDown
            // 
            lightnessNumericUpDown.Location = new Point(371, 63);
            lightnessNumericUpDown.Name = "lightnessNumericUpDown";
            lightnessNumericUpDown.Size = new Size(48, 23);
            lightnessNumericUpDown.TabIndex = 2;
            lightnessNumericUpDown.ValueChanged += LightnessNumericUpDown_ValueChanged;
            // 
            // saturationNumericUpDown
            // 
            saturationNumericUpDown.Location = new Point(371, 33);
            saturationNumericUpDown.Name = "saturationNumericUpDown";
            saturationNumericUpDown.Size = new Size(48, 23);
            saturationNumericUpDown.TabIndex = 2;
            saturationNumericUpDown.ValueChanged += SaturationNumericUpDown_ValueChanged;
            // 
            // hueNumericUpDown
            // 
            hueNumericUpDown.Location = new Point(371, 3);
            hueNumericUpDown.Name = "hueNumericUpDown";
            hueNumericUpDown.Size = new Size(48, 23);
            hueNumericUpDown.TabIndex = 2;
            hueNumericUpDown.ValueChanged += HueNumericUpDown_ValueChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.Controls.Add(saturationColorSlider, 1, 1);
            tableLayoutPanel1.Controls.Add(hueColorSlider, 1, 0);
            tableLayoutPanel1.Controls.Add(saturationNumericUpDown, 2, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(hueNumericUpDown, 2, 0);
            tableLayoutPanel1.Controls.Add(lightnessNumericUpDown, 2, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(alphaColorSlider, 1, 3);
            tableLayoutPanel1.Controls.Add(alphaNumericUpDown, 2, 3);
            tableLayoutPanel1.Controls.Add(lightnessColorSlider, 1, 2);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.00062F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0006275F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0006275F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 24.9981289F));
            tableLayoutPanel1.Size = new Size(428, 123);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 90);
            label4.Name = "label4";
            label4.Size = new Size(18, 15);
            label4.TabIndex = 1;
            label4.Text = "A:";
            // 
            // alphaColorSlider
            // 
            alphaColorSlider.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            alphaColorSlider.Channel = RgbaChannel.Alpha;
            alphaColorSlider.Color = Color.Peru;
            alphaColorSlider.Location = new Point(33, 94);
            alphaColorSlider.Margin = new Padding(3, 4, 3, 4);
            alphaColorSlider.Name = "alphaColorSlider";
            alphaColorSlider.Size = new Size(332, 25);
            alphaColorSlider.TabIndex = 0;
            alphaColorSlider.Value = 52F;
            alphaColorSlider.ValueChanged += AlphaColorSlider_ValueChanged;
            // 
            // alphaNumericUpDown
            // 
            alphaNumericUpDown.Location = new Point(371, 93);
            alphaNumericUpDown.Name = "alphaNumericUpDown";
            alphaNumericUpDown.Size = new Size(48, 23);
            alphaNumericUpDown.TabIndex = 2;
            alphaNumericUpDown.ValueChanged += AlphaNumericUpDown_ValueChanged;
            // 
            // HslaColorEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "HslaColorEditor";
            Size = new Size(434, 129);
            ((System.ComponentModel.ISupportInitialize)lightnessNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)saturationNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)hueNumericUpDown).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)alphaNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private HueColorSlider hueColorSlider;
        private SaturationColorSlider saturationColorSlider;
        private LightnessColorSlider lightnessColorSlider;
        private RgbaColorSlider alphaColorSlider;
        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown hueNumericUpDown;
        private NumericUpDown saturationNumericUpDown;
        private NumericUpDown lightnessNumericUpDown;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private NumericUpDown alphaNumericUpDown;
    }
}
