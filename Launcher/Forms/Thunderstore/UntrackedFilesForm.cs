using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    public partial class UntrackedFilesForm : ThemeableForm
    {
        public List<string> SelectedForKeep { get; private set; } = [];
        public List<string> SelectedForDeletion { get; private set; } = [];

        public UntrackedFilesForm(List<string> files)
        {
            InitializeComponent();
            Icon = LauncherConstants.AppIcon;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = okButton;
            okButton.DialogResult = DialogResult.OK;
            foreach (string file in files)
                filesClb.Items.Add(file, false);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedForDeletion = filesClb.CheckedItems.Cast<string>().ToList();
            SelectedForKeep = filesClb.Items.Cast<string>()
                .Except(SelectedForDeletion)
                .ToList();
            DialogResult = DialogResult.OK;
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filesClb.Items.Count; i++)
                filesClb.SetItemChecked(i, true);
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filesClb.Items.Count; i++)
                filesClb.SetItemChecked(i, false);
        }
    }
}
