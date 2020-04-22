using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations
{
    public class Calculation
    {

        public int[] StepinVertexNotNapryamGraph(int[,] a, int n)
        {
            int[] result = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j && a[i, j] != 0)
                        result[i]++;
                    if (a[i, j] >= 1)
                    {

                        result[i]++;
                        result[j]++;
                    }
                }
            }
            return result;
        }
        public int[] StepinVxodyVertexNapryamGraph(int[,] a, int n)
        {
            int[] result = new int[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = 0;

                for (int j = 0; j < n; j++)
                {
                    if (a[j, i] != 0)
                        result[i]++;
                }
            }
            return result;
        }
        public int[] StepinVixodyVertexNapryamGraph(int[,] a, int n)
        {
            int[] result = new int[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] != 0)
                        result[i]++;
                }
            }
            return result;
        }

        public bool DetectIsolatedVertex(int stepv)
        {
            if (stepv == 0)
                return true;
            else
                return false;
        }
        public bool DetectHangingVertex(int stepv)
        {
            if (stepv == 1)
                return true;
            else
                return false;
        }

        public int[] PayWayxForDFS(int[,] adjencymatrix, int n)
        {
            //int[,] res = new int[n, n];
            bool[] viseted = new bool[n];
            int[] prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                prev[i] = -1;
                viseted[i] = false;
                //for (int j = 0; j < n; j++)
                //    res[i, j] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                if (viseted[i] == false)
                    DFS(i, ref viseted, ref prev, adjencymatrix, n);
            }

            //for (int i = 0; i < n; i++)
            //{
            //    res[prev[i], i] = 1;
            //}

            return prev;
        }

        private void DFS(int start, ref bool[] visited, ref int[] prev, int[,] adjencymatrix, int n)
        {
            visited[start] = true;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    prev[i] = adjencymatrix[start, i];
                    DFS(i, ref visited, ref prev, adjencymatrix, n);
                }
            }
        }
    }
}
