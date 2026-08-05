using launcherdotnet.PluginAPI;
using System;
using System.Collections.Generic;
using System.Text;
using ThunderstoreModManager;

namespace MSZInstaller
{
    // Here is a neat way to handle optional dependencies.
    // If you put the code paths where the optional dependency is hit
    // in another class, the CLR won't try to load the type until you 
    // use that class.

    // In this case, ThunderstoreConfig would never be loaded until I call SetSlug().

    // It's not exactly a guarantee, so be careful, but people myself included
    // have been doing optional dependencies like this for a while,
    // so it would be stupid of Microsoft to change it.

    // Feel free to use this code in your own plugin.

    public static class OptionalThunderstoreWrapper
    {
        public static void SetSlug(string slug, PluginGameInfo info)
        {
            ThunderstoreConfig config = ThunderstoreConfig.Load(info.DataDirectory, ThunderstoreConfig.SourceId);
            config.ThunderstoreSlug = slug;
            config.Save(info.DataDirectory, ThunderstoreConfig.SourceId);
        }
    }
}
