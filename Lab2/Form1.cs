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

namespace Lab2
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
            matrix = GraphHelper.GenerateAdjanceMatrixLab2(n, 9, 3, 0, 8, checkBox1.Checked);
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
            matrix = GraphHelper.GenerateAdjanceMatrixLab2(n, 9, 3, 0, 8, checkBox1.Checked);

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
            DrawingGraph drawing = new DrawingGraph(graphics, n, 1, this.Size.Width - dataGridView1.Width, this.Size.Height);
            drawing.DrawGraph((int[,])matrix.Clone(), null, DrawingGraphs.Enums.TypeLocationVertex.RectangleWithCenter, checkBox1.Checked, 0);
            Analitics();

        }
        private void Analitics()
        {
            int r = 0;
            bool p = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (checkBox1.Checked)
            {
                dataGridView1.Columns.Add("0", "Напівстепень виходу");
                dataGridView1.Columns.Add("1", "Напівстепень входу");
                dataGridView1.Columns.Add("2", "Висяча");
                dataGridView1.Columns.Add("3", "Ізольована");
                int[] vxod = GraphHelper.StepinVxodyVertexNapryamGraph(matrix, n);
                int[] vixod = GraphHelper.StepinVixodyVertexNapryamGraph(matrix, n);
                r = vxod[0] + vixod[0];
                for (int i = 0; i < n; i++)
                {
                    if (r != vxod[i] + vixod[i])
                        p = false;

                    dataGridView1.Rows.Add(vixod[i].ToString(), vxod[i].ToString(),
                        GraphHelper.DetectHangingVertex(vxod[i] + vixod[i]).ToString(), GraphHelper.DetectIsolatedVertex(vxod[i] + vixod[i]).ToString());
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

            }
            else
            {
                dataGridView1.Columns.Add("0", "Степінь");
                dataGridView1.Columns.Add("1", "Висяча");
                dataGridView1.Columns.Add("2", "Ізольована");
                int[] step = GraphHelper.StepinVertexNotNapryamGraph(matrix, n);
                r = step[0];
                for (int i = 0; i < n; i++)
                {
                    if (r != step[i])
                        p = false;
                    dataGridView1.Rows.Add(step[i].ToString(),
                        GraphHelper.DetectHangingVertex(step[i]).ToString(), GraphHelper.DetectIsolatedVertex(step[i]).ToString());
                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

            }
            //dataGridView1.Columns[0].HeaderCell.Value = "Vertex";
            if (p == true)
            {
                label4.Text = "true";
                textBox2.Text = r.ToString();
            }
            else
            {
                label4.Text = "false";
                textBox2.Text = "-";
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            if (IsDrawing)
                Draw();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
