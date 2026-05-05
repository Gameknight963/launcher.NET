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
            LauncherLogger.WriteLine($"Fetching packages from {url}");
            string json = await _http.GetStringAsync(url);
            LauncherLogger.WriteLine($"Got response, length: {json.Length}");
            LauncherLogger.WriteLine($"First 500 chars: {json[..Math.Min(500, json.Length)]}");
            List<ThunderstorePackage>? result = JsonConvert.DeserializeObject<List<ThunderstorePackage>>(json);
            LauncherLogger.WriteLine($"Deserialized {result?.Count ?? 0} packages", true);
            if (result?.Count > 0)
            {
                LauncherLogger.WriteLine($"First package name: '{result[0].Name}'");
                LauncherLogger.WriteLine($"First package versions: {result[0].Versions.Count}");
            }
            return result ?? [];
        }

        public static async Task<List<ThunderstorePackage>?> GetPackagesPageAsync(string communitySlug, int page = 1)
        {
            string url = $"{BaseUrl}/api/experimental/package/?community={communitySlug}&page={page}";
            LauncherLogger.WriteLine($"Fetching page: {url}");
            string json = await _http.GetStringAsync(url);
            LauncherLogger.WriteLine($"Got response, length: {json.Length}");
            ThunderstorePaginatedResponse? resp = JsonConvert.DeserializeObject<ThunderstorePaginatedResponse>(json);
            if (resp == null)
                LauncherLogger.WriteLine("Deserialization returned null.");
            else
                LauncherLogger.WriteLine($"Got {resp.Results.Count} packages, next: {resp.Next ?? "none"}");
            return resp?.Results;
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
