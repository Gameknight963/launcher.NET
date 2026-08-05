using launcherdotnet;
using launcherdotnet.Launcher;
using launcherdotnet.PluginAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThunderstoreModManager
{
    public class ThunderstoreConfig : PluginData<ThunderstoreConfig>
    {
        public const string SourceId = "modstate";

        [JsonProperty("installedMods")]
        public List<InstalledMod> InstalledMods { get; set; } = [];

        [JsonProperty("thunderstoreSlug")]
        public string? ThunderstoreSlug;

        [JsonProperty("baselineFiles")]
        public List<string>? BaselineFiles { get; set; } = null;

        [JsonIgnore]
        public bool HasBaseline => BaselineFiles != null;

        public void TakeBaseline(string gameRootDirectory, Func<string, bool>? filter = null)
        {
            string[] files = Directory.GetFiles(gameRootDirectory, "*", SearchOption.AllDirectories);
            BaselineFiles = files
                .Select(f => Path.GetRelativePath(gameRootDirectory, f))
                .Where(f => filter == null || filter(f))
                .ToList();
            LauncherLogger.WriteLine($"Took baseline snapshot: {BaselineFiles.Count} files");
        }

        public List<string> GetUntrackedFiles(string gameRootDirectory)
        {
            HashSet<string> knownFiles = InstalledMods
                .SelectMany(m => m.Files)
                .Concat(BaselineFiles ?? [])
                .Select(f => f.Replace('\\', '/'))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return Directory.GetFiles(gameRootDirectory, "*", SearchOption.AllDirectories)
                .Select(f => Path.GetRelativePath(gameRootDirectory, f).Replace('\\', '/'))
                .Where(f => !knownFiles.Contains(f))
                .ToList();
        }
    }
}
