using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using Microsoft.VisualBasic;
    using System.IO;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public partial class LauncherForm : System.Windows.Forms.Form
    {
        public string IdleStatus;
        public string IdleInstallHint;
        public string BASE = AppDomain.CurrentDomain.BaseDirectory;
        public LauncherForm()

        {
            InitializeComponent();
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            IdleStatus = status.Text;
            IdleInstallHint = InstallHint.Text;
            SetStatus(IdleStatus);
            SetSidebarMode(SidebarMode.Idle);
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
                item.Tag = game;
                gamesView.Items.Add(item);
            }
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
                    break;

                case SidebarMode.GameSelected:
                    DeleteButton.Visible = true;
                    break;
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (gamesView.SelectedItems.Count == 0 || !(gamesView.SelectedItems[0].Tag is GameInfo game)) return;
            DialogResult result = MessageBox.Show($"Delete {game.Label}?",
                "Confirmation",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (result == DialogResult.Cancel) return;
            string? deletedFolder = GameService.DeleteGame(game);
            SetStatus($"Deleted \"{game.Label}\"");
            Console.WriteLine($"Deleted {deletedFolder}");
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
            GameInfo newGame = new GameInfo { Label = result};
            await Install.DownloadAndInstallGameAsync(
                "",
                Path.Combine(BASE, "Games"),
                newGame, 
                SetStatus);
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
            SetStatus($"Selected: {selectedItem.Text}");
            SetSidebarMode(SidebarMode.GameSelected);
                if (gamesView.SelectedItems.Count == 0 || !(gamesView.SelectedItems[0].Tag is GameInfo game)) return;
                InstallHint.Text = Path.GetFileName(game.Path);
        }

        private void RefreshList_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            sw.Stop();
            SetStatus($"Reread games.json, {sw.Elapsed.TotalMilliseconds}ms");
        }
    }
}
