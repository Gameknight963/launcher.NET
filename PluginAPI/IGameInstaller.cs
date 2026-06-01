namespace launcherdotnet.PluginAPI
{
    public interface IGameInstaller : ILauncherPlugin
    {
        /// <summary>
        /// Install the game to the given directory.
        /// </summary>
        /// <param name="installDir">Where the game should be installed. This directory should directly contain the game files.</param>
        /// <param name="progress">Reports progress from 0 to 100, displayed on the installer's progress bar.</param>
        /// <param name="status">Reports a status message string, displayed on the bottom of the installer.</param>
        /// <param name="release">The <see cref="ReleaseInfo"/> the user selected to install.</param>
        /// <returns>The <see cref="PluginGameInfo"/> of the installed game. Return null to cancel the installation.</returns>
        Task<PluginGameInfo?> Install(
            string installDir, 
            IProgress<double> progress,
            IProgress<string> status,
            ReleaseInfo? release = null);

        /// <summary>
        /// The name of the game your installer plugin installs. Used for the game selection dropdown
        /// </summary>
        string GameName { get; } 

        /// <summary>
        /// Whether the installer will prompt the user to choose a Label or not. If false, defaults to GameName
        /// </summary>
        bool PromptForLabel { get; }

        /// <summary>
        /// Gets the releases of your plugin. This should be fetched on initialization and cached to prevent delays in the UI.
        /// If you're making a versionless installer, return null.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ReleaseInfo>? GetReleases();
    }
}   