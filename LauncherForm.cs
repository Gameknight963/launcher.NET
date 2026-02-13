using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using Microsoft.VisualBasic;
    using System.IO;
    public partial class LauncherForm : System.Windows.Forms.Form
    {
        public string IdleStatus = "Status will appear here";
        public LauncherData Data = LauncherDataManager.ReadLauncherData();
        public LauncherForm()
        {
            InitializeComponent();
            UpdateGameList(gamesView, Data);
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
                gamesView.Items.Add(item);
            }
        }

        public void AddNewInstance(string label)
        {
            RereadLauncherData();
            LauncherData data = Data;
            data.Versions.Add(new GameInfo
            {
                Label = label,
            });
            Data = data;
            LauncherDataManager.SaveLauncherData(data);
        }
        public void RereadLauncherData()
        {
            Data = LauncherDataManager.ReadLauncherData();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Install.DownloadAndInstallGameAsync(
                "bujehvbe",
                Path.Combine(Directory.GetCurrentDirectory(), "Games"),
                "salamalonekabatrabaslatrowerebakaedro",
                SetStatus);
            RereadLauncherData(); // MUST do this here
            UpdateGameList(gamesView, Data);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string result = Interaction.InputBox(
                "Enter a label for this instance:",
                "Set Game Label");
            AddNewInstance(result);
            UpdateGameList(gamesView, Data);
        }

        private void gamesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gamesView.SelectedItems.Count < 1)
            {
                SetStatus(IdleStatus);
                return;
            }
            ListViewItem selectedItem = gamesView.SelectedItems[0];
            SetStatus($"Selected: {selectedItem.Text}");
            if (selectedItem.SubItems[1].Text == "")
            {
                Console.WriteLine("eujfwufhn");
            }
        }
    }
}
