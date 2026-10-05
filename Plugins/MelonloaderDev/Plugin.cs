using launcherdotnet.PluginAPI;
using MelonloaderDev;
using System.Windows.Forms;

[assembly: LauncherPlugin(typeof(Plugin), 
    "Melonloader Developer Tools",
    "Tools to help with Melonloader development such as automated" +
    "Visual Studio development setup",
    "2.3.0")]

namespace MelonloaderDev
{
    public class Plugin : IMenuPlugin
    {
        readonly ToolStripMenuItem _item = new ("Melonloader");

        public MenuPluginType MenuPluginType => MenuPluginType.GamePluginActions;

        public ToolStripMenuItem GetMenuItem() => _item;

        public Task Initialize() => Task.CompletedTask;

        public async void InitializeMainThread()
        {
            _item.Click += Item_Click;
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            PluginLogger.WriteLine("Hello from MelonloaderDev");
        }
    }
}
