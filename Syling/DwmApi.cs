using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet.Syling
{
    internal static class DwmApi
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);


        public enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
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


        public static void SetAccentState(IntPtr hwnd, AccentState accentState)
        {
            AccentPolicy accent = new AccentPolicy
            {
                AccentState = accentState
            };

            int size = Marshal.SizeOf(accent);

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(accent, ptr, false);

            WindowCompositionAttributeData data = new()
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                Data = ptr,
                SizeOfData = size
            };

            SetWindowCompositionAttribute(hwnd, ref data);

            Marshal.FreeHGlobal(ptr);
        }

        public static void SetWindowAttribute(IntPtr hwnd, int attribute, int value)
        {
            int val = value;
            DwmSetWindowAttribute(hwnd, attribute, ref val, sizeof(int));
        }
    }
}
