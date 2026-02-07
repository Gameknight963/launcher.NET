using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace launcherdotnet
{
    public class Install
    {
        public static async Task<string> DownloadAndInstallGameAsync(string gameIdOrUrl, string destinationDir, Action<string> setStatus)
        {
            setStatus("Preparing temporary directory...");
            string tempDir = Path.Combine(destinationDir, "temp");
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
            Directory.CreateDirectory(tempDir);
            
            setStatus("Downloading...");
            string zipPath = Path.Combine(tempDir, "dummyGame.zip");
            if (!File.Exists(zipPath))
            {
                using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    string folderName = "DummyGameFolder/";
                    var entry = archive.CreateEntry(folderName + "DummyGame.exe");
                    using var stream = entry.Open();
                    byte[] content = System.Text.Encoding.UTF8.GetBytes("echo hello");
                    stream.Write(content, 0, content.Length);
                }
            }
            await Task.Delay(500); // simulate async download

            setStatus($"Extracting {zipPath}...");
            string extractDir = Path.Combine(tempDir, "extracted");
            Directory.CreateDirectory(extractDir);
            ZipFile.ExtractToDirectory(zipPath, extractDir);

            var extractedItems = Directory.GetDirectories(extractDir)
                .Concat(Directory.GetFiles(extractDir))
                .ToList();

            if (extractedItems.Count != 1)
                throw new Exception("ZIP should contain exactly one folder or file at top level.");

            string extractedFolder = extractedItems[0];

            string finalFolder = Path.Combine(destinationDir, Path.GetFileName(extractedFolder));
            if (Directory.Exists(finalFolder))
            {
                DialogResult result = MessageBox.Show("An instance of the downloaded version already exists. Overwrite?",
                    "Overwrite? ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    Directory.Delete(finalFolder, true);
                    Directory.Move(extractedFolder, finalFolder);
                }
                if (result == DialogResult.No)
                    MessageBox.Show("Install aborted.",
                        "Notice", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }

            setStatus("Cleaning up...");
            Directory.Delete(tempDir, true);

            Console.WriteLine($"[INFO] Installed game to {finalFolder}");
            setStatus($"Installed to {finalFolder}");
            return finalFolder;
        }
    }
}
