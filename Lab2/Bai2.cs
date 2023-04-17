using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Bai2 : Form
    {
        public Bai2()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            FileStream fs = new FileStream(ofd.SafeFileName.ToString(), FileMode.Open);
            int linecount = 1, countword = 0;
            string filePath = System.IO.Path.GetFullPath("fs");
            string fileName = System.IO.Path.GetFileName("fs");
            long length = new System.IO.FileInfo("fs").Length;
            StreamReader sr = new StreamReader("fs");
            richTextBox1.Text = sr.ReadLine();
            textBox2.Text = length.ToString() + "byte";
            textBox3.Text = Convert.ToString(filePath);
            textBox1.Text = System.IO.Path.GetFileName("fs");
            while (sr.ReadLine() != null)
            {
                linecount++;
            }
            string[] source = richTextBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            countword = source.Count();
            textBox4.Text = Convert.ToString(linecount);
            textBox5.Text = Convert.ToString(countword);
            textBox6.Text = Convert.ToString(richTextBox1.Text.Length);

            //  sr.Close();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
