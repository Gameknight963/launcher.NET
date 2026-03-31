using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace launcherdotnet
{
    public class ThemeableForm : Form
    {
        private ControlStyle _headerStyle = new(Color.White, Color.Black);

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
            using SolidBrush backBrush = new SolidBrush(_headerStyle.BackColor!.Value);
            using SolidBrush foreBrush = new SolidBrush(_headerStyle.ForeColor!.Value);
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.Header!.Text, e.Font!, foreBrush, e.Bounds);
            }
        }

        private void Lv_DrawItem(object? sender, DrawListViewItemEventArgs e) => e.DrawDefault = false;
    }
}
