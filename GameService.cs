using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public class GameService
    {
        public static void AddNewInstance(string label)
        {
            LauncherData data = LauncherDataManager.ReadLauncherData();
            data.Versions.Add(new GameInfo
            {
                Label = label,
            });
            LauncherDataManager.SaveLauncherData(data);
        }
    }
}
