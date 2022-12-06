using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using ExcelDataReader;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDeTai
{
       public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }

        ADO ado = new ADO();

        // Tìm kiếm Detai
        private void tim2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string detai = txtDeTai.Text.ToString();
            string noithuctap = txtNoiThucTap.Text.ToString();
            string kinhphimin = txtKinhPhiDau.Text.ToString();
            string kinhphimax = txtKinhPhiCuoi.Text.ToString();
            DataSet ds = ado.TimKiemDeTai(detai, noithuctap, kinhphimin, kinhphimax);
            RenderGirdDanhSachDeTai(ds);
        }

        // Thêm mới DeTai
        private void thêmMơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemMoi themMoi = new ThemMoi();
            themMoi.quanLy = this;
            themMoi.Show();         
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = ado.LoadDanhSachDeTai();
            RenderGirdDanhSachDeTai(ds);
        }

        public void RenderGirdDanhSachDeTai(DataSet ds)
        {
            gridDanhSachDeTai.DataSource = ds.Tables["TBLDeTai"];
        }

        // Clear : Làm sạch TextBox và Grid
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtDeTai.Text = "";
            txtNoiThucTap.Text = "";
            txtKinhPhiDau.Text = "";
            txtKinhPhiCuoi.Text = "";
            txtDeTai.Focus();
            DataSet ds = ado.LoadDanhSachDeTai();
            ds.Tables["TBLDeTai"].Clear();
            gridDanhSachDeTai.DataSource = ds.Tables["TBLDeTai"];
        }

        // Sửa DeTai
        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.quanLy = this;
            update.Madt = gridDanhSachDeTai.CurrentRow.Cells[0].Value.ToString();
            update.Show();
        }

        // Xóa DeTai
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string maDt = gridDanhSachDeTai.CurrentRow.Cells[0].Value.ToString();

            if(ado.KiemTraKhoaNgoaiDeTai(maDt) > 0)
            {
                MessageBox.Show("Đề tài này được tham chiếu ở bảng khác. Không thể xóa !");
                return;
            }

            int kq = ado.DeleteDeTai(maDt);
            if (kq > 0)
            {
                MessageBox.Show("Xóa đề tài thành công");
                DataSet ds = ado.LoadDanhSachDeTai();
                RenderGirdDanhSachDeTai(ds);

            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }

        // Xuất file PDF
        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDanhSachDeTai.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(gridDanhSachDeTai.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in gridDanhSachDeTai.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in gridDanhSachDeTai.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Dữ liệu Export thành công!!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mô tả lỗi :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được Export!!!", "Info");
            }
        }


        // Xuất file Excel
        private void xuấtExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ado.ToExcel(gridDanhSachDeTai, saveFileDialog1.FileName);
            }
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
