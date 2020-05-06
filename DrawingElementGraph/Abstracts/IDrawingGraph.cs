using DrawingGraphs.Enums;

namespace DrawingGraphs.Abstracts
{
    interface IDrawingGraph
    {
        void DrawGraph(int[,] matrix, int[,] weigthmatrix, TypeLocationVertex type, bool arrow, int interval);
    }
}
