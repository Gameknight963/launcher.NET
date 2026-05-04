using launcherdotnet.Styling;
using System.ComponentModel;

namespace launcherdotnet.Launcher.Forms
{
    public partial class GameInfoEditor : ThemeableForm
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GameInfo? EditedGameInfo { get; set; }

        public GameInfoEditor(GameInfo game)
        {
            InitializeComponent();
            labelBox.Text = game.Label;
            nameBox.Text = game.GameName;
            thunderstoreSlugBox.Text = game.ThunderstoreCommunitySlug;
            gameExeBox.Text = game.RelativePath;
            gameRootDirBox.Text = game.RelativeRootDirectory;
            guidLabel.Text = game.Id;
        }

        private GameInfo ExtractGameInfo()
        {
            return new GameInfo()
            {
                Label = labelBox.Text,
                GameName = nameBox.Text,
                ThunderstoreCommunitySlug = thunderstoreSlugBox.Text,
                RelativePath = gameExeBox.Text,
                RelativeRootDirectory = gameRootDirBox.Text,
            };
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            GameInfo game = ExtractGameInfo();
            if (!game.IsValid(out string reason))
                CoolMessageBox.Show(reason, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            EditedGameInfo = game;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void copyGUIDButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(guidLabel.Text);
        }
    }
}
