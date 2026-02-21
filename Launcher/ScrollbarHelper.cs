using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet.Launcher
{
    internal class ScrollbarHelper
    {
        [DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_BOTH = 3;

        public enum Scrollbar
        {
            Horz,
            Vert,
            Both
        }

        public static void Set(Control control, Scrollbar bar, bool show)
        {
            int wBar = bar switch
            {
                Scrollbar.Horz => SB_HORZ,
                Scrollbar.Vert => SB_VERT,
                Scrollbar.Both => SB_BOTH,
                _ => SB_BOTH
            };
            ShowScrollBar(control.Handle, wBar, show);
        }
    }
}