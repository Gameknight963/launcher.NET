using System.ComponentModel;

namespace launcherdotnet.Syling
{
    public class ThemeableForm : Form
    {
        private readonly ControlStyle _headerStyle = new();

        private bool _useGdiText = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeManager.Theme ActiveTheme { get; set; }

        public enum TextRenderMode
        {
            Auto,
            AutoStrict,
            Gdi,
            DrawString
        }

        public ThemeableForm()
        {
            Load += (sender, e) => ApplyTheme(ThemeManager.ActiveTheme, TextRenderMode.Auto);
            SetTextRenderMode(TextRenderMode.Auto);
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

        public void SetTextRenderMode(TextRenderMode mode)
        {
            if (mode == TextRenderMode.Auto)
                _useGdiText = !(ActiveTheme == ThemeManager.Theme.Acrylic && ActiveTheme == ThemeManager.Theme.Blur);

            if (mode == TextRenderMode.AutoStrict)
                _useGdiText = (ActiveTheme == ThemeManager.Theme.Light && ActiveTheme == ThemeManager.Theme.Dark);

            if (mode == TextRenderMode.Gdi)
                _useGdiText = true;

            if (mode == TextRenderMode.DrawString)
                _useGdiText = false;

        }

        public void ApplyTheme(ThemeManager.Theme theme, TextRenderMode? textMode = null)
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
            ActiveTheme = theme;
            ApplyControlTheme(this, theme);
            ThemeManager.ApplyThemeToForm(this, theme);
            if (textMode.HasValue) SetTextRenderMode(textMode.Value);
        }

        private void Lv_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            if (_useGdiText)
            {
                using Brush backBrush = new SolidBrush(e.Item!.Selected ? SystemColors.Highlight : e.Item.BackColor);
                using Brush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
            }
            else
            {
                using Brush backBrush = new SolidBrush(e.Item!.Selected ? SystemColors.Highlight : e.Item.BackColor);
                e.Graphics.FillRectangle(backBrush, e.Bounds);

                string text = e.SubItem!.Text;
                Font font = e.SubItem.Font;
                Rectangle bounds = e.Bounds;

                Color textColor = _headerStyle.ForeColor!.Value;
                Color shadowColor = Color.FromArgb(120, 0, 0, 0);

                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    font,
                    new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width, bounds.Height),
                    shadowColor,
                    TextFormatFlags.Left
                );

                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    font,
                    bounds,
                    textColor,
                    TextFormatFlags.Left
                );
            }
        }

        private void Lv_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (_useGdiText)
            {
                using SolidBrush backBrush = new(_headerStyle.BackColor!.Value);
                using SolidBrush foreBrush = new(_headerStyle.ForeColor!.Value);

                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
            else
            {
                using SolidBrush backBrush = new(_headerStyle.BackColor!.Value);
                e.Graphics.FillRectangle(backBrush, e.Bounds);

                TextRenderer.DrawText(
                    e.Graphics,
                    e.Header!.Text,
                    e.Font!,
                    new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height),
                    Color.FromArgb(120, 0, 0, 0),
                    TextFormatFlags.Left
                );

                TextRenderer.DrawText(
                    e.Graphics,
                    e.Header!.Text,
                    e.Font!,
                    e.Bounds,
                    _headerStyle.ForeColor!.Value,
                    TextFormatFlags.Left
                );
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
