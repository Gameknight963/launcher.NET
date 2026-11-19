using launcherdotnet.PluginAPI;
using MLInstallerSharp;


[assembly: LauncherPlugin(typeof(MelonloaderDev.Plugin), 
    "Melonloader Developer Tools",
    "Tools to help with Melonloader development",
    "2.4.0")]

namespace MelonloaderDev
{
    public class Plugin : IMenuPlugin
    {
        readonly ToolStripMenuItem _item = new("Melonloader");
        readonly ToolStripMenuItem _installItem = new("Install...");

        public MenuPluginType MenuPluginType => MenuPluginType.GamePluginActions;
        List<MLVersion> _versions = new();
        SynchronizationContext _uiContext = null!;

        public ToolStripMenuItem GetMenuItem() => _item;

        public async Task Initialize()
        {
            _versions = await MLManager.FetchAvailableVersionsAsync();
            _uiContext.Post(_ =>
            {
                foreach (MLVersion v in _versions)
                {
                    ToolStripMenuItem versionItem = new ToolStripMenuItem
                    {
                        Text = v.Version.ToString(),
                        Tag = v,
                    };
                    versionItem.Click += VersionItem_Click;
                    _installItem.DropDownItems.Add(versionItem);
                }
            }, null);
        }

        private void VersionItem_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem s = (ToolStripMenuItem)sender!;
            MLVersion version = (MLVersion)s.Tag!;

        }

        public void InitializeMainThread()
        {
            _uiContext = SynchronizationContext.Current
                ?? throw new InvalidOperationException("No UI synchronization context.");
            _item.DropDownItems.Add(_installItem);
            _item.Click += Item_Click;
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            PluginLogger.WriteLine("Hello from MelonloaderDev");
        }
    }
}
