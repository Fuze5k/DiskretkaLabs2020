using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingGraphs;
using DrawingGraphs.Logic;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private int[,] weightMatrix;
        private int[,] matrix;
        private int n = 10;
        private bool IsDrawing = false;
        private Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            this.graphics = this.CreateGraphics();
            matrix = GraphHelper.GenerateAdjanceMatrixLab5(10, 9, 3, 0, 8, checkBox1.Checked);
            weightMatrix = GraphHelper.GenerateWeightMatrixLab5((int[,])matrix.Clone(), n, 9, 3, 0, 8, checkBox1.Checked);
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
            matrix = GraphHelper.GenerateAdjanceMatrixLab5(n, 9, 3, 0, 8, checkBox1.Checked);
            GraphHelper.GenerateWeightMatrixLab5((int[,])matrix.Clone(), n, 9, 3, 0, 8, checkBox1.Checked);
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
            graphics.Clear(Color.White);
            DrawingGraph drawing = new DrawingGraph(graphics, n, 1, this.Size.Width, this.Size.Height);
            drawing.DrawGraph((int[,])matrix.Clone(), weightMatrix, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, checkBox1.Checked, 0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[,] kgm = GraphHelper.KraskalAlgorithm(n, (int[,])matrix.Clone(), (int[,])weightMatrix.Clone());
            Form form = new Form();
            form.Show();
            form.Width = 820;
            form.Height = 500;
            DrawingGraph drawing = new DrawingGraph(form.CreateGraphics(), n, 1, form.Width, form.Height);
            drawing.DrawGraph(kgm, weightMatrix, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, checkBox1.Checked, 1000);
        }



        private void button6_Click(object sender, EventArgs e)
        {
            int[,] b = (int[,])weightMatrix.Clone();
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
    }
}
