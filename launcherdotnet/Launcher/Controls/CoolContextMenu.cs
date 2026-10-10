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

        // todo: updates with application gradient color automatically
        // may require some refactoring
        public DwmColor AccentColor = DwmColor.FromAbgr(ThemeManager.ActiveGradientColor);

        public CoolContextMenu()
        {
            Renderer = new CoolRenderer();
            BackColor = Color.FromArgb(0, 0, 0, 0);
            ForeColor = Color.White;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DwmApi.SetAccentState(Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, AccentColor.ToAbgr());
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);
            if (e.Item is ToolStripMenuItem item)
            {
                ConfigureMenuItem(item);
            }
        }

        private void ConfigureMenuItem(ToolStripMenuItem item)
        {
            ConfigureDropDown(item.DropDown);

            item.DropDown.ItemAdded += (s, e) =>
            {
                if (e.Item is ToolStripMenuItem childItem)
                {
                    ConfigureMenuItem(childItem);
                }
            };

            foreach (ToolStripMenuItem child in item.DropDownItems.OfType<ToolStripMenuItem>())
            {
                ConfigureMenuItem(child);
            }
        }

        private void ConfigureDropDown(ToolStripDropDown dropDown)
        {
            dropDown.BackColor = Color.FromArgb(0, 0, 0, 0);
            dropDown.ForeColor = ForeColor;
            dropDown.Renderer = Renderer;

            dropDown.HandleCreated += (s, e) =>
            {
                DwmApi.SetAccentState(dropDown.Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, AccentColor.ToAbgr());
            };

            if (dropDown.IsHandleCreated)
            {
                DwmApi.SetAccentState(dropDown.Handle, AccentState.ACCENT_ENABLE_BLURBEHIND, AccentColor.ToAbgr());
            }
        }
    }
}
