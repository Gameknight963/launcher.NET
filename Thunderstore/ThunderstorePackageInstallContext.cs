namespace launcherdotnet.Thunderstore
{
    public record ThunderstorePackageInstallContext
    {
        public string Name { get; set; }
        public string DownloadUrl { get; set; }
        public string Version { get; set; }

        public ThunderstorePackageInstallContext(string name, string downloadUrl, string version)
        {
            Name = name;
            DownloadUrl = downloadUrl;
            Version = version;
        }
    }
}
