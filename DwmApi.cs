using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet
{
    internal static class DwmApi
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        public static void ExtendFrame(IntPtr hwnd)
        {
            MARGINS margins = new MARGINS
            {
                Left = -1,
                Right = -1,
                Top = -1,
                Bottom = -1
            };

            DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }

        public static void UnextendFrame(IntPtr hwnd)
        {
            MARGINS margins = new MARGINS
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0
            };

            DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }

        public static void EnableImmersiveDarkMode(IntPtr hwnd)
        {
            int useDark = 1;
            int attribute = 20;

            DwmSetWindowAttribute(hwnd, attribute, ref useDark, sizeof(int));
        }

        public static void DisableImmersiveDarkMode(IntPtr hwnd)
        {
            int useDark = 0;
            int attribute = 20;

            DwmSetWindowAttribute(hwnd, attribute, ref useDark, sizeof(int));
        }


        public static void SetWindowAttribute(IntPtr hwnd, int attribute, int value)
        {
            int val = value;
            DwmSetWindowAttribute(hwnd, attribute, ref val, sizeof(int));
        }
    }
}
