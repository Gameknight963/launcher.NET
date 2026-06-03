namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Base interface for all launcher.net plugins.
    /// </summary>
    public interface ILauncherPlugin
    {
        /// <summary>
        /// Called once when the plugin is loaded before any other plugin methods.
        /// Use it to perform initialization tasks such as loading cached
        /// data or fetching release information.
        /// </summary>
        Task Initialize();
    }
}
