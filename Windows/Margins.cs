using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Defines the margins used when extending the window frame into the client area.
    /// This structure is used by DWM to specify how much of each side of the window
    /// should be affected by frame extension.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        /// <summary>
        /// The width of the left margin in pixels.
        /// </summary>
        public required int Left;

        /// <summary>
        /// The width of the right margin in pixels.
        /// </summary>
        public required int Right;

        /// <summary>
        /// The height of the top margin in pixels.
        /// </summary>
        public required int Top;

        /// <summary>
        /// The height of the bottom margin in pixels.
        /// </summary>
        public required int Bottom;

        /// <summary>
        /// Extends the frame over the entire window.
        /// Equivalent to setting all margins to -1.
        /// </summary>
        public static readonly Margins FullWindow = new()
        {
            Left = -1,
            Right = -1,
            Top = -1,
            Bottom = -1
        };

        /// <summary>
        /// Prevents the frame from being extended into the client area.
        /// Equivalent to setting all margins to 0.
        /// </summary>
        public static readonly Margins Zero = new()
        {
            Left = 0,
            Right = 0,
            Top = 0,
            Bottom = 0
        };
    }
}
