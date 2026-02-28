using launcherdotnet.Launcher;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace launcherdotnet
{
    internal class LauncherDataManager
    {
        private static string GetDataPath()
        {
            return Path.Combine(Config.BaseDir, "games.json");
        }
        public static LauncherData? ReadLauncherData()
        {
            string dataPath = GetDataPath();
            LauncherData data;
            if (!File.Exists(dataPath))
            {
                data = new LauncherData();
                return data;
            }
            try
            {
                string json = File.ReadAllText(dataPath);
                data = JsonConvert.DeserializeObject<LauncherData>(json) ?? new LauncherData();
                return data;
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error reading games.json: {ex.GetType().Name}:", true);
                if (LauncherSettings.Settings.VerboseLogging)
                {
                    LauncherLogger.Write($"\n");
                    LauncherLogger.Error(ex.ToString(), false);
                }
                else LauncherLogger.WriteLine("Enable verbose logging to see full exception.", true);

                MessageBox.Show($"A {ex.GetType().Name} occured while reading LauncherData to games.json: " +
                    $"{ex.Message} Check the console for more details.",
                    "Error reading LauncherData",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
        }

        public static void SaveLauncherData(LauncherData data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(GetDataPath(), json);
            }
            catch(Exception ex)
            {
                LauncherLogger.Error($"Error writing games.json: {ex.GetType().Name}:", true);
                if (LauncherSettings.Settings.VerboseLogging)
                {
                    LauncherLogger.Write($"\n");
                    LauncherLogger.Error(ex.ToString(), false);
                }
                else LauncherLogger.WriteLine("Enable verbose logging to see full exception.", true);

                MessageBox.Show($"A {ex.GetType().Name} occured while saving LauncherData to games.json: " +
                    $"{ex.Message} Check the console for more details.",
                    "Error saving LauncherData",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
    public class LauncherData
    {
        public List<GameInfo> Versions { get; set; } = new List<GameInfo>();
    }
}
