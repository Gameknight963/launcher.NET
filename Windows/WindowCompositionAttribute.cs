using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Specifies the window composition attribute to modify using the undocumented
    /// SetWindowCompositionAttribute API.
    /// </summary>
    public enum WindowCompositionAttribute
    {
        WCA_UNDEFINED = 0,

        /// <summary>
        /// Enables or disables non-client area rendering.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_NCRENDERING_ENABLED = 1,

        /// <summary>
        /// Controls the non-client rendering policy.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_NCRENDERING_POLICY = 2,

        /// <summary>
        /// Disables window transition animations.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_TRANSITIONS_FORCEDISABLED = 3,

        /// <summary>
        /// Allows painting of the non-client area.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_ALLOW_NCPAINT = 4,

        /// <summary>
        /// Gets the bounds of the caption buttons.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_CAPTION_BUTTON_BOUNDS = 5,

        WCA_NONCLIENT_RTL_LAYOUT = 6,
        WCA_FORCE_ICONIC_REPRESENTATION = 7,

        /// <summary>
        /// Gets the extended frame bounds of the window.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_EXTENDED_FRAME_BOUNDS = 8,

        WCA_HAS_ICONIC_BITMAP = 9,
        WCA_THEME_ATTRIBUTES = 10,
        WCA_NCRENDERING_EXILED = 11,
        WCA_NCADORNMENTINFO = 12,

        /// <summary>
        /// Excludes the window from Live Preview.
        /// Supported since Windows Vista.
        /// </summary>
        WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,

        WCA_VIDEO_OVERLAY_ACTIVE = 14,
        WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,

        /// <summary>
        /// Prevents the window from being included in Peek previews.
        /// Supported since Windows 7.
        /// </summary>
        WCA_DISALLOW_PEEK = 16,

        /// <summary>
        /// Cloaks or uncloaks a window.
        /// Supported since Windows 8.
        /// </summary>
        WCA_CLOAK = 17,

        /// <summary>
        /// Indicates whether the window is currently cloaked.
        /// Supported since Windows 8.
        /// </summary>
        WCA_CLOAKED = 18,

        /// <summary>
        /// Specifies the accent policy used for effects such as blur and acrylic.
        /// Supported since Windows 10.
        /// </summary>
        WCA_ACCENT_POLICY = 19,

        WCA_FREEZE_REPRESENTATION = 20,
        WCA_EVER_UNCLOAKED = 21,
        WCA_VISUAL_OWNER = 22,

        /// <summary>
        /// Represents the last valid window composition attribute value.
        /// </summary>
        WCA_LAST = 23
    }
}
