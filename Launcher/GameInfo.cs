using launcherdotnet.Launcher.Settings;
using Newtonsoft.Json;

namespace launcherdotnet.Launcher
{
    public class GameInfo
    {
        public string Label { get; set; } = "";
        public string RelativePath { get; set; } = "";
        public string RelativeRootDirectory { get; set; } = "";
        public string GameName { get; set; } = "";
        public bool RunWithCmd { get; set; } = false;
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ThunderstoreCommunitySlug { get; set; } = null;
        public bool ModManagable = true;
        [JsonIgnore]
        public bool HasThunderstoreIntegration => ThunderstoreCommunitySlug != null;
        [JsonIgnore]
        public string AbsolutePath => Path.Combine(LauncherConstants.BaseDir, RelativePath);
        [JsonIgnore]
        public string AbsoluteRootDirectory => Path.Combine(LauncherConstants.BaseDir, RelativeRootDirectory);
        [JsonIgnore]
        public bool IsInsideLauncherRoot
        {
            get
            {
                string exeFull = Path.GetFullPath(Path.Combine(LauncherConstants.BaseDir, RelativePath));
                string rootFull = Path.GetFullPath(Path.Combine(LauncherConstants.BaseDir, RelativeRootDirectory));
                return exeFull.StartsWith(LauncherConstants.BaseDir) && rootFull.StartsWith(LauncherConstants.BaseDir);
            }
        }
        public bool IsValid(out string reason)
        {
            // Label cannot be empty
            if (string.IsNullOrWhiteSpace(Label))
            {
                reason = "Label cannot be empty.";
                return false;
            }
            // Path cannot be empty
            if (string.IsNullOrWhiteSpace(RelativePath))
            {
                reason = "RelativePath cannot be empty.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(RelativeRootDirectory))
            {
                reason = "RelativeRootDirectory cannot be empty.";
                return false;
            }
            // Relative paths cannot be absolute paths
            if (Path.IsPathRooted(RelativePath))
            {
                reason = "RelativePath cannot be an absolute path.";
                return false;
            }
            if (Path.IsPathRooted(RelativeRootDirectory))
            {
                reason = "RelativeRootDirectory cannot be an absolute path.";
                return false;
            }
            // Cannot escape launcher root
            if (!AbsolutePath.StartsWith(LauncherConstants.BaseDir, StringComparison.OrdinalIgnoreCase))
            {
                reason = $"AbsolutePath '{AbsolutePath}' is outside of launcher root.";
                return false;
            }
            if (!AbsoluteRootDirectory.StartsWith(LauncherConstants.BaseDir, StringComparison.OrdinalIgnoreCase))
            {
                reason = $"AbsoluteRootDirectory '{AbsoluteRootDirectory}' is outside of launcher root.";
                return false;
            }

            reason = "";
            return true;
        }

        public void EnsurePathsValid()
        {
            if (!IsValid(out string reason))
                throw new InvalidOperationException(reason);
        }
    }
}
