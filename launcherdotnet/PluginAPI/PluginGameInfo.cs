using launcherdotnet.Launcher;

namespace launcherdotnet.PluginAPI
{
    public class PluginGameInfo
    {
        /// <summary>
        /// The executable used to launch your game.
        /// </summary>
        public required string ExePath;
        /// <summary>
        /// Whether the game should be run using a cmd command.
        /// </summary>
        public bool RunWithCmd = false;

        [Obsolete("launcher.net will no longer respond to thunderstore-related properties")]
        public string? ThunderstoreCommunitySlug;

        [Obsolete("Specify a mod manager with ModManagerId. This field does nothing.")]
        public bool ModManageable = true;

        public readonly string Id = Guid.NewGuid().ToString();

        public string DataDirectory => GameInfo.GetDataDirectory(Id);

        /// <summary>
        /// The label this game will have. Override's the user's selection, so only specify if you're using <see cref="LabelQueryTime.Never"/>.
        /// </summary>
        public string? Label;
        /// <summary>
        /// The id of the <see cref="IModSource"/> used to manage mods for this game.
        /// Leave null to disable mod management.
        /// </summary>
        public string? ModManagerId;
        /// <summary>
        /// The name of this game (Lethal Company, Repo, etc).
        /// </summary>
        /// <remarks>Leaving it blank will default to <see cref="IGameInstaller.GameName"/>.</remarks>
        public string? GameName;
    }
}
