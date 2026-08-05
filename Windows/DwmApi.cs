using System.Runtime.InteropServices;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Provides access to Desktop Window Manager (DWM) and window composition APIs.
    /// </summary>
    public static partial class DwmApi
    {
        internal static partial class Native
        {
            [LibraryImport("dwmapi.dll")]
            internal static unsafe partial int DwmSetWindowAttribute(
                nint hwnd,
                int attribute,
                int* value,
                int size);

            [LibraryImport("dwmapi.dll")]
            internal static unsafe partial int DwmExtendFrameIntoClientArea(
                nint hwnd,
                Margins* margins);

            [LibraryImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static unsafe partial bool SetWindowCompositionAttribute(
                nint hwnd,
                WindowCompositionAttributeData* data);
        }

        /// <summary>
        /// Extends the window frame into the client area using DWM.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="margins">The margins describing how far the frame should extend.</param>
        /// <exception cref="ExternalException">
        /// Thrown if DWM fails to extend the frame.
        /// </exception>
        public static unsafe void ExtendFrame(nint hwnd, Margins margins)
        {
            Marshal.ThrowExceptionForHR(Native.DwmExtendFrameIntoClientArea(hwnd, &margins));
        }
        /// <summary>
        /// Enables or disables extending the window frame into the client area.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="extending">
        /// <see langword="true"></see> if the frame should extend across 
        /// the client area, otherwise, <see langword="false"/>.
        /// </param>
        public static unsafe void ExtendFrame(nint hwnd, bool extending)
        {
            Margins margins = extending ? Margins.FullWindow : Margins.Zero;
            Marshal.ThrowExceptionForHR(Native.DwmExtendFrameIntoClientArea(hwnd, &margins));
        }

        /// <summary>
        /// Enables extending the window frame into the client area.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        public static void ExtendFrame(nint hwnd) => ExtendFrame(hwnd, true);

        /// <summary>
        /// Disables extending the window frame into the client area.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        public static void UnextendFrame(nint hwnd) => ExtendFrame(hwnd, false);

        /// <summary>
        /// Enables or disables immersive dark mode for the window title bar.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="enable">
        /// <see langword="true"></see> to enable immersive darkmode,
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <exception cref="ExternalException">
        /// Thrown if DWM fails to set immersive dark mode.
        /// </exception>
        public static void SetImmersiveDarkMode(nint hwnd, bool enable)
        {
            DwmSetWindowAttribute(hwnd,
                DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE,
                enable ? 1 : 0);
        }

        /// <summary>
        /// Enables immersive dark mode for the window title bar.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <exception cref="ExternalException">
        /// Thrown if DWM fails to enable immersive dark mode.
        /// </exception>
        public static void EnableImmersiveDarkMode(nint hwnd) => SetImmersiveDarkMode(hwnd, true);

        /// <summary>
        /// Disables immersive dark mode for the window title bar.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <exception cref="ExternalException">
        /// Thrown if DWM fails to disable immersive dark mode.
        /// </exception>
        public static void DisableImmersiveDarkMode(nint hwnd) => SetImmersiveDarkMode(hwnd, false);

        /// <summary>
        /// Applies a window composition attribute using the undocumented 
        /// SetWindowCompositionAttribute API.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="data">The composition attribute data to apply.</param>
        /// <returns>
        /// <see langword="true"/> if the attribute was applied successfully;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static unsafe bool SetWindowCompositionAttribute(nint hwnd, WindowCompositionAttributeData data)
        {
            return Native.SetWindowCompositionAttribute(hwnd, &data);
        }

        /// <summary>
        /// Sets the accent effect applied to a window.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="accentState">The accent effect to apply.</param>
        /// <param name="gradientColor">
        /// The ARGB color used by the effect. If omitted, a default color is selected.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the accent policy was applied successfully;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static unsafe bool SetAccentState(nint hwnd, AccentState accentState, int? gradientColor = null)
        {
            AccentPolicy accent = new AccentPolicy
            {
                AccentState = accentState,
                AccentFlags = 2,
                GradientColor = gradientColor ??
                    (accentState == AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND ?
                        0x66000000 : 0x00000000),
                AnimationId = 0
            };

            WindowCompositionAttributeData data = new()
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                Data = (nint)(&accent),
                SizeOfData = sizeof(AccentPolicy)
            };

            return SetWindowCompositionAttribute(hwnd, data);
        }

        /// <summary>
        /// Attempts to set a DWM window attribute value and returns the resulting HRESULT.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="attribute">The DWM attribute to modify.</param>
        /// <param name="value">The value to assign to the attribute.</param>
        /// <returns>
        /// The HRESULT returned by DWM. A value of <c>0</c> indicates success;
        /// otherwise, the result represents the failure code returned by DWM.
        /// </returns>
        /// <remarks>
        /// Unlike <see cref="DwmSetWindowAttribute(nint, DwmWindowAttribute, int)"/>,
        /// this method does not throw an exception when the operation fails.
        /// This allows callers to handle unsupported attributes or other native failures
        /// manually.
        /// </remarks>
        public static unsafe int TryDwmSetWindowAttribute(
            nint hwnd,
            DwmWindowAttribute attribute,
            int value)
        {
            return Native.DwmSetWindowAttribute(
                hwnd,
                (int)attribute,
                &value,
                sizeof(int));
        }

        /// <summary>
        /// Sets a DWM window attribute value.
        /// </summary>
        /// <param name="hwnd">The handle of the window.</param>
        /// <param name="attribute">The attribute to modify.</param>
        /// <param name="value">The value to assign to the attribute.</param>
        /// <exception cref="ExternalException">
        /// Thrown if DWM fails to set the attribute.
        /// </exception>
        public static unsafe void DwmSetWindowAttribute(
            nint hwnd,
            DwmWindowAttribute attribute,
            int value)
        {
            int val = value;
            int result = Native.DwmSetWindowAttribute(
                hwnd,
                (int)attribute,
                &val,
                sizeof(int));

            Marshal.ThrowExceptionForHR(result);
        }

        public class Support
        {
            /// <summary>
            /// Gets whether immersive dark mode window attributes are supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 10 version 1809 (build 17763).
            /// </remarks>
            public static bool ImmersiveDarkMode =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17763);

            /// <summary>
            /// Gets whether the system backdrop window attribute is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 11 version 22H2 (build 22621).
            /// </remarks>
            public static bool SystemBackdrop =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22621);

            /// <summary>
            /// Gets whether acrylic system backdrops are supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 11 version 22H2 (build 22621).
            /// </remarks>
            public static bool AcrylicBackdrop =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22621);

            /// <summary>
            /// Gets whether Mica system backdrops are supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 11 version 22H2 (build 22621).
            /// </remarks>
            public static bool MicaBackdrop =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 22621);

            /// <summary>
            /// Gets whether DWM frame extension into the client area is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows Vista.
            /// </remarks>
            public static bool FrameExtension => 
                OperatingSystem.IsWindowsVersionAtLeast(6, 0);

            /// <summary>
            /// Gets whether the SetWindowCompositionAttribute API is available.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 8 (build 9200).
            /// </remarks>
            public static bool WindowCompositionAttribute =>
                OperatingSystem.IsWindowsVersionAtLeast(6, 2);
            /// <summary>
            /// Gets whether the gradient accent effect is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 10 version 1507 (build 10240).
            /// </remarks>
            public static bool AccentGradient =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240);

            /// <summary>
            /// Gets whether the transparent gradient accent effect is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 10 version 1507 (build 10240).
            /// </remarks>
            public static bool AccentTransparentGradient =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240);

            /// <summary>
            /// Gets whether the blur-behind accent effect is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 10 version 1507 (build 10240).
            /// </remarks>
            public static bool AccentBlurBehind =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 10240);

            /// <summary>
            /// Gets whether the acrylic blur accent effect is supported.
            /// </summary>
            /// <remarks>
            /// Supported since Windows 10 version 1803 (build 17134).
            /// </remarks>
            public static bool AccentAcrylicBlurBehind =>
                OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17134);
        }
    }
}
