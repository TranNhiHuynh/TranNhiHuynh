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
    public partial class ThemMoiHDDT : Form
    {
        ADO ado = new ADO();
        public Form1 quanly;
        public ThemMoiHDDT()
        {
            InitializeComponent();
        }

        private void ThemMoiHDDT_Load(object sender, EventArgs e)
        {

            DataSet dsDeTai = ado.LoadDanhSachDeTai();
           
            foreach(DataRow dr in dsDeTai.Tables["TBLDeTai"].Rows)
            {
                cbbMadt.Items.Add(dr["Madt"] + " - " + dr["Tendt"]);
            }


            DataSet dsSinhVien = ado.LoadDanhSachSinhVien();

            foreach (DataRow dr in dsSinhVien.Tables["TBLSinhVien"].Rows)
            {
                cbbSinhvien.Items.Add(dr["Masv"] + " - " + dr["Hotensv"]);
            }


            DataSet dsGiangVien = ado.LoadDanhSachGiangVien();

            foreach (DataRow dr in dsGiangVien.Tables["TBLGiangVien"].Rows)
            {
                cbbGiangvien.Items.Add(dr["Magv"] + " - " + dr["Hotengv"]);
            }
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cbbMadt.SelectedIndex < 0 || cbbSinhvien.SelectedIndex < 0
              || cbbGiangvien.SelectedIndex < 0 )
            {
                MessageBox.Show("Vui lòng chọn thông tin đầy đủ !");
                return;
            }

            string[] arrMadt = cbbMadt.SelectedItem.ToString().Split('-');

            string[] arrSinhvien = cbbSinhvien.SelectedItem.ToString().Split('-');

            string[] arrGiangVien = cbbGiangvien.SelectedItem.ToString().Split('-');        

            if (ado.KiemTraK9HuongDan(arrSinhvien[0].Trim()) > 0)
            {
                MessageBox.Show("Bị trùng khóa chính. Sinh viên này đã làm đề tài rồi !");
                return;
            }

            if (ado.InsertHuongDan(arrSinhvien[0].ToString().Trim(), arrMadt[0].ToString().Trim(), arrGiangVien[0].ToString().Trim()) > 0)
            {
                MessageBox.Show("Thêm thành công");
                quanly.LoadGirdView();
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }


        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
