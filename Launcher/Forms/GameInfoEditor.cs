using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms
{
    public partial class GameInfoEditor : ThemeableForm
    {
        public GameInfoEditor(GameInfo game)
        {
            InitializeComponent();
            labelBox.Text = game.Label;
            nameBox.Text = game.GameName;
            thunderstoreSlugBox.Text = game.ThunderstoreCommunitySlug;
            gameExeBox.Text = game.RelativePath;
            gameRootDirBox.Text = game.RelativeRootDirectory;
        }

        private void okButton_Click(object sender, EventArgs e)
        {

        }
    }
}
