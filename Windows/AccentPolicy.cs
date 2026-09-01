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
    [StructLayout(LayoutKind.Sequential)]
    public struct AccentPolicy
    {
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
        /// Difficult to find information on what this does. I wouldn't recommend using it
        /// </param>
        public AccentPolicy(
            AccentState accentState = AccentState.ACCENT_DISABLED,
            AccentFlags accentFlags = Windows.AccentFlags.ACCENT_FLAG_ENABLE_GRADIENT_COLOR,
            int gradientColor = 0x00000000,
            int animationId = 0)
        {
            AccentState = accentState;
            AccentFlags = (int)accentFlags;
            GradientColor = gradientColor;
            AnimationId = animationId;
        }

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
        /// Difficult to find information on what this does. I wouldn't recommend using it
        /// </param>
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

        /// <summary>
        /// The accent effect to apply.
        /// </summary>
        public AccentState AccentState;

        /// <summary>
        /// Flags controlling accent rendering behavior.
        /// The meaning of these flags can vary between Windows versions
        /// </summary>
        public int AccentFlags;

        /// <summary>
        /// The color used by the accent effect in ARGB format.
        /// </summary>
        public int GradientColor;

        /// <summary>
        /// The animation identifier used by the accent system.
        /// </summary>
        public int AnimationId;
    }
}
