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
            public required int Left;
            public required int Right;
            public required int Top;
            public required int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState = AccentState.ACCENT_DISABLED;
            public int AccentFlags = 2;
            public int GradientColor = 0x00000000;
            public int AnimationId = 0;
            public AccentPolicy(
                AccentState accentState = AccentState.ACCENT_DISABLED,
                int accentFlags = 2,
                int gradientColor = 0x00000000,
                int animationId = 0)
            {
                AccentState = accentState;
                AccentFlags = accentFlags;
                GradientColor = gradientColor;
                AnimationId = animationId;
            }
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
                AccentState = accentState,
                AccentFlags = 2,
                GradientColor = 0x00000000,
                AnimationId = 0
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
