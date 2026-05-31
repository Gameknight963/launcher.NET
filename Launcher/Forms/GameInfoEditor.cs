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
        private GameModState _state;

        public GameInfoEditor(GameInfo game)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            _game = game;
            labelBox.Text = game.Label;
            nameBox.Text = game.GameName;
            thunderstoreSlugBox.Text = game.ThunderstoreCommunitySlug;
            gameExeBox.Text = game.RelativePath;
            gameRootDirBox.Text = game.RelativeRootDirectory;
            guidLabel.Text = game.Id;
            runsWithCmdCheck.Checked = game.RunWithCmd;
            modManageableBox.Checked = game.ModManagable;

            _state = GameModState.Load(game.AbsoluteRootDirectory);
            baselineFilesAmountLabel.Text = $"{_state.BaselineFiles?.Count ?? 0} file(s) in baseline";

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
                ThunderstoreCommunitySlug = thunderstoreSlugBox.Text,
                RelativePath = gameExeBox.Text,
                RelativeRootDirectory = gameRootDirBox.Text,
                Id = guidLabel.Text,
                RunWithCmd = runsWithCmdCheck.Checked,
                ModManagable = modManageableBox.Checked
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

        private void baselineButton_Click(object sender, EventArgs e)
        {
            if (CoolMessageBox.Show(
                "This will mark all files not belonging to a tracked mod as vanilla game files.\n" +
                "Any previously recorded baseline will be overwritten.\n\nContinue?",
                "Recalculate Baseline",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) != DialogResult.OK) return;

            HashSet<string> modFiles = _state.InstalledMods
                .SelectMany(m => m.Files)
                .ToHashSet();

            string[] allFiles = Directory.GetFiles(_game.AbsoluteRootDirectory, "*", SearchOption.AllDirectories);
            _state.TakeBaseline(_game.AbsoluteRootDirectory, f => !modFiles.Contains(f));

            _state.Save(_game.AbsoluteRootDirectory);
            LauncherLogger.WriteLine($"Recalculated baseline: {_state.BaselineFiles?.Count ?? 0} files");
            CoolMessageBox.Show("Baseline recalculated successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
