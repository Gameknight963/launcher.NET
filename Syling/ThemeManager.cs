using Microsoft.Win32;
using static launcherdotnet.Syling.ThemeableForm;

namespace launcherdotnet.Syling
{
    public class ThemeManager
    {
        public static Theme ActiveTheme { get; private set; }
        public static Theme ResolvedTheme => ResolveTheme(ActiveTheme);
        public static TextRenderMode ActiveTextRenderMode { get; private set; }
        private static bool? _cachedSystemLightTheme;

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
            System,
            Light,
            Dark,
            ExtendFrame,
            ExtendFrameDark,
            Blur,
            Acrylic
        }

        public enum TextRenderMode
        {
            Auto,
            AutoStrict,
            TextRenderer,
            ShadowText
        }

        public static Color DarkMainColor => Color.FromArgb(30, 30, 30);
        public static Color AcrylicMainColor => Color.FromArgb(20, 20, 30);
        public static Color DarkModeButtonColor => Color.FromArgb(30, 30, 50);
        public static Color DarkModeButtonBorder => Color.FromArgb(60, 60, 60);

        public static void ApplyThemeToForm(Form form, Theme theme)
        {
            // don't attempt to theme the comboboxes. just don't.
            switch (theme)
            {
                case Theme.Light:
                    ApplyLightTheme(form);
                    return;

                case Theme.Dark:
                    ApplyDarkTheme(form);
                    return;

                case Theme.ExtendFrame:
                case Theme.ExtendFrameDark:
                    ApplyExtendFrameTheme(form, theme == Theme.ExtendFrame);
                    return;

                case Theme.Blur:
                    ApplyBlurTheme(form);
                    return;

                case Theme.Acrylic:
                    ApplyAcrylicTheme(form);
                    return;
                case Theme.System:
                    if (IsSystemLightTheme())
                        ApplyLightTheme(form);
                    else
                        ApplyDarkTheme(form);
                    return;

            }
            throw new NotImplementedException("The requested theme is not implemented.");
        }

        private static void ApplyLightTheme(Form form)
        {
            DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
            DwmApi.UnextendFrame(form.Handle);
            DwmApi.DisableImmersiveDarkMode(form.Handle);

            SetColorRecursive(form, new ControlStyle(SystemColors.Control, Color.Black),
                c => c is not ListView && c is not Button && c is not TextBox && c is not CheckedListBox && c is not ComboBox);
            SetColorRecursive(form, new ControlStyle(SystemColors.Window, Color.Black),
                c => c is ListView || c is TextBox);
            SetColorRecursive(form, new ButtonStyle(SystemColors.Window, Color.Black, FlatStyle.Standard),
                c => c is Button);
            SetColorRecursive(form, new ControlStyle(SystemColors.Window, Color.Black),
                c => c is CheckedListBox);

            form.BackColor = SystemColors.Control;
        }
        private static void ApplyDarkTheme(Form form)
        {
            DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
            DwmApi.UnextendFrame(form.Handle);
            DwmApi.EnableImmersiveDarkMode(form.Handle);

            SetColorRecursive(form, new ControlStyle(DarkMainColor, Color.White),
                c => c is not Label && c is not Button && c is not ComboBox);
            SetColorRecursive(form, new ControlStyle(DarkMainColor, Color.White),
                c => c is Label);
            SetColorRecursive(form, new ButtonStyle(DarkModeButtonColor, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder),
                c => c is Button);
        }
        private static void ApplyExtendFrameTheme(Form form, bool light = true)
        {
            DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
            if (light)
                DwmApi.DisableImmersiveDarkMode(form.Handle);
            else
                DwmApi.EnableImmersiveDarkMode(form.Handle);

            DwmApi.ExtendFrame(form.Handle);

            SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                c => c is not Label && c is not Button && c is not ComboBox);
            SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                c => c is Label);
            SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder),
                c => c is Button);
        }
        private static void ApplyBlurTheme(Form form)
        {
            DwmApi.EnableImmersiveDarkMode(form.Handle);
            DwmApi.UnextendFrame(form.Handle);

            SetColorRecursive(form, new ControlStyle(AcrylicMainColor, Color.White),
                c => c is not Label && c is not Button && c is not ComboBox);
            SetColorRecursive(form, new ControlStyle(AcrylicMainColor, Color.White),
                c => c is Label);
            SetColorRecursive(form, new ButtonStyle(AcrylicMainColor, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder),
                c => c is Button);

            DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_ENABLE_BLURBEHIND);
        }
        private static void ApplyAcrylicTheme(Form form)
        {
            DwmApi.EnableImmersiveDarkMode(form.Handle);
            DwmApi.UnextendFrame(form.Handle);

            SetColorRecursive(form, new ControlStyle(AcrylicMainColor, Color.White),
                c => c is not Label && c is not Button && c is not ComboBox);
            SetColorRecursive(form, new ControlStyle(AcrylicMainColor, Color.White),
                c => c is Label);
            SetColorRecursive(form, new ButtonStyle(AcrylicMainColor, Color.White, FlatStyle.Flat, null, DarkModeButtonBorder),
                c => c is Button);

            DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND);
        }


        public static void SetGlobalTheme(Theme theme, TextRenderMode? mode = null)
        {
            ActiveTheme = theme;
            if (mode.HasValue) ActiveTextRenderMode = mode.Value;

            foreach (Form form in Application.OpenForms)
            {
                if (form is ThemeableForm tf)
                {
                    tf.ApplyTheme(theme, mode);
                }
            }
        }

        /// <returns>True if TextRenderer, false if ShadowText</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool ResolveTextRenderMode(Theme theme, TextRenderMode mode)
        {
            Theme resolvedTheme = ResolveTheme(theme);
            switch (mode)
            {
                case TextRenderMode.Auto:
                    return !(resolvedTheme == Theme.Acrylic || resolvedTheme == Theme.Blur);

                case TextRenderMode.AutoStrict:
                    return resolvedTheme == Theme.Light || resolvedTheme == Theme.Dark;

                case TextRenderMode.TextRenderer:
                    return true;

                case TextRenderMode.ShadowText:
                    return false;                
            }
            throw new InvalidOperationException($"The requested textrendermode is invalid: {mode}");
        }

        public static Theme ResolveTheme(Theme theme)
        {
            return theme switch
            {
                Theme.System => IsSystemLightTheme() ? Theme.Light : Theme.Dark,
                _ => theme
            };
        }

        /// <returns>True if light theme, false if dark theme.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool IsSystemLightTheme()
        {
            if (_cachedSystemLightTheme.HasValue)
                return _cachedSystemLightTheme.Value;

            _cachedSystemLightTheme = IsSystemLightThemeRaw();
            return _cachedSystemLightTheme.Value;
        }

        /// <returns>True if light theme, false if dark theme.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static bool IsSystemLightThemeRaw()
        {
            object? value = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "AppsUseLightTheme",
                1) ?? 
                throw new InvalidOperationException("Failed to read system theme from registry (AppsUseLightTheme). The value was not found.");
            return (int)value == 1;
        }
    }
}
