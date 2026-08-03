using launcherdotnet.Helpers;
using launcherdotnet.Launcher.Settings;
using launcherdotnet.PluginAPI;
using launcherdotnet.Styling;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace launcherdotnet.Launcher.Forms
{
    public partial class GameModManager : ThemeableForm
    {
        readonly GameInfo _game;
        readonly IModSource _source;

        public GameModManager(GameInfo game)
        {
            if (game.ModManagerId == null) throw new ArgumentException("Cannot open this form on a game that has no ModManagerId.");
            _source = PluginRegistry.ModSources.First(x => x.Id == game.ModManagerId);

            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            uninstallButton.Enabled = false;

            ResizeColumns();
            modsLv.SizeChanged += (sender, e) => ResizeColumns();
            CancelButton = closeButton;
            _game = game;

            installModsButton.Text = $"{_source.DisplayName}";

            RefreshList();
        }

        private void RefreshList()
        {
            modsLv.Items.Clear();
            foreach (InstalledMod mod in _source.GetInstalledMods(_game))
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

        private void ModsLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            uninstallButton.Enabled = modsLv.SelectedIndices.Count > 0;
        }

        private async void InstallModsButton_Click(object sender, EventArgs e)
        {
            await _source.OpenModBrowser(_game);
            RefreshList();
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (modsLv.SelectedIndices.Count == 0) return;
            List<InstalledMod> selected = modsLv.SelectedItems
                .Cast<ListViewItem>()
                .Select(x => (InstalledMod)x.Tag!)
                .ToList();
            foreach (InstalledMod mod in selected)
            {
                if (!_source.UninstallMod(_game, mod)) break;
            }
            RefreshList();
        }

        //private async void InstallFromZip_Click(object sender, EventArgs e)
        //{
        //    using OpenFileDialog dialog = new();
        //    dialog.Filter = "Zip archive (*.zip)|*.zip";
        //    dialog.Title = "Select a package";
        //    if (dialog.ShowDialog() != DialogResult.OK) return;
        //    await ModInstaller.InstallZipAsync(dialog.FileName, _game, OnMissingInfo);
        //    RefreshList();
        //}

        //private async void InstallFromDll_Click(object sender, EventArgs e)
        //{
        //    using OpenFileDialog dialog = new();
        //    dialog.Filter = ".NET assembly (*.dll)|*.dll";
        //    dialog.Title = "Select an assembly";
        //    if (dialog.ShowDialog() != DialogResult.OK) return;
        //    (string, string, string)? modInfo = MissingInfoForm("Fill in some info for this mod:",
        //        (Path.GetFileNameWithoutExtension(dialog.SafeFileName), "", ""));
        //    if (modInfo is not (string name, string owner, string version)) return;
        //    if (!ModInstaller.TryInstallDllAsync(dialog.FileName, _game, name, owner, version))
        //    {
        //        LauncherLogger.Error("Could not find a Mods folder or a BepInEx\\plugins folder.");
        //        CoolMessageBox.Show("launcher.net does not know how to install assemblies for this game.", "Installation Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //}

        private static (string, string, string)? OnMissingInfo() => MissingInfoForm(null);
        private static (string, string, string)? MissingInfoForm(string? labelText, (string, string, string)? modInfo = null)
        {
            throw new Exception();
            //using FillMissingModInfo form = new(labelText, modInfo);
            //form.ShowDialog();
            //if (form.DialogResult == DialogResult.Cancel) return null;
            //return form.EditedInfo;
        }
    }
}
