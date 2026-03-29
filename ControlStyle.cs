using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet.Launcher
{
    public class ControlStyle
    {
        public Color? BackColor { get; set; }
        public Color? ForeColor { get; set; }
        public BorderStyle? BorderStyle { get; set; }
        public Font? Font { get; set; }
        
        public ControlStyle(
            Color? backColor = null,
            Color? foreColor = null,
            BorderStyle? borderStyle = null,
            Font? font = null)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            BorderStyle = borderStyle;
            Font = font;
        }
    }
}
