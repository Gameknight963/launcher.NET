using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using launcherdotnet.Helpers;

namespace launcherdotnet.Launcher.Forms
{
    public partial class ThunderstoreModBrowser : ThemeableForm
    {
        private List<ThunderstorePackage> _packages = [];

        public ThunderstoreModBrowser(GameInfo game)
        {
            InitializeComponent();
            CancelButton = cancelButton;
            AcceptButton = okButton;
            modsLv.VirtualMode = true;
            modsLv.RetrieveVirtualItem += ModsLv_RetrieveVirtualItem;
            UpdateModsLv(game);
        }

        private void ModsLv_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(_packages[e.ItemIndex].Name);
        }

        async void UpdateModsLv(GameInfo game)
        {
            if (game.ThunderstoreCommunitySlug is not string slug) return;
            UseWaitCursor = true;
            _packages = await ThunderstoreClient.GetPackagesAsync(slug);
            modsLv.VirtualListSize = _packages.Count;
            UseWaitCursor = false;
        }
    }
}
