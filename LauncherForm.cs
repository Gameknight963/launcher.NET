using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using System.IO;
    public partial class LauncherForm : System.Windows.Forms.Form
    {
        public LauncherForm()
        {
            InitializeComponent();
        }
        public void SetStatus(string text)
        {
            status.Text = text;
        }
        public void UpdateGameList(ListBox listBox, LauncherData data)
        {
            listBox.Items.Clear();

            foreach (var game in data.Versions)
            {
                listBox.Items.Add(game.Label);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Install.DownloadAndInstallGameAsync(
                "bujehvbe", 
                Path.Combine(Directory.GetCurrentDirectory(), "Games"),
                SetStatus);
            UpdateGameList(gamesList, LauncherDataManager.ReadLauncherData());
        }
    }
}
