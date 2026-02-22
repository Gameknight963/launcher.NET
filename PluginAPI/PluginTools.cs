using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class PluginTools
    {
        /// <summary>
        /// Finds the most likely game EXE in a folder
        /// </summary>
        /// <param name="folderPath">The folder to search</param>
        /// <param name="gameSearchOptions"><see cref="GameSearchOptions"/> paramaters to use.</param>
        /// <returns>Full path to the found game EXE</returns>
        /// <exception cref="FileNotFoundException">Thrown if no suitable EXE is found</exception>
        public static string FindGameExe(string folderPath, GameSearchOptions gameSearchOptions = new())
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            string folderName = Path.GetFileName(folderPath);

            // Get all exe files in the search directory (or top level, depending on options)

            SearchOption searchOption = gameSearchOptions.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string[] exes = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);

            List<string> candidates = exes.ToList();

            // Exclude some common helper EXEs
            if (gameSearchOptions.ExcludeHelpers)
            {
                candidates = candidates
                    .Where(f => !f.EndsWith("UnityCrashHandler64.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("CrashReport.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("Uninstall.exe", StringComparison.OrdinalIgnoreCase) &&
                                !f.EndsWith("unins000.exe", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (candidates.Count == 0)
                throw new FileNotFoundException("No suitable executable found.");

            // Try to find EXE matching the folder name
            var matchByName = candidates.FirstOrDefault(f =>
                Path.GetFileNameWithoutExtension(f).IndexOf(folderName, StringComparison.OrdinalIgnoreCase) >= 0);

            if (matchByName != null)
                return matchByName;

            // Otherwise, pick the largest EXE
            return candidates.OrderByDescending(f => new FileInfo(f).Length).First();
        }

        /// <summary>
        /// Options that control how <see cref="PluginTools.FindGameExe"/> searches for game executables.
        /// </summary>
        public struct GameSearchOptions
        {
            /// <summary>
            /// If <c>true</c>, the search will include all subdirectories of the specified folder.
            /// If <c>false</c>, only the top-level folder will be searched.
            /// Default is <c>true</c>.
            /// </summary>
            public bool SearchSubdirectories { get; set; }

            /// <summary>
            /// If <c>true</c>, the search will include helpers such as the Unity crash handler.
            /// If <c>false</c>, helpers will be ignored.
            /// Default is <c>false</c>.
            /// </summary>
            public bool ExcludeHelpers { get; set; }
            
            /// <summary>
            /// Initializes a new instance of the <see cref="GameSearchOptions"/> struct with optional settings.
            /// </summary>
            /// <param name="searchSubdirs">
            /// Determines whether subdirectories should be included in the search.
            /// Defaults to <c>true</c>.
            /// </param>
            /// <param name="excludeHelpers">
            /// Determines whether common helper executables should be excluded.
            /// Defaults to <c>false</c>.
            /// </param>
            public GameSearchOptions(bool searchSubdirs = true, bool excludeHelpers = false)
            {
                SearchSubdirectories = searchSubdirs;
                ExcludeHelpers = excludeHelpers;
            }
        }
    }
}
