namespace launcherdotnet.PluginAPI
{
    public interface IPluginWithSettings : ILauncherPlugin
    {
        Form CreateSettingsForm();
    }
}
