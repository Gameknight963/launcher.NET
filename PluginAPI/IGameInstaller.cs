using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public interface IGameInstaller : ILauncherPlugin
    {
        string Install(string installDir);
    }
}
