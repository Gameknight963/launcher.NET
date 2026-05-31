namespace launcherdotnet.PluginAPI
{
    public interface ILauncherPlugin
    {
        /// <summary>
        /// Called when the plugin is loaded.
        /// </summary>
        Task Initialize();
    }
}
