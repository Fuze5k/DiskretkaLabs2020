using DrawingGraphs.Abstracts;

namespace DrawingGraphs.Logic
{
    abstract class Figure : IFigure
    {
        public abstract int Radius { get; }
        public int n { get; set; }
        public int heightform { get; set; }
        public int widthform { get; set; }


        protected Figure(int n, int heightform, int widthform)
        {

            this.n = n;
            this.heightform = heightform;
            this.widthform = widthform;
        }

        public Figure()
        {

        }

        public abstract int[,] GetMatrixLocationVertex();
        public abstract int GetRadiusCircleForRectangle(int n, int heightform, int widthform);
    }
}
