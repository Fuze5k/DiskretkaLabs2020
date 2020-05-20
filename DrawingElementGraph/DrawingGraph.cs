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

        public List<int>[] DrawDijkstra(int[,] adjacencymatrix, int[,] weigthmatrix, TypeLocationVertex type, bool arrow)
        {
            List<Point> vertex = GetPointLocationVertex(type);
            DrawVertexCircle(vertex);
            DrawVertexChisla(vertex);
            return Dijkstra(adjacencymatrix, weigthmatrix, n, vertex, arrow, Color.Red, 1500, true);
        }

        private List<int>[] Dijkstra(int[,] adjencymatrix, int[,] weigthmatrix, int n, List<Point> vertex, bool arrow, Color color, int interval, bool drawchisla)
        {
            List<int>[] paths = new List<int>[n + 1];
            bool[] v = new bool[n];
            int[] d = new int[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = false;
                d[i] = Int32.MaxValue;
            }

            d[0] = 0;
            int minvalue = 0;
            paths[0] = new List<int> { 0 };
            while (minvalue != Int32.MaxValue)
            {
                minvalue = Int32.MaxValue;
                int indminvalue = 0;
                for (int i = 0; i < n; i++)
                {
                    if (v[i] == false && d[i] < minvalue)
                    {
                        minvalue = d[i];
                        indminvalue = i;
                    }
                }


                if (minvalue != Int32.MaxValue)
                {
                    DrawCircle(figure.Radius, vertex[indminvalue].X, vertex[indminvalue].Y, this.widthline, Color.Red);
                    for (int i = 0; i < n; i++)
                    {
                        if (adjencymatrix[indminvalue, i] != 0)
                        {
                            if (weigthmatrix[indminvalue, i] + d[indminvalue] < d[i])
                            {
                                d[i] = d[indminvalue] + weigthmatrix[indminvalue, i];
                                paths[i] = new List<int>(paths[indminvalue]);
                                paths[i].Add(i);
                                DrawOneLine(indminvalue, i, adjencymatrix, vertex, arrow, Color.Red, weigthmatrix);
                                System.Threading.Thread.Sleep(interval);
                            }
                        }
                    }
                    v[indminvalue] = true;
                    DrawCircle(figure.Radius, vertex[indminvalue].X, vertex[indminvalue].Y, this.widthline, Color.Black);
                }
            }
            paths[n] = new List<int>();
            for (int i = 0; i < n; i++)
            {
                paths[n].Add(d[i]);
            }
            return paths;
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
                    DrawVertexString(weigthmatrix[indp1, indp2].ToString(), (int)(figure.Radius / 4), vertexpoint[vertexpoint.Count / 2].X - 5, vertexpoint[vertexpoint.Count / 2].Y, Color.Red);
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
                DrawCircle(figure.Radius, vertex[i].X, vertex[i].Y, this.widthline, Color.Black);
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
