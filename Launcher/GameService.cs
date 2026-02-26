using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    internal class GameService
    {
        public static void UpsertGame(GameInfo game)
        {
            LauncherData data = LauncherDataManager.ReadLauncherData();

            var existing = data.Versions.FirstOrDefault(g => g.Id == game.Id);

            if (existing != null)
            {
                existing.Label = game.Label;
                existing.RelativePath = game.RelativePath;
                existing.RunWithCmd = game.RunWithCmd;
            }
            else
            {
                data.Versions.Add(game);
            }

            LauncherDataManager.SaveLauncherData(data);
        }
        public static string DeleteGame(GameInfo game)
        {
            string folder = game.RelativeRootDirectory;
            try
            {
                game.EnsurePathsValid();
                if (!Directory.Exists(folder))
                    throw new InvalidOperationException("Game does not exist.");
                if (Path.GetPathRoot(folder) == folder)
                    throw new InvalidOperationException("Refusing to delete root directory.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Check if games");
            }
            Directory.Delete(game.AbsoluteRootDirectory, true);
            RemoveMissingGames();
            return folder;
        }

        public static bool RemoveMissingGames()
        {
            var data = LauncherDataManager.ReadLauncherData();
            int before = data.Versions.Count;

            data.Versions = data.Versions
                .Where(g => !string.IsNullOrWhiteSpace(g.RelativePath) && File.Exists(g.AbsolutePath))
                .ToList();

            if (data.Versions.Count != before)
                LauncherDataManager.SaveLauncherData(data);

            return data.Versions.Count != before;
        }
    }
}
