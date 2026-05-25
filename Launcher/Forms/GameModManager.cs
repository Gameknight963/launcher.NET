using launcherdotnet.Helpers;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class GameModManager : ThemeableForm
    {
        GameModState _manifest;
        readonly GameInfo _game;

        public GameModManager(GameInfo game)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            uninstallButton.Enabled = false;

            ResizeColumns();
            modsLv.SizeChanged += (sender, e) => ResizeColumns();
            CancelButton = closeButton;

            _manifest = GameModState.Load(game.AbsoluteRootDirectory);
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

        private void installModsButton_Click(object sender, EventArgs e)
        {
            ThunderstoreModBrowser browser = new(_game);
            browser.ShowDialog();
            _manifest = GameModState.Load(_game.AbsoluteRootDirectory);
            RefreshList();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            if (modsLv.SelectedIndices.Count == 0) return;
            List<InstalledMod> selected = modsLv.SelectedItems
                .Cast<ListViewItem>()
                .Select(x => (InstalledMod)x.Tag!)
                .ToList();

            string names = string.Join(Environment.NewLine, selected.Select(m => m.Name));

            // warn if other mods depend on what we're removing
            List<InstalledMod> dependents = _manifest.InstalledMods
                .Except(selected)
                .Where(m => m.Dependencies.Any(d => selected.Any(s => d.StartsWith($"{s.Owner}-{s.Name}-"))))
                .ToList();

            if (dependents.Count > 0)
            {
                string dependentNames = string.Join(Environment.NewLine, dependents.Select(m => m.Name));
                LauncherLogger.Warn($"Uninstall requested for mods that have dependents: {string.Join(", ", selected.Select(m => m.Name))}");
                if (CoolMessageBox.Show(
                    $"The following mods depend on one or more mods you are trying to uninstall:\n" +
                    $"{dependentNames}\n\nUninstalling may cause them to break. Continue anyway?",
                    "Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) != DialogResult.OK) return;
            }

            if (CoolMessageBox.Show(
                $"Are you sure you would like to uninstall the following mods?\n{names}",
                "Confirm Uninstall",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) != DialogResult.OK) return;

            // find orphaned dependencies after removal
            HashSet<string> stillRequired = _manifest.InstalledMods
                .Except(selected)
                .SelectMany(m => m.Dependencies)
                .ToHashSet();

            List<InstalledMod> orphans = _manifest.InstalledMods
                .Except(selected)
                .Where(m => m.IsDependency && !stillRequired.Any(d => d.StartsWith($"{m.Owner}-{m.Name}-")))
                .ToList();

            if (orphans.Count > 0)
            {
                string orphanNames = string.Join(Environment.NewLine, orphans.Select(m => m.Name));
                LauncherLogger.WriteLine($"Found {orphans.Count} orphaned dependencies: {string.Join(", ", orphans.Select(m => m.Name))}");
                if (CoolMessageBox.Show(
                    $"The following dependencies are no longer needed by any installed mod:\n{orphanNames}" +
                    $"\n\nWould you like to remove them too?",
                    "Remove Unused Dependencies",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK)
                    selected.AddRange(orphans);
            }

            foreach (InstalledMod mod in selected)
            {
                LauncherLogger.WriteLine($"Uninstalling {mod.Name} v{mod.Version}");
                foreach (string path in mod.Files)
                {
                    string fullPath = Path.Combine(_game.AbsoluteRootDirectory, path);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                        LauncherLogger.WriteLine($"Deleted {path}");
                    }
                    else
                    {
                        LauncherLogger.Warn($"File not found, skipping: {path}");
                    }
                }
                _manifest.InstalledMods.Remove(mod);
                LauncherLogger.WriteLine($"Removed {mod.Name} from manifest");
            }

            _manifest.Save(_game.AbsoluteRootDirectory);
            LauncherLogger.WriteLine("Manifest saved.");
            RefreshList();
        }
    }
}
