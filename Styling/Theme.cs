using launcherdotnet.Windows;

namespace launcherdotnet.Styling
{
    /// <summary>
    /// Represents a complete visual theme that can be applied to a window.
    /// </summary>
    public class Theme
    {
        public delegate void ApplyThemeDelegate(Form form, int gradientColor);

        /// <summary>
        /// The method used to apply this theme to a form.
        /// </summary>
        public readonly ApplyThemeDelegate Apply;

        /// <summary>
        /// The default control style used by this theme.
        /// </summary>
        public readonly ControlStyle MainStyle;

        /// <summary>
        /// Whether text shadows should be enabled on certain elements.
        /// Can make transparency look better.
        /// </summary>
        public readonly bool UseShadowText;

        /// <summary>
        /// Whether headers should be owner-drawn. If enabled, 
        /// the styles from <see cref="MainStyle"/> will be used.
        /// Otherwise, normal UxTheme drawing.
        /// </summary>
        public readonly bool UseOwnerDrawHeaders;

        /// <summary>
        /// The unique identifier used to reference this theme.
        /// </summary>
        /// <remarks>
        /// Follow the convention 'namespace.identifier' to avoid collisions.
        /// </remarks>
        public readonly string Id;

        /// <summary>
        /// The display name of this theme intended for presentation to users.
        /// </summary>
        public readonly string UserFriendlyName;

        /// <summary>
        /// Registers this theme to <see cref="Themes"/>, making it appear
        /// appear in the settings theme selector.
        /// </summary>
        /// <remarks>
        /// This only applies on the next time settings is opened.
        /// </remarks>
        /// <param name="overwrite"><see langword="true"/> to overwrite an already registered theme with the existing <see cref="Id"/>.
        /// If <see langword="false"/>, and a theme with the same <see cref="Id"/> is already registered, throws an exception.</param>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="overwrite"/> is <see langword="false"/>
        /// and a theme with the same <see cref="Id"/> is already registed.</exception>
        public void Register(bool overwrite = false)
        {
            if (!overwrite && _themes.ContainsKey(Id))
                throw new InvalidOperationException($"'{Id}' is already registered.");
            _themes[Id] = this;
        }

        public Theme(
            string id,
            string userFriendlyName,
            ApplyThemeDelegate apply,
            ControlStyle style,
            bool useShadowText = true,
            bool useOwnerDrawHeaders = true,
            bool register = false)
        {
            Id = id;
            UserFriendlyName = userFriendlyName;
            Apply = apply;
            MainStyle = style;
            UseShadowText = useShadowText;
            UseOwnerDrawHeaders = useOwnerDrawHeaders;
            if (register) Register();
        }

        public override bool Equals(object? obj) => obj is Theme other && Id == other.Id;
        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Theme? a, Theme? b) => a?.Id == b?.Id;
        public static bool operator !=(Theme? a, Theme? b) => a?.Id != b?.Id;

        // ------------------- STATIC -------------------

        private static readonly Dictionary<string, Theme> _themes = new();

        /// <summary>
        /// All registered themes. The key is the <see cref="Id"/>.
        /// </summary>
        public static IReadOnlyDictionary<string, Theme> Themes => _themes;

        /// <summary>
        /// Tries to get a theme with the given id.
        /// </summary>
        /// <param name="id">The <see cref="Id"/> of the theme to get.</param>
        /// <returns>The <see cref="Theme"/> with the matching Id, otherwise, <see langword="null"></see>.</returns>
        public static Theme? FromId(string id) => _themes.TryGetValue(id, out Theme? theme) ? theme : null;

        internal static readonly Color DarkMainColor = Color.FromArgb(30, 30, 30);
        internal static readonly Color AcrylicButtonColor = Color.FromArgb(20, 20, 30);
        internal static readonly Color DarkButtonColor = Color.FromArgb(30, 30, 50);
        internal static readonly Color DarkButtonBorder = Color.FromArgb(60, 60, 60);

        /// <summary>
        /// Represents the default light theme.
        /// Uses the standard Windows control colors with no non-client visual effects.
        /// </summary>
        public static readonly Theme Light = new(
            "launcherdotnet.light_theme",
            "Light",
            (form, gradientColor) =>
            {
                DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_DISABLED);
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
            useOwnerDrawHeaders: false,
            register: true
        );

        /// <summary>
        /// Represents the default dark theme.
        /// Uses dark control colors and enables immersive dark mode.
        /// </summary>
        public static readonly Theme Dark = new(
            "launcherdotnet.dark_theme",
            "Dark",
            (form, gradientColor) =>
            {
                DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_DISABLED);
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
            useOwnerDrawHeaders: true,
            register: true
            );

