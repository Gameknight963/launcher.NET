using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackage
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; } = "";
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("full_name")]
        public string FullName { get; set; } = "";
        [JsonProperty("owner")]
        public string Owner { get; set; } = "";
        [JsonProperty("package_url")]
        public string PackageUrl { get; set; } = "";
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }
        [JsonProperty("rating_score")]
        public int RatingScore { get; set; }
        [JsonProperty("is_pinned")]
        public bool IsPinned { get; set; }
        [JsonProperty("is_deprecated")]
        public bool IsDeprecated { get; set; }
        [JsonProperty("total_downloads")]
        public int TotalDownloads { get; set; }
        [JsonProperty("latest")]
        public ThunderstoreVersion? Latest { get; set; }
    }
}
