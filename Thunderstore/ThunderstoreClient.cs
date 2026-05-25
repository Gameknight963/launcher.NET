using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using launcherdotnet.Networking;

namespace launcherdotnet.Thunderstore
{
    public static class ThunderstoreClient
    {
        public const string BaseUrl = "https://thunderstore.io";
        private static readonly HttpClient _http = LauncherHttp.Client;

        /// <summary>
        /// This endpoint will not include the Uuid4 of the package.
        /// </summary>
        public static async Task<ThunderstorePackage?> GetPackageAsync(string owner, string name)
        {
            string url = $"{BaseUrl}/api/experimental/package/{owner}/{name}/";
            LauncherLogger.WriteLine($"Fetching package: {url}");
            using Stream stream = await _http.GetStreamAsync(url);
            using StreamReader streamReader = new(stream);
            using JsonTextReader jsonReader = new(streamReader);

            JsonSerializer serializer = new();
            return serializer.Deserialize<ThunderstorePackage>(jsonReader);
        }

        public static async Task<ThunderstoreVersion?> GetPackageVersionAsync(string owner, string name, string version)
        {
            string url = $"{BaseUrl}/api/experimental/package/{owner}/{name}/{version}/";
            LauncherLogger.WriteLine($"Fetching package version: {url}");
            using Stream stream = await _http.GetStreamAsync(url);
            using StreamReader streamReader = new(stream);
            using JsonTextReader jsonReader = new(streamReader);
            JsonSerializer serializer = new();
            return serializer.Deserialize<ThunderstoreVersion>(jsonReader);
        }

        public static async Task<List<ThunderstoreVersion>> GetPackageVersionsAsync(string communitySlug, string uuid4)
        {
            string url = $"{BaseUrl}/c/{communitySlug}/api/v1/package/{uuid4}/";
            LauncherLogger.WriteLine($"Fetching versions: {url}");

            using Stream stream = await _http.GetStreamAsync(url);
            using StreamReader streamReader = new(stream);
            using JsonTextReader jsonReader = new(streamReader);

            JsonSerializer serializer = new();

            JObject? obj = serializer.Deserialize<JObject>(jsonReader);
            if (obj == null) return [];

            JToken? versionsToken = obj["versions"];
            if (versionsToken == null) return [];

            return versionsToken.ToObject<List<ThunderstoreVersion>>() ?? [];
        }

        public static async Task<string> GetPackageReadmeAsync(ThunderstorePackage pkg)
        {
            if (pkg.Latest is null) throw new InvalidOperationException(
                "Unable to fetch readme of a package that does not have a latest release");
            string url = $"https://thunderstore.io/api/experimental/package/" +
                $"{pkg.Namespace}/{pkg.Name}/{pkg.Latest.VersionNumber}/readme/";
            string json = await _http.GetStringAsync(url);
            return JObject.Parse(json)["markdown"]?.ToString() ?? "";
        }

        public static async Task<List<string>> GetPackageListIndexAsync(string communitySlug)
        {
            string url = $"{BaseUrl}/c/{communitySlug}/api/v1/package-listing-index/";
            LauncherLogger.WriteLine($"Fetching package list index: {url}");
            byte[] compressed = await _http.GetByteArrayAsync(url);
            LauncherLogger.WriteLine("Decompressing...");
            string json = DecompressGzip(compressed);
            LauncherLogger.WriteLine("Deserializing...");
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