        /// <summary>
        /// Represents a theme that automatically follows the system appearance setting.
        /// Applies either the <see cref="Light"/> or <see cref="Dark"/> theme depending on the current Windows theme.
        /// </summary>
        public static readonly Theme System = new(
            "launcherdotnet.system_theme",
            "System",
            (form, gradientColor) =>
            {
                Theme real = ThemeManager.IsSystemLightTheme() ? Light : Dark;
                real.Apply(form, gradientColor);
            },
            ThemeManager.IsSystemLightTheme() ? 
                new ControlStyle(SystemColors.Control, SystemColors.ControlText) : 
                new ControlStyle(DarkMainColor, Color.White),
            useShadowText: false,
            useOwnerDrawHeaders: !ThemeManager.IsSystemLightTheme(),
            register: true
        );

        ///// <summary>
        ///// Represents a theme that extends the DWM frame into the client area.
        ///// </summary>
        //public static readonly Theme ExtendFrame = new(
        //    "launcherdotnet.extendframe_theme",
        //    "Extended frame",
        //    (form, gradientColor) =>
        //    {
        //        DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_DISABLED);
        //        DwmApi.DisableImmersiveDarkMode(form.Handle);
        //        DwmApi.ExtendFrame(form.Handle);

        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is not Label && c is not Button && c is not ComboBox);
        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is Label);
        //        ThemeManager.SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
        //            c => c is Button);
        //    },
        //    new ControlStyle(Color.Black, Color.White),
        //    register: true
        //);

        ///// <summary>
        ///// Represents a dark variant of the extended frame theme.
        ///// Extends the DWM frame into the client area and enables immersive dark mode.
        ///// </summary>
        //public static readonly Theme ExtendFrameDark = new(
        //    "launcherdotnet.extendframe_dark_theme",
        //    "Extended frame (dark)",
        //    (form, gradientColor) =>
        //    {
        //        DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_DISABLED);
        //        DwmApi.EnableImmersiveDarkMode(form.Handle);
        //        DwmApi.ExtendFrame(form.Handle);

        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is not Label && c is not Button && c is not ComboBox);
        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is Label);
        //        ThemeManager.SetColorRecursive(form, new ButtonStyle(Color.Black, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
        //            c => c is Button);

        //    },
        //    new ControlStyle(Color.Black, Color.White),
        //    register: true
        //);

        ///// <summary>
        ///// Represents a theme that applies the Windows blur-behind composition effect.
        ///// </summary>
        //public static readonly Theme Blur = new(
        //    "launcherdotnet.blur_theme",
        //    "Blur",
        //    (form, gradientColor) =>
        //    {
        //        DwmApi.EnableImmersiveDarkMode(form.Handle);
        //        DwmApi.UnextendFrame(form.Handle);
        //        DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, gradientColor);

        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is not Label && c is not Button && c is not ComboBox);
        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is Label);
        //        ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
        //            c => c is Button);

        //    },
        //    new ControlStyle(Color.Black, Color.White),
        //    register: true
        //);

        ///// <summary>
        ///// Represents a theme that applies the Windows acrylic blur composition effect.
        ///// </summary>
        //public static readonly Theme Acrylic = new(
        //    "launcherdotnet.acrylic_theme",
        //    "Acrylic",
        //    (form, gradientColor) =>
        //    {
        //        DwmApi.EnableImmersiveDarkMode(form.Handle);
        //        DwmApi.UnextendFrame(form.Handle);
        //        DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND, gradientColor);

        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is not Label && c is not Button && c is not ComboBox);
        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is Label);
        //        ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
        //            c => c is Button);
        //    },
        //    new ControlStyle(Color.Black, Color.White),
        //    register: true
        //);

        ///// <summary>
        ///// Represents a theme that applies a transparent gradient composition effect.
        ///// </summary>
        //public static readonly Theme TransparentGradient = new(
        //    "launcherdotnet.transparent_gradient_theme",
        //    "Transparent gradient",
        //    (form, gradientColor) =>
        //    {
        //        DwmApi.EnableImmersiveDarkMode(form.Handle);
        //        DwmApi.UnextendFrame(form.Handle);
        //        DwmApi.SetAccentState(form.Handle, AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT, gradientColor);

        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is not Label && c is not Button && c is not ComboBox);
        //        ThemeManager.SetColorRecursive(form, new ControlStyle(Color.Black, Color.White),
        //            c => c is Label);
        //        ThemeManager.SetColorRecursive(form, new ButtonStyle(AcrylicButtonColor, Color.White, FlatStyle.Flat, null, DarkButtonBorder),
        //            c => c is Button);

        //    },
        //    new ControlStyle(Color.Black, Color.White),
        //    register: true
        //);
    }
}
