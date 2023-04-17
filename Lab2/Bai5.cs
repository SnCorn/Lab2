using Lab2;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace lab02
{
    public partial class bai5 : Form
    {
        public bai5()
        {
            InitializeComponent();
        }
        private void bai5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputPath = Path.Combine(Application.StartupPath, "input5.txt");
            string outputPath = Path.Combine(Application.StartupPath, "output5.zip");

            using (var zipFile = ZipFile.Open(outputPath, ZipArchiveMode.Create))
            {
                zipFile.CreateEntryFromFile(inputPath, Path.GetFileName(inputPath), CompressionLevel.Optimal);


            }

            string unzipPath = Path.Combine(Application.StartupPath, "output5.zip");

            using (var zipFile = ZipFile.OpenRead(unzipPath))
            {
                foreach (var entry in zipFile.Entries)
                {
                    string outputFilePath = Path.Combine(Application.StartupPath, "output5.txt");

                    if (File.Exists(outputFilePath))
                    {
                        File.Delete(outputFilePath);
                    }

                    entry.ExtractToFile(outputFilePath);
                }
            }

            MessageBox.Show("Done");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Form1 = new Form1();
            Form1.Show();
            Hide();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}