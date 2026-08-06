using launcherdotnet.Launcher.Settings;
using launcherdotnet.Windows;
using Newtonsoft.Json;

namespace launcherdotnet.Styling
{
    public class JsonTheme : IThemeProvider
    {
        static readonly JsonSerializerSettings _settings = new()
        {
            Converters = { new StyleConverter() }
        };

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
            foreach(ThemeRule rule in Rules)
            {
                ThemeManager.SetColorRecursive(form, rule.Style, c => rule.Matches(c));
            }
            DwmApi.SetAccentState(form.Handle, AccentState, gradientColor);
            DwmApi.SetImmersiveDarkMode(form.Handle, Effects.HasFlag(WindowEffects.DarkMode));
            DwmApi.ExtendFrame(form.Handle, Effects.HasFlag(WindowEffects.ExtendFrame));
        }

        public List<ThemeRule> Rules = new();

        public class ThemeRule
        {
            public List<string> Include = new();
            public List<string> Exclude = new();
            public required ControlStyle Style;

            public bool Matches(Control control)
            {
                if (Exclude.Any(t => IsMatch(control, t))) return false;
                if (Include.Count == 0) return true;
                return Include.Any(t => IsMatch(control, t));
            }

            static bool IsMatch(Control control, string typeName)
            {
                Type? resolved = Type.GetType(typeName);
                if (resolved == null)
                {
                    LauncherLogger.Warn($"Theme references nonexistant type '{typeName}'");
                    return false;
                }
                return resolved.IsInstanceOfType(control);
            }
        }

        public static JsonTheme? Load(string path)
        {
            string json = File.ReadAllText(path);

            JsonTheme? theme = JsonConvert.DeserializeObject<JsonTheme>(json, _settings);
            if (theme == null)
                LauncherLogger.Error($"Could not deserialize theme '{path}'");
            return theme;
        }

        internal static IEnumerable<JsonTheme> LoadAll(string folderPath)
        {
            return Directory.GetFiles(folderPath).Select(Load).OfType<JsonTheme>();
        }

        internal static void RegisterAll()
        {
            Directory.CreateDirectory(LauncherConstants.ThemesDir);
            foreach (JsonTheme theme in LoadAll(LauncherConstants.ThemesDir))
            {
                theme.GetTheme().Register();
                LauncherLogger.WriteLine($"Registered {theme.Id}", true);
            }
        }
    }
}
