using launcherdotnet.Styling;
using launcherdotnet.Helpers;

namespace launcherdotnet.Plugins.SteamGameCopy
{
    public partial class SteamCopyForm : ThemeableForm
    {
        public SteamGame? SelectedGame;

        public SteamCopyForm(List<SteamGame> games)
        {
            InitializeComponent();
            CancelButton = cancelButton;
            AcceptButton = okButton;
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += OkButton_Click;
            okButton.Enabled = false;
            gamesLv.SelectedIndexChanged += GamesLv_SelectedIndexChanged;
            gamesLv.SizeChanged += GamesLv_SizeChanged;
            ResizeColumns();
            foreach (SteamGame game in games)
            {
                ListViewItem item = new(game.Name);
                item.SubItems.Add(FormatSize(game.SizeOnDisk));
                item.Tag = game;
                gamesLv.Items.Add(item);
            }
        }

        private void GamesLv_SizeChanged(object? sender, EventArgs e) => ResizeColumns();

        void ResizeColumns()
        {
            int remaining = gamesLv.ClientSize.Width - (gamesLv.Columns[0].Width);
            gamesLv.Columns[1].Width = Math.Max(remaining, 100);
            ScrollbarHelper.Set(gamesLv, ScrollbarHelper.Scrollbar.Horz, false);
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            SelectedGame = (SteamGame)gamesLv.SelectedItems[0].Tag!;
            Close();
        }

        private void GamesLv_SelectedIndexChanged(object? sender, EventArgs e)
        {
            okButton.Enabled = !(gamesLv.SelectedIndices.Count == 0);
        }

        public static string FormatSize(long bytes)
        {
            // future proof ig
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB" };

            double size = bytes;
            int unit = 0;

            while (size >= 1024 && unit < units.Length - 1)
            {
                size /= 1024;
                unit++;
            }

            return $"{size:0.##} {units[unit]}";
        }
    }
}
