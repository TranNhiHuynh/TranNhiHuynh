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

namespace QuanLyHuongDanDeTai
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        ADO ado = new ADO();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> listTrangThai = new List<string>{"Tất cả","Đã có điểm","Chưa có điểm" };

            cbbTrangThai.DataSource = listTrangThai;
            LoadGirdView();
        }

        public void LoadGirdView()
        {
            DataSet ds = ado.LoadDanhSachHuongDanDeTai();
            dataGridView1.DataSource = ds.Tables["TBLHuongDanDeTai"];
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = ado.TimKiemDeTai(txtDetai.Text, txtSinhvien.Text, txtGiangvien.Text, cbbTrangThai.SelectedValue.ToString());          
            dataGridView1.DataSource = ds.Tables["TBLHuongDanDeTai"];
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtDetai.Text = "";
            txtGiangvien.Text = "";
            txtSinhvien.Text = "";
            cbbTrangThai.Text = "Tất cả";
            txtDetai.Focus();
            DataSet ds = ado.LoadDanhSachHuongDanDeTai();
            ds.Tables["TBLHuongDanDeTai"].Clear();
            dataGridView1.DataSource = ds.Tables["TBLHuongDanDeTai"];
        }

        private void thêmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemMoiHDDT themmoi = new ThemMoiHDDT();
            themmoi.quanly = this;
            themmoi.Show();
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string maSV = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            int kq = ado.DeleteHuongDan(maSV);
            if (kq > 0)
            {
                MessageBox.Show("Xóa hướng dẫn đề tài thành công");
                LoadGirdView();
            }
            else
            {
                MessageBox.Show("Xóa thất bại !");
            }
        }

        private void nhậpKQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhapKetQuaDeTai frmNKQ = new NhapKetQuaDeTai();
            frmNKQ.quanly = this;
            frmNKQ.Madt = dataGridView1.CurrentRow.Cells[5].Value.ToString() + " - " + dataGridView1.CurrentRow.Cells[6].Value.ToString();
            frmNKQ.Sinhvien = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " - " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frmNKQ.Giangvien =  dataGridView1.CurrentRow.Cells[3].Value.ToString() + " - " + dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frmNKQ.Show();
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
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
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
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

        private void xuấtExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ado.ToExcel(dataGridView1, saveFileDialog1.FileName);
            }
        }
    }
}
