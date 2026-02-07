using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace launcherdotnet
{
    public class Install
    {
        public static async Task<string?> DownloadAndInstallGameAsync(string gameIdOrUrl, string destinationDir, Action<string> setStatus)
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
            bool shouldIUpdateConfig = true;

                if (Directory.Exists(finalFolder))
                {
                    shouldIUpdateConfig = false;
                    DialogResult result = MessageBox.Show("An instance of the downloaded version already exists. Overwrite?",
                        "Overwrite? ",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Directory.Delete(finalFolder, true);
                    }
                    if (result == DialogResult.No)
                    {
                        MessageBox.Show("Install aborted.",
                        "Notice", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        return null;
                    }
                }

                Directory.Move(extractedFolder, finalFolder);

            LauncherData data = LauncherDataManager.ReadLauncherData();
            if (shouldIUpdateConfig == true)
            {
                string exePath = FindGameExe(finalFolder);
                data.Versions.Add(new GameInfo
                {
                    Label = Path.GetFileName(finalFolder),
                    Path = exePath
                });
                LauncherDataManager.SaveLauncherData(data);
            }

            setStatus("Cleaning up...");
            Directory.Delete(tempDir, true);

            Console.WriteLine($"[INFO] Installed game to {finalFolder}");
            setStatus($"Installed to {finalFolder}");
            return finalFolder;
        }
        /// <summary>
        /// Finds the most likely game EXE in a folder
        /// </summary>
        /// <param name="folderPath">The folder to search</param>
        /// <returns>Full path to the game EXE</returns>
        /// <exception cref="FileNotFoundException">Thrown if no suitable EXE is found</exception>
        public static string FindGameExe(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            string folderName = Path.GetFileName(folderPath);

            // Get all .exe files in the top-level folder
            string[] exes = Directory.GetFiles(folderPath, "*.exe", SearchOption.TopDirectoryOnly);

            // Exclude common helper EXEs
            var candidates = exes
                .Where(f => !f.EndsWith("UnityCrashHandler64.exe", StringComparison.OrdinalIgnoreCase) &&
                            !f.EndsWith("CrashReport.exe", StringComparison.OrdinalIgnoreCase) &&
                            !f.EndsWith("Uninstall.exe", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (candidates.Count == 0)
                throw new FileNotFoundException("No suitable game EXE found in folder.");

            // 1️⃣ Try to find EXE matching the folder name
            var matchByName = candidates.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).IndexOf(folderName, StringComparison.OrdinalIgnoreCase) >= 0);

            if (matchByName != null)
                return matchByName;

            // 2️⃣ Otherwise, pick the largest EXE
            return candidates.OrderByDescending(f => new FileInfo(f).Length).First();
        }
    }
}
