using Newtonsoft.Json;

namespace launcherdotnet.Launcher
{
    public class InstalledMod
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("owner")]
        public string Owner { get; set; } = "";
        [JsonProperty("version")]
        public string Version { get; set; } = "";
        [JsonProperty("files")]
        public List<string> Files { get; set; } = [];
        [JsonProperty("isDependency")]
        public bool IsDependency { get; set; }
        [JsonProperty("dependencies")]
        public List<string> Dependencies { get; set; } = [];
        [JsonProperty("installedAt")]
        public DateTime InstalledAt { get; set; } = DateTime.UtcNow;
    }
}
