using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace launcherdotnet
{
    public class GameInfo
    {
        public string Label { get; set; } = "";
        public string RelativePath { get; set; } = "";
        public string RelativeRootDirectory { get; set; } = "";
        public string GameName { get; set; } = "Unspecified";
        public bool RunWithCmd { get; set; } = false;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonIgnore]
        public string AbsolutePath => Path.Combine(Config.BaseDir, RelativePath);
        [JsonIgnore]
        public string AbsoluteRootDirectory => Path.Combine(Config.BaseDir, RelativeRootDirectory);
        [JsonIgnore]
        public bool IsInsideLauncherRoot
        {
            get
            {
                string exeFull = Path.GetFullPath(Path.Combine(Config.BaseDir, RelativePath));
                string rootFull = Path.GetFullPath(Path.Combine(Config.BaseDir, RelativeRootDirectory));
                return exeFull.StartsWith(Config.BaseDir, StringComparison.OrdinalIgnoreCase);
            }
        }
        public void EnsurePathsValid()
        {
            // Path cannot be empty
            if (string.IsNullOrWhiteSpace(RelativePath))
                throw new InvalidOperationException("RelativePath is empty.");

            if (string.IsNullOrWhiteSpace(RelativeRootDirectory))
                throw new InvalidOperationException("RelativeRootDirectory is empty.");

            // Cannot escape launcher root
            if (!AbsolutePath.StartsWith(Config.BaseDir, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"FullPath '{AbsolutePath}' is outside of launcher root.");

            if (!AbsoluteRootDirectory.StartsWith(Config.BaseDir, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"FullRootDirectory '{AbsoluteRootDirectory}' is outside of launcher root.");

            // relative paths cannot be absolute paths
            if (Path.IsPathRooted(RelativePath))
                throw new InvalidOperationException("RelativePath cannot be an absolute path.");

            if (Path.IsPathRooted(RelativeRootDirectory))
                throw new InvalidOperationException("RelativeRootDirectory cannot be an absolute path.");
        }
    }
}
