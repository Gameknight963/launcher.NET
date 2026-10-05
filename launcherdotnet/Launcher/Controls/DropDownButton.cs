using System.ComponentModel;

namespace launcherdotnet.Launcher.Controls
{
    public class DropDownButton : Button
    {
        [DefaultValue(null)]
        [Browsable(true)]
        public ContextMenuStrip? DropDownMenu { get; set; }

        protected override void OnClick(EventArgs e)
        {
            if (DropDownMenu == null)
                LauncherLogger.Warn("DropDownButton.OnClick called with a null context menu");
            DropDownMenu?.Show(this, new Point(0, Height));
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawArrow(e.Graphics);
        }

        private void DrawArrow(Graphics g)
        {
            int x = Width - 14;
            int y = Height / 2 - 1;

            g.FillPolygon(Brushes.Black, new Point[]
            {
                new Point(x, y),
                new Point(x + 8, y),
                new Point(x + 4, y + 4),
            });
        }
    }
}
