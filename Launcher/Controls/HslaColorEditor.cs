using Cyotek.Windows.Forms;
using System.ComponentModel;

namespace launcherdotnet.Launcher.Controls
{
    [DefaultEvent("ColorChanged")]
    public partial class HslaColorEditor : UserControl
    {
        private double _hue, _saturation, _lightness;
        private int _alpha;

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                HslColor hsl = new HslColor(value);
                _hue = hsl.H;
                _saturation = hsl.S;
                _lightness = hsl.L;
                _alpha = hsl.A;
                UpdateControls();
            }
        }

        public event EventHandler? ColorChanged;

        private Color _color = Color.Red;
        private bool _updating;

        public HslaColorEditor()
        {
            InitializeComponent();
            hueNumericUpDown.Maximum = 360;
            saturationNumericUpDown.Maximum = 100;
            lightnessNumericUpDown.Maximum = 100;
            alphaNumericUpDown.Maximum = 255;
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (_updating) return;
            _updating = true;
            lightnessColorSlider.ValueChanged -= LightnessColorSlider_ValueChanged;
            saturationColorSlider.ValueChanged -= SaturationColorSlider_ValueChanged;
            hueNumericUpDown.Value = (decimal)_hue;
            saturationNumericUpDown.Value = (decimal)(_saturation * 100);
            lightnessNumericUpDown.Value = (decimal)(_lightness * 100);
            alphaNumericUpDown.Value = _alpha;
            hueColorSlider.Value = (float)_hue;
            saturationColorSlider.Color = Color.FromArgb(255, new HslColor(255, _hue, _saturation, 0.5));
            saturationColorSlider.Value = (float)(_saturation * 100);
            lightnessColorSlider.Color = Color.FromArgb(255, _color);
            lightnessColorSlider.Value = (float)(_lightness * 100);
            alphaColorSlider.Color = _color;
            // slider doesn't render checkerboard if you set alpha to 255, setting it to 254 is a workaround
            alphaColorSlider.Color = Color.FromArgb(254, _color);
            alphaColorSlider.Value = _alpha;
            lightnessColorSlider.ValueChanged += LightnessColorSlider_ValueChanged;
            saturationColorSlider.ValueChanged += SaturationColorSlider_ValueChanged;
            _updating = false;
        }

        private void HueColorSlider_ValueChanged(object sender, EventArgs e)
            => hueNumericUpDown.Value = (decimal)hueColorSlider.Value;

        private void SaturationColorSlider_ValueChanged(object? sender, EventArgs e)
            => saturationNumericUpDown.Value = (decimal)saturationColorSlider.Value;

        private void LightnessColorSlider_ValueChanged(object? sender, EventArgs e)
            => lightnessNumericUpDown.Value = (decimal)lightnessColorSlider.Value;

        private void AlphaColorSlider_ValueChanged(object sender, EventArgs e)
            => alphaNumericUpDown.Value = (decimal)alphaColorSlider.Value;

        private void HueNumericUpDown_ValueChanged(object sender, EventArgs e) => RebuildColor();
        private void SaturationNumericUpDown_ValueChanged(object sender, EventArgs e) => RebuildColor();
        private void LightnessNumericUpDown_ValueChanged(object sender, EventArgs e) => RebuildColor();
        private void AlphaNumericUpDown_ValueChanged(object sender, EventArgs e) => RebuildColor();

        private void RebuildColor()
        {
            if (_updating) return;
            ColorChanged?.Invoke(this, EventArgs.Empty);
            _hue = (double)hueNumericUpDown.Value;
            _saturation = (double)saturationNumericUpDown.Value / 100.0;
            _lightness = (double)lightnessNumericUpDown.Value / 100.0;
            _alpha = (int)alphaNumericUpDown.Value;
            _color = new HslColor(_alpha, _hue, _saturation, _lightness);
            UpdateControls();
        }
    }
}
