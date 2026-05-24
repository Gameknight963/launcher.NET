using launcherdotnet.Styling;

namespace launcherdotnet.Launcher.Forms.Thunderstore
{
    //public partial class ReviewAndConfirm : ThemeableForm
    //{
    //    public ReviewAndConfirm(IEnumerable<string> mods, IEnumerable<string>? dependencies = null)
    //    {
    //        InitializeComponent();
    //        StartPosition = FormStartPosition.CenterParent;
    //        CancelButton = cancelButton;
    //        AcceptButton = okButton;
    //        //modsLb.DrawMode = DrawMode.OwnerDrawFixed;
    //        //modsLb.DrawItem += ModsLb_DrawItem;
    //        foreach (string s in mods)
    //            modsLb.Items.Add((s, false));
    //        if (dependencies == null) return;
    //        foreach (string s in dependencies)
    //            modsLb.Items.Add((s, true));
    //    }
    //}

    public partial class ReviewAndConfirm : ThemeableForm
    {
        public ReviewAndConfirm(IEnumerable<string> mods, int dependenciesAmount)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            CancelButton = cancelButton;
            AcceptButton = okButton;
            okButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
            cancelButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            foreach (string s in mods)
                modsLb.Items.Add(s);
            label1.Text = $"{mods.Count()} mod(s) selected for download:";
            label2.Text = $"{dependenciesAmount} dependencies are hidden";
        }
    }
}
