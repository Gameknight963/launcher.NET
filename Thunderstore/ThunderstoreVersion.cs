using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public record ThunderstoreVersion
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

        public async Task<List<ThunderstoreVersion>> FetchDependenciesAsync()
        {
            List<ThunderstoreVersion> result = [];
            foreach (string dep in Dependencies)
            {
                string[] parts = dep.Split('-');
                if (parts.Length < 3) continue;
                string owner = parts[0];
                string version = parts[^1];
                string name = string.Join("-", parts[1..^1]);
                ThunderstoreVersion? ver = await ThunderstoreClient.GetPackageVersionAsync(owner, name, version);
                if (ver != null) result.Add(ver);
            }
            return result;
        }
    }
}
