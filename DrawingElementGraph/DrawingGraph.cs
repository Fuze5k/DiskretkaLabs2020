using System.Drawing;
using System.Collections.Generic;
using DrawingGraphs.Abstracts;
using DrawingGraphs.Logic;
using DrawingGraphs.Enums;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DrawingGraphs
{
    public class DrawingGraph : DrawingBaseElementGraph, IDrawingGraph
    {
        private IFigure figure;

        private Random rand = new Random();

        private int n { get; set; }
        private int widthform { get; set; }
        private int heightform { get; set; }
        //private int rcircle { get; set; }
        private float widthline { get; set; }

        public DrawingGraph(Graphics graphics, int n, float widthline, int widthform, int heightform) : base(graphics)
        {
            this.widthline = widthline;
            this.n = n;
            this.widthform = widthform;
            this.heightform = heightform;
            this.figure = new MyRectangle(n, heightform, widthform);

        }
        public DrawingGraph(Graphics graphics, int n, int rcircle, float widthline, int widthform, int heightform)
            : this(graphics, n, widthline, widthform, heightform)
        {
            this.figure = new MyRectangle(n, rcircle, heightform, widthform);
        }
        public void DrawGraph(int[,] adjacencymatrix, int[,] weigthmatrix, TypeLocationVertex type, bool arrow, int interval)
        {
            List<Point> vertex = GetPointLocationVertex(type);
            DrawVertexCircle(vertex);
            DrawVertexChisla(vertex);

            DrawAllLine(adjacencymatrix, vertex, arrow, interval, Color.Black, weigthmatrix);
        }

        public void DrawNewNumericGraph(int[,] adjacencymatrix, TypeLocationVertex type, bool arrow)
        {
            List<Point> vertex = GetPointLocationVertex(type);
            DrawVertexCircle(vertex);

            PayWayxForDFS(adjacencymatrix, n, vertex, arrow, Color.Red, 0, true);

        }
        public void DrawStagesGraph(int[,] adjacencymatrix, TypeLocationVertex type, bool arrow)
        {
            List<Point> vertex = GetPointLocationVertex(type);
            DrawVertexCircle(vertex);
            DrawVertexChisla(vertex);


            PayWayxForDFS(adjacencymatrix, n, vertex, arrow, Color.Red, 1500, false);

        }
        private int[] PayWayxForDFS(int[,] adjencymatrix, int n, List<Point> vertex, bool arrow, Color color, int interval, bool drawchisla)
        {
            int numvertex = 2;

            bool[] viseted = new bool[n];
            int[] prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                prev[i] = -1;
                viseted[i] = false;
            }
            for (int i = 0; i < n; i++)
            {
                if (viseted[i] == false)
                {
                    if (drawchisla)
                        DrawVertexString((i + 1).ToString(), (int)(figure.Radius / 1.5), vertex[i].X - figure.Radius / 2, vertex[i].Y - (int)(figure.Radius * 1.5), Color.Black);
                    DFS(i, ref viseted, ref prev, adjencymatrix, n, vertex, arrow, interval, color, ref numvertex, drawchisla);
                }
            }
            return prev;
        }

        private void DFS(int start, ref bool[] visited, ref int[] prev, int[,] adjacencymatrix, int n, List<Point> vertex,
            bool arrow, int interval, Color color, ref int numvertex, bool drawchisla)
        {
            visited[start] = true;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && adjacencymatrix[start, i] != 0)
                {
                    prev[i] = start;
                    if (drawchisla)
                        DrawVertexString((numvertex).ToString(), (int)(figure.Radius / 1.5), vertex[i].X - figure.Radius / 2, vertex[i].Y - (int)(figure.Radius * 1.5), Color.Black);
                    numvertex++;
                    DrawOneLine(prev[i], i, adjacencymatrix, vertex, arrow, color, null);
                    System.Threading.Thread.Sleep(interval);
                    DFS(i, ref visited, ref prev, adjacencymatrix, n, vertex, arrow, interval, color, ref numvertex, drawchisla);
                }
            }
        }

        private void DrawOneLine(int indp1, int indp2, int[,] adjacencymatrix, List<Point> vertex, bool arrow, Color color, int[,] weigthmatrix)
        {
            List<Point> vertexcircle = new List<Point>();
            for (int i = 0; i < vertex.Count; i++)
                vertexcircle.Add(new Point(vertex[i].X - figure.Radius, vertex[i].Y - figure.Radius));

            if (!arrow)
                adjacencymatrix[indp2, indp1] = 0;

            Point p1 = new Point(vertex[indp1].X - figure.Radius, vertex[indp1].Y - figure.Radius);
            Point p2 = new Point(vertex[indp2].X - figure.Radius, vertex[indp2].Y - figure.Radius);
            if (indp1 != indp2)
            {
                List<Point> vertexpoint = new List<Point>() { p1, p2 };

                if (arrow)
                    if (adjacencymatrix[indp1, indp2] == adjacencymatrix[indp2, indp1])
                        vertexpoint.Insert(1, GeneratePoint(vertexcircle, p1, p2));

                vertexpoint = PavingTheWay(vertexcircle, vertexpoint, indp1, indp2);
                vertexpoint = DeleteExcessivePoint(vertexpoint, vertexcircle, indp1, indp2);

                Point vect = new Point(vertexpoint[1].X - p1.X, vertexpoint[1].Y - p1.Y);
                int rad = figure.Radius + 5;
                double alpha = Math.Atan2(vect.Y, vect.X);
                p1.X += (int)(rad * Math.Cos(alpha));
                p1.Y += (int)(rad * Math.Sin(alpha));
                vertexpoint[0] = new Point(p1.X, p1.Y);

                for (int z = 0; z < vertexpoint.Count - 2; z++)
                {
                    DrawLine(vertexpoint[z], vertexpoint[z + 1], this.widthline, color);

                }

                vect = new Point(p2.X - vertexpoint[vertexpoint.Count - 2].X, p2.Y - vertexpoint[vertexpoint.Count - 2].Y);
                alpha = Math.Atan2(vect.Y, vect.X);
                p2.X -= (int)(rad * Math.Cos(alpha));
                p2.Y -= (int)(rad * Math.Sin(alpha));

                if (arrow)
                {
                    DrawLineWithArrow(vertexpoint[vertexpoint.Count - 2], p2, this.widthline, color);
                }
                else
                {
                    DrawLine(vertexpoint[vertexpoint.Count - 2], p2, this.widthline, color);
                }

                vertexpoint[vertexpoint.Count - 1] = p2;
                if (weigthmatrix != null)
                    DrawVertexString(weigthmatrix[indp1, indp2].ToString(), (int)(figure.Radius / 4), vertexpoint[vertexpoint.Count / 2].X-5, vertexpoint[vertexpoint.Count / 2].Y, Color.Red);
            }
            else
            {
                if (arrow)
                {
                    DrawArcWithArrow(figure.Radius / 2, p1.X - figure.Radius / 2, p1.Y + figure.Radius / 2, this.widthline, color);
                }
                else
                {
                    DrawArc(figure.Radius / 2, p1.X - figure.Radius / 2, p1.Y + figure.Radius / 2, this.widthline, color);
                }
            }
        }

        private void DrawAllLine(int[,] adjacencymatrix, List<Point> vertex, bool arrow, int interval, Color color, int[,] weigthmatrix)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (adjacencymatrix[i, j] >= 1 /*&& adjacencymatrix[i, j] != adjacencymatrix[j, i]*/)
                    {
                        DrawOneLine(i, j, adjacencymatrix, vertex, arrow, color, weigthmatrix);
                        System.Threading.Thread.Sleep(interval);
                    }
                }
            }
        }
        private List<Point> PavingTheWay(List<Point> vertexcircle, List<Point> vertexpoint, int i, int j)
        {
            bool p = true;

            for (int z = 0; z < vertexpoint.Count - 1; z++)
            {
                for (int k = 0; k < vertexcircle.Count; k++)
                {
                    if (k != i && k != j)
                    {
                        if (IntersectionLineCircle(vertexpoint[z].X, vertexpoint[z].Y, vertexpoint[z + 1].X, vertexpoint[z + 1].Y, vertexcircle[k].X, vertexcircle[k].Y, figure.Radius - 1))
                        {
                            p = false;
                            break;
                        }
                    }
                }
                if (p == false)
                {
                    vertexpoint.Insert(z + 1, GeneratePoint(vertexcircle, vertexpoint[z], vertexpoint[z + 1]));
                    z--;
                    p = true;
                }
            }
            return vertexpoint;
        }

        private Point GeneratePoint(List<Point> vertexcircle, Point p1, Point p2)
        {
            int p1p2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);

            while (true)
            {

                int mx = rand.Next(Math.Min(p1.X, p2.X) - figure.Radius * 2, Math.Max(p1.X, p2.X) + figure.Radius * 2);
                int my = rand.Next(Math.Min(p1.Y, p2.Y) - figure.Radius * 2, Math.Max(p1.Y, p2.Y) + figure.Radius * 2);
                if (p1.X == p2.X)
                {
                    mx = rand.Next((int)(p1.X - figure.Radius * 2.5), (int)(p1.X + figure.Radius * 2.5));
                }
                if (p1.Y == p2.Y)
                {
                    my = rand.Next((int)(p1.Y - figure.Radius * 2.5), (int)(p1.Y + figure.Radius * 2.5));
                }
                bool p = true;
                int dp = Math.Min((mx - p1.X) * (mx - p1.X) + (my - p1.Y) * (my - p1.Y),
                    (mx - p2.X) * (mx - p2.X) + (my - p2.Y) * (my - p2.Y));

                if (mx < 0 || mx > this.widthform || my < 0 || my > this.heightform)
                    p = false;
                for (int j = 0; j < vertexcircle.Count; j++)
                {
                    double dm = (mx - vertexcircle[j].X) * (mx - vertexcircle[j].X) +
                        (my - vertexcircle[j].Y) * (my - vertexcircle[j].Y);
                    if (dm < figure.Radius * figure.Radius || dp < p1p2 / 9)
                    {
                        p = false;
                        break;
                    }
                }
                if (p)
                    return new Point(mx, my);
            }
        }
        public List<Point> DeleteExcessivePoint(List<Point> vertexpoint, List<Point> vertexcircle, int i, int j)
        {
            bool p = true;

            for (int z = 0; z < vertexpoint.Count - 2; z++)
            {
                for (int w = z + 2; w < vertexpoint.Count - 1; w++)
                {
                    for (int k = 0; k < vertexcircle.Count; k++)
                    {
                        if (k != i && k != j)
                        {
                            if (IntersectionLineCircle(vertexpoint[z].X, vertexpoint[z].Y, vertexpoint[w].X, vertexpoint[w].Y, vertexcircle[k].X, vertexcircle[k].Y, figure.Radius - 1))
                            {
                                p = false;
                                break;
                            }
                        }
                    }
                    if (p)
                    {
                        vertexpoint.RemoveRange(z + 1, w - z - 1);
                        z--;
                        break;
                    }
                    else
                    {
                        p = true;
                    }
                }
            }
            return vertexpoint;
        }
        //private float sqr(float x)
        //{
        //    return x * x;
        //}

        /* private bool IntersectionLineCircle(float x1, float y1, float x2, float y2, float x0, float y0, float r)
         {
             //y0 = this.heightform - y0;
             //y1 = this.heightform - y1;
             //y2 = this.heightform - y2;

             float t = ((x0 - x1) * (x2 - x1) + (y0 - y1) * (y2 - y1)) /
                 ((float)Math.Pow(x2 - x1, 2) + (float)Math.Pow(y2 - y1, 2));
             if (t < 0)
                 t = 0;
             if (t > 1)
                 t = 1;
             double l = Math.Sqrt(Math.Pow((x1 - x0 + (x2 - x1 * t)), 2) + 
                 Math.Pow((y1-y0+(y2-y1)*t),2));
             if (l > r)
                 return false;
             else 
                 return true;
             //if (x0 + r * 0.9 < Math.Min(x1, x2))
             //    return false;
             //if (x0 - r * 0.9 > Math.Max(x1, x2))
             //    return false;
             //if (y0 + r * 0.9 < Math.Min(y1, y2))
             //    return false;
             //if (y0 - r * 0.9 > Math.Max(y1, y2))
             //    return false;


             //double A = y1 - y2;
             //double B = x1 - x2;
             //double C = y1 * x2 - y2 * x1;
             //double H = Math.Abs(A * x0 + B * y0 + C) / Math.Sqrt(A * A + B * B);
             //if (H > r)
             //{

             //    return false;
             //}
             //else
             //{
             //    return true;
             //}
             //double eps = 0.0000000001;
             //float dx01 = x1 - x0, dy01 = y1 - y0, dx12 = x2 - x1, dy12 = y2 - y1;
             //float a = sqr(dx12) + sqr(dy12);

             //float k = dx01 * dx12 + dy01 * dy12;
             //float c = sqr(dx01) + sqr(dy01) - sqr(r);
             //float d1 = sqr(k) - a * c;
             //if (d1 < 0)
             //    return false;
             //else if (Math.Abs(d1) < eps)
             //{
             //    float t = -k / a;
             //    float xi = x1 + t * dx12, yi = y1 + t * dy12;

             //    if (t > 0 - eps && t < 1 + eps)
             //        return true;

             //    else
             //        return false;

             //}
             //else
             //{
             //    float t1 = (float)(-k + Math.Sqrt(d1)) / a, t2 = (float)(-k - Math.Sqrt(d1)) / a;
             //    if (t1 > t2)
             //    {
             //        float qw = t1;
             //        t1 = t2;
             //        t2 = qw;
             //    }
             //    float xi1 = x1 + t1 * dx12, yi1 = y1 + t1 * dy12;
             //    float xi2 = x1 + t2 * dx12, yi2 = y1 + t2 * dy12;
             //    if (t1 >= 0 - eps && t2 <= 1 + eps)
             //        if (t1 > 0 - eps && t2 < 1 + eps)
             //            return true;

             //        else
             //            return true;

             //    else if (t2 <= 0 + eps || t1 >= 1 - eps)
             //        if (t2 < 0 + eps || t1 > 1 - eps)
             //            return false;

             //        else
             //            return true;

             //    else
             //        return true;
             //}
         }
         */
        bool IntersectionLineCircle(float x1, float y1, float x2, float y2, float x0, float y0, float radius)
        {
            float x01 = x1 - x0;
            float y01 = y1 - y0;
            float x02 = x2 - x0;
            float y02 = y2 - y0;

            float dx = x02 - x01;
            float dy = y02 - y01;

            float a = dx * dx + dy * dy;
            float b = (float)2.0 * (x01 * dx + y01 * dy);
            float c = x01 * x01 + y01 * y01 - radius * radius;

            if (-b < 0) return (c < 0);
            if (-b < (2.0 * a)) return (4.0 * a * c - b * b < 0);
            return (a + b + c < 0);
        }

        private void DrawVertexChisla(List<Point> vertex)
        {
            for (int i = 0; i < vertex.Count; i++)
            {
                DrawVertexString((i + 1).ToString(), (int)(figure.Radius / 1.5), vertex[i].X - figure.Radius / 2, vertex[i].Y - (int)(figure.Radius * 1.5), Color.Black);
            }
        }
        private void DrawVertexCircle(List<Point> vertex)
        {
            for (int i = 0; i < vertex.Count; i++)
            {
                DrawCircle(figure.Radius, vertex[i].X, vertex[i].Y, this.widthline);
            }
        }
        private List<Point> GetPointLocationVertex(TypeLocationVertex type)
        {
            int[,] matrix;
            switch (type)
            {
                case TypeLocationVertex.RectangleWithCenter:
                    matrix = figure.GetMatrixLocationVertex();
                    break;
                default:
                    matrix = new int[0, 0];
                    break;
            }

            return GetPointLocationFromMatrix(matrix);
        }

        private List<Point> GetPointLocationFromMatrix(int[,] matrix)
        {
            List<Point> points = new List<Point>();
            Point point = new Point();
            for (int i = 0; i < heightform; i++)
            {
                for (int j = 0; j < widthform; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        point.X = j;
                        point.Y = i;
                        points.Add(point);

                    }

                }
            }

            return points;
        }
    }
}
