namespace DrawingGraphs.Abstracts
{
    interface IFigure
    {
        int Radius { get; }
        int[,] GetMatrixLocationVertex();
        int GetRadiusCircleForRectangle(int n, int heightform, int widthform);

    }
}
