using System.Runtime.InteropServices;

namespace launcherdotnet.Styling
{
    internal static partial class DwmApi
    {
        [LibraryImport("dwmapi.dll")]
        internal static partial int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [LibraryImport("dwmapi.dll")]
        internal static partial int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

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

        [LibraryImport("user32.dll")]
        private static partial int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public required int Left;
            public required int Right;
            public required int Top;
            public required int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy(
            AccentState accentState = AccentState.ACCENT_DISABLED,
            int accentFlags = 2,
            int gradientColor = 0x00000000,
            int animationId = 0)
        {
            public AccentState AccentState = accentState;
            public int AccentFlags = accentFlags;
            public int GradientColor = gradientColor;
            public int AnimationId = animationId;
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

            _ = DwmExtendFrameIntoClientArea(hwnd, ref margins);
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

            _ = DwmSetWindowAttribute(hwnd, attribute, ref useDark, sizeof(int));
        }

        public static void DisableImmersiveDarkMode(IntPtr hwnd)
        {
            int useDark = 0;
            int attribute = 20;

            _ = DwmSetWindowAttribute(hwnd, attribute, ref useDark, sizeof(int));
        }


        public static void SetAccentState(IntPtr hwnd, AccentState accentState, int? gradientColor = null)
        {
            AccentPolicy accent = new AccentPolicy
            {
                AccentState = accentState,
                AccentFlags = 2,
                GradientColor = gradientColor ??
                (accentState == AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND ?
                0x66000000 :
                0x00000000),
                AnimationId = 0
            };

            LauncherLogger.WriteLine($"SetAccentState: {hwnd}, {accentState}, {gradientColor}");

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
