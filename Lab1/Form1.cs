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

namespace Lab1
{
    public partial class Form1 : Form
    {
        private int[,] matrix;
        private int n = 2;
        private bool IsDrawing = false;
        private Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            this.graphics = this.CreateGraphics();
            matrix = GraphHelper.GenerateMatrixLab1(10, 9, 3, 0, 8);
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
            matrix = GraphHelper.GenerateMatrixLab1(n, 9, 3, 0, 8);

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
            drawing.DrawGraph(matrix, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, checkBox1.Checked);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
