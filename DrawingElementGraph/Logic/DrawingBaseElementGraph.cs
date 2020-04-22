using DrawingGraphs.Abstracts;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingGraphs.Logic
{
    public class DrawingBaseElementGraph
    {
        public Graphics graphics { get; set; }
        private DrawingBaseElementGraph()
        {

        }
        public DrawingBaseElementGraph(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void DrawCircle(int r, int x, int y, float width)
        {
            int d = 2 * r;
            Pen pen = new Pen(Color.Black, width);
            //Rectangle rectangle = new Rectangle(x, y, r, r);
            try
            {
                graphics.DrawEllipse(pen, x - d, y - d, d, d);
            }
            catch (Exception ex)
            {

            }
        }

        public void DrawArc(int r, int x, int y, float width, Color color)
        {
            int d = 2 * r;
            Pen pen = new Pen(color, width);
            //Rectangle rectangle = new Rectangle(x, y, r, r);
            try
            {
                graphics.DrawArc(pen, x - (float)(d * 1.5), y - d, (float)(d * 1.3), d, 60, 240);
            }
            catch (Exception ex)
            {

            }
        }
        public void DrawArcWithArrow(int r, int x, int y, float width, Color color)
        {
            int d = 2 * r;
            AdjustableArrowCap myArrow = new AdjustableArrowCap(width * 4, width * 5);
            Pen pen = new Pen(color, width);
            pen.CustomEndCap = myArrow;
            //Rectangle rectangle = new Rectangle(x, y, r, r);
            try
            {
                graphics.DrawArc(pen, x - (float)(d * 1.5), y - d, (float)(d * 1.3), d, 60, 240);
            }
            catch (Exception ex)
            {

            }
        }
        public void DrawLine(Point point1, Point point2, float width, Color color)
        {
            Pen pen = new Pen(color, width);
            //Point point1 = new Point(x1, y1);
            //Point point2 = new Point(x2, y2);
            graphics.DrawLine(pen, point1, point2);

        }

        public void DrawVertexString(string str, int font, int x, int y)
        {
            if (font < 1)
                font = 1;
            Font f = new Font("Arial", font);
            SolidBrush brush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            try
            {

                graphics.DrawString(str, f, brush, x, y, stringFormat);
            }
            catch (Exception ex)
            {
            }
        }

        public void DrawLineWithArrow(Point point1, Point point2, float width, Color color)
        {
            AdjustableArrowCap myArrow = new AdjustableArrowCap(width * 4, width * 5);
            Pen capPen = new Pen(color);
            //capPen.CustomStartCap = myArrow;
            capPen.CustomEndCap = myArrow;
            graphics.DrawLine(capPen, point1, point2);

        }

        ~DrawingBaseElementGraph()
        {
            graphics.Dispose();
        }
    }
}
