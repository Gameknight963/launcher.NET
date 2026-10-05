namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Represents a plugin that contributes an item to a dropdown menu.
    /// </summary>
    public interface IMenuPlugin: ILauncherPlugin
    {
        /// <summary>
        /// The <see cref="PluginAPI.MenuPluginType"/> this plugin is.
        /// </summary>
        public MenuPluginType MenuPluginType { get; }

        /// <summary>
        /// Gets the menu item to display in the dropdown.
        /// </summary>
        /// <returns>A <see cref="ToolStripMenuItem"/>, or <see langword="null"/> to not display one.</returns>
        ToolStripMenuItem? GetMenuItem();
    }
}
