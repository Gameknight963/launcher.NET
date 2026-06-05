namespace launcherdotnet.Launcher.Controls
{
    public class CheckerboardPanel : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            int cellSize = 8;
            using SolidBrush lightBrush = new SolidBrush(Color.White);
            using SolidBrush darkBrush = new SolidBrush(Color.LightGray);
            for (int y = 0; y < ClientRectangle.Height; y += cellSize)
            {
                for (int x = 0; x < ClientRectangle.Width; x += cellSize)
                {
                    bool isLight = ((x / cellSize) + (y / cellSize)) % 2 == 0;

                    e.Graphics.FillRectangle(
                        isLight ? lightBrush : darkBrush,
                        ClientRectangle.X + x,
                        ClientRectangle.Y + y,
                        cellSize,
                        cellSize);
                }
            }
            using SolidBrush brush = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brush, ClientRectangle);
        }
        public CheckerboardPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
