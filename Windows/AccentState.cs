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
        /// This is an undocumented feature introduced in Windows 10.
        /// </summary>
        ACCENT_ENABLE_GRADIENT = 1,

        /// <summary>
        /// Enables a transparent gradient effect.
        /// This is an undocumented feature introduced in Windows 10.
        /// </summary>
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,

        /// <summary>
        /// Enables a blur-behind effect.
        /// This is an undocumented feature introduced in Windows 10.
        /// </summary>
        ACCENT_ENABLE_BLURBEHIND = 3,

        /// <summary>
        /// Enables an acrylic blur effect.
        /// This is an undocumented feature introduced in Windows 10 version 1803.
        /// </summary>
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
    }
}
