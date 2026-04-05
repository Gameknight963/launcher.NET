using System.ComponentModel;
using System.Globalization;

namespace launcherdotnet.Syling
{
    public class ThemeableForm : Form
    {
        private readonly ControlStyle _headerStyle = new();

        private bool _useGdiText = true;
        private static bool IsDesignTime => LicenseManager.UsageMode == LicenseUsageMode.Designtime;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeManager.Theme ActiveTheme { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ThemeManager.Theme ResolvedTheme => ThemeManager.ResolveTheme(ActiveTheme);

        private readonly HashSet<Control> _themedControls = new();

        public enum TextRenderMode
        {
            Auto,
            AutoStrict,
            Gdi,
            DrawString
        }

        public ThemeableForm()
        {
            if (IsDesignTime) return;
            Load += (sender, e) => ApplyTheme(ThemeManager.ActiveTheme, ThemeManager.TextRenderMode);
        }

        private void ApplyControlTheme(Control c, ThemeManager.Theme theme)
        {
            if (IsDesignTime) return;
            if (c is ListView lv)
            {
                if (_themedControls.Add(lv))
                {
                    lv.DrawColumnHeader += Lv_DrawColumnHeader;
                    lv.DrawItem += Lv_DrawItem;
                    lv.DrawSubItem += Lv_DrawSubItem;
                }

                lv.OwnerDraw = (theme != ThemeManager.Theme.Light);
            }

            if (c is TabControl tc)
            {
                if (_themedControls.Add(tc))
                {
                    tc.DrawItem += Tc_DrawItem;
                }

                if (theme == ThemeManager.Theme.Light)
                {
                    tc.DrawMode = TabDrawMode.Normal;
                    return;
                }

                tc.DrawMode = TabDrawMode.OwnerDrawFixed;
            }

            foreach (Control child in c.Controls)
                ApplyControlTheme(child, theme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Control c in _themedControls)
                {
                    if (c is ListView lv)
                    {
                        lv.DrawColumnHeader -= Lv_DrawColumnHeader;
                        lv.DrawItem -= Lv_DrawItem;
                        lv.DrawSubItem -= Lv_DrawSubItem;
                    }
                    else if (c is TabControl tc)
                    {
                        tc.DrawItem -= Tc_DrawItem;
                    }
                }

                _themedControls.Clear();
            }

            base.Dispose(disposing);
        }

        public void SetTextRenderMode(TextRenderMode mode)
        {
            if (IsDesignTime) return;
            switch (mode)
            {
                case TextRenderMode.Auto:
                    _useGdiText = !(ActiveTheme == ThemeManager.Theme.Acrylic || ActiveTheme == ThemeManager.Theme.Blur);
                    break;

                case TextRenderMode.AutoStrict:
                    _useGdiText = ActiveTheme == ThemeManager.Theme.Light || ActiveTheme == ThemeManager.Theme.Dark;
                    break;

                case TextRenderMode.Gdi:
                    _useGdiText = true;
                    break;

                case TextRenderMode.DrawString:
                    _useGdiText = false;
                    break;
            }
        }

        public void ApplyTheme(ThemeManager.Theme theme, TextRenderMode? textMode = null)
        {
            if (IsDesignTime) return;

            ThemeManager.Theme resolvedTheme = ThemeManager.ResolveTheme(theme);
            switch (resolvedTheme)
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

            ApplyControlTheme(this, resolvedTheme);
            ThemeManager.ApplyThemeToForm(this, resolvedTheme);

            if (textMode.HasValue)
                SetTextRenderMode(textMode.Value);
        }

        private void DrawShadowText(Graphics g, string text, Font font, Rectangle bounds, Color textColor)
        {
            Color shadowColor = Color.FromArgb(120, 0, 0, 0);

            TextRenderer.DrawText(
                g,
                text,
                font,
                new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width, bounds.Height),
                shadowColor,
                TextFormatFlags.Left
            );

            TextRenderer.DrawText(
                g,
                text,
                font,
                bounds,
                textColor,
                TextFormatFlags.Left
            );
        }

        private void Lv_DrawSubItem(object? sender, DrawListViewSubItemEventArgs e)
        {
            Color back = e.Item!.Selected ? SystemColors.Highlight : e.Item.BackColor;

            using (Brush backBrush = new SolidBrush(back))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            if (_useGdiText)
            {
                using Brush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
                e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
            }
            else
            {
                DrawShadowText(
                    e.Graphics,
                    e.SubItem!.Text,
                    e.SubItem.Font,
                    e.Bounds,
                    _headerStyle.ForeColor!.Value
                );
            }
        }

        private void Lv_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush backBrush = new(_headerStyle.BackColor!.Value))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            if (_useGdiText)
            {
                using Brush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
            else
            {
                DrawShadowText(
                    e.Graphics,
                    e.Header!.Text,
                    e.Font!,
                    e.Bounds,
                    _headerStyle.ForeColor!.Value
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

            e.Graphics.FillRectangle(backBrush, bounds);

            TextRenderer.DrawText(
                e.Graphics,
                tab.Text,
                tc.Font,
                bounds,
                _headerStyle.ForeColor!.Value,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            if (tc.TabPages.Count > 0)
            {
                Rectangle lastTabRect = tc.GetTabRect(tc.TabPages.Count - 1);

                Rectangle background = new Rectangle(
                    lastTabRect.Right,
                    0,
                    tc.Right - lastTabRect.Right,
                    lastTabRect.Height + 1
                );

                e.Graphics.FillRectangle(backBrush, background);
            }
        }
    }
}
