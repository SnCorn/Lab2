using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using Lab2;

namespace lab02
{   

    public partial class Bai4 : Form
    {
        //tạo ds sinh viên
        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        public Bai4()
        {
            InitializeComponent();
        }

        bool checkThongSo(string person)
        {
            string[] info = person.Split('\n');

            // Kiểm tra tên
            if (!Regex.IsMatch(info[0], "^[a-zA-Z\\s]+$"))
            {
                MessageBox.Show("Tên không đúng định dạng");
                return false;
            }

            // Kiểm tra ID
            if (!int.TryParse(info[1], out int id) || info[1].Length != 8)
            {
                MessageBox.Show("ID không đúng định dạng");
                return false;
            }

            // Kiểm tra số điện thoại
            if (!Regex.IsMatch(info[2], "^0[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại không đúng định dạng");
                return false;
            }

            // Kiểm tra điểm số
            if (!double.TryParse(info[3], out double d1) || !double.TryParse(info[4], out double d2) || !double.TryParse(info[5], out double d3))
            {
                MessageBox.Show("Điểm số không đúng định dạng");
                return false;
            }

            if (d1 < 0 || d1 > 10 || d2 < 0 || d2 > 10 || d3 < 0 || d3 > 10)
            {
                MessageBox.Show("Điểm số không đúng định dạng");
                return false;
            }

            return true;
        }

        //In
        void Inthongtin(SinhVien sv)
        {
            textBox8.Text = sv.HoTen;
            textBox9.Text = Convert.ToString(sv.MSSV);
            textBox10.Text = sv.DienThoai;
            textBox11.Text = Convert.ToString(sv.DiemMon1);
            textBox12.Text = Convert.ToString(sv.DiemMon2);
            textBox13.Text = Convert.ToString(sv.DiemMon3);
            textBox14.Text = Convert.ToString(sv.DiemTrungBinh);
        }

        private void buttReadFi_Click(object sender, EventArgs e)
        {
            // Đọc danh sách sinh viên từ file bằng BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            List<SinhVien> danhSachSinhVien;
            using (FileStream fs = new FileStream("input4.txt", FileMode.Open))
            {
                danhSachSinhVien = (List<SinhVien>)formatter.Deserialize(fs);
            }

            //DTB
            danhSachSinhVien.ForEach(sv => sv.DiemTrungBinh = (sv.DiemMon1 + sv.DiemMon2 + sv.DiemMon3) / 3.0f);

            //Ghi dữ liệu
            using (FileStream fs2 = new FileStream("output4.txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs2, danhSachSinhVien);
            }

            Inthongtin(danhSachSinhVien[0]);

            //Hiển thị thông tin vào richTextBox1
            richTextBox1.Text = string.Join("\n\n", danhSachSinhVien.Select(sv => $"{sv.HoTen}\n{sv.MSSV}\n{sv.DienThoai}\n{sv.DiemMon1}\n{sv.DiemMon2}\n{sv.DiemMon3}\n{sv.DiemTrungBinh}\n"));
        }

        private void buttBack_Click(object sender, EventArgs e)
        {
            int index = int.Parse(STT.Text);
            List<SinhVien> danhSachSinhVien = new List<SinhVien>();
            using (StreamReader reader = new StreamReader("output4.txt"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                danhSachSinhVien = (List<SinhVien>)formatter.Deserialize(reader.BaseStream);
            }

            if (index == 1)
                MessageBox.Show("Đây là sinh viên đầu tiên");
            else
            {
                index--;
                STT.Text = Convert.ToString(index);
                Inthongtin(danhSachSinhVien[index - 1]);
            }
        }

        private void buttNext_Click(object sender, EventArgs e)
        {
            int index;
            if (int.TryParse(STT.Text, out index))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("output4.txt", FileMode.Open))
                {
                    List<SinhVien> danhSachSinhVien = (List<SinhVien>)formatter.Deserialize(fs);

                    if (index >= danhSachSinhVien.Count)
                    {
                        MessageBox.Show("Đây là sinh viên cuối cùng");
                    }
                    else
                    {
                        index++;
                        STT.Text = Convert.ToString(index);
                        Inthongtin(danhSachSinhVien[index - 1]);
                    }
                }
            }
            else
            {
                MessageBox.Show("Số thứ tự không hợp lệ");
            }
        }


        private void buttAdd_Click(object sender, EventArgs e)
        {
            string person = textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text + "\n" + textBox4.Text + "\n" + textBox5.Text + "\n" + textBox6.Text;
            if (!checkThongSo(person))
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                return;
            }
            // Lấy thông tin của sinh viên từ form
            SinhVien sv = new SinhVien();
            if (float.TryParse(textBox4.Text, out float diemMon1) &&
            float.TryParse(textBox5.Text, out float diemMon2) &&
            float.TryParse(textBox6.Text, out float diemMon3))
            {
                sv.HoTen = textBox1.Text;
                sv.MSSV = int.Parse(textBox2.Text);
                sv.DienThoai = textBox3.Text;
                sv.DiemMon1 = diemMon1;
                sv.DiemMon2 = diemMon2;
                sv.DiemMon3 = diemMon3;
                // Tính điểm trung bình
                sv.DiemTrungBinh = (sv.DiemMon1 + sv.DiemMon2 + sv.DiemMon3) / 3.0f;

                // Thêm sinh viên vào danh sách
                danhSachSinhVien.Add(sv);

                // Xóa thông tin của sinh viên vừa nhập
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                // In thông tin sinh viên vừa thêm vào RichTextBox
                richTextBox1.Text += person + "\n" + "\n";
            }
            else
            {
                MessageBox.Show("Điểm các môn học phải là số thực.");
            }
        }

            private void butwWrFi_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("input4.txt", FileMode.OpenOrCreate);
            //chuyển data thành byte và lưu vào file
            formatter.Serialize(fs, danhSachSinhVien);
            fs.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            richTextBox1.Clear();
            MessageBox.Show("Đã lưu");
        }

        private void buttB5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void STT_Click(object sender, EventArgs e)
        {

        }
    }
    [Serializable]
    public class SinhVien
    {
        public string HoTen { get; set; }
        public int MSSV { get; set; }
        public string DienThoai { get; set; }
        public float DiemMon1 { get; set; }
        public float DiemMon2 { get; set; }
        public float DiemMon3 { get; set; }
        public float DiemTrungBinh { get; set; }
    }
}