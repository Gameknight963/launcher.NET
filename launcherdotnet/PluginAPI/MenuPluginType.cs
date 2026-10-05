namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Represents the types of menus an <see cref="IMenuPlugin"/> can provide.
    /// </summary>
    public enum MenuPluginType : byte
    {
        /// <summary>
        /// Appears in the "Plugin actions" button in the left sidebar.
        /// </summary>
        GamePluginActions,
    }
}
