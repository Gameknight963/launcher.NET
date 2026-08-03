using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;
using System.ComponentModel;

namespace launcherdotnet.Launcher.Forms
{
    public partial class GameInfoEditor : ThemeableForm
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GameInfo? EditedGameInfo { get; set; }
        private readonly GameInfo _game;

        public GameInfoEditor(GameInfo game)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            _game = game;
            labelBox.Text = game.Label;
            nameBox.Text = game.GameName;
            modManagerIdBox.Text = game.ModManagerId;
            gameExeBox.Text = game.RelativePath;
            gameRootDirBox.Text = game.RelativeRootDirectory;
            guidLabel.Text = game.Id;
            runsWithCmdCheck.Checked = game.RunWithCmd;

            AcceptButton = okButton;
            CancelButton = cancelButton;
            labelBox.Select();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private GameInfo ExtractGameInfo()
        {
            return new GameInfo()
            {
                Label = labelBox.Text,
                GameName = nameBox.Text,
                ModManagerId = modManagerIdBox.Text == string.Empty ? null : modManagerIdBox.Text,
                RelativePath = gameExeBox.Text,
                RelativeRootDirectory = gameRootDirBox.Text,
                Id = guidLabel.Text,
                RunWithCmd = runsWithCmdCheck.Checked,
            };
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            GameInfo game = ExtractGameInfo();
            if (!game.IsValid(out string reason))
            {
                CoolMessageBox.Show(reason, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
