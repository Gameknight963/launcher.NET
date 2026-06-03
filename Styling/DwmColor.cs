using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace launcherdotnet.Styling
{
    [JsonConverter(typeof(DwmColorConverter))]
    public class DwmColor(Color color)
    {
        public Color Color { get; set; } = color;

        public static implicit operator Color(DwmColor? dwmColor) => dwmColor?.Color ?? Color.Empty;
        public static explicit operator DwmColor(Color color) => new DwmColor(color);

        /// <summary>
        /// Converts this color to Windows DWM ABGR format (0xAABBGGRR).
        /// </summary>
        public int ToAbgr()
        {
            return (Color.A << 24)
                 | (Color.B << 16)
                 | (Color.G << 8)
                 | Color.R;
        }

        /// <summary>
        /// Returns a Windows DWM format string (0xAABBGGRR).
        /// </summary>
        public static string ToDwmString(Color color)
        {
            int abgr = (color.A << 24)
                     | (color.B << 16)
                     | (color.G << 8)
                     | color.R;

            return $"0x{abgr:X8}";
        }

        /// <summary>
        /// Returns a hex string in 0xAABBGGRR format.
        /// </summary>
        public override string ToString()
        {
            return $"0x{ToAbgr():X8}";
        }

        /// <summary>
        /// Creates a DwmColor from a raw ABGR integer.
        /// </summary>
        public static DwmColor FromAbgr(int abgr)
        {
            byte a = (byte)((abgr >> 24) & 0xFF);
            byte b = (byte)((abgr >> 16) & 0xFF);
            byte g = (byte)((abgr >> 8) & 0xFF);
            byte r = (byte)(abgr & 0xFF);

            return new DwmColor(Color.FromArgb(a, r, g, b));
        }

        /// <summary>
        /// Parses a string in 0xAABBGGRR format.
        /// </summary>
        public static DwmColor Parse(string? value)
        {
            if (!TryParse(value, out DwmColor? result))
                throw new FormatException($"Invalid DWM color: '{value}'");

            return result!;
        }

        /// <summary>
        /// Attempts to parse a string in 0xAABBGGRR format.
        /// </summary>
        public static bool TryParse(string? value, [NotNullWhen(true)] out DwmColor? color)
        {
            color = null;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                ? value[2..]
                : value;

            if (!int.TryParse(value, NumberStyles.HexNumber, null, out int abgr))
                return false;

            color = FromAbgr(abgr);
            return true;
        }
    }
}