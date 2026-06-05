namespace launcherdotnet.Styling
{
    public class Theme
    {
        public delegate void ApplyThemeDelegate(Form form, int gradientColor);

        public readonly ApplyThemeDelegate Apply;
        public readonly ControlStyle MainStyle;
        public readonly bool UseShadowText;
        public readonly bool UseOwnerDrawHeaders;
        public readonly string Name;

        public Theme(
            string name,
            ApplyThemeDelegate apply,
            ControlStyle style,
            bool useShadowText = true,
            bool useOwnerDrawHeaders = true)
        {
            Name = name; 
            Apply = apply;
            MainStyle = style;
            UseShadowText = useShadowText;
            UseOwnerDrawHeaders = useOwnerDrawHeaders;
            if (_themes.ContainsKey(name))
                throw new InvalidOperationException($"A theme with the name '{name}' is already registered.");
            _themes[name] = this;
        }

        public override bool Equals(object? obj) => obj is Theme other && Name == other.Name;
        public override int GetHashCode() => Name.GetHashCode();
        public static bool operator ==(Theme? a, Theme? b) => a?.Name == b?.Name;
        public static bool operator !=(Theme? a, Theme? b) => a?.Name != b?.Name;


        private static readonly Dictionary<string, Theme> _themes = new();
        public static Theme FromName(string name) => _themes[name];

        // ------------------- STATIC -------------------

        public static readonly Color DarkMainColor = Color.FromArgb(30, 30, 30);
        public static readonly Color AcrylicButtonColor = Color.FromArgb(20, 20, 30);
        public static readonly Color DarkButtonColor = Color.FromArgb(30, 30, 50);
        public static readonly Color DarkButtonBorder = Color.FromArgb(60, 60, 60);

        public static readonly Theme Light = new(
            "Light",
            (form, gradientColor) =>
            {
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
                DwmApi.UnextendFrame(form.Handle);
                DwmApi.DisableImmersiveDarkMode(form.Handle);

                ThemeManager.SetColorRecursive(form, new ControlStyle(SystemColors.Control, SystemColors.ControlText),
                    c => c is not ListView && c is not Button && c is not TextBox && c is not CheckedListBox && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(SystemColors.Window, SystemColors.ControlText),
                    c => c is ListView || c is TextBox);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(SystemColors.Window, SystemColors.ControlText, FlatStyle.Standard),
                    c => c is Button);
                ThemeManager.SetColorRecursive(form, new ControlStyle(SystemColors.Window, SystemColors.ControlText),
                    c => c is CheckedListBox);
            },
            new ControlStyle(SystemColors.Control, SystemColors.ControlText),
            useShadowText: false,
            useOwnerDrawHeaders: false
        );

        public static readonly Theme Dark = new(
            "Dark",
            (form, gradientColor) =>
                {
                    DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
                    DwmApi.UnextendFrame(form.Handle);
                    DwmApi.EnableImmersiveDarkMode(form.Handle);

                    ThemeManager.SetColorRecursive(form, new ControlStyle(DarkMainColor, Color.White),
                        c => c is not Label && c is not Button && c is not ComboBox);
                    ThemeManager.SetColorRecursive(form, new ControlStyle(DarkMainColor, Color.White),
                        c => c is Label);
                    ThemeManager.SetColorRecursive(form, new ButtonStyle(DarkButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                        c => c is Button);
                },
                new ControlStyle(DarkMainColor, Color.White),
                useShadowText: false,
                useOwnerDrawHeaders: true
            );

        public static readonly Theme System = new(
            "System",
            (form, gradientColor) =>
            {
                Theme real = ThemeManager.IsSystemLightTheme() ? Light : Dark;

                real.Apply(form, gradientColor);
            },
            new ControlStyle(Color.Empty, Color.Empty),
            useShadowText: false,
            useOwnerDrawHeaders: !ThemeManager.IsSystemLightTheme()
        );

        public static readonly Theme ExtendFrame = new(
            "ExtendFrame",
            (form, gradientColor) =>
            {
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
                DwmApi.DisableImmersiveDarkMode(form.Handle);
                DwmApi.ExtendFrame(form.Handle);

                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is not Label && c is not Button && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is Label);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                    c => c is Button);
            },
            new ControlStyle(Color.Black, Color.White)
        );

        public static readonly Theme ExtendFrameDark = new(
            "ExtendFrameDark",
            (form, gradientColor) =>
            {
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_DISABLED);
                DwmApi.EnableImmersiveDarkMode(form.Handle);
                DwmApi.ExtendFrame(form.Handle);

                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is not Label && c is not Button && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is Label);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                    c => c is Button);
            },
            new ControlStyle(Color.Black, Color.White)
        );

        public static readonly Theme Blur = new(
            "Blur",
            (form, gradientColor) =>
            {
                DwmApi.EnableImmersiveDarkMode(form.Handle);
                DwmApi.UnextendFrame(form.Handle);
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_ENABLE_BLURBEHIND, gradientColor);

                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is not Label && c is not Button && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is Label);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                    c => c is Button);
            },
            new ControlStyle(Color.Black, Color.White)
        );

        public static readonly Theme Acrylic = new(
            "Acrylic",
            (form, gradientColor) =>
            {
                DwmApi.EnableImmersiveDarkMode(form.Handle);
                DwmApi.UnextendFrame(form.Handle);
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND, gradientColor);

                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is not Label && c is not Button && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is Label);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                    c => c is Button);
            },
            new ControlStyle(Color.Black, Color.White)
        );

        public static readonly Theme TransparentGradient = new(
            "TransparentGradient",
            (form, gradientColor) =>
            {
                DwmApi.EnableImmersiveDarkMode(form.Handle);
                DwmApi.UnextendFrame(form.Handle);
                DwmApi.SetAccentState(form.Handle, DwmApi.AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT, gradientColor);

                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is not Label && c is not Button && c is not ComboBox);
                ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
                    c => c is Label);
                ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
                    c => c is Button);
            },
            new ControlStyle(Color.Black, Color.White)
        );
    }
}
