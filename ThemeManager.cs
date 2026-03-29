using launcherdotnet.Launcher;
using System.Drawing;

namespace launcherdotnet
{
    public class ThemeManager
    {
        public static void SetColorRecursive(Control parent, ControlStyle style, Func<Control, bool>? filter = null)
        {
            if (filter == null || filter(parent))
            {
                if (style.BackColor.HasValue)
                    parent.BackColor = style.BackColor.Value;

                if (style.ForeColor.HasValue)
                    parent.ForeColor = style.ForeColor.Value;

                if (style.Font != null)
                    parent.Font = style.Font;

                if (style.BorderStyle.HasValue)
                {
                    if (parent is TextBox tb)
                        tb.BorderStyle = style.BorderStyle.Value;

                    else if (parent is RichTextBox rtb)
                        rtb.BorderStyle = style.BorderStyle.Value;

                    else if (parent is Panel panel)
                        panel.BorderStyle = BorderStyle.Fixed3D;
                }
            }

            if (parent is Button btn && style is ButtonStyle bStyle)
            {
                if (bStyle.FlatStyle.HasValue)
                    btn.FlatStyle = bStyle.FlatStyle.Value;

                if (btn.FlatStyle == FlatStyle.Flat)
                {
                    if (bStyle.BorderSize.HasValue)
                        btn.FlatAppearance.BorderSize = bStyle.BorderSize.Value;

                    if (bStyle.BorderColor.HasValue)
                        btn.FlatAppearance.BorderColor = bStyle.BorderColor.Value;

                    if (bStyle.HoverBackColor.HasValue)
                        btn.FlatAppearance.MouseOverBackColor = bStyle.HoverBackColor.Value;

                    if (bStyle.DownBackColor.HasValue)
                        btn.FlatAppearance.MouseDownBackColor = bStyle.DownBackColor.Value;
                }
            }

            foreach (Control c in parent.Controls)
            {
                SetColorRecursive(c, style, filter);
            }
        }

        public enum Theme
        {
            Light,
            Dark,
            ExtendFrame,
            Acrylic
        }

        public static Color DarkMainColor => Color.FromArgb(30, 30, 30);
        public static Color AcrylicMainColor => Color.FromArgb(20, 20, 30);
        public static Color DarkModeButtonColor => Color.FromArgb(30, 30, 50);
        public static Color DarkModeButtonBorder => Color.FromArgb(60, 60, 60);
        public static void ApplyThemeToForm(Form form, Theme theme)
        {
            switch (theme)
            {
                case Theme.Light:
                    DwmApi.UnextendFrame(form.Handle);
                    DwmApi.DisableImmersiveDarkMode(form.Handle);
                    SetColorRecursive(form, new ControlStyle(SystemColors.Control, Color.Black), 
                        c => c is not ListView && c is not Button && c is not TextBox);
                    SetColorRecursive(form, new ControlStyle(Color.White, Color.Black), c => c is ListView || c is TextBox);
                    SetColorRecursive(form, new ButtonStyle(Color.White, Color.Black, FlatStyle.Standard), c => c is Button);
                    form.BackColor = SystemColors.Control;
                    break;
                case Theme.Dark:
                    DwmApi.UnextendFrame(form.Handle);
                    DwmApi.EnableImmersiveDarkMode(form.Handle);
                    SetColorRecursive(form, new ControlStyle(DarkMainColor), c => c is not Label);
                    SetColorRecursive(form, new ControlStyle(DarkMainColor, Color.White), c => c is Label);
                    SetColorRecursive(form, new ButtonStyle(DarkModeButtonColor, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder), c => c is Button);
                    break;
                case Theme.ExtendFrame:
                    DwmApi.EnableImmersiveDarkMode(form.Handle);
                    DwmApi.ExtendFrame(form.Handle);
                    SetColorRecursive(form, new ControlStyle(Color.Black), c => c is not Label && c is not Button);
                    SetColorRecursive(form, new ControlStyle(Color.Black, Color.White), c => c is Label);
                    SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder), c => c is Button);
                    break;
                case Theme.Acrylic:
                    DwmApi.EnableImmersiveDarkMode(form.Handle);
                    DwmApi.UnextendFrame(form.Handle);
                    SetColorRecursive(form, new ControlStyle(AcrylicMainColor), c => c is not Label && c is not Button);
                    SetColorRecursive(form, new ControlStyle(null, Color.White), c => c is Label);
                    SetColorRecursive(form, new ButtonStyle(AcrylicMainColor, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder), c => c is Button);
                    DwmApi.EnableBlur(form.Handle);
                    break;
            }
        }
    }
}
