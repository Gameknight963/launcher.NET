namespace launcherdotnet.Styling
{
    public class ButtonStyle : ControlStyle
    {
        public FlatStyle? FlatStyle { get; set; }
        public int? BorderSize { get; set; }
        public Color? BorderColor { get; set; }
        public Color? HoverBackColor { get; set; }
        public Color? DownBackColor { get; set; }

        public ButtonStyle(
            Color backColor,
            Color foreColor,
            FlatStyle? flatStyle = null,
            int? borderSize = null,
            Color? borderColor = null,
            Color? hoverBackColor = null,
            Color? downBackColor = null,
            Font? font = null) : base(backColor, foreColor)
        {
            FlatStyle = flatStyle;
            BorderSize = borderSize;
            BorderColor = borderColor;
            HoverBackColor = hoverBackColor;
            DownBackColor = downBackColor;
            Font = font;
        }
    }
}
