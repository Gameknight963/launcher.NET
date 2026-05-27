using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms
{
    public partial class BigImageViewer : ThemeableForm
    {
        public BigImageViewer(string url, int? width = null, int? height = null)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.LoadAsync(url);
            CancelButton = button1;
            pictureBox1.LoadCompleted += (s, e) =>
            {
                if (e.Error != null) return;
                if (width != null && height != null)
                {
                    Width = width.Value;
                    Height = height.Value;
                    return;
                }
                if (pictureBox1.Image != null)
                {
                    float aspectRatio = (float)pictureBox1.Image.Width / pictureBox1.Image.Height;
                    int w = Math.Clamp(pictureBox1.Image.Width, 500, 1000);
                    int h = (int)(w / aspectRatio);
                    ClientSize = new Size(w, h);
                }
            };
        }

        public static void Show(string url)
        {
            new BigImageViewer(url).ShowDialog();
        }

        public static void Show(string url, int width, int height)
        {
            new BigImageViewer(url, width, height).ShowDialog();
        }
    }
}
