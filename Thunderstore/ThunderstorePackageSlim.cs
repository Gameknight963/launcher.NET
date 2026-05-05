namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackageSlim
    {
        public string Name { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsDeprecated { get; set; }
        public string? DownloadUrl { get; set; }
        public string? IconUrl { get; set; }

        public static ThunderstorePackageSlim FromPackage(ThunderstorePackage package)
        {
            return new ThunderstorePackageSlim
            {
                Name = package.Name,
                Owner = package.Owner,
                Description = package.Description,
                IsDeprecated = package.IsDeprecated,
                DownloadUrl = package.LatestVersion?.DownloadUrl,
                IconUrl = package.LatestVersion?.Icon
            };
        }

        public async Task<ThunderstorePackage?> FetchFullPackageAsync()
            => await ThunderstoreClient.GetPackageAsync(Owner, Name);
    }
}
