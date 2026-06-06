using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace launcherdotnet.PluginAPI
{
    public class PluginTools
    {
        private static readonly string[] _helperExeNames =
        [
            "UnityCrashHandler64.exe",
            "UnityCrashHandler32.exe",
            "CrashReport.exe",
            "Uninstall.exe",
            "unins000.exe",
            "dxsetup.exe",
            "vcredist_x64.exe",
            "vcredist_x86.exe",
            "vc_redist.x64.exe",
            "vc_redist.x86.exe",
            "DirectX.exe",
            "setup.exe",
            "install.exe",
            "dotNetFx.exe",
            "UEPrereqSetup_x64.exe",
            "UEPrereqSetup_x86.exe",
            "PhysXSetup.exe",
        ];

        private static readonly string[] _nonGameNamePatterns =
        [
            "setup", "redist", "install", "vcredist", "directx",
            "prereq", "crashhandler", "crash_handler", "unins"
        ];
        private static string NormalizeName(string s) =>
            Regex.Replace(s, @"[\s_\-]", "", RegexOptions.None).ToLowerInvariant();

        /// <summary>
        /// Finds the most likely game EXE in a folder
        /// </summary>
        /// <param name="folderPath">The folder to search</param>
        /// <param name="gameSearchOptions"><see cref="GameSearchOptions"/> paramaters to use.</param>
        /// <param name="path">The found EXE. Not null if returned true.</param>
        /// <returns>Whether a suitable EXE was found or not.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static bool FindGameExe(string folderPath, [NotNullWhen(true)] out string? path, GameSearchOptions gameSearchOptions = new())
        {
            if (!Directory.Exists(folderPath)) throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
            string folderName = Path.GetFileName(folderPath);
            SearchOption searchOption = gameSearchOptions.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] exes = Directory.GetFiles(folderPath, "*.exe", searchOption);
            List<string> candidates = exes.ToList();

            // exclude known helper exes by filename
            if (gameSearchOptions.ExcludeHelpers)
            {
                candidates = candidates
                    .Where(f =>
                    {
                        string fileName = Path.GetFileName(f);
                        string fileNameLower = Path.GetFileNameWithoutExtension(f).ToLowerInvariant();
                        return !_helperExeNames.Any(h => fileName.Equals(h, StringComparison.OrdinalIgnoreCase))
                            && !_nonGameNamePatterns.Any(p => fileNameLower.Contains(p));
                    })
                    .ToList();
            }

            if (candidates.Count == 0)
            {
                path = null;
                return false;
            }

            // prefer top-level exes
            List<string> topLevel = candidates
                .Where(f => Path.GetDirectoryName(f) == folderPath)
                .ToList();

            List<string> searchCandidates = topLevel.Count > 0 ? topLevel : candidates;

            if (searchCandidates.Count == 1)
            {
                path = searchCandidates[0];
                return true;
            }

            // fuzzy name match
            string normalizedFolder = NormalizeName(folderName);
            string? matchByName = searchCandidates.FirstOrDefault(f =>
            {
                string normalizedExe = NormalizeName(Path.GetFileNameWithoutExtension(f));
                return normalizedExe.Contains(normalizedFolder) || normalizedFolder.Contains(normalizedExe);
            });

            // if no match in top-level, retry with full candidate list
            if (matchByName == null && topLevel.Count > 0 && topLevel.Count != candidates.Count)
            {
                matchByName = candidates.FirstOrDefault(f =>
                {
                    string normalizedExe = NormalizeName(Path.GetFileNameWithoutExtension(f));
                    return normalizedExe.Contains(normalizedFolder) || normalizedFolder.Contains(normalizedExe);
                });
                if (matchByName == null) searchCandidates = candidates; // also use full list for size fallback
            }

            if (matchByName != null)
            {
                path = matchByName;
                return true;
            }

            // fall back to MultipleExesMode
            path = gameSearchOptions.MultipleExesMode switch
            {
                MultipleExesMode.PickLargest => candidates.OrderByDescending(f => new FileInfo(f).Length).First(),
                MultipleExesMode.PickSmallest => candidates.OrderBy(f => new FileInfo(f).Length).First(),
                MultipleExesMode.Alphabetical => candidates.OrderBy(f => Path.GetFileName(f), StringComparer.OrdinalIgnoreCase).First(),
                MultipleExesMode.ReturnFalse => null,
                _ => candidates.OrderByDescending(f => new FileInfo(f).Length).First()
            };

            if (path == null) return false;
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
        /// <param name="multipleExesMode">What to do if there are multiple canadite exes</param>
        public struct GameSearchOptions(bool searchSubdirs = true, bool excludeHelpers = false, MultipleExesMode multipleExesMode = MultipleExesMode.PickLargest)
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

            /// <summary>
            /// What to do if there are multiple canadite exes
            /// </summary>
            public MultipleExesMode MultipleExesMode { get; set; } = multipleExesMode;

            public static GameSearchOptions SearchExcludeHelpers => new(true, true);
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

        public static string FormatSize(long bytes)
        {
            // future proof ig
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB" };

            double size = bytes;
            int unit = 0;

            while (size >= 1024 && unit < units.Length - 1)
            {
                size /= 1024;
                unit++;
            }

            return $"{size:0.##} {units[unit]}";
        }
    }
}
