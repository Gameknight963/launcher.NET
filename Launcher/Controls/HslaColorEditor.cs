using Cyotek.Windows.Forms;
using System.ComponentModel;

namespace launcherdotnet.Launcher.Controls
{
    public partial class HslaColorEditor : UserControl
    {
        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                UpdateControls(value);
            }
        }
        private Color _color = Color.Red;
        public HslaColorEditor()
        {
            InitializeComponent();
            hueNumericUpDown.Maximum = 360;
            saturationNumericUpDown.Maximum = 100;
            lightnessNumericUpDown.Maximum = 100;
            alphaNumericUpDown.Maximum = 255;

            UpdateControls(_color);
        }

        private bool _updating;

        private void UpdateControls(Color color)
        {
            if (_updating) return;
            _updating = true;

            HslColor hsl = new HslColor(color);

            hueNumericUpDown.Value = (decimal)hsl.H;
            saturationNumericUpDown.Value = (decimal)(hsl.S * 100);
            lightnessNumericUpDown.Value = (decimal)(hsl.L * 100);
            alphaNumericUpDown.Value = _color.A;

            hueColorSlider.Value = (float)hsl.H;
            saturationColorSlider.Value = (float)(hsl.S * 100);
            lightnessColorSlider.Value = (float)(hsl.L * 100);
            alphaColorSlider.Value = _color.A;
            _updating = false;

        }

        private void UpdateColorFromControls()
        {
            if (_updating) return;
            _updating = true;

            HslColor hsl = new HslColor(
            (int)alphaNumericUpDown.Value,
            (double)hueNumericUpDown.Value,
            (double)saturationNumericUpDown.Value / 100.0,
            (double)lightnessNumericUpDown.Value / 100.0);
            _updating = false;
        }


        private void HueColorSlider_ValueChanged(object sender, EventArgs e)
        {
            hueNumericUpDown.Value = (decimal)hueColorSlider.Value;
            UpdateColorFromControls();
        }

        private void SaturationColorSlider_ValueChanged(object sender, EventArgs e)
        {
            saturationNumericUpDown.Value = (decimal)saturationColorSlider.Value;
            UpdateColorFromControls();
        }

        private void LightnessColorSlider_ValueChanged(object sender, EventArgs e)
        {
            lightnessNumericUpDown.Value = (decimal)lightnessColorSlider.Value;
            UpdateColorFromControls();
        }

        private void AlphaColorSlider_ValueChanged(object sender, EventArgs e)
        {
            alphaNumericUpDown.Value = (decimal)alphaColorSlider.Value;
            UpdateColorFromControls();
        }

        private void HueNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            hueColorSlider.Value = (float)hueNumericUpDown.Value;
            UpdateColorFromControls();
        }

        private void SaturationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            saturationColorSlider.Value = (float)saturationNumericUpDown.Value;
            UpdateColorFromControls();
        }

        private void LightnessNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            lightnessColorSlider.Value = (float)lightnessNumericUpDown.Value;
            UpdateColorFromControls();
        }

        private void AlphaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            alphaColorSlider.Value = (float)alphaNumericUpDown.Value;
            UpdateColorFromControls();
        }
    }
}
