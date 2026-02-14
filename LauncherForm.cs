using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using Microsoft.VisualBasic;
    using System.IO;
    public partial class LauncherForm : System.Windows.Forms.Form
    {
        public string IdleStatus;
        public string IdleInstallHint;
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
        private async void button1_Click(object sender, EventArgs e)
        {
            if (gamesView.SelectedItems.Count < 1) return;
            await Install.DownloadAndInstallGameAsync(
                "bujehvbe",
                Path.Combine(Directory.GetCurrentDirectory(), "Games"),
                gamesView.SelectedItems[0].Text, // legends will remember salamalonekabatrabaslatrowerebakaedro
                SetStatus);
            // todo: fix duplication on install
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            GameService.AddNewInstance(result);
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
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
            UpdateGameList(gamesView, LauncherDataManager.ReadLauncherData());
            SetStatus("Reread games.json.");
        }
    }
}
