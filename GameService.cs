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
        public static string? DeleteGame(GameInfo game)
        {
            LauncherData data = LauncherDataManager.ReadLauncherData();
            if (string.IsNullOrWhiteSpace(game.Path) || !File.Exists(game.Path)) return null;

            string folder = Path.GetDirectoryName(game.Path)!;

            while (!folder.EndsWith(game.Id, StringComparison.OrdinalIgnoreCase))
            {
                folder = Directory.GetParent(folder)!.FullName;
            }
            Directory.Delete(folder, true);
            RemoveMissingGames();
            return folder;
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
