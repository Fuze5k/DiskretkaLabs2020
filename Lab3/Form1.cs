using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingGraphs;
using DrawingGraphs.Logic;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private int[,] matrix;
        private int[,] notnapryammatrix;
        private int n = 2;
        private bool arrow;
        private bool IsDrawing = false;
        private Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            this.graphics = this.CreateGraphics();
            matrix = GraphHelper.GenerateAdjanceMatrixLab3(n, 9, 3, 0, 8, checkBox1.Checked);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            IsDrawing = true;
            try
            {
                n = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception ex)
            {
                n = 10;
                MessageBox.Show("n must be a number!!!");
            }
            matrix = GraphHelper.GenerateAdjanceMatrixLab3(n, 9, 3, 0, 8, checkBox1.Checked);

            Draw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            if (IsDrawing)
                Draw();
        }

        private void Draw()
        {
            arrow = checkBox1.Checked;
            CreateNotNapryamMatrix();
            graphics.Clear(Color.White);
            DrawingGraph drawing = new DrawingGraph(graphics, n, 1, this.Size.Width - 100, this.Size.Height);
            drawing.DrawGraph((int[,])matrix.Clone(), null, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, arrow, 0);

        }

        private void CreateNotNapryamMatrix()
        {
            notnapryammatrix = (int[,])matrix.Clone();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (notnapryammatrix[i, j] != 0)
                        notnapryammatrix[j, i] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (notnapryammatrix[i, j] != 0)
                        notnapryammatrix[j, i] = notnapryammatrix[i, j];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            if (IsDrawing)
                Draw();
        }





        private void buttonAdjacencyMatrix_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])matrix.Clone(); ;
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            DataGridView dataGridView = new DataGridView();
            dataGridView.Width = 500;
            dataGridView.Height = 500;
            for (int i = 0; i < n; i++)
            {
                dataGridView.Columns.Add(i.ToString(), (i + 1).ToString());
            }
            for (int i = 0; i < n; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = b[i, j].ToString();
                }
            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            form.Controls.Add(dataGridView);
        }

        private void buttonDegreeOfVertices_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            DataGridView dataGridView = new DataGridView();
            dataGridView.Width = 500;
            dataGridView.Height = 500;
            int r = 0;
            bool p = true;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            if (checkBox1.Checked)
            {
                dataGridView.Columns.Add("0", "Напівстепень виходу");
                dataGridView.Columns.Add("1", "Напівстепень входу");
                dataGridView.Columns.Add("2", "Висяча");
                dataGridView.Columns.Add("3", "Ізольована");
                int[] vxod = GraphHelper.StepinVxodyVertexNapryamGraph(matrix, n);
                int[] vixod = GraphHelper.StepinVixodyVertexNapryamGraph(matrix, n);
                r = vxod[0] + vixod[0];
                for (int i = 0; i < n; i++)
                {
                    if (r != vxod[i] + vixod[i])
                        p = false;

                    dataGridView.Rows.Add(vixod[i].ToString(), vxod[i].ToString(),
                        GraphHelper.DetectHangingVertex(vxod[i] + vixod[i]).ToString(), GraphHelper.DetectIsolatedVertex(vxod[i] + vixod[i]).ToString());
                    dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            else
            {
                dataGridView.Columns.Add("0", "Степінь");
                dataGridView.Columns.Add("1", "Висяча");
                dataGridView.Columns.Add("2", "Ізольована");
                int[] step = GraphHelper.StepinVertexNotNapryamGraph(matrix, n);
                r = step[0];
                for (int i = 0; i < n; i++)
                {
                    if (r != step[i])
                        p = false;
                    dataGridView.Rows.Add(step[i].ToString(),
                        GraphHelper.DetectHangingVertex(step[i]).ToString(), GraphHelper.DetectIsolatedVertex(step[i]).ToString());
                    dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            form.Controls.Add(dataGridView);
        }

        private void buttonPathsOfLength2_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])matrix.Clone();
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            ListBox listBox = new ListBox();
            listBox.Width = 500;
            listBox.Height = 500;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (b[i, j] != 0)
                        for (int z = 0; z < n; z++)
                        {
                            if (b[j, z] != 0)
                            {
                                listBox.Items.Add((i + 1).ToString() + " - " + (j + 1).ToString() + " - " + (z + 1).ToString());
                            }
                        }
                }
            }

            form.Controls.Add(listBox);

        }
        private void buttonPathsOfLength3_Click(object sender, EventArgs e)
        {

            int[,] b = (int[,])matrix.Clone();
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            ListBox listBox = new ListBox();
            listBox.Width = 500;
            listBox.Height = 500;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (b[i, j] != 0)
                        for (int z = 0; z < n; z++)
                            if (b[j, z] != 0)
                                for (int q = 0; q < n; q++)
                                    if (b[z, q] != 0)
                                        listBox.Items.Add((i + 1).ToString() + " - " + (j + 1).ToString()
                                            + " - " + (z + 1).ToString() + " - " + (q + 1).ToString());

            form.Controls.Add(listBox);
        }
        private void buttonReachabilityMatrix_Click(object sender, EventArgs e)
        {
            bool[,] b = GraphHelper.ReachabilityMatrix(matrix, n);
            if (!arrow)
            {
                b = GraphHelper.ReachabilityMatrix(notnapryammatrix, n);
            }
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            DataGridView dataGridView = new DataGridView();
            dataGridView.Width = 500;
            dataGridView.Height = 500;
            for (int i = 0; i < n; i++)
            {
                dataGridView.Columns.Add(i.ToString(), (i + 1).ToString());
            }
            for (int i = 0; i < n; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = (b[i, j] == true ? 1 : 0).ToString();
                }
            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            form.Controls.Add(dataGridView);
        }






        private void buttonComponentsOfStrongConnectivity_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])matrix.Clone();
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            List<List<int>> result = GraphHelper.ComponentsOfStrongConnectivity(b, n);
            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            ListBox listBox = new ListBox();
            listBox.Width = 500;
            listBox.Height = 500;
            for (int i = 0; i < result.Count; i++)
            {
                string s = "";
                for (int j = 0; j < result[i].Count; j++)
                {
                    s += (result[i][j] + 1).ToString() + " ";
                }
                listBox.Items.Add((i + 1).ToString() + " Компонента -> " + s);
            }
            form.Controls.Add(listBox);
        }



        private void buttonConnectivityMatrix_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])matrix.Clone();
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            bool[,] res = GraphHelper.StrongConnectivityMatrix(b, n);

            Form form = new Form();
            form.Show();
            form.AutoSize = true;
            DataGridView dataGridView = new DataGridView();
            dataGridView.Width = 500;
            dataGridView.Height = 500;
            for (int i = 0; i < n; i++)
            {
                dataGridView.Columns.Add(i.ToString(), (i + 1).ToString());
            }
            for (int i = 0; i < n; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = (res[i, j] == true ? 1 : 0).ToString();
                }
            }
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            form.Controls.Add(dataGridView);
        }

        private void buttonCondensationGraph_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])matrix.Clone();
            if (!arrow)
            {
                b = notnapryammatrix;
            }
            buttonComponentsOfStrongConnectivity_Click(sender, e);
            Form form = new Form();
            form.Show();
            List<List<int>> result = GraphHelper.ComponentsOfStrongConnectivity(b, n);
            form.Width = 800;
            form.Height = 500;
            int[,] m = new int[result.Count, result.Count];

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if (i != j)
                    {
                        if (!arrow)
                        {
                            m[i, j] = 1;
                        }
                        else
                        {
                            for (int z = 0; z < result[i].Count; z++)
                            {
                                for (int w = 0; w < result[j].Count; w++)
                                {
                                    if (b[result[i][z], result[j][w]] != 0)
                                        m[i, j] = 1;
                                }
                            }
                        }
                    }
                }
            }

            DrawingGraph drawing = new DrawingGraph(form.CreateGraphics(), result.Count, 1, this.Size.Width, this.Size.Height);
            drawing.DrawGraph(m, null, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, arrow, 0);
        }
    }
}
