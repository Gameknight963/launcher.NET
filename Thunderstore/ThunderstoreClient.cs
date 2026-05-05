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

        public static async Task<List<string>> GetPackageListIndexAsync(string communitySlug)
        {
            string url = $"{BaseUrl}/c/{communitySlug}/api/v1/package-listing-index/";
            LauncherLogger.WriteLine($"Fetching package list index: {url}");
            byte[] compressed = await _http.GetByteArrayAsync(url);
            string json = DecompressGzip(compressed);
            List<string>? chunkUrls = JsonConvert.DeserializeObject<List<string>>(json);
            LauncherLogger.WriteLine($"Got {chunkUrls?.Count ?? 0} chunk URLs");
            return chunkUrls ?? [];
        }

        public static async Task<List<ThunderstorePackageSlim>> GetPackageListChunkAsync(string chunkUrl)
        {
            LauncherLogger.WriteLine($"Fetching chunk: {chunkUrl}");
            using Stream compressed = await _http.GetStreamAsync(chunkUrl);
            using GZipStream gzipStream = new(compressed, CompressionMode.Decompress);
            using StreamReader streamReader = new(gzipStream);
            using JsonTextReader jsonReader = new(streamReader);
            JsonSerializer serializer = new();
            serializer.Converters.Add(new ThunderstorePackageSlimConverter());
            List<ThunderstorePackageSlim>? packages = serializer.Deserialize<List<ThunderstorePackageSlim>>(jsonReader);
            LauncherLogger.WriteLine($"Got {packages?.Count ?? 0} packages from chunk");
            return packages ?? [];
        }

        private static string DecompressGzip(byte[] compressed)
        {
            using MemoryStream inputStream = new(compressed);
            using GZipStream gzipStream = new(inputStream, CompressionMode.Decompress);
            using StreamReader reader = new(gzipStream);
            return reader.ReadToEnd();
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
