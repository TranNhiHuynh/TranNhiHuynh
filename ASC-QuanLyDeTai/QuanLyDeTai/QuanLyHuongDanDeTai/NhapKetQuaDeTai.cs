using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHuongDanDeTai
{
    public partial class NhapKetQuaDeTai : Form
    {
        ADO ado = new ADO();

        public Form1 quanly;
        public string Madt;
        public string Sinhvien;
        public string Giangvien;
        public NhapKetQuaDeTai()
        {
            InitializeComponent();
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NhapKetQuaDeTai_Load(object sender, EventArgs e)
        {
            txtMadt.Text = this.Madt;
            txtMadt.Enabled = false;
            txtSinhvien.Text = this.Sinhvien;
            txtSinhvien.Enabled = false;
            txtGiangvien.Text = this.Giangvien;
            txtGiangvien.Enabled = false;
            txtKetqua.Focus();            
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txtKetqua.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập kết quả !");
                return;
            }

            if(float.Parse(txtKetqua.Text) > 10 || float.Parse(txtKetqua.Text) < 0)
            {
                MessageBox.Show("Giá trị điểm không hợp lệ. Vui lòng nhập giá trị thang điểm từ 0 -> 10");
                return;
            }

            string[] arrMasv = txtSinhvien.Text.Split('-');
            if (ado.UpdateKetQua(arrMasv[0].Trim(), txtKetqua.Text.Trim()) > 0)
            {
                MessageBox.Show("Nhập kết quả thành công !");
                quanly.LoadGirdView();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nhập kết quả thất bại !");

            }
        }
    }
}
