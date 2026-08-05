using launcherdotnet.PluginAPI;
using ThunderstoreModManager;
using ThunderstoreModManager.ThunderstoreAPI;

namespace launcherdotnet.Plugins.SteamGameCopy
{
    // see ../MSZInstaller/OptionalThunderstoreWrapper.cs for an explanation of this clas

    public static class OptionalThunderstoreWrapper
    {
        public static void SetSlug(string? slug, PluginGameInfo info)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(info.DataDirectory, ThunderstoreConfig.SourceId);
            config.ThunderstoreSlug = slug;
            config.Save(info.DataDirectory, ThunderstoreConfig.SourceId);
        }

        public static async Task<bool> DoesSlugExist(string slug) => await ThunderstoreClient.DoesThunderstoreCommunityExist(slug);
    }
}
