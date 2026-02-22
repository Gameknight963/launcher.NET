using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class PluginGameInfo
    {
        /// <summary>
        /// The path of the game's executable.
        /// </summary>
        public string ExePath { get; set; }
        /// <summary>
        /// Whether the game should be run through 
        /// cmd when launched. False by default.
        /// </summary>
        public bool RunWithCmd { get; set; } = false;
        public PluginGameInfo(string exePath, bool runWithCmd = false)
        {
            ExePath = exePath;
            RunWithCmd = runWithCmd;
        }
    }
}
