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
        /// <summary>
        /// The slug used by Thunderstore APIs to identify your game.
        /// </summary>
        public string? ThunderstoreCommunitySlug;
        /// <summary>
        /// Whether the launcher.net's mod manager should be enabled for this game. Defaults to true.
        /// </summary>
        /// <remarks>Note that users can still turn it on manually.</remarks>
        public bool ModManageable = true;
        /// <summary>
        /// The label this game will have. Override's the user's selection, so only specify if you're using <see cref="LabelQueryTime.Never"/>.
        /// </summary>
        public string? Label;
        /// <summary>
        /// The name of this game (Lethal Company, Repo, etc).
        /// </summary>
        /// <remarks>Leaving it blank will default to <see cref="IGameInstaller.GameName"/>.</remarks>
        public string? GameName;
    }
}
