using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms
{
    public partial class BigImageViewer : ThemeableForm
    {
        public BigImageViewer(string url)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.LoadAsync(url);
            CancelButton = button1;
            pictureBox1.LoadCompleted += (s, e) =>
            {
                if (e.Error != null) return;
                if (pictureBox1.Image != null)
                    ClientSize = new Size(pictureBox1.Image.Width, pictureBox1.Image.Height);
            };
        }

        public BigImageViewer(string url, int width, int height)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.LoadAsync(url);
            CancelButton = button1;
            Width = width;
            Height = height;
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
