using launcherdotnet.Helpers;
using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class GameModManager : ThemeableForm
    {
        GameModState _state;
        readonly GameInfo _game;

        public GameModManager(GameInfo game)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            uninstallButton.Enabled = false;

            ResizeColumns();
            modsLv.SizeChanged += (sender, e) => ResizeColumns();
            CancelButton = closeButton;

            _state = GameModState.Load(game.AbsoluteRootDirectory);
            _game = game;

            RefreshList();
        }

        private void RefreshList()
        {
            modsLv.Items.Clear();
            foreach (InstalledMod mod in _state.InstalledMods)
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
            if (_game.ThunderstoreCommunitySlug == null)
            {
                CoolMessageBox.Show("This game does not have a Thunderstore slug. " +
                    "Set one in the Game Properties Editor.", "No Thunderstore Slug",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ThunderstoreModBrowser browser = new(_game);
            browser.ShowDialog();
            _state = GameModState.Load(_game.AbsoluteRootDirectory);
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
            List<InstalledMod> dependents = _state.InstalledMods
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
            HashSet<string> stillRequired = _state.InstalledMods
                .Except(selected)
                .SelectMany(m => m.Dependencies)
                .ToHashSet();

            List<InstalledMod> orphans = _state.InstalledMods
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
                _state.InstalledMods.Remove(mod);
                LauncherLogger.WriteLine($"Removed {mod.Name} from manifest");
            }

            if (_state.InstalledMods.Count == 0 && _state.HasBaseline)
                CleanUpUntrackedFiles();

            _state.Save(_game.AbsoluteRootDirectory);
            LauncherLogger.WriteLine("Manifest saved.");
            RefreshList();
        }

        private void CleanUpUntrackedFiles()
        {
            List<string> untracked = _state.GetUntrackedFiles(_game.AbsoluteRootDirectory);
            if (untracked.Count == 0)
            {
                CoolMessageBox.Show("No untracked files found.", "Clean Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UntrackedFilesForm form = new(untracked);
            if (form.ShowDialog() != DialogResult.OK) return;
            foreach (string file in form.SelectedForDeletion)
            {
                string fullPath = Path.Combine(_game.AbsoluteRootDirectory, file);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    LauncherLogger.WriteLine($"Deleted untracked file: {file}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_state.HasBaseline)
            {
                CoolMessageBox.Show("No baseline snapshot exists for this game. Use the GameInfo editor to recalculate one.",
                    "No Baseline",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            CleanUpUntrackedFiles();
        }
    }
}
