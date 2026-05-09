namespace launcherdotnet.Thunderstore
{
    public record ThunderstorePackageInstallContext
    {
        public string Name { get; set; }
        public string DownloadUrl { get; set; }
        public string Owner { get; set; }
        public string Version { get; set; }

        public ThunderstorePackageInstallContext(string name, string downloadUrl, string version, string owner)
        {
            Name = name;
            DownloadUrl = downloadUrl;
            Version = version;
            Owner = owner;
        }

        public static ThunderstorePackageInstallContext FromPackageSlim(ThunderstorePackageSlim slim, string version)
        {
            if (slim.DownloadUrl == null) throw new ArgumentException("Package slim download url is null");
            return new ThunderstorePackageInstallContext(slim.Name, slim.DownloadUrl, version, slim.Owner);
        }

        public async Task<ThunderstoreVersion?> FetchThunderstoreVersionAsync()
        {
            ThunderstoreVersion? ver = await ThunderstoreClient.GetPackageVersionAsync(Owner, Name, Version);
            if (ver is null) return null;
            return ver;
        }
    }
}
