using Newtonsoft.Json;

namespace launcherdotnet.Launcher
{
    public class GameModState
    {
        public const string FileName = "launcherdotnet_modstate.json";

        [JsonProperty("installedMods")]
        public List<InstalledMod> InstalledMods { get; set; } = [];

        [JsonProperty("baselineFiles")]
        public List<string>? BaselineFiles { get; set; } = null;

        [JsonIgnore]
        public bool HasBaseline => BaselineFiles != null;

        public static GameModState Load(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, FileName);
            if (!File.Exists(path)) return new GameModState();
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GameModState>(json) ?? new GameModState();
        }

        public void Save(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, FileName);
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void TakeBaseline(string gameRootDirectory, Func<string, bool>? filter = null)
        {
            string[] files = Directory.GetFiles(gameRootDirectory, "*", SearchOption.AllDirectories);
            BaselineFiles = files
                .Select(f => Path.GetRelativePath(gameRootDirectory, f))
                .Where(f => f != FileName && (filter == null || filter(f)))
                .ToList();
            LauncherLogger.WriteLine($"Took baseline snapshot: {BaselineFiles.Count} files");
        }

        public List<string> GetUntrackedFiles(string gameRootDirectory)
        {
            HashSet<string> knownFiles = InstalledMods
                .SelectMany(m => m.Files)
                .Concat(BaselineFiles ?? [])
                .ToHashSet();

            return Directory.GetFiles(gameRootDirectory, "*", SearchOption.AllDirectories)
                .Select(f => Path.GetRelativePath(gameRootDirectory, f))
                .Where(f => !knownFiles.Contains(f) && f != FileName)
                .ToList();
        }
    }
}
