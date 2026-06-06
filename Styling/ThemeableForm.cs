using System.ComponentModel;
using System.Runtime.InteropServices;

namespace launcherdotnet.Styling
{
    public class ThemeableForm : Form
    {
        // it warns because of the design time guards unless we do this
        protected Theme ActiveTheme = null!;

        protected bool UseShadowText = false;
        private static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            System.Diagnostics.Process.GetCurrentProcess().ProcessName
                is "devenv" or "DesignToolsServer";
        private readonly HashSet<Control> _themedControls = new();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool InheritGlobalTheme { get; protected set; } = true;

        /// <summary>
        /// Whether the active theme's gradient color could be considered light or not.
        /// </summary>
        /// <returns>true if the theme is light, and false if dark, otherwise based off the gradient color.</returns>
        public bool IsThemeColoredLight()
        {
            Color color = ActiveTheme.MainStyle.BackColor;
            double luminance =
                0.2126 * color.R +
                0.7152 * color.G +
                0.0722 * color.B;
            
            return luminance <= 160.0;
        }
        public ThemeableForm()
        {
            if (IsDesignTime) return;
            if (ThemeManager.ActiveTheme == null) return;
            ActiveTheme = ThemeManager.ActiveTheme;
            Load += (sender, e) =>
            {
                if (InheritGlobalTheme) ApplyTheme(ThemeManager.ActiveTheme, ThemeManager.ActiveGradientColor, ThemeManager.UseVisualStyles);
            };
        }

        protected virtual void OnThemeWasApplied() { }

        private void ApplyControlTheme(Control c, Theme theme)
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

                lv.OwnerDraw = theme.UseOwnerDrawHeaders;
            }

            if (c is TabControl tc)
            {
                if (_themedControls.Add(tc))
                {
                    tc.DrawItem += Tc_DrawItem;
                }
                tc.DrawMode = theme.UseOwnerDrawHeaders ? TabDrawMode.OwnerDrawFixed : TabDrawMode.Normal;
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

        public void ApplyTheme(Theme theme, int gradientColor, bool useVisualStyles)
        {
            if (IsDesignTime) return;
            ActiveTheme = theme;
            UseShadowText = theme.UseShadowText;
            ApplyControlTheme(this, theme);
            theme.Apply(this, gradientColor, useVisualStyles);
            OnThemeWasApplied();
            Refresh();
        }

        private static void DrawShadowText(Graphics g, string text, Font font, Rectangle bounds, Color textColor)
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

            if (UseShadowText)
            {
                DrawShadowText(
                    e.Graphics,
                    e.SubItem!.Text,
                    e.SubItem.Font,
                    e.Bounds,
                    ActiveTheme.MainStyle.ForeColor
                );
            }
            else
            {
                using Brush foreBrush = new SolidBrush(ActiveTheme.MainStyle.ForeColor);
                e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
            }

            if (UseShadowText)
            {
                DrawShadowText(
                    e.Graphics,
                    e.SubItem!.Text,
                    e.SubItem.Font,
                    e.Bounds,
                    ActiveTheme.MainStyle.ForeColor
                );
            }
            else
            {
                using Brush foreBrush = new SolidBrush(ActiveTheme.MainStyle.ForeColor);
                e.Graphics.DrawString(e.SubItem!.Text, e.SubItem.Font, foreBrush, e.Bounds);
            }
        }

        private void Lv_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush backBrush = new(ActiveTheme.MainStyle.BackColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            if (UseShadowText)
            {
                DrawShadowText(
                    e.Graphics,
                    e.Header!.Text,
                    e.Font!,
                    e.Bounds,
                    ActiveTheme.MainStyle.ForeColor
                );
            }
            else
            {
                using Brush foreBrush = new SolidBrush(ActiveTheme.MainStyle.ForeColor!);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
        }

        private void Lv_DrawItem(object? sender, DrawListViewItemEventArgs e) => e.DrawDefault = false;

        private void Tc_DrawItem(object? sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender!;
            TabPage tab = tc.TabPages[e.Index];

            Rectangle bounds = tc.GetTabRect(e.Index);

            using SolidBrush backBrush = new(ActiveTheme.MainStyle.BackColor);

            e.Graphics.FillRectangle(backBrush, bounds);

            TextRenderer.DrawText(
                e.Graphics,
                tab.Text,
                tc.Font,
                bounds,
                ActiveTheme.MainStyle.ForeColor,
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
