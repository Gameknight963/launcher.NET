using launcherdotnet.PluginAPI;
using Newtonsoft.Json;
using System.IO.Compression;

namespace launcherdotnet.Thunderstore
{
    public static class ThunderstoreClient
    {
        private static readonly HttpClient _http = new();
        public const string BaseUrl = "https://thunderstore.io";

        public static async Task<List<ThunderstorePackage>> GetPackagesAsync(string communitySlug)
        {
            string url = $"{BaseUrl}/c/{communitySlug}/api/v1/package/";
            string json = await _http.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<ThunderstorePackage>>(json) ?? [];
        }

        public static async Task DownloadModAsync(ThunderstoreVersion version, string modsDirectory)
        {
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "mod.zip");

            byte[] data = await _http.GetByteArrayAsync(version.DownloadUrl);
            await File.WriteAllBytesAsync(zipPath, data);

            using ZipArchive zip = ZipFile.OpenRead(zipPath);
            zip.ExtractToDirectory(modsDirectory, overwriteFiles: true);
        }
    }
}
