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
        }

        public static Color DarkModeMainColor => Color.FromArgb(30, 30, 30);
        public static void ApplyThemeToForm(Form form, Theme theme)
        {
            switch (theme)
            {
                case Theme.Light:
                    DwmApi.DisableImmersiveDarkMode(form.Handle);
                    SetColorRecursive(form, new ControlStyle(SystemColors.Control, Color.Black), 
                        c => c is not ListView && c is not Button && c is not TextBox);
                    SetColorRecursive(form, new ControlStyle(Color.White, Color.Black), c => c is ListView || c is Button || c is TextBox);
                    form.BackColor = SystemColors.Control;
                    break;
                case Theme.Dark:
                    DwmApi.EnableImmersiveDarkMode(form.Handle);
                    SetColorRecursive(form, new ControlStyle(DarkModeMainColor), c => c is not Label);
                    SetColorRecursive(form, new ControlStyle(null, Color.White), c => c is Label || c is Button);
                    break;
                case Theme.ExtendFrame:
                    DwmApi.EnableImmersiveDarkMode(form.Handle);
                    DwmApi.ExtendFrame(form.Handle);
                    SetColorRecursive(form, new ControlStyle(Color.Black), c => c is not Label && c is not Button);
                    SetColorRecursive(form, new ControlStyle(null, Color.White), c => c is Label || c is Button);
                    break;
            }
        }
    }
}
