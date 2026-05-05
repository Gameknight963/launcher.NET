using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePaginatedResponse
    {
        [JsonProperty("next")]
        public string? Next { get; set; }
        [JsonProperty("previous")]
        public string? Previous { get; set; }
        [JsonProperty("results")]
        public List<ThunderstorePackage> Results { get; set; } = [];
    }
}
