using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDeTai
{
    public partial class Update : Form
    {
        public QuanLy quanLy;
        ADO ado = new ADO();

        public string Madt;
        public Update()
        {
            InitializeComponent();
        }

        // Xử lí lưu khi Update Detai
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtTendt.Text == "" || txtNoithuctap.Text == "" || txtKinhphi.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !");
                return;
            }

            int value;
            if(int.TryParse(txtKinhphi.Text, out value) == false)
            {
                MessageBox.Show("Kinh phí chưa hợp lệ. Vui lòng nhập lại !");
                return;
            }

            int kq = ado.UpdateDeTai(txtMadt.Text, txtTendt.Text, txtKinhphi.Text, txtNoithuctap.Text);
            if (kq > 0)
            {
                MessageBox.Show("Sửa đề tài thành công");
                DataSet ds = ado.LoadDanhSachDeTai();
                quanLy.RenderGirdDanhSachDeTai(ds);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa thất bại");            
            }
        }

        private void Update_Load(object sender, EventArgs e)
        {
            txtMadt.Text = Madt;
            txtMadt.Enabled = false;
        }
    }
}
