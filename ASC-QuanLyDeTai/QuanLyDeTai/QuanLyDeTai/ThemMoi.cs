using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDeTai
{
    public partial class ThemMoi : Form
    {
        ADO ado = new ADO();
        public QuanLy quanLy;
        public ThemMoi()
        {
            InitializeComponent();
        }

        // Xử lí khi lưu khi thêm DeTai
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(txtMadt.Text =="" || txtTendt.Text =="" || txtNoithuctap.Text =="" || txtKinhphi.Text =="")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !");
                return;
            }

            int value;
            if (int.TryParse(txtKinhphi.Text, out value) == false)
            {
                MessageBox.Show("Kinh phí chưa hợp lệ. Vui lòng nhập lại !");
                return;
            }

            if (ado.KiemTraK9DeTai(txtMadt.Text) > 0)
            {
                MessageBox.Show("Lỗi khóa chính. Mã đề tài bị trùng !");
                return;
            }

            int kq = ado.InsertDeTai(txtMadt.Text, txtTendt.Text, txtKinhphi.Text, txtNoithuctap.Text);

            if(kq > 0)
            {
                MessageBox.Show("Thêm đề tài thành công");
                DataSet ds = ado.LoadDanhSachDeTai();
                quanLy.RenderGirdDanhSachDeTai(ds);
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm đề tài thất bại !");
            }
        }

        // Xử lí đóng Form
        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemMoi_Load(object sender, EventArgs e)
        {

        }
    }
}
