using Newtonsoft.Json;

namespace launcherdotnet.Launcher
{
    public class ModManifest
    {
        [JsonProperty("installedMods")]
        public List<InstalledMod> InstalledMods { get; set; } = [];

        public static ModManifest Load(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, "launcherdotnet_manifest.json");
            if (!File.Exists(path)) return new ModManifest();
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ModManifest>(json) ?? new ModManifest();
        }

        public void Save(string gameRootDirectory)
        {
            string path = Path.Combine(gameRootDirectory, "launcherdotnet_manifest.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
