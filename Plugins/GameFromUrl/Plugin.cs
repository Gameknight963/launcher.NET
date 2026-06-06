using launcherdotnet.Launcher.Forms;
using launcherdotnet.Networking;
using launcherdotnet.PluginAPI;
using launcherdotnet.Plugins.GameFromUrl;
using System.IO.Compression;
using System.Windows.Forms;

[assembly: LauncherPlugin(typeof(Plugin),
    "Game from Url",
    "Downloads a ZIP from a URL and installs it",
    "2.0.0")]

namespace launcherdotnet.Plugins.GameFromUrl
{
    public class Plugin : IGameInstaller
    {
        public string GameName => "Game from Url";

        public LabelQueryTime PromptForLabel => LabelQueryTime.Never;

        public IEnumerable<string>? GetReleases() => null;

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public async Task<PluginGameInfo?> Install(string installDir, IProgress<double> progress, IProgress<string> status, string? version = null)
        {
            string? url = CoolInputBox.Prompt("Enter a URL to download from:");
            if (url == null) return null; // user cancelled
            string url2 = url;
            if (!url.Contains("://")) url = "https://" + url;
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) || !uri.Host.Contains('.'))
            {
                CoolMessageBox.Show($"'{url2}' is not a valid URL.", "Invalid URL");
                return null;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                CoolMessageBox.Show("Only HTTP and HTTPS supported.", "Unsupported Protocol", icon: MessageBoxIcon.Error);
                return null;
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);

            using HttpResponseMessage response = await LauncherHttp.Client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            using InstanceTempDir temp = new();
            string zipPath = Path.Combine(temp.Path, "game.zip");
            await using FileStream fileStream = File.Create(zipPath);
            await using Stream downloadStream = await response.Content.ReadAsStreamAsync();
            byte[] header = new byte[4];
            int bytesRead = await downloadStream.ReadAsync(header);

            bool isZip =
                bytesRead == 4 &&
                header[0] == 0x50 &&
                header[1] == 0x4B &&
                header[2] == 0x03 &&
                header[3] == 0x04;

            if (!isZip)
            {
                CoolMessageBox.Show(
                    "The URL does not point to a ZIP file.",
                    "Invalid File",
                    icon: MessageBoxIcon.Error);

                return null;
            }
            await fileStream.WriteAsync(header.AsMemory(0, bytesRead));

            long downloadedBytes = bytesRead;
            long totalBytes = response.Content.Headers.ContentLength ?? -1;

            byte[] buffer = new byte[81920];

            while (true)
            {
                int read = await downloadStream.ReadAsync(buffer);
                if (read == 0) break;
                await fileStream.WriteAsync(buffer.AsMemory(0, read));
                downloadedBytes += read;

                if (totalBytes > 0)
                {
                    double percent = (double)downloadedBytes * 100 / totalBytes;
                    progress.Report(percent);
                    status.Report($"Downloaded {downloadedBytes} out of {totalBytes} bytes ({percent}%)");
                }
            }

            await fileStream.FlushAsync();
            fileStream.Dispose();
            status.Report("Extracting archive...");
            await ZipFile.ExtractToDirectoryAsync(zipPath, installDir);

            if (!PluginTools.FindGameExe(installDir, out string? path, PluginTools.GameSearchOptions.SearchExcludeHelpers))
            {
                if (CoolMessageBox.Show("The executable of the game could not be found.\n" +
                    "Would you like to select it manually?\n" +
                    "Cancelling will abort and delete the downloaded game.",
                    "User Input Required",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    Directory.Delete(installDir, true);
                    return null;
                }
                using OpenFileDialog dialog = new();
                dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                dialog.Title = "Select the game executable";
                if (dialog.ShowDialog() != DialogResult.OK) return null;
                path = dialog.FileName;
            }

            return new PluginGameInfo
            {
                ExePath = path,
                Label = Launcher.LauncherDialogs.QueryLabel(Path.GetFileNameWithoutExtension(path))
            };
        }
    }
}
