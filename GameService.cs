using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public class GameService
    {
        [Obsolete("Use UpsertGame() instead.")]
        public static void AddNewInstance(string label)
        {
            LauncherData data = LauncherDataManager.ReadLauncherData();
            data.Versions.Add(new GameInfo
            {
                Label = label,
            });
            LauncherDataManager.SaveLauncherData(data);
        }
        public static void UpsertGame(GameInfo game)
        {
            LauncherData data = LauncherDataManager.ReadLauncherData();

            var existing = data.Versions.FirstOrDefault(g => g.Id == game.Id);

            if (existing != null)
            {
                existing.Label = game.Label;
                existing.Path = game.Path;
            }
            else
            {
                data.Versions.Add(game);
            }

            LauncherDataManager.SaveLauncherData(data);
        }
        public static bool DeleteGame(GameInfo game)
        {
            bool deleted = false;

            if (!string.IsNullOrWhiteSpace(game.Path) && File.Exists(game.Path))
            {
                try
                {
                    Directory.Delete(Path.GetDirectoryName(game.Path)!, true);
                    deleted = true;
                }
                catch
                {
                    throw new InvalidOperationException($"Error deleting {game.Path}!");
                }
            }

            var data = LauncherDataManager.ReadLauncherData();
            int before = data.Versions.Count;

            data.Versions = data.Versions
                .Where(g => g.Id != game.Id)
                .ToList();

            if (data.Versions.Count != before)
            {
                LauncherDataManager.SaveLauncherData(data);
                deleted = true;
            }

            return deleted;
        }

        public static bool RemoveMissingGames()
        {
            var data = LauncherDataManager.ReadLauncherData();
            int before = data.Versions.Count;

            data.Versions = data.Versions
                .Where(g => !string.IsNullOrWhiteSpace(g.Path) && File.Exists(g.Path))
                .ToList();

            if (data.Versions.Count != before)
                LauncherDataManager.SaveLauncherData(data);

            return data.Versions.Count != before;
        }
    }
}
