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
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
        }

        private void Bai3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("input3.txt", FileMode.Open);
            StreamReader rd = new StreamReader(fs);
            string input = rd.ReadToEnd();
            richTextBox1.Text = input;
            rd.Close();
        }
        private static bool Parentheses(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
                return false;
            if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
                return false;
            return true;
        }
        private static double ApplyOP(char op, double b, double a)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return a / b;

            }

            return 0;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private static double CalculateExpression(string expression)
        {
            char[] tokens = expression.ToCharArray();

            Stack<double> values = new Stack<double>();
            Stack<char> ops = new Stack<char>();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == ' ')
                    continue;
                if (tokens[i] >= '0' && tokens[i] <= '9')
                {
                    StringBuilder sbuf = new StringBuilder();

                    while (i < tokens.Length && tokens[i] >= '0' && tokens[i] <= '9')
                    {
                        sbuf.Append(tokens[i++]);
                    }

                    values.Push(double.Parse(sbuf.ToString()));
                    i--;
                }
                else if (tokens[i] == '(')
                    ops.Push(tokens[i]);
                else if (tokens[i] == ')')
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(ApplyOP(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Pop();
                }
                else if (tokens[i] == '+' || tokens[i] == '-' || tokens[i] == '/' || tokens[i] == '*')
                {
                    while (ops.Count > 0 && Parentheses(tokens[i], ops.Peek()))
                    {
                        values.Push(ApplyOP(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Push(tokens[i]);
                }

            }
            while (ops.Count > 0)
            {
                values.Push(ApplyOP(ops.Pop(), values.Pop(), values.Pop()));
            }
            return values.Pop();
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
                string[] lines = richTextBox1.Text.Split('\n');
                StringBuilder sb = new StringBuilder();
                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();
                    if (trimmedLine.Length == 0) 
                    continue;
                    double result = CalculateExpression(trimmedLine);
                    sb.AppendFormat("{0} = {1}\n", trimmedLine, result);
                }
                richTextBox1.Text = sb.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
