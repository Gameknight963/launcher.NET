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

    public partial class LauncherForm : System.Windows.Forms.Form
    {
        public string IdleStatus;
        public string IdleInstallHint;
        public string BASE = AppDomain.CurrentDomain.BaseDirectory;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_BOTH = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        public LauncherForm()
        {
            InitializeComponent();
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            IdleStatus = status.Text;
            IdleInstallHint = InstallHint.Text;
            SetStatus(IdleStatus);
            SetSidebarMode(SidebarMode.Idle);
            gamesView.SizeChanged += (sender, e) => ResizeColumns();
            Updater.CheckForUpdates();
        }

        public void SetStatus(string text)
        {
            status.Text = text;
        }

        public void UpdateGameList(ListView gamesView, LauncherData data)
        {
            gamesView.Items.Clear();
            foreach (var game in data.Versions)
            {
                ListViewItem item = new ListViewItem(game.Label);
                item.SubItems.Add(game.Path);
                item.Tag = game;
                gamesView.Items.Add(item);
            }
        }

        private void ResizeColumns()
        {
            int remaining = gamesView.ClientSize.Width - gamesView.Columns[0].Width;

            // disable horizontal scrollbar with black magic
            ShowScrollBar(gamesView.Handle, SB_HORZ, false);

            gamesView.Columns[1].Width = Math.Max(remaining, 230);
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
                    InstallSometingButton.Visible = false;
                    break;

                case SidebarMode.GameSelected:
                    DeleteButton.Visible = true;
                    LaunchButton.Visible = true;
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
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            if (result != result.Trim())
            {
                MessageBox.Show("Label must not contain trailing or leading whitespace.",
                        "Invalid name",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            GameInfo newGame = new GameInfo { Label = result };
            await Install.DownloadAndInstallGameAsync(
                "",
                LauncherSettings.GamesDir,
                newGame,
                SetStatus);
            LauncherLogger.WriteLine($"Installing {result} to {LauncherSettings.GamesDir}");
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            GameService.UpsertGame(newGame);
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
            InstallHint.Text = Path.GetFileName(game.Path);
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
            psi.FileName = "cmd.exe";
            psi.Arguments = $"/k \"{game.Path}\"";
            psi.UseShellExecute = true;
            psi.CreateNoWindow = true;

            try
            {
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                SetStatus($"Failed to launch: {ex.GetType().Name}");
            }
        }

        private void InstallSometingButton_Click(object sender, EventArgs e)
        {
            GameInfo? game = GetSelectedGame();
            if (game == null) return;
            ModloaderInstallForm form = new ModloaderInstallForm(game);
            form.Show();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
        }
    }
}
