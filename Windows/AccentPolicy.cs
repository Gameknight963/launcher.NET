using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace launcherdotnet.Windows
{
    /// <summary>
    /// Defines the configuration used by the undocumented Windows accent composition API.
    /// Controls visual effects such as transparency, blur, and acrylic rendering.
    /// </summary>
    /// <param name="accentState">
    /// The accent effect to apply.
    /// </param>
    /// <param name="accentFlags">
    /// Flags controlling accent rendering behavior.
    /// The meaning of these flags is undocumented and may vary between Windows versions, so
    /// probably don't touch it.
    /// </param>
    /// <param name="gradientColor">
    /// The color used by the accent effect in ARGB format.
    /// </param>
    /// <param name="animationId">
    /// The animation identifier used by the accent system.
    /// Its behavior is undocumented, so also probably don't touch it.
    /// </param>
    [StructLayout(LayoutKind.Sequential)]
    public struct AccentPolicy(
        AccentState accentState = AccentState.ACCENT_DISABLED,
        int accentFlags = 2,
        int gradientColor = 0x00000000,
        int animationId = 0)
    {
        /// <summary>
        /// The accent effect to apply.
        /// </summary>
        public AccentState AccentState = accentState;

        /// <summary>
        /// Flags controlling accent rendering behavior.
        /// The meaning of these flags is undocumented and may vary between Windows versions.
        /// </summary>
        public int AccentFlags = accentFlags;

        /// <summary>
        /// The color used by the accent effect in ARGB format.
        /// </summary>
        public int GradientColor = gradientColor;

        /// <summary>
        /// The animation identifier used by the accent system.
        /// Its behavior is undocumented.
        /// </summary>
        public int AnimationId = animationId;
    }
}
