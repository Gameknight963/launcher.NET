using Newtonsoft.Json;

namespace launcherdotnet.Launcher
{
    public class GameModState
    {
        private const string _fileName = "launcherdotnet_modstate.json";

        [JsonProperty("installedMods")]
        public List<InstalledMod> InstalledMods { get; set; } = [];

        [JsonProperty("baselineFiles")]
        public List<string>? BaselineFiles { get; set; } = null;

        [JsonIgnore]
        public bool HasBaseline => BaselineFiles != null;

        public static GameModState Load(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, _fileName);
            if (!File.Exists(path)) return new GameModState();
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GameModState>(json) ?? new GameModState();
        }

        public void Save(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, _fileName);
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public void TakeBaseline(string gameRootDirectory)
        {
            string[] files = Directory.GetFiles(gameRootDirectory, "*", SearchOption.AllDirectories);
            BaselineFiles = files
                .Select(f => Path.GetRelativePath(gameRootDirectory, f))
                .ToList();
            LauncherLogger.WriteLine($"Took baseline snapshot: {BaselineFiles.Count} files");
        }
    }
}
