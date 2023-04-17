using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Bai1 : Form
    {
        public Bai1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            // in

            FileStream fs = new FileStream("input1.txt", FileMode.OpenOrCreate);
            string filePath = System.IO.Path.GetFullPath("fs");
            string input;
            StreamReader sr = new StreamReader(fs);
            input = sr.ReadToEnd();
            richTextBox1.Text = input;
            sr.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // out
            FileStream fs = new FileStream("output1.txt", FileMode.OpenOrCreate);
            string input;
            input = richTextBox1.Text;
            StreamWriter sw = new StreamWriter(fs);
            input = input.ToUpper();
            sw.WriteLine(input);
            sw.Close();
            StreamReader sr = new StreamReader("fs");
            richTextBox2.Text = input;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
