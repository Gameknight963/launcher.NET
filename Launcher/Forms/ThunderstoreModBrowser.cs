using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using launcherdotnet.Helpers;

namespace launcherdotnet.Launcher.Forms
{
    public partial class ThunderstoreModBrowser : ThemeableForm
    {
        public ThunderstoreModBrowser(GameInfo game)
        {
            InitializeComponent();
            CancelButton = cancelButton;
            AcceptButton = okButton;
            UpdateModsLv(game);
        }

        async void UpdateModsLv(GameInfo game)
        {
            if (game.ThunderstoreCommunitySlug is not string slug) return;
            UseWaitCursor = true;
            List<ThunderstorePackage> packages = await ThunderstoreClient.GetPackagesAsync(slug);
            UseWaitCursor = false;
            foreach (ThunderstorePackage p in packages)
            {
                modsLv.Items.Add(p.Name);
            }
        }
    }
}
