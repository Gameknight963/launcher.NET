namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Represents a plugin that exposes a settings UI to the user.
    /// </summary>
    public interface IPluginWithSettings : ILauncherPlugin
    {
        /// <summary>
        /// Creates the settings form displayed by the launcher.
        /// launcher.net will display the form using <see cref="Form.ShowDialog()"/>
        /// and is responsible for disposing the returned instance.
        /// </summary>
        /// <returns>
        /// A form that allows the user to configure the plugin.
        /// </returns>
        Form CreateSettingsForm();
    }
}
