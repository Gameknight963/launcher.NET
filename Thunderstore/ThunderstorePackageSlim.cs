namespace launcherdotnet.Thunderstore
{
    public class ThunderstorePackageSlim
    {
        public string Name { get; set; } = "";
        public string Owner { get; set; } = "";
        public string FullName { get; set; } = "";
        public bool IsDeprecated { get; set; }
        public string? DownloadUrl { get; set; }
        public string? IconUrl { get; set; }
        /// <summary>
        /// Uuid4 may not be included depending on which endpoint you call
        /// </summary>
        public string? Uuid4 { get; set; }

        public static ThunderstorePackageSlim FromPackage(ThunderstorePackage package)
        {
            return new ThunderstorePackageSlim
            {
                Name = package.Name,
                Owner = package.Owner,
                FullName = package.FullName,
                IsDeprecated = package.IsDeprecated,
                DownloadUrl = package.Latest?.DownloadUrl,
                IconUrl = package.Latest?.Icon,
                Uuid4 = package.Uuid4
            };
        }

        /// <summary>
        /// This will include the Uuid4 of the package.
        /// </summary>
        /// <returns></returns>
        public async Task<ThunderstorePackage?> FetchFullPackageAsync()
        {
            ThunderstorePackage? pkg = await ThunderstoreClient.GetPackageAsync(Owner, Name);
            if (pkg == null) return null;
            pkg.Uuid4 = Uuid4;
            return pkg;
        }
    }
}