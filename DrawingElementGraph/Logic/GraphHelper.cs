using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingGraphs.Logic
{
    public class GraphHelper
    {
        public static int[,] GenerateAdjanceMatrixLab1(int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            return GenerateAdjanceMatrixForLabs(n, n1, n2, n3, n4, 1.0, 0.02, 0.005, 0.25, napryam);
        }
        public static int[,] GenerateAdjanceMatrixLab2(int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            return GenerateAdjanceMatrixForLabs(n, n1, n2, n3, n4, 1.0, 0.01, 0.01, 0.3, napryam);
        }
        public static int[,] GenerateAdjanceMatrixLab3(int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            return GenerateAdjanceMatrixForLabs(n, n1, n2, n3, n4, 1.0, 0.005, 0.005, 0.27, napryam);
        }
        public static int[,] GenerateAdjanceMatrixLab4(int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            return GenerateAdjanceMatrixForLabs(n, n1, n2, n3, n4, 1.0, 0.01, 0.005, 0.15, napryam);
        }
        public static int[,] GenerateAdjanceMatrixLab5(int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            return GenerateAdjanceMatrixForLabs(n, n1, n2, n3, n4, 1.0, 0.01, 0.005, 0.05, napryam);
        }
        public static int[,] GenerateWeightMatrixLab5(int[,] matrix, int n, int n1, int n2, int n3, int n4, bool napryam)
        {
            Random random = new Random(n1 * 1000 + n2 * 100 + n3 * 10 + n4);
            int[,] a = matrix;
            int[,] w = new int[n, n];
            int[,] triangleArr = new int[n, n];
            int[,] wt = new int[n, n];
            bool[,] b = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    w[i, j] = 0;
                    triangleArr[i, j] = 0;
                    wt[i, j] = 0;
                    b[i, j] = false;
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = j - 1; i >= 0; i--)
                {
                    triangleArr[i, j] = 1;
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    wt[i, j] = Convert.ToInt32(random.NextDouble() * 100 * a[i, j]);
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    b[i, j] = Convert.ToBoolean(wt[i, j]);
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    wt[i, j] = (Convert.ToInt32(b[i, j] && !b[j, i] || b[i, j] && b[j, i]) * triangleArr[i, j]) * wt[i, j];
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    w[i, j] = wt[i, j] + wt[j, i];
                }
            }

            return w;
        }

        private static int[,] GenerateAdjanceMatrixForLabs(int n, int n1, int n2, int n3, int n4, double h1, double h2, double h3, double h4, bool napryam)
        {
            int[,] a = new int[n, n];
            Random random = new Random(n1 * 1000 + n2 * 100 + n3 * 10 + n4);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int T = random.Next(0, 2) + random.Next(0, 2);
                    a[i, j] = Convert.ToInt32(Math.Floor(((h1 - n3 * h2 - n4 * h3 - h4) * T)));
                }
            }
            if (!napryam)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (a[i, j] > 0)
                            a[j, i] = a[i, j];
            return a;
        }


        public static int[,] KraskalAlgorithm(int n, int[,] adjencematrix, int[,] weigthmatrix)
        {
            int INF = 32000;

            int[,] arbol = new int[n, n];
            int[] belongs = new int[n]; // позначає, чи належить дереву вершина

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    arbol[i, j] = 0;
            for (int i = 0; i < n; i++)
                belongs[i] = i;

            int nodoA = 0;
            int nodoB = 0;
            int arcos = 1;
            while (arcos < n)
            {
                // Знайти найлегше ребро, що не утворює циклів і зберегти вершини і вагу.
                int min = INF;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (min > weigthmatrix[i, j] && belongs[i] != belongs[j] && adjencematrix[i, j] != 0)
                        {
                            min = weigthmatrix[i, j];
                            nodoA = i;
                            nodoB = j;
                        }
                // Якщо вершини не належать до одного дерева, додаємо ребро між ними до дерева.
                if (belongs[nodoA] != belongs[nodoB])
                {
                    arbol[nodoA, nodoB] = min;
                    arbol[nodoB, nodoA] = min;

                    // Усі вершини дерева nodoB зараз належать до дерева nodoA.
                    int temp = belongs[nodoB];
                    belongs[nodoB] = belongs[nodoA];
                    for (int k = 0; k < n; k++)
                        if (belongs[k] == temp)
                            belongs[k] = belongs[nodoA];

                    arcos++;
                }
            }
            return arbol;
        }

        public static int[,] DegreeOfTheMatrix(int[,] adjancematrix, int n, int degree)
        {
            int[,] b = adjancematrix;
            int[,] c = adjancematrix;
            for (int q = 0; q < degree; q++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int s = 0;
                        for (int z = 0; z < n; z++)
                        {
                            s += b[i, z] * adjancematrix[z, j];
                        }
                        c[i, j] = s;
                    }
                }
                b = (int[,])c.Clone();
            }
            return c;
        }
        public static bool[,] ReachabilityMatrix(int[,] adjancematrix, int n)
        {
            bool[,] b = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    b[i, j] = adjancematrix[i, j] == 0 ? false : true;
                }
            }
            bool[,] c = (bool[,])b.Clone(); ;

            for (int i = 0; i < n * 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int z = 0; z < n; z++)
                    {
                        if (b[j, z] == false)
                        {
                            int s = 0;

                            for (int w = 0; w < n; w++)
                            {
                                s += (b[j, w] == false ? 0 : 1) * adjancematrix[w, z];
                            }
                            if (s != 0)
                            {
                                c[j, z] = true;
                            }
                        }

                    }
                }
                b = (bool[,])c.Clone(); ;
            }
            return c;
        }
        public static bool[,] StrongConnectivityMatrix(int[,] adjancematrix, int n)
        {
            bool[,] b = ReachabilityMatrix(adjancematrix, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    b[i, j] = b[i, j] == b[j, i] ? b[i, j] : false;
                    b[j, i] = b[i, j];
                }
            }
            return b;
        }


        public static List<List<int>> ComponentsOfStrongConnectivity(int[,] b, int n)
        {
            ComponentsStrongConnectivity componentsStrongConnectivity = new ComponentsStrongConnectivity();
            return componentsStrongConnectivity.ComponentsOfStrongConnectivity(b, n);
        }

        private class ComponentsStrongConnectivity
        {


            private List<List<int>> g = new List<List<int>>();
            private List<List<int>> gr = new List<List<int>>();
            private List<List<int>> result = new List<List<int>>();
            private List<bool> used = new List<bool>();
            private List<int> order = new List<int>();
            private List<int> component = new List<int>();
            public List<List<int>> ComponentsOfStrongConnectivity(int[,] b, int n)
            {

                for (int i = 0; i < n; i++)
                {
                    g.Add(new List<int>());
                    gr.Add(new List<int>());
                }
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {
                        if (b[i, j] != 0)
                        {
                            g[i].Add(j);
                            gr[j].Add(i);
                        }
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    used.Add(false);
                }
                for (int i = 0; i < n; ++i)
                    if (!used[i])
                        dfs1(i);
                for (int i = 0; i < n; i++)
                {
                    used[i] = false;
                }
                for (int i = 0; i < n; ++i)
                {
                    int v = order[n - 1 - i];
                    if (!used[v])
                    {
                        dfs2(v);
                        result.Add(new List<int>(component));
                        component.Clear();
                    }
                }
                return result;
            }

            private void dfs1(int v)
            {
                used[v] = true;
                for (int i = 0; i < g[v].Count; ++i)
                    if (!used[g[v][i]])
                        dfs1(g[v][i]);
                order.Add(v);
            }
            private void dfs2(int v)
            {
                used[v] = true;
                component.Add(v);
                for (int i = 0; i < gr[v].Count; ++i)
                    if (!used[gr[v][i]])
                        dfs2(gr[v][i]);
            }
        }
        public static int[] StepinVertexNotNapryamGraph(int[,] a, int n)
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
        public static int[] StepinVxodyVertexNapryamGraph(int[,] a, int n)
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
        public static int[] StepinVixodyVertexNapryamGraph(int[,] a, int n)
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

        public static bool DetectIsolatedVertex(int stepv)
        {
            if (stepv == 0)
                return true;
            else
                return false;
        }
        public static bool DetectHangingVertex(int stepv)
        {
            if (stepv == 1)
                return true;
            else
                return false;
        }

        public static int[,] PayWayxForDFS(int[,] adjacencymatrix, int n)
        {
            int[,] res = new int[n, n];
            bool[] viseted = new bool[n];
            int[] prev = new int[n];
            for (int i = 0; i < n; i++)
            {
                prev[i] = -1;
                viseted[i] = false;
                for (int j = 0; j < n; j++)
                    res[i, j] = 0;
            }
            for (int i = 0; i < n; i++)
            {
                if (viseted[i] == false)
                    DFS(i, ref viseted, ref prev, adjacencymatrix, n);
            }

            prev[0] = 0;
            for (int i = 0; i < n; i++)
            {
                res[prev[i], i] = 1;
            }

            return res;
        }

        private static void DFS(int start, ref bool[] visited, ref int[] prev, int[,] adjacencymatrix, int n)
        {
            visited[start] = true;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && adjacencymatrix[start, i] != 0)
                {
                    prev[i] = start;
                    DFS(i, ref visited, ref prev, adjacencymatrix, n);
                }
            }
        }

    }
}
