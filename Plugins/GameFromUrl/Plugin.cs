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

        private static bool IsZipMagicBytes(byte[] header, int bytesRead) =>
            bytesRead == 4 &&
            header[0] == 0x50 && header[1] == 0x4B &&
            header[2] == 0x03 && header[3] == 0x04;

        private static async Task CopyFileToZip(string sourcePath, string zipPath, IProgress<double> progress, IProgress<string> status)
        {
            long totalBytes = new FileInfo(sourcePath).Length;
            long copiedBytes = 0;
            byte[] buffer = new byte[81920];

            await using FileStream src = File.OpenRead(sourcePath);
            await using FileStream dst = File.Create(zipPath);

            // Validate ZIP magic bytes
            byte[] header = new byte[4];
            int bytesRead = await src.ReadAsync(header);
            if (!IsZipMagicBytes(header, bytesRead))
                throw new InvalidDataException("The file is not a ZIP archive.");

            await dst.WriteAsync(header.AsMemory(0, bytesRead));
            copiedBytes += bytesRead;

            while (true)
            {
                int read = await src.ReadAsync(buffer);
                if (read == 0) break;
                await dst.WriteAsync(buffer.AsMemory(0, read));
                copiedBytes += read;

                if (totalBytes > 0)
                {
                    double percent = (double)copiedBytes * 100 / totalBytes;
                    progress.Report(percent);
                    status.Report($"Copied {copiedBytes} out of {totalBytes} bytes ({percent:F1}%)");
                }
            }
        }

        private static async Task DownloadToZip(Uri uri, string zipPath, IProgress<double> progress, IProgress<string> status)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            using HttpResponseMessage response = await LauncherHttp.Client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using FileStream fileStream = File.Create(zipPath);
            await using Stream downloadStream = await response.Content.ReadAsStreamAsync();

            byte[] header = new byte[4];
            int bytesRead = await downloadStream.ReadAsync(header);
            if (!IsZipMagicBytes(header, bytesRead))
                throw new InvalidDataException("The URL does not point to a ZIP file.");

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
                    status.Report($"Downloaded {downloadedBytes} out of {totalBytes} bytes ({percent:F1}%)");
                }
            }

            await fileStream.FlushAsync();
        }

        private static async Task CopyFolderToInstallDir(string sourceDir, string installDir, IProgress<double> progress, IProgress<string> status)
        {
            string[] files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            long totalBytes = files.Sum(f => new FileInfo(f).Length);
            long copiedBytes = 0;
            byte[] buffer = new byte[81920];

            foreach (string srcFile in files)
            {
                string relative = Path.GetRelativePath(sourceDir, srcFile);
                string destFile = Path.Combine(installDir, relative);
                Directory.CreateDirectory(Path.GetDirectoryName(destFile)!);

                await using FileStream src = File.OpenRead(srcFile);
                await using FileStream dst = File.Create(destFile);

                int read;
                while ((read = await src.ReadAsync(buffer)) > 0)
                {
                    await dst.WriteAsync(buffer.AsMemory(0, read));
                    copiedBytes += read;

                    if (totalBytes > 0)
                    {
                        double percent = (double)copiedBytes * 100 / totalBytes;
                        progress.Report(percent);
                        status.Report($"Copying {relative} — {copiedBytes} of {totalBytes} bytes ({percent:F1}%)");
                    }
                }
            }
        }

        public async Task<PluginGameInfo?> Install(string installDir, IProgress<double> progress, IProgress<string> status, string? version = null)
        {
            string? input = UrlInputBox.Prompt();
            if (input == null) return null;

            if (!input.Contains("://"))
                input = "https://" + input;

            if (!Uri.TryCreate(input, UriKind.Absolute, out Uri? uri) ||
                (uri.Scheme != Uri.UriSchemeFile && !uri.Host.Contains('.')))
            {
                CoolMessageBox.Show($"'{input}' is not a valid URL or file path.", "Invalid Input");
                return null;
            }

            bool isFileScheme = uri.Scheme == Uri.UriSchemeFile;
            bool isHttp = uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;

            if (!isFileScheme && !isHttp)
            {
                CoolMessageBox.Show("Only HTTP, HTTPS, and file:// are supported.", "Unsupported Protocol", icon: MessageBoxIcon.Error);
                return null;
            }

            bool isDirectory = isFileScheme && Directory.Exists(uri.LocalPath);
            bool isFile = isFileScheme && File.Exists(uri.LocalPath);

            if (isFileScheme && !isDirectory && !isFile)
            {
                CoolMessageBox.Show($"Path not found: {uri.LocalPath}", "Not Found", icon: MessageBoxIcon.Error);
                return null;
            }

            try
            {
                if (isDirectory)
                {
                    status.Report("Copying folder...");
                    await CopyFolderToInstallDir(uri.LocalPath, installDir, progress, status);
                }
                else if (isFile)
                {
                    status.Report("Copying file...");
                    using InstanceTempDir temp = new();
                    string zipPath = Path.Combine(temp.Path, "game.zip");
                    await CopyFileToZip(uri.LocalPath, zipPath, progress, status);
                    status.Report("Extracting archive...");
                    await ZipFile.ExtractToDirectoryAsync(zipPath, installDir);
                }
                else
                {
                    status.Report("Downloading...");
                    using InstanceTempDir temp = new();
                    string zipPath = Path.Combine(temp.Path, "game.zip");
                    await DownloadToZip(uri, zipPath, progress, status);
                    status.Report("Extracting archive...");
                    await ZipFile.ExtractToDirectoryAsync(zipPath, installDir);
                }
            }
            catch (InvalidDataException ex)
            {
                CoolMessageBox.Show(ex.Message, "Invalid File", icon: MessageBoxIcon.Error);
                return null;
            }

            if (!PluginTools.FindGameExe(installDir, out string? path, PluginTools.GameSearchOptions.SearchExcludeHelpers))
            {
                if (CoolMessageBox.Show(
                    "The executable of the game could not be found.\n" +
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
