using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyHuongDanDeTai
{
    internal class ADO
    {
        SqlConnection conn = new SqlConnection("Data Source =DESKTOP-1UU6TIV; Initial Catalog = ThucTap; User ID =sa; Password =123");
        
        public DataSet LoadDanhSachHuongDanDeTai()
        { 
            string querySQL = "Select kh.Makhoa, sv.Masv, sv.Hotensv, gv.Magv, gv.Hotengv, hd.Madt, dt.Tendt, hd.KetQua\r\nfrom (((TBLHuongDan hd INNER JOIN TBLSinhVien sv on hd.Masv = sv.Masv) INNER JOIN TBLKhoa kh on sv.Makhoa = kh.Makhoa)\r\nINNER JOIN TBLGiangVien gv on gv.Magv = hd.Magv) INNER JOIN TBLDeTai dt on hd.Madt = dt.Madt";
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLHuongDanDeTai");
            return dataSet;
        }

        public DataSet LoadDanhSachDeTai()
        {
            string querySQL = "Select * FROM TBLDeTai";
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLDeTai");
            return dataSet;
        }

        public DataSet LoadDanhSachSinhVien()
        {
            string querySQL = "Select * FROM TBLSinhVien";
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLSinhVien");
            return dataSet;
        }
        public DataSet LoadDanhSachGiangVien()
        {
            string querySQL = "Select * FROM TBLGiangVien";
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLGiangVien");
            return dataSet;
        }

        public DataSet TimKiemDeTai(string P_TenDeTai, string P_TenSV, string P_TenGV, string P_TrangThai)
        {

            string querySQL = "Select kh.Makhoa, sv.Masv, sv.Hotensv, gv.Magv, gv.Hotengv, hd.Madt, dt.Tendt, hd.KetQua\r\nfrom (((TBLHuongDan hd INNER JOIN TBLSinhVien sv on hd.Masv = sv.Masv) INNER JOIN TBLKhoa kh on sv.Makhoa = kh.Makhoa)\r\nINNER JOIN TBLGiangVien gv on gv.Magv = hd.Magv) INNER JOIN TBLDeTai dt on hd.Madt = dt.Madt "
                + "WHERE ";

            if (P_TenDeTai == "")
            {
                querySQL += " dt.Tendt is not null ";
            }
            else
            {
                querySQL += " dt.Tendt = '" + P_TenDeTai + "' ";
            }

            if (P_TenGV == "")
            {
                querySQL += " and gv.Hotengv is not null ";
            }
            else
            {
                querySQL += " and gv.Hotengv = '" + P_TenGV + "' ";
            }

            if (P_TenSV == "")
            {
                querySQL += " and sv.Hotensv is not null ";
            }
            else
            {
                querySQL += " and sv.Hotensv = '" + P_TenSV + "' ";
            }

            if (P_TrangThai == "Tất cả")
            {
                querySQL += "";
            }

            if (P_TrangThai == "Đã có điểm")
            {
                querySQL += " and hd.KetQua is not null";
            }

            if (P_TrangThai == "Chưa có điểm")
            {
                querySQL += " and hd.KetQua is null";
            }

            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLHuongDanDeTai");
            return dataSet;
        }
        public int InsertHuongDan(string P_Masv, string P_Madt, string P_Magv)
        {
            conn.Open();
            string querySQL = "Insert Into TBLHuongDan Values ( " + int.Parse(P_Masv) + ",'" + P_Madt + "'," + int.Parse(P_Magv) + ", null )";
            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public int KiemTraK9HuongDan(string P_Masv)
        {
            conn.Open();
            string querySQL = "Select count(*) from TBLHuongDan Where Masv = '"+P_Masv+"'";

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = (int)cmd.ExecuteScalar();
            conn.Close();
            return kq;
        }
        public int UpdateKetQua(string P_Masv, string P_Ketqua)
        {
            conn.Open();
            string querySQL = "UPDATE TBLHuongDan "
                              + " SET KetQua = " + P_Ketqua
                             + " WHERE Masv = " +P_Masv;

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public int DeleteHuongDan(string P_Masv)
        {
            conn.Open();
            string querySQL = "DELETE FROM TBLHuongDan "
                             + " WHERE Masv = " + P_Masv ;

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public void ToExcel(DataGridView dataGridView1, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet.Name = "Quản lý Đề tài";

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

                workbook.SaveAs(fileName);

                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
    }
}
