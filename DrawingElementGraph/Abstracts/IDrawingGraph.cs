using DrawingGraphs.Enums;

namespace DrawingGraphs.Abstracts
{
    interface IDrawingGraph
    {
        void DrawGraph(int[,] matrix, TypeLocationVertex type,bool arrow);
    }
}
