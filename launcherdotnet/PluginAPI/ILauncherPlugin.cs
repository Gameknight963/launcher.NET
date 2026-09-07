namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Base interface for all launcher.net plugins.
    /// </summary>
    public interface ILauncherPlugin
    {
        /// <summary>
        /// Called once just after InitializeMainThread. This method must be thread-safe.
        /// Use it to perform initialization tasks such as loading cached
        /// data or fetching release information.
        /// </summary>
        Task Initialize();

        /// <summary>
        /// Called once when the plugin is loaded before any other plugin methods.
        /// Runs synchronously on the main thread, do not perform network/disk IO and such
        /// </summary>
        void InitializeMainThread() { }
    }
}
