using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace launcherdotnet
{
    public class LauncherDataManager
    {
        private static string GetDataPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "games.json");
        }
        public static LauncherData ReadLauncherData()
        {
            string dataPath = GetDataPath();
            LauncherData data;
            if (File.Exists(dataPath))
            {
                string json = File.ReadAllText(dataPath);
                data = JsonConvert.DeserializeObject<LauncherData>(json) ?? new LauncherData();
            }
            else
            {
                data = new LauncherData(); 
            }
            return data;
        }
        public static void SaveLauncherData(LauncherData data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(GetDataPath(), json);
        }
    }
    public class GameInfo
    {
        public string Label { get; set; } = "";
        public string Path { get; set; } = "";
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
    public class LauncherData
    {
        public string? Name { get; set; }
        public List<GameInfo> Versions { get; set; } = new List<GameInfo>();
    }
}
