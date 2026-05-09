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
            foreach (string s in mods)
                modsLb.Items.Add((s, false));
            label1.Text = dependenciesAmount.ToString();
        }
    }
}
