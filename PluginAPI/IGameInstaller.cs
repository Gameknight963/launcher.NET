namespace launcherdotnet.PluginAPI
{
    public interface IGameInstaller : ILauncherPlugin
    {
        /// <summary>
        /// Install the game to the given directory.
        /// </summary>
        /// <param name="installDir">Where the game should be installed. This directory should directly contain the game files.</param>
        /// <param name="release">The <see cref="ReleaseInfo"/> the user selected to install.</param>
        /// <param name="progress">Reports progress from 0 to 100, displayed on the installer's progress bar.</param>
        /// <param name="status">Reports a status message string, displayed on the bottom of the installer.</param>
        /// <returns>The path of the installed game.</returns>
        Task<PluginGameInfo> Install(string installDir, 
            ReleaseInfo release,
            IProgress<double> progress,
            IProgress<string> status);
        /// <summary>
        /// The name of the game your installer plugin installs.
        /// </summary>
        string GameName { get; }
    }
}   