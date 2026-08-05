using launcherdotnet.Windows;

namespace launcherdotnet.Styling
{
    public class JsonTheme : IThemeProvider
    {
        public string? Id;
        public string? UserFriendlyName;
        public ControlStyle? BaseStyle;
        public bool UseShadowText = true;
        public bool UseOwnerDrawHeaders = true;
        
        public AccentState AccentState;
        public WindowEffects Effects;

        public Theme GetTheme()
        {
            if (Id == null) throw new InvalidOperationException("Theme must have an ID");
            if (UserFriendlyName == null) throw new InvalidOperationException("Theme must have a UserFriendlyName");
            if (BaseStyle == null) throw new InvalidOperationException("Theme must have a BaseStyle");

            return new Theme(
                Id,
                UserFriendlyName,
                ApplyTheme,
                BaseStyle,
                UseShadowText,
                UseOwnerDrawHeaders
                );
        }

        void ApplyTheme(Form form, int gradientColor)
        {
            foreach(KeyValuePair<Type, ControlStyle> rule in Rules)
            {
                ThemeManager.SetColorRecursive(form, rule.Value, x => rule.Key.IsInstanceOfType(x));
            }
            DwmApi.SetAccentState(form.Handle, AccentState, gradientColor);
            DwmApi.SetImmersiveDarkMode(form.Handle, Effects.HasFlag(WindowEffects.DarkMode));
            DwmApi.ExtendFrame(form.Handle, Effects.HasFlag(WindowEffects.ExtendFrame));
        }

        /// <summary>
        /// selector, controlstyle
        /// </summary>
        public Dictionary<Type, ControlStyle> Rules = new();
    }
}
