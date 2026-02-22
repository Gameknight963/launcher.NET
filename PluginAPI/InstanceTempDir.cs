using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public class InstanceTempDir : IDisposable
    {
        public string Path { get; }

        public InstanceTempDir()
        {
            Path = System.IO.Path.Combine(LauncherSettings.TempDir, Guid.NewGuid().ToString());
            Directory.CreateDirectory(Path);
        }

        public void Dispose()
        {
            try
            {
                if (Directory.Exists(Path))
                    Directory.Delete(Path, true);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Error deleting temporary files: {ex.Message}\nStack trace:\n{ex.StackTrace}");
            }
        }
    }
}