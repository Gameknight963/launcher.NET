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
                item.Tag = game; // THIS IS THE FRICKING KEY BRAINWAVES
                gamesView.Items.Add(item);
            }
        }
        private async void DeleteButton_Click(object sender, EventArgs e)
        {
            if (gamesView.SelectedItems.Count == 0 || !(gamesView.SelectedItems[0].Tag is GameInfo game)) return;
            string? deletedFolder = GameService.DeleteGame(game);
            SetStatus($"Deleted \"{game.Label}\"");
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // legends will remember salamalonekabatrabaslatrowerebakaedro
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            GameInfo newGame = new GameInfo { Label = result, Path = ""};
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
                InstallHint.Text = "Select an instance to see it's filename";
                return;
            }
            ListViewItem selectedItem = gamesView.SelectedItems[0];
            SetStatus($"Selected: {selectedItem.Text}");
            if (selectedItem.SubItems[1].Text == "")
            {
                InstallHint.Text = "No game installed here.";
            }
            else
            {
                InstallHint.Text = Path.GetFileName(selectedItem.SubItems[1].Text);
            }
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
