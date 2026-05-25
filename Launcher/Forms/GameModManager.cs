using launcherdotnet.Helpers;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class GameModManager : ThemeableForm
    {
        ModManifest _manifest;
        readonly GameInfo _game;

        public GameModManager(GameInfo game)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            uninstallButton.Enabled = false;

            ResizeColumns();
            modsLv.SizeChanged += (sender, e) => ResizeColumns();
            CancelButton = closeButton;

            _manifest = ModManifest.Load(game.AbsoluteRootDirectory);
            _game = game;

            RefreshList();
        }

        private void RefreshList()
        {
            modsLv.Items.Clear();
            foreach (InstalledMod mod in _manifest.InstalledMods)
            {
                ListViewItem item = new(mod.Name);
                item.SubItems.Add(mod.Version);
                item.SubItems.Add(mod.IsDependency ? "Dependency" : "Mod");
                item.Tag = mod;
                modsLv.Items.Add(item);
            }
        }

        private void ResizeColumns()
        {
            int remaining = modsLv.ClientSize.Width - (modsLv.Columns[0].Width + modsLv.Columns[1].Width);
            modsLv.Columns[2].Width = Math.Max(remaining, 230);
            ScrollbarHelper.Set(modsLv, ScrollbarHelper.Scrollbar.Horz, false);
        }

        private void modsLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            uninstallButton.Enabled = modsLv.SelectedIndices.Count > 0;
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            List<InstalledMod> selected = modsLv.SelectedItems
                .Cast<ListViewItem>()
                .Select(x => (InstalledMod)x.Tag!)
                .ToList();
            string names = string.Join(Environment.NewLine, selected.Select(m => m.Name));
            if (CoolMessageBox.Show(
                $"Are you sure you would like to uninstall the following mods?\n{names}")
                != DialogResult.OK) return;

            foreach (InstalledMod mod in selected)
            {
                foreach (string path in mod.Files)
                {
                    string fullPath = Path.Combine(_game.AbsoluteRootDirectory, path);
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                }
                _manifest.InstalledMods.Remove(mod);
            }
            _manifest.Save(_game.AbsoluteRootDirectory);
            RefreshList();
        }

        private void installModsButton_Click(object sender, EventArgs e)
        {
            ThunderstoreModBrowser browser = new(_game);
            browser.ShowDialog();
            _manifest = ModManifest.Load(_game.AbsoluteRootDirectory);
            RefreshList();
        }
    }
}
