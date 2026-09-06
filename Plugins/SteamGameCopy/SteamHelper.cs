using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace launcherdotnet.Plugins.SteamGameCopy
{
    public static partial class SteamHelper
    {
        public static string? GetSteamPath()
        {
            return Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\Valve\Steam",
                "SteamPath",
                null) as string;
        }

        [GeneratedRegex("\"path\"\\s+\"([^\"]+)\"")]
        private static partial Regex VdfPathRegex();

        [GeneratedRegex("\"(?<key>[^\"]+)\"\\s+\"(?<value>[^\"]+)\"")]
        private static partial Regex AcfFieldRegex();

        public static List<string> GetLibraryFolders(string steamPath)
        {
            List<string> folders = [Path.Combine(steamPath, "steamapps")];
            string vdf = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
            if (!File.Exists(vdf)) return folders;
            string content = File.ReadAllText(vdf);
            foreach (Match match in VdfPathRegex().Matches(content))
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
                    Dictionary<string, string> fields = new(StringComparer.OrdinalIgnoreCase);
                    foreach (Match m in AcfFieldRegex().Matches(content))
                        fields.TryAdd(m.Groups["key"].Value, m.Groups["value"].Value);

                    if (!fields.TryGetValue("name", out string? name) || !fields.TryGetValue("installdir", out string? installdir))
                        continue;
                    string fullPath = Path.Combine(library, "common", installdir);
                    if (!Directory.Exists(fullPath)) continue;
                    long.TryParse(fields.GetValueOrDefault("SizeOnDisk", "0"), out long size);
                    games.Add(new SteamGame(name, fullPath, size));
                }
            }
            return games;
        }
    }

    public record SteamGame(string Name, string RootDirectory, long SizeOnDisk);
}
