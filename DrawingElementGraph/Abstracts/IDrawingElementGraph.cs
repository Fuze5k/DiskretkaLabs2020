using System.Collections.Generic;
using System.Drawing;

namespace DrawingGraphs.Abstracts
{
    interface IDrawingElementGraph
    {

        void DrawLine(Point point1, Point point2, float width);
        /// <summary>
        /// Draw black circle
        /// </summary>
        /// <param name="r">radius of circle.</param>
        void DrawCircle(int r, int x, int y, float width);
        void DrawVertexString(string str, int font, int x, int y);
        void DrawLineWithArrow(Point point1, Point point2, float width);
    }
}
