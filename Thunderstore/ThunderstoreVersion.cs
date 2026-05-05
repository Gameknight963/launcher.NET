using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstoreVersion
    {
        [JsonProperty("version_number")]
        public string VersionNumber { get; set; } = "";
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; } = "";
        [JsonProperty("file_size")]
        public long FileSize { get; set; }
        [JsonProperty("dependencies")]
        public List<string> Dependencies { get; set; } = [];
        [JsonProperty("icon")]
        public string Icon { get; set; } = "";
    }
}
