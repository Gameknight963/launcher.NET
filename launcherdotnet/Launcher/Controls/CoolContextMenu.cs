using launcherdotnet.Styling;
using launcherdotnet.Windows;

namespace launcherdotnet.Launcher.Controls
{
    public class CoolContextMenu : ContextMenuStrip
    {
        public class CoolRenderer : ToolStripProfessionalRenderer
        {
            public byte ImageMarginOpacity { get; set; } = 30;

            private Color _backColor;
            private Color _realBackColor;

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                if (_backColor != e.BackColor)
                {
                    _backColor = e.BackColor;
                    _realBackColor = Color.FromArgb(ImageMarginOpacity, _backColor);
                }
                e.Graphics.Clear(e.BackColor);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                Color color = Color.FromArgb(ImageMarginOpacity, _realBackColor);
                using SolidBrush bruh = new(color);
                e.Graphics.FillRectangle(bruh, e.AffectedBounds);
            }
        }

        public CoolContextMenu()
        {
            Renderer = new CoolRenderer();
            BackColor = Color.FromArgb(0, 0, 0, 0);
            ForeColor = Color.White;
        }

        // todo: updates with application gradient color automatically
        // may require some refactoring
        public DwmColor AccentColor = DwmColor.FromAbgr(ThemeManager.ActiveGradientColor);

        private void HookMenuItem(ToolStripMenuItem item)
        {
            item.DropDownOpened += Item_DropDownOpened;

            foreach (ToolStripMenuItem child in item.DropDownItems.OfType<ToolStripMenuItem>())
            {
                HookMenuItem(child);
            }
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);

            if (e.Item is ToolStripMenuItem item)
            {
                HookMenuItem(item);
            }
        }

        private void Item_DropDownOpened(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.DropDown.IsHandleCreated)
            {
                DwmApi.SetAccentState(item.DropDown.Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, AccentColor.ToAbgr());
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DwmApi.SetAccentState(Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, AccentColor.ToAbgr());
        }
    }
}
