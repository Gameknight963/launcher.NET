using ColorCode.Styling;
using launcherdotnet.Launcher.Settings;
using launcherdotnet.Windows;
using Newtonsoft.Json;

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
            public List<Type?> Include = new();
            public List<Type?> Exclude = new();
            public required ControlStyle Style;

            public bool Matches(Control control)
            {
                if (Exclude.Any(x => x?.IsInstanceOfType(control) == true))
                    return false;

                if (Include.Count == 0)
                    return true;

                return Include.Any(x => x?.IsInstanceOfType(control) == true);
            }
        }


        public static JsonTheme Load(string path)
        {
            string json = File.ReadAllText(path);

            JsonTheme? theme = JsonConvert.DeserializeObject<JsonTheme>(json)
                ?? throw new InvalidDataException("Failed to deserialize theme.");
            return theme;
        }

        public static IEnumerable<JsonTheme> LoadAll(string folderPath)
        {
            return Directory.GetFiles(folderPath).Select(Load);
        }

        internal static void RegisterAll()
        {
            Directory.CreateDirectory(LauncherConstants.ThemesDir);
            foreach (JsonTheme theme in LoadAll(LauncherConstants.ThemesDir))
            {
                theme.GetTheme().Register();
                LauncherLogger.WriteLine($"Registered {theme.Id}");
            }
        }
    }
}
