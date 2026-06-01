using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace launcherdotnet.PluginAPI
{
    public class PluginTools
    {
        /// <summary>
        /// Finds the most likely game EXE in a folder
        /// </summary>
        /// <param name="folderPath">The folder to search</param>
        /// <param name="gameSearchOptions"><see cref="GameSearchOptions"/> paramaters to use.</param>
        /// <param name="path">The found EXE</param>
        /// <returns>Whether a suitable EXE was found or not.</returns>
        /// <exception cref="FileNotFoundException">Thrown if no suitable EXE is found</exception>
        public static bool FindGameExe(string folderPath, [NotNullWhen(true)] out string? path, GameSearchOptions gameSearchOptions = new())
        {
            if (!Directory.Exists(folderPath)) throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            string folderName = Path.GetFileName(folderPath);

            // Get all exe files in the search directory (or top level, depending on options)

            SearchOption searchOption = gameSearchOptions.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] exes = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);

            List<string> candidates = exes.ToList();

            // Exclude some common helper EXEs
            if (gameSearchOptions.ExcludeHelpers)
            {
                LauncherLogger.WriteLine("excluding helprs");
                candidates = candidates
                    .Where(f => !f.EndsWith("UnityCrashHandler64.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("CrashReport.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("Uninstall.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("unins000.exe", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (candidates.Count == 0)
            {
                path = null;
                return false; 
            }

            // Try to find EXE matching the folder name
            string? matchByName = candidates.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).Contains(folderName, StringComparison.OrdinalIgnoreCase));

            if (matchByName != null)
            {
                path = matchByName;
                return true;
            }

            // Otherwise, pick the largest EXE
            path = candidates.OrderByDescending(f => new FileInfo(f).Length).First();
            return true;
        }

        /// <summary>
        /// Options that control how <see cref="FindGameExe"/> searches for game executables.
        /// </summary>
        /// <param name="searchSubdirs">
        /// Determines whether subdirectories should be included in the search.
        /// </param>
        /// <param name="excludeHelpers">
        /// Determines whether common helper executables should be excluded.
        /// </param>
        public struct GameSearchOptions(bool searchSubdirs = true, bool excludeHelpers = false)
        {
            /// <summary>
            /// If true, the search will include all subdirectories of the specified folder.
            /// If false, only the top-level folder will be searched.
            /// </summary>
            public bool SearchSubdirectories { get; set; } = searchSubdirs;

            /// <summary>
            /// If true, the search will include helpers such as the Unity crash handler.
            /// </summary>
            public bool ExcludeHelpers { get; set; } = excludeHelpers;

            public static GameSearchOptions SearchExcludeHelpers => new(true, true);
            public static GameSearchOptions SearchTopLevelOnly => new(false, false);
        }

        /// <summary>
        /// Guesses the Thunderstore slug of a game from it's name.
        /// </summary>
        /// <returns></returns>
        public static string ToThunderstoreSlug(string name)
        {
            name = name.ToLowerInvariant();
            name = Regex.Replace(name, @"[^a-z0-9\s-]", "");
            name = Regex.Replace(name, @"\s+", " ").Trim();
            name = name.Replace(" ", "-");
            name = Regex.Replace(name, @"-+", "-");
            return name;
        }

        public static void CopyDirectoryWithProgress(
            string sourceDir,
            string destDir,
            IProgress<double> progress,
            IProgress<string> status)
        {
            if (!Directory.Exists(sourceDir)) throw new DirectoryNotFoundException($"{sourceDir}: no such directory");
            string[] files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            int total = files.Length;
            int done = 0;

            foreach (string file in files)
            {
                string relative = Path.GetRelativePath(sourceDir, file);
                string target = Path.Combine(destDir, relative);

                Directory.CreateDirectory(Path.GetDirectoryName(target)!);

                File.Copy(file, target, true);

                done++;
                progress.Report(((double)done / total) * 100);
                status.Report($"Copying {done}/{total}");
            }
        }
    }
}
