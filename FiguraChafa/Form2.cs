using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RunnerRedBayesiana
{
    public partial class Form2 : Form
    {
        String title;
        public int nVertices;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = title; 
        }
        public Form2(String titulo)
        {
            title = titulo;
            nVertices = 0;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nVertices = (int)numericUpDown1.Value;
            this.Close();
        }

    }
}
