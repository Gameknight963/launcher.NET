using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackage
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("full_name")]
        public string FullName { get; set; } = "";
        [JsonProperty("owner")]
        public string Owner { get; set; } = "";
        [JsonProperty("description")]
        public string Description { get; set; } = "";
        [JsonProperty("rating_score")]
        public int RatingScore { get; set; }
        [JsonProperty("is_deprecated")]
        public bool IsDeprecated { get; set; }
        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }
        [JsonProperty("categories")]
        public List<string> Categories { get; set; } = [];
        [JsonProperty("versions")]
        public List<ThunderstoreVersion> Versions { get; set; } = [];
        [JsonIgnore]
        public ThunderstoreVersion? LatestVersion => Versions.FirstOrDefault();
    }
}
