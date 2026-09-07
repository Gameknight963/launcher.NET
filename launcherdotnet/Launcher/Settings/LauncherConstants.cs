using launcherdotnet.Styling;
using Semver;
using System.Reflection;

namespace launcherdotnet.Launcher.Settings
{
    /// <summary>
    /// Represents some constant or otherwise immutable fields that define how launcher.net works.
    /// </summary>
    public static class LauncherConstants
    {
        /// <summary>
        /// Base directory of the application (identical to <see cref="AppContext.BaseDirectory"/>)
        /// </summary>
        public static readonly string BaseDir = AppContext.BaseDirectory;

        /// <summary>
        /// The path of a temporary directory used to store temporary files.
        /// </summary>
        public static readonly string TempDir = Path.Combine(BaseDir, "temp");

        /// <summary>
        /// The path of the directory used to store games.
        /// </summary>
        public static readonly string GamesDir = Path.Combine(BaseDir, "games");

        /// <summary>
        /// The path of the directory used to game-specific persistant plugin data
        /// created by <see cref="PluginAPI.PluginGameData{T}"/>
        /// </summary>
        public static readonly string DataDir = Path.Combine(BaseDir, "data");

        /// <summary>
        /// The path of the directory used to store theme.json files
        /// </summary>
        public static readonly string ThemesDir = Path.Combine(BaseDir, "themes");

        /// <summary>
        /// The path of the directory where plugins are loaded from.
        /// </summary>
        public static readonly string PluginsDir = Path.Combine(BaseDir, "plugins");

        public const string StartupRegistryKeyName = "launcherdotnet";
        public const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// the repo owner of launcher.net
        /// </summary>
        public const string RepoOwner = "Gameknight963";

        /// <summary>
        /// the repo name of launcher.net
        /// </summary>
        public const string RepoName = "launcher.NET";

        /// <summary>
        /// Represents the Github API URL that can be queried for launcher.net releases.
        /// </summary>
        /// <remarks>
        /// Constructed from <see cref="RepoOwner"/> and <see cref="RepoName"/>.
        /// </remarks>
        public static readonly string ReleasesAPIUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases";

        /// <summary>
        /// Represents a URL that leads to the the launcher.net Github Releases page.
        /// </summary>
        /// <remarks>
        /// Constructed from <see cref="RepoOwner"/> and <see cref="RepoName"/>.
        /// </remarks>
        public static readonly string RelesesPage = $"https://github.com/{RepoOwner}/{RepoName}/releases";

        /// <summary>
        /// Represents a URL that leads to the the launcher.net Github main page.
        /// </summary>
        /// <remarks>
        /// Constructed from <see cref="RepoOwner"/> and <see cref="RepoName"/>.
        /// </remarks>
        public static readonly string GithubPage = $"https://github.com/{RepoOwner}/{RepoName}";

        /// <summary>
        /// Represents the default <see cref="Theme"/> of launcher.net.
        /// </summary>
        public static readonly Theme DefaultTheme = Theme.Light;

        /// <summary>
        /// Represents the application <see cref="Icon"/> of launcher.net.
        /// </summary>
        public static readonly Icon AppIcon;

        /// <summary>
        /// Represents a string representing the current version of this build of launcher.net.
        /// </summary>
        public static readonly string CurrentVersionString = Assembly.GetExecutingAssembly().
            GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.
            InformationalVersion
            .Split('+')[0];

        /// <summary>
        /// Represents the current version of this build of launcher.net.
        /// </summary>
        public static readonly SemVersion CurrentVersion = SemVersion.Parse(CurrentVersionString);

        static LauncherConstants()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("launcherdotnet.icon.ico")!;
            AppIcon = new Icon(stream);
        }
    }
}
