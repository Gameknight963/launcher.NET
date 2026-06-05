using launcherdotnet.Styling;
using Semver;
using System.Reflection;

namespace launcherdotnet.Launcher.Settings
{
    public static class LauncherConstants
    {
        public static readonly string BaseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
        public static readonly string TempDir = Path.Combine(BaseDir, "temp");
        public static readonly string GamesDir = Path.Combine(BaseDir, "games");

        public static readonly string PluginsDir = Path.Combine(BaseDir, "plugins");

        public const string StartupRegistryKeyName = "launcherdotnet";
        public const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public const string RepoOwner = "Gameknight963"; // if you're forking this repo, you'll need to change this
        public const string RepoName = "launcher.NET";
        public static readonly string ReleasesAPIUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases";
        public static readonly string RelesesPage = $"https://github.com/{RepoOwner}/{RepoName}/releases";
        public static readonly string GithubPage = $"https://github.com/{RepoOwner}/{RepoName}";

        public static readonly Theme DefaultTheme = Theme.Light;

        public static readonly Icon AppIcon;

        public static readonly string CurrentVersionString = Assembly.GetExecutingAssembly().
            GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.
            InformationalVersion
            .Split('+')[0];
        public static readonly SemVersion CurrentVersion = SemVersion.Parse(CurrentVersionString);

        static LauncherConstants()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("launcherdotnet.icon.ico")!;
            AppIcon = new Icon(stream);
        }
    }
}
