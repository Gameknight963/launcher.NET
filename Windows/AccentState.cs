using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Specifies the visual accent effect applied by the undocumented
    /// SetWindowCompositionAttribute API.
    /// </summary>
    public enum AccentState
    {
        /// <summary>
        /// Disables accent effects.
        /// </summary>
        ACCENT_DISABLED = 0,

        /// <summary>
        /// Enables a gradient color effect.
        /// </summary>
        ACCENT_ENABLE_GRADIENT = 1,

        /// <summary>
        /// Enables a transparent gradient effect.
        /// </summary>
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,

        /// <summary>
        /// Enables a blur-behind effect.
        /// </summary>
        ACCENT_ENABLE_BLURBEHIND = 3,

        /// <summary>
        /// Enables an acrylic blur effect.
        /// Introduced in Windows 10 1803.
        /// </summary>
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4

        /// <summary>
        /// Draw the host backdrop effect (like windows 11 mica effect.)
        /// Introduced in Windows 10 1809.
        /// </summary>
        ACCENT_ENABLE_HOSTBACKDROP = 5,

        /// <summary>
        /// Unknown. Seems to draw background fully transparent. I don't recommend you use this
        /// </summary>
        ACCENT_INVALID_STATE = 6,
    }
}
