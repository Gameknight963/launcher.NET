using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.Windows
{
    public enum DwmWindowAttribute
    {
        /// <summary>
        /// Enables or disables non-client area rendering.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_NCRENDERING_ENABLED = 1,

        /// <summary>
        /// Specifies the non-client rendering policy.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_NCRENDERING_POLICY = 2,

        /// <summary>
        /// Forces transitions to be disabled.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_TRANSITIONS_FORCEDISABLED = 3,

        /// <summary>
        /// Allows or prevents non-client area painting.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_ALLOW_NCPAINT = 4,

        /// <summary>
        /// Retrieves the bounds of the caption buttons.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_CAPTION_BUTTON_BOUNDS = 5,

        /// <summary>
        /// Controls right-to-left layout of the non-client area.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_NONCLIENT_RTL_LAYOUT = 6,

        /// <summary>
        /// Forces iconic representation for the window.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_FORCE_ICONIC_REPRESENTATION = 7,

        /// <summary>
        /// Controls the window's Flip3D policy.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_FLIP3D_POLICY = 8,

        /// <summary>
        /// Gets the extended frame bounds of the window.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_EXTENDED_FRAME_BOUNDS = 9,

        /// <summary>
        /// Specifies whether the window has an iconic bitmap.
        /// Supported since Windows Vista.
        /// </summary>
        DWMWA_HAS_ICONIC_BITMAP = 10,

        /// <summary>
        /// Prevents the window from being peeked.
        /// Supported since Windows 7.
        /// </summary>
        DWMWA_DISALLOW_PEEK = 11,

        /// <summary>
        /// Excludes the window from Aero Peek.
        /// Supported since Windows 7.
        /// </summary>
        DWMWA_EXCLUDED_FROM_PEEK = 12,

        /// <summary>
        /// Cloaks or uncloaks the window.
        /// Supported since Windows 8.
        /// </summary>
        DWMWA_CLOAK = 13,

        /// <summary>
        /// Indicates the cloaking state of the window.
        /// Supported since Windows 8.
        /// </summary>
        DWMWA_CLOAKED = 14,

        /// <summary>
        /// Freezes the window representation.
        /// Supported since Windows 8.
        /// </summary>
        DWMWA_FREEZE_REPRESENTATION = 15,

        /// <summary>
        /// Enables passive update mode.
        /// Supported since Windows 10.
        /// </summary>
        DWMWA_PASSIVE_UPDATE_MODE = 16,

        /// <summary>
        /// Enables the host backdrop brush.
        /// Supported since Windows 10 version 1809.
        /// </summary>
        DWMWA_USE_HOSTBACKDROPBRUSH = 17,

        /// <summary>
        /// Enables immersive dark mode for the window.
        /// Supported since Windows 10 version 1809.
        /// </summary>
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,

        /// <summary>
        /// Controls the window corner preference (square, rounded, or rounded small).
        /// Supported since Windows 11.
        /// </summary>
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,

        /// <summary>
        /// Sets the border color of the window.
        /// Supported since Windows 11.
        /// </summary>
        DWMWA_BORDER_COLOR = 34,

        /// <summary>
        /// Sets the caption color of the window.
        /// Supported since Windows 11.
        /// </summary>
        DWMWA_CAPTION_COLOR = 35,

        /// <summary>
        /// Sets the title bar text color.
        /// Supported since Windows 11.
        /// </summary>
        DWMWA_TEXT_COLOR = 36,

        /// <summary>
        /// Gets the visible frame border thickness.
        /// Supported since Windows 11.
        /// </summary>
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS = 37,

        /// <summary>
        /// Controls the system backdrop type (Mica, Acrylic, etc.).
        /// Supported since Windows 11 version 22H2.
        /// </summary>
        DWMWA_SYSTEMBACKDROP_TYPE = 38,

        /// <summary>
        /// Enables the legacy Mica effect flag.
        /// Supported on Windows 11.
        /// </summary>
        DWMWA_MICA_EFFECT = 1029
    }
}
