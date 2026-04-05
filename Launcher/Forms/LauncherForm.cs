using launcherdotnet.Syling;
using launcherdotnet.Helpers;
using launcherdotnet.Launcher.Settings;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace launcherdotnet.Launcher.Forms
{
    internal partial class LauncherForm : ThemeableForm
    {
        public string IdleStatus;
        public string IdleInstallHint;

        private LauncherData? _cachedData;

        public LauncherForm()
        {
            InitializeComponent();
            IdleStatus = status.Text;
            IdleInstallHint = InstallHint.Text;

            gamesView.SizeChanged += (sender, e) => ResizeColumns();
            this.KeyPreview = true;
            this.KeyDown += LauncherForm_KeyDown;

            gamesView.Focus();
            SearchBox.SetPlaceholder("Search by name and game...");
            SetStatus(IdleStatus);
            SetSidebarMode(SidebarMode.Idle);

            _cachedData = LauncherDataManager.ReadLauncherData();
            UpdateGameList(gamesView);
            ResizeColumns();
            Task.Run(async () => await Updater.CheckForUpdates());
        }

        public void SetStatus(string text)
        {
            status.Text = text;
        }

        public void UpdateGameList(ListView gamesView)
        {
            _cachedData = LauncherDataManager.ReadLauncherData();
            if (_cachedData == null) return;

            gamesView.BeginUpdate();
            gamesView.Items.Clear();

            foreach (GameInfo game in _cachedData.Versions)
            {
                ListViewItem item = new(game.Label);
                item.SubItems.Add(game.GameName);
                item.Tag = game;
                gamesView.Items.Add(item);
            }
            gamesView.EndUpdate();
        }

        private void ResizeColumns()
        {
            int remaining = gamesView.ClientSize.Width - gamesView.Columns[0].Width;

            // disable horizontal scrollbar with black magic
            ScrollbarHelper.Set(gamesView, ScrollbarHelper.Scrollbar.Horz, false);

            gamesView.Columns[1].Width = Math.Max(remaining, 230);
        }
        private bool IsPanelSelected()
        {
            foreach (Control c in Panel.Controls)
            {
                if (c.Focused) return true;
            }
            return false;
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string query = SearchBox.Text;
            gamesView.BeginUpdate();
            gamesView.Items.Clear();

            // yes this reads it every time. is it slow? yes? is it my problem? nope
            // ok it is now fixed :)
            if (_cachedData == null)
            {
                gamesView.EndUpdate();
                return;
            }

            foreach (GameInfo game in _cachedData.Versions)
            {
                if (game.Label.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    game.GameName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    ListViewItem item = new(game.Label);
                    item.SubItems.Add(game.GameName);
                    item.Tag = game;
                    gamesView.Items.Add(item);
                }
            }

            gamesView.EndUpdate();
        }


        public GameInfo? GetSelectedGame()
        {
            if (gamesView.SelectedItems.Count == 0 || gamesView.SelectedItems[0].Tag is not GameInfo game)
                return null;
            return game;
        }

        public enum SidebarMode
        {
            Idle,
            GameSelected
        }

        private void DeleteGameGUI(GameInfo game)
        {
            DialogResult result = DialogResult.OK;
            if (LauncherSettings.Settings.ConfirmDelete == true)
                result = CoolMessageBox.Show($"Permanently delete \"{game.Label}\"?",
                    "Confirmation",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation);

            if (result == DialogResult.Cancel) return;
            string? deletedFolder = GameService.DeleteGame(game);
            SetStatus($"Deleted \"{game.Label}\"");
            LauncherLogger.WriteLine($"Deleted {deletedFolder}", true);
            SetSidebarMode(SidebarMode.Idle);
            InstallHint.Text = IdleInstallHint;
            UpdateGameList(gamesView);
        }

        private void SetSidebarMode(SidebarMode mode)
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
        private void LaunchGameSafe(GameInfo game)
        {
            try
            {
                GameService.LaunchGame(game);
            }
            catch (Exception ex)
            {
                LauncherLogger.Error($"Failed to launch:\n{ex}");
                CoolMessageBox.Show($"Failed to launch game: {ex.Message} Check the console for more details.",
                    "Launch error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                SetStatus($"Failed to launch: {ex.GetType().Name}");
            }
        }

        private void RenameGameGUI(GameInfo game)
        {
            string result = Interaction.InputBox("Enter a new label:", "Rename", game.Label);
            if (string.IsNullOrWhiteSpace(result)) return;
            game.Label = result;
            GameService.UpsertGame(game);
            UpdateGameList(gamesView);
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game != null) DeleteGameGUI(game);
        }

        private void InstallGame()
        {
            // legends will remember salamalonekabatrabaslatrowerebakaedro
            using (GameInstallForm form = new GameInstallForm())
            {
                form.ShowDialog();
                if (form.Success)
                    UpdateGameList(gamesView);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            InstallGame();
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
            RefreshGamesView();
        }
         
        private void LaunchButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game != null) LaunchGameSafe(game);
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
            if (game != null) RenameGameGUI(game);
        }

        private void InstallSometingButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            ModloaderInstallForm form = new ModloaderInstallForm(game);
            form.ShowDialog();
        }

        private async void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            await Task.Run(async() => form.ShowDialog());
        }

        private void RefreshGamesView()
        {
            bool focused = gamesView.Focused;
            int? index = gamesView.FirstSelectedIndex();
            Stopwatch sw = Stopwatch.StartNew();
            UpdateGameList(gamesView);
            if (index != null)
            {
                gamesView.Items[(int)index].Focused = true;
                gamesView.Items[(int)index].Selected = true;
            }
            if (focused) gamesView.Focus();
            sw.Stop();
            SetStatus($"Reread games.json, {sw.Elapsed.TotalMilliseconds}ms");
        }

        private void LauncherForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Q) this.Close();

            if (e.Alt && e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                gamesView.SelectVisibleIndex(e.KeyCode - Keys.D1);
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
                gamesView.Focus();
            if (e.KeyCode == Keys.Right)
            {
                if (SearchBox.Focused || IsPanelSelected()) return;
                LaunchButton.Select();
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Delete)
            {
                GameInfo? game = GetSelectedGame();
                if (game != null) DeleteGameGUI(game);
            }

            if (e.KeyCode == Keys.F2 && gamesView.Focused)
            {
                GameInfo? game = GetSelectedGame();
                if (game != null) RenameGameGUI(game);
            }
            if (e.Control && e.KeyCode == Keys.O)
            {
                GameInfo? game = GetSelectedGame();
                if (game == null) return;
                Process.Start(new ProcessStartInfo
                {
                    FileName = game.AbsoluteRootDirectory,
                    UseShellExecute = true
                });
            }
            if ((e.Control && (e.KeyCode == Keys.L || e.KeyCode == Keys.F)) || e.KeyCode == Keys.NumPad0)
            {
                SearchBox.Select();
                SearchBox.SelectAll();
            }
            if ((e.Control && e.KeyCode == Keys.R) || e.KeyCode == Keys.F5)
                RefreshGamesView();
            if (e.Control && e.KeyCode == Keys.I)
                InstallGame();

            if (e.Control && e.KeyCode == Keys.Oemcomma)
            {
                SettingsForm form = new();
                form.ShowDialog();
            }

            if (e.KeyCode == Keys.Enter)
            {
                GameInfo? game = GetSelectedGame();
                if (game == null) return;
                LaunchGameSafe(game);
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                bool empty = SearchBox.TextLength == 0;
                SearchBox.Clear();
                if(!empty) RefreshGamesView();
                gamesView.Focus();
            }
        }
    }
}
