[Flags]
/// <summary>
/// Undocumented ACCENT_FLAGS. Use anything besides
/// <see cref="ACCENT_FLAG_ENABLE_GRADIENT_COLOR" />
/// at your own risk
/// </summary>
enum AccentFlags
{
    /// <summary>
    /// No flags.
    /// </summary>
    ACCENT_FLAG_NONE = 0,
    
    /// <summary>
    /// Same bit as <see cref="ACCENT_FLAG_ENABLE_GRADIENT_COLOR" />,
    /// but results in the modern acrylic recipe under these conditions:
    /// <br>
    /// 1. The operating system is Windows 11 22H2+ <br>
    /// 2. <see cref="ACCENT_ENABLE_ACRYLICBLURBEHIND" /> is enabled.
    /// </summary>
    ACCENT_FLAG_ENABLE_MODERN_ACRYLIC_RECIPE = 1 << 1,

    /// <summary>
    /// Enables the gradient color (without this flag, the OS chooses).
    /// </summary>
    ACCENT_FLAG_ENABLE_GRADIENT_COLOR = 1 << 1,

    ACCENT_FLAG_ENABLE_FULLSCREEN = 1 << 2,
    ACCENT_FLAG_ENABLE_BORDER_LEFT = 1 << 5,
    ACCENT_FLAG_ENABLE_BORDER_TOP = 1 << 6,
    ACCENT_FLAG_ENABLE_BORDER_RIGHT = 1 << 7,
    ACCENT_FLAG_ENABLE_BORDER_BOTTOM = 1 << 8,
    ACCENT_FLAG_ENABLE_BLUR_RECT = 1 << 9,

    ACCENT_FLAG_ENABLE_BORDER = 
        ACCENT_FLAG_ENABLE_BORDER_LEFT | 
        ACCENT_FLAG_ENABLE_BORDER_TOP |
        ACCENT_FLAG_ENABLE_BORDER_RIGHT |
        ACCENT_FLAG_ENABLE_BORDER_BOTTOM
}