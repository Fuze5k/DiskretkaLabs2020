namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAdjacencyMatrix = new System.Windows.Forms.Button();
            this.buttonDegreeOfVertices = new System.Windows.Forms.Button();
            this.buttonPathsOfLength2 = new System.Windows.Forms.Button();
            this.buttonPathsOfLength3 = new System.Windows.Forms.Button();
            this.buttonReachabilityMatrix = new System.Windows.Forms.Button();
            this.buttonComponentsOfStrongConnectivity = new System.Windows.Forms.Button();
            this.buttonConnectivityMatrix = new System.Windows.Forms.Button();
            this.buttonCondensationGraph = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(233, 517);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(121, 21);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Напрямлений";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "n:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(81, 518);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "10";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(388, 510);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Drawing";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(535, 510);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "DrawingNewMatrix";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAdjacencyMatrix
            // 
            this.buttonAdjacencyMatrix.Location = new System.Drawing.Point(816, 44);
            this.buttonAdjacencyMatrix.Name = "buttonAdjacencyMatrix";
            this.buttonAdjacencyMatrix.Size = new System.Drawing.Size(130, 30);
            this.buttonAdjacencyMatrix.TabIndex = 19;
            this.buttonAdjacencyMatrix.Text = "AdjacencyMatrix";
            this.buttonAdjacencyMatrix.UseVisualStyleBackColor = true;
            this.buttonAdjacencyMatrix.Click += new System.EventHandler(this.buttonAdjacencyMatrix_Click);
            // 
            // buttonDegreeOfVertices
            // 
            this.buttonDegreeOfVertices.Location = new System.Drawing.Point(816, 102);
            this.buttonDegreeOfVertices.Name = "buttonDegreeOfVertices";
            this.buttonDegreeOfVertices.Size = new System.Drawing.Size(130, 30);
            this.buttonDegreeOfVertices.TabIndex = 20;
            this.buttonDegreeOfVertices.Text = "DegreeOfVertices";
            this.buttonDegreeOfVertices.UseVisualStyleBackColor = true;
            this.buttonDegreeOfVertices.Click += new System.EventHandler(this.buttonDegreeOfVertices_Click);
            // 
            // buttonPathsOfLength2
            // 
            this.buttonPathsOfLength2.Location = new System.Drawing.Point(816, 153);
            this.buttonPathsOfLength2.Name = "buttonPathsOfLength2";
            this.buttonPathsOfLength2.Size = new System.Drawing.Size(130, 30);
            this.buttonPathsOfLength2.TabIndex = 20;
            this.buttonPathsOfLength2.Text = "PathsOfLength2";
            this.buttonPathsOfLength2.UseVisualStyleBackColor = true;
            this.buttonPathsOfLength2.Click += new System.EventHandler(this.buttonPathsOfLength2_Click);
            // 
            // buttonPathsOfLength3
            // 
            this.buttonPathsOfLength3.Location = new System.Drawing.Point(816, 206);
            this.buttonPathsOfLength3.Name = "buttonPathsOfLength3";
            this.buttonPathsOfLength3.Size = new System.Drawing.Size(130, 30);
            this.buttonPathsOfLength3.TabIndex = 20;
            this.buttonPathsOfLength3.Text = "PathsOfLength3";
            this.buttonPathsOfLength3.UseVisualStyleBackColor = true;
            this.buttonPathsOfLength3.Click += new System.EventHandler(this.buttonPathsOfLength3_Click);
            // 
            // buttonReachabilityMatrix
            // 
            this.buttonReachabilityMatrix.Location = new System.Drawing.Point(816, 257);
            this.buttonReachabilityMatrix.Name = "buttonReachabilityMatrix";
            this.buttonReachabilityMatrix.Size = new System.Drawing.Size(130, 30);
            this.buttonReachabilityMatrix.TabIndex = 20;
            this.buttonReachabilityMatrix.Text = "ReachabilityMatrix";
            this.buttonReachabilityMatrix.UseVisualStyleBackColor = true;
            this.buttonReachabilityMatrix.Click += new System.EventHandler(this.buttonReachabilityMatrix_Click);
            // 
            // buttonComponentsOfStrongConnectivity
            // 
            this.buttonComponentsOfStrongConnectivity.Location = new System.Drawing.Point(816, 313);
            this.buttonComponentsOfStrongConnectivity.Name = "buttonComponentsOfStrongConnectivity";
            this.buttonComponentsOfStrongConnectivity.Size = new System.Drawing.Size(130, 43);
            this.buttonComponentsOfStrongConnectivity.TabIndex = 20;
            this.buttonComponentsOfStrongConnectivity.Text = "ComponentsOf StrongConnectivity";
            this.buttonComponentsOfStrongConnectivity.UseVisualStyleBackColor = true;
            this.buttonComponentsOfStrongConnectivity.Click += new System.EventHandler(this.buttonComponentsOfStrongConnectivity_Click);
            // 
            // buttonConnectivityMatrix
            // 
            this.buttonConnectivityMatrix.Location = new System.Drawing.Point(816, 377);
            this.buttonConnectivityMatrix.Name = "buttonConnectivityMatrix";
            this.buttonConnectivityMatrix.Size = new System.Drawing.Size(130, 43);
            this.buttonConnectivityMatrix.TabIndex = 20;
            this.buttonConnectivityMatrix.Text = "StrongConnectivityMatrix";
            this.buttonConnectivityMatrix.UseVisualStyleBackColor = true;
            this.buttonConnectivityMatrix.Click += new System.EventHandler(this.buttonConnectivityMatrix_Click);
            // 
            // buttonCondensationGraph
            // 
            this.buttonCondensationGraph.Location = new System.Drawing.Point(816, 443);
            this.buttonCondensationGraph.Name = "buttonCondensationGraph";
            this.buttonCondensationGraph.Size = new System.Drawing.Size(130, 42);
            this.buttonCondensationGraph.TabIndex = 20;
            this.buttonCondensationGraph.Text = "Condensation Graph";
            this.buttonCondensationGraph.UseVisualStyleBackColor = true;
            this.buttonCondensationGraph.Click += new System.EventHandler(this.buttonCondensationGraph_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 561);
            this.Controls.Add(this.buttonComponentsOfStrongConnectivity);
            this.Controls.Add(this.buttonReachabilityMatrix);
            this.Controls.Add(this.buttonPathsOfLength3);
            this.Controls.Add(this.buttonPathsOfLength2);
            this.Controls.Add(this.buttonConnectivityMatrix);
            this.Controls.Add(this.buttonCondensationGraph);
            this.Controls.Add(this.buttonDegreeOfVertices);
            this.Controls.Add(this.buttonAdjacencyMatrix);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAdjacencyMatrix;
        private System.Windows.Forms.Button buttonDegreeOfVertices;
        private System.Windows.Forms.Button buttonPathsOfLength2;
        private System.Windows.Forms.Button buttonPathsOfLength3;
        private System.Windows.Forms.Button buttonReachabilityMatrix;
        private System.Windows.Forms.Button buttonComponentsOfStrongConnectivity;
        private System.Windows.Forms.Button buttonConnectivityMatrix;
        private System.Windows.Forms.Button buttonCondensationGraph;
    }
}

