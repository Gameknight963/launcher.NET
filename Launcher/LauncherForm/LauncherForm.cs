using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using launcherdotnet.Launcher;
    using Microsoft.VisualBasic;
    using Semver;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    internal partial class LauncherForm : System.Windows.Forms.Form
    {
        public string IdleStatus;
        public string IdleInstallHint;
        public string BASE = AppDomain.CurrentDomain.BaseDirectory;

        public LauncherForm()
        {
            InitializeComponent();
            IdleStatus = status.Text;
            IdleInstallHint = InstallHint.Text;

            gamesView.SizeChanged += (sender, e) => ResizeColumns();
            this.KeyDown += LauncherForm_KeyDown;

            SearchBox.Focus();
            SetStatus(IdleStatus);
            SetSidebarMode(SidebarMode.Idle);
            LauncherData? data = LauncherDataManager.ReadLauncherData();
            UpdateGameList(gamesView, data);

            Updater.CheckForUpdates();
        }

        public void SetStatus(string text)
        {
            status.Text = text;
        }

        public static void UpdateGameList(ListView gamesView, LauncherData? data)
        {
            if (data == null) return;
            gamesView.Items.Clear();
            foreach (var game in data.Versions)
            {
                ListViewItem item = new ListViewItem(game.Label);
                item.SubItems.Add(game.GameName);
                item.Tag = game;
                gamesView.Items.Add(item);
            }
        }

        private void ResizeColumns()
        {
            int remaining = gamesView.ClientSize.Width - gamesView.Columns[0].Width;

            // disable horizontal scrollbar with black magic
            ScrollbarHelper.Set(gamesView, ScrollbarHelper.Scrollbar.Horz, false);

            gamesView.Columns[1].Width = Math.Max(remaining, 230);
        }

        private void LauncherForm_KeyDown(object? sender, KeyEventArgs e)
        {
            // not in use rn
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string query = SearchBox.Text.ToLower();
            gamesView.Items.Clear();
            // yes this reads it every time. could it be faster? yes. is it slow? nope its still pretty fast
            LauncherData? data = LauncherDataManager.ReadLauncherData(); 
            if (data == null) return;
            List<GameInfo> g = new();
            foreach (GameInfo game in data.Versions)
            {
                if (game.GameName.ToLower().Contains(query) || game.Label.ToLower().Contains(query)) 
                    g.Add(game);
            }
            UpdateGameList(gamesView, new LauncherData { Versions = g});
        }

        public GameInfo? GetSelectedGame()
        {
            if (gamesView.SelectedItems.Count == 0 || !(gamesView.SelectedItems[0].Tag is GameInfo game))
                return null;
            return game;
        }

        public enum SidebarMode
        {
            Idle,
            GameSelected
        }

        public void SetSidebarMode(SidebarMode mode)
        {
            switch (mode)
            {
                case SidebarMode.Idle:
                    DeleteButton.Visible = false;
                    LaunchButton.Visible = false;
                    OpenFolderButton.Visible = false;
                    RenameButton.Visible = false;
                    InstallSometingButton.Visible = false;
                    break;

                case SidebarMode.GameSelected:
                    DeleteButton.Visible = true;
                    LaunchButton.Visible = true;
                    OpenFolderButton.Visible = true;
                    RenameButton.Visible = true;
                    InstallSometingButton.Visible = true;
                    break;
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            DialogResult result = DialogResult.OK;
            if (LauncherSettings.Settings.ConfirmDelete == true)
                result = MessageBox.Show($"Permanently delete \"{game.Label}\"?",
                    "Confirmation",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel) return;
            string? deletedFolder = GameService.DeleteGame(game);
            SetStatus($"Deleted \"{game.Label}\"");
            LauncherLogger.WriteLine($"Deleted {deletedFolder}", true);
            SetSidebarMode(SidebarMode.Idle);
            InstallHint.Text = IdleInstallHint;
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // legends will remember salamalonekabatrabaslatrowerebakaedro
            GameInstallForm form = new GameInstallForm();
            form.ShowDialog();
            LauncherData? data = LauncherDataManager.ReadLauncherData();
            UpdateGameList(gamesView, data);
        }

        private void gamesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gamesView.SelectedItems.Count < 1)
            {
                SetStatus(IdleStatus);
                InstallHint.Text = IdleInstallHint;
                SetSidebarMode(SidebarMode.Idle);
                return;
            }
            ListViewItem selectedItem = gamesView.SelectedItems[0];
            SetStatus($"Selected: \"{selectedItem.Text}\"");
            SetSidebarMode(SidebarMode.GameSelected);
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            InstallHint.Text = Path.GetFileName(game.AbsolutePath);
        }

        private void RefreshList_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            sw.Stop();
            SetStatus($"Reread games.json, {sw.Elapsed.TotalMilliseconds}ms");
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = true;

            if (game.RunWithCmd)
            {
                psi.FileName = "cmd.exe";
                psi.Arguments = $"/k \"{game.AbsolutePath}\"";
                psi.CreateNoWindow = true;
            }
            else
            {
                psi.FileName = game.AbsolutePath;
                psi.CreateNoWindow = false;
            }

            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                SetStatus($"Failed to launch: {ex.GetType().Name}");
            }
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            Process.Start(new ProcessStartInfo
            {
                FileName = game.AbsoluteRootDirectory,
                UseShellExecute = true
            });
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            string result = Interaction.InputBox("Enter a new label:", "Rename", game.Label);
            if (string.IsNullOrWhiteSpace(result)) return;
            game.Label = result;
            GameService.UpsertGame(game);
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
        }

        private void InstallSometingButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            ModloaderInstallForm form = new ModloaderInstallForm(game);
            form.ShowDialog();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }
    }
}
