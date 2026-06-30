using launcherdotnet.PluginAPI;
using System.IO.Compression;
using System.Text.Json.Nodes;
using Semver;
using launcherdotnet;

[assembly: LauncherPlugin(typeof(MSZInstaller.Plugin),
    "Miside Zero Installer",
    "Downloads and installs Miside Zero from my Github mirror",
    "2.0.1")]

namespace MSZInstaller
{
    public class Plugin : IGameInstaller
    {
        public string GameName => "Miside Zero";

        public LabelQueryTime PromptForLabel => LabelQueryTime.BeforeInstall;

        private const string _releases = "https://api.github.com/repos/Gameknight963/MSZVersionArchive/releases";

        // first string is version, 2nd string is download url
        readonly Dictionary<string, string> _versions = new();

        public async Task Initialize()
        {
            PluginLogger.Log(launcherdotnet.Networking.LauncherHttp.Client.DefaultRequestHeaders.UserAgent.ToString());

            HttpResponseMessage resp = await launcherdotnet.Networking.LauncherHttp.Client.GetAsync(_releases);
            resp.EnsureSuccessStatusCode();

            string responseString = await resp.Content.ReadAsStringAsync();
            JsonArray releases = JsonNode.Parse(responseString)?.AsArray()
                ?? throw new InvalidOperationException("Invalid stable releases API response.");

            foreach (JsonNode? release in releases)
            {
                string? version = release?["tag_name"]?.ToString();
                if (version == null) continue;

                JsonArray? assets = release?["assets"]?.AsArray();
                if (assets == null) continue;

                JsonNode? downloadAsset = assets.FirstOrDefault(x =>
                    x?["name"]?.ToString().Contains("win64") == true);

                if (downloadAsset == null) continue;

                string downloadUrl = downloadAsset["browser_download_url"]?.ToString() ?? "";
                if (string.IsNullOrEmpty(downloadUrl)) continue;

                _versions[version] = downloadUrl;
            }
        }

        public async Task<PluginGameInfo?> Install(
            string installDir, 
            IProgress<double> progress, 
            IProgress<string> status, 
            string? version = null)

        {
            using HttpClient http = new HttpClient();

            using HttpResponseMessage response = await http.GetAsync(_versions[version!], HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            using InstanceTempDir tempDir = new InstanceTempDir();
            string zipPath = Path.Combine(tempDir.Path, $"MSZ-{Guid.NewGuid()}.zip");

            // --- download with progress ---
            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            using (FileStream fileStream = File.Create(zipPath))
            {
                long total = response.Content.Headers.ContentLength ?? -1;
                long readSoFar = 0;
                byte[] buffer = new byte[81920]; // 80KB buffer
                int bytesRead;
                while ((bytesRead = await responseStream.ReadAsync(buffer)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                    readSoFar += bytesRead;

                    if (total > 0)
                        progress?.Report(readSoFar * 100.0 / total);

                    status?.Report($"Downloading... {readSoFar / 1024.0 / 1024.0:0.0} MB");
                }
            }

            // --- extract with per-entry progress ---
            using ZipArchive archive = ZipFile.OpenRead(zipPath);
            int totalEntries = archive.Entries.Count;
            int extractedEntries = 0;

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                string destinationPath = Path.Combine(installDir, entry.FullName);

                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                if (!string.IsNullOrEmpty(entry.Name))
                    entry.ExtractToFile(destinationPath, true);

                extractedEntries++;
                progress?.Report(100.0 * extractedEntries / totalEntries);
                status?.Report($"Extracting {entry.FullName} ({extractedEntries}/{totalEntries})");
            }

            string exePath = Path.Combine(installDir, "MiSide Zero.exe");
            if (!File.Exists(exePath))
                throw new FileNotFoundException($"Extraction failed: could not find {exePath}");

            return new PluginGameInfo
            {
                ExePath = exePath,
                ThunderstoreCommunitySlug = "miside-zero",
            };
        }

        public IEnumerable<string>? GetReleases() => _versions.Keys;
    }
}
