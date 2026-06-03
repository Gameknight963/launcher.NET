namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Installs a game into launcher.net.
    /// Implement this interface directly, or inherit from
    /// <see cref="GameInstallerBase"/> for default implementations.
    /// </summary>
    public interface IGameInstaller : ILauncherPlugin
    {
        /// <summary>
        /// Install the game to the given directory.
        /// </summary>
        /// <param name="installDir">Where the game should be installed. This directory should directly contain the game files.</param>
        /// <param name="progress">Reports progress from 0 to 100, displayed on the installer's progress bar.</param>
        /// <param name="status">Reports a status message string, displayed on the bottom of the installer.</param>
        /// <param name="version">The version the user selected to install.</param>
        /// <returns>The <see cref="PluginGameInfo"/> of the installed game. Return null to cancel the installation.</returns>
        Task<PluginGameInfo?> Install(
            string installDir, 
            IProgress<double> progress,
            IProgress<string> status,
            string? version = null);

        /// <summary>
        /// The name of the game your installer plugin installs. Used for the game selection dropdown
        /// </summary>
        string GameName { get; }

        /// <summary>
        /// When to Prompt for a Label. If "Never", defaults to GameName
        /// </summary>
        LabelQueryTime PromptForLabel { get; }

        /// <summary>
        /// Gets the releases of your plugin. This should be fetched on initialization and cached to prevent delays in the UI.
        /// If you're making a versionless installer, return null.
        /// </summary>
        IEnumerable<string>? GetReleases();
    }
}   