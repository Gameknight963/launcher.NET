using System.Runtime.InteropServices.Marshalling;

namespace launcherdotnet
{
    using System.IO;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Install.DownloadAndInstallGameAsync("bujehvbe",  Path.Combine(Directory.GetCurrentDirectory(), "Games"));
        }
    }
}
