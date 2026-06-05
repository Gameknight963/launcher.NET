using Microsoft.Win32;

namespace launcherdotnet.Styling
{
    public static class ThemeManager
    {
        // ActiveTheme gets loaded in Settings on start so its ok to bang it
        public static Theme ActiveTheme { get; private set; } = null!;
        public static int ActiveGradientColor { get; private set; }

        public static void SetColorRecursive(Control parent, ControlStyle style, Func<Control, bool>? filter = null)
        {
            if (filter == null || filter(parent))
            {
                parent.BackColor = style.BackColor;
                parent.ForeColor = style.ForeColor;

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

        public static void SetGlobalTheme(Theme theme, int gradientColor)
        {
            ActiveTheme = theme;
            ActiveGradientColor = gradientColor;

            foreach (Form form in Application.OpenForms)
            {
                if (form is ThemeableForm tf && tf.InheritGlobalTheme)
                {
                    tf.ApplyTheme(theme, gradientColor);
                }
            }
        }

        /// <returns>True if light theme, false if dark theme.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool IsSystemLightTheme()
        {
            object value = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme",
                1) ?? 
                throw new InvalidOperationException("Failed to read system theme from registry (AppsUseLightTheme). The value was not found.");
            return (int)value == 1;
        }
    }
}
