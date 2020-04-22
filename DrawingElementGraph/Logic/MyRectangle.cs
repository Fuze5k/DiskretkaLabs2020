using System;

namespace DrawingGraphs.Logic
{
    class MyRectangle : Figure
    {
        private int radius;
        public override int Radius
        {
            get
            {
                return radius;
            }
        }
        public MyRectangle(int n, int heightform, int widthform)
            : base(n, heightform, widthform)
        {
            radius = GetRadiusCircleForRectangle(n, heightform, widthform);
        }
        public MyRectangle(int n, int radius, int heightform, int widthform)
            : base(n, heightform, widthform)
        {
            this.radius = radius;
        }
        private MyRectangle()
        {

        }


        public override int[,] GetMatrixLocationVertex()
        {
            int[,] a = new int[heightform, widthform];
            for (int i = 0; i < heightform; i++)
                for (int j = 0; j < widthform; j++)
                    a[i, j] = 0;

            switch (n)
            {
                case 1:
                    a[(int)(heightform /1.3), (int)(widthform /1.3)] = 1;
                    break;
                case 2:
                    a[heightform / 2, widthform / 2 - 2 * radius] = 1;
                    a[heightform / 2, widthform / 2 + 2 * radius] = 1;
                    break;
                case 3:
                    a[heightform / 2 - 2 * radius, widthform / 2 - 2 * radius] = 1;
                    a[heightform / 2 + 2 * radius, widthform / 2 + 2 * radius] = 1;
                    a[heightform / 2 + 2 * radius, widthform / 2 - 2 * radius] = 1;
                    break;
                case 4:
                    a[heightform / 2 - 2 * radius, widthform / 2 - 2 * radius] = 1;
                    a[heightform / 2 - 2 * radius, widthform / 2 + 2 * radius] = 1;
                    a[heightform / 2 + 2 * radius, widthform / 2 + 2 * radius] = 1;
                    a[heightform / 2 + 2 * radius, widthform / 2 - 2 * radius] = 1;
                    break;
                default:
                    return GetStandartLocationVertex();
            }



            return a;
        }

        private int[,] GetStandartLocationVertex()
        {
            int[,] a = new int[heightform, widthform];
            for (int i = 0; i < heightform; i++)
                for (int j = 0; j < widthform; j++)
                    a[i, j] = 0;

            a[heightform / 2, widthform / 2] = 1;
            int k = n - 1;
            //int diam = 2 * radius;
            int l = 0, r = 0, t = 0, d = 0;
            for (int i = 0; i < k; i++)
            {
                if (i % 4 == 0)
                {
                    t++;
                }
                if (i % 4 == 1)
                {
                    d++;
                }
                if (i % 4 == 2)
                {
                    l++;
                }
                if (i % 4 == 3)
                {
                    r++;
                }

            }

            int x1 = (int)(widthform * 0.2);
            int y1 = (int)(heightform * 0.2);
            int x2 = (int)(widthform * 0.8);
            int y2 = (int)(heightform * 0.75);

            int stept = (x2 - x1) / (t);
            for (int i = x1, j = 0; j < t; i += stept, j++)
            {
                a[y1, i] = 1;
            }

            int stepr = (y2 - y1) / (r);
            for (int i = y1, j = 0; j < r; i += stepr, j++)
            {
                a[i, x2] = 1;
            }


            int stepd = (x1 - x2) / (d);
            for (int i = x2, j = 0; j < d; i += stepd, j++)
            {
                a[y2, i] = 1;
            }

            int stepl = (y1 - y2) / (l);
            for (int i = y2, j = 0; j < l; i += stepl, j++)
            {
                a[i, x1] = 1;
            }



            return a;
        }

        public override int GetRadiusCircleForRectangle(int n, int heightform, int widthform)
        {
            int r = 10;
            try
            {
                r = (int)Math.Min(heightform / (n * 1.5), widthform / (n * 1.5));
            }
            catch (Exception e)
            {
                //throw new Exception("select another value n!!the graph does not fit this form");
                r = 10;
            }
            if (n <= 5)
            {
               return (int)Math.Min(heightform / (n * 3), widthform / (n * 3));

            }
            else
            {

                return r;
            }
        }
    }

}
