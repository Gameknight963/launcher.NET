using launcherdotnet.Launcher;
using launcherdotnet.PluginAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThunderstoreModManager
{
    public class ThunderstoreData
    {
        [JsonProperty("installedMods")]
        public List<InstalledMod> InstalledMods { get; set; } = [];
    }
}
