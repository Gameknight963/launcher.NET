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
        /// <returns>Full path to the found game EXE</returns>
        /// <exception cref="FileNotFoundException">Thrown if no suitable EXE is found</exception>
        public static string FindGameExe(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            string folderName = Path.GetFileName(folderPath);

            // Get all exe files in the search directory
            string[] exes = Directory.GetFiles(folderPath, "*.exe", SearchOption.AllDirectories);

            // Exclude some common helper EXEs
            var candidates = exes
                .Where(f => !f.EndsWith("UnityCrashHandler64.exe", StringComparison.OrdinalIgnoreCase) &&
                            !f.EndsWith("CrashReport.exe", StringComparison.OrdinalIgnoreCase) &&
                            !f.EndsWith("Uninstall.exe", StringComparison.OrdinalIgnoreCase) &&
                            !f.EndsWith("unins000.exe", StringComparison.OrdinalIgnoreCase))
                .ToList();

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
    }
}
