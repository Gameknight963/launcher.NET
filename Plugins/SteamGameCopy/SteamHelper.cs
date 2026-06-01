using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace launcherdotnet.Plugins.SteamGameCopy
{
    public static class SteamHelper
    {
        public static string? GetSteamPath()
        {
            return Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Valve\Steam",
                "SteamPath",
                null) as string;
        }

        public static List<string> GetLibraryFolders(string steamPath)
        {
            List<string> folders = [Path.Combine(steamPath, "steamapps")];
            string vdf = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
            if (!File.Exists(vdf)) return folders;
            string content = File.ReadAllText(vdf);
            foreach (Match match in Regex.Matches(content, "\"path\"\\s+\"([^\"]+)\""))
                folders.Add(Path.Combine(match.Groups[1].Value.Replace("\\\\", "\\"), "steamapps"));
            return folders;
        }

        public static List<SteamGame> GetInstalledGames(string steamPath)
        {
            List<SteamGame> games = [];
            foreach (string library in GetLibraryFolders(steamPath))
            {
                if (!Directory.Exists(library)) continue;
                foreach (string acf in Directory.GetFiles(library, "appmanifest_*.acf"))
                {
                    string content = File.ReadAllText(acf);
                    string? name = GetValue(content, "name");
                    string? installdir = GetValue(content, "installdir");
                    string? sizeStr = GetValue(content, "SizeOnDisk");
                    if (name == null || installdir == null) continue;
                    string fullPath = Path.Combine(library, "common", installdir);
                    if (!Directory.Exists(fullPath)) continue;
                    long size = long.Parse(sizeStr ?? "0");
                    games.Add(new SteamGame(name, fullPath, size));
                }
            }
            return games;
        }

        private static string? GetValue(string content, string key)
        {
            Match match = Regex.Match(content, $"\"{key}\"\\s+\"([^\"]+)\"");
            return match.Success ? match.Groups[1].Value : null;
        }
    }

    public record SteamGame(string Name, string RootDirectory, long SizeOnDisk);
}
