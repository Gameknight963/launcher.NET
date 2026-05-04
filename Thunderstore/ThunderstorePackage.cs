using Newtonsoft.Json;

namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackage
    {
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Description { get; set; } = "";
        public int RatingScore { get; set; }
        public bool IsDeprecated { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<string> Categories { get; set; } = [];
        public List<ThunderstoreVersion> Versions { get; set; } = [];

        [JsonIgnore]
        public ThunderstoreVersion? LatestVersion => Versions.FirstOrDefault();
    }
}
