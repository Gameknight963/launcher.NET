namespace launcherdotnet
{
    public class ThemeableForm : Form
    {
        private readonly ControlStyle _headerStyle = new();

        public ThemeableForm()
        {
            this.Load += (sender, e) => ApplyTheme(ThemeManager.ActiveTheme);
        }

        private void ApplyControlTheme(Control c, ThemeManager.Theme theme)
        {
            if (c is ListView lv)
            {
                lv.OwnerDraw = (theme != ThemeManager.Theme.Light);

                lv.DrawColumnHeader -= Lv_DrawColumnHeader;
                lv.DrawColumnHeader += Lv_DrawColumnHeader;

                lv.DrawItem -= Lv_DrawItem;
                lv.DrawItem += Lv_DrawItem;

                lv.DrawSubItem -= Lv_DrawSubItem;
                lv.DrawSubItem += Lv_DrawSubItem;
            }
             
            if (c is TabControl tc)
            {
                if (theme == ThemeManager.Theme.Light)
                {
                    tc.DrawMode = TabDrawMode.Normal;
                    return;
                }
                tc.DrawMode = TabDrawMode.OwnerDrawFixed;

                tc.DrawItem -= Tc_DrawItem;
                tc.DrawItem += Tc_DrawItem;
            }

            foreach (Control child in c.Controls)
                ApplyControlTheme(child, theme);
        }

        public void ApplyTheme(ThemeManager.Theme theme)
        {
            switch (theme)
            {
                case ThemeManager.Theme.Light:
                    break;
                case ThemeManager.Theme.Dark:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = ThemeManager.DarkMainColor;
                    break;
                case ThemeManager.Theme.ExtendFrame:
                case ThemeManager.Theme.ExtendFrameDark:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = Color.Black;
                    break;
                case ThemeManager.Theme.Blur:
                case ThemeManager.Theme.Acrylic:
                    _headerStyle.ForeColor = Color.White;
                    _headerStyle.BackColor = ThemeManager.AcrylicMainColor;
                    break;
            }
            ApplyControlTheme(this, theme);
            ThemeManager.ApplyThemeToForm(this, theme);
        }

        private void Lv_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            using Brush backBrush = new SolidBrush(e.Item!.Selected ? SystemColors.Highlight : e.Item.BackColor);
            using Brush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
        }

        private void Lv_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using SolidBrush backBrush = new(_headerStyle.BackColor!.Value);
            using SolidBrush foreBrush = new(_headerStyle.ForeColor!.Value);
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
        }

        private void Lv_DrawItem(object? sender, DrawListViewItemEventArgs e) => e.DrawDefault = false;

        private void Tc_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender!;
            TabPage tab = tc.TabPages[e.Index];

            Rectangle bounds = tc.GetTabRect(e.Index);

            using SolidBrush backBrush = new(_headerStyle.BackColor!.Value);
            {
                e.Graphics.FillRectangle(backBrush, bounds);

                TextRenderer.DrawText(
                    e.Graphics,
                    tab.Text,
                    tc.Font,
                    bounds,
                    _headerStyle.ForeColor!.Value,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }
    }
}
