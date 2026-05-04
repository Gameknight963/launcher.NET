namespace launcherdotnet.Thunderstore
{
    public class ThunderstoreVersion
    {
        public string VersionNumber { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public long FileSize { get; set; }
        public List<string> Dependencies { get; set; } = [];
    }
}
