using Semver;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace launcherdotnet
{
    public static class Config
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string DefaultTempDir = Path.Combine(BaseDir, "temp");
        public static readonly string DefaultGamesDir = Path.Combine(BaseDir, "games");

        public static readonly string PluginsDir = Path.Combine(BaseDir, "plugins");

        public const string StartupRegistryKeyName = "launcherdotnet";
        public const string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public const string RepoOwner = "Gameknight963"; // if you're forking this repo, you'll need to change this
        public const string RepoName = "launcher.NET";
        public static readonly string ReleasesAPIUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases";
        public static readonly string RelesesPage = $"https://github.com/{RepoOwner}/{RepoName}/releases";
        public static readonly string GithubPage = $"https://github.com/{RepoOwner}/{RepoName}";


        public static string? CurrentVersionString => Assembly.GetExecutingAssembly().
            GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.
            InformationalVersion
            .Split('+')[0];
        public static SemVersion CurrentVersion => SemVersion.Parse(CurrentVersionString!);
    }
}
