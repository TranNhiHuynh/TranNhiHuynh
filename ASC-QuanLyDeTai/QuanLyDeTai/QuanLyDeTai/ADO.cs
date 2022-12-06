using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace QuanLyDeTai
{
    internal class ADO
    {
        SqlConnection conn = new SqlConnection("Data Source =DESKTOP-1UU6TIV; Initial Catalog = ThucTap; User ID =sa; Password =123");

        public DataSet LoadDanhSachDeTai()
        {
            DataSet dataSet = new DataSet();
            string querySQL = "SELECT * FROM TBLDeTai";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);   
            dataAdapter.Fill(dataSet, "TBLDeTai");
            return dataSet;
        }

        public DataSet TimKiemDeTai(string P_DeTai, string P_NoiThucTap, string P_KinhPhiMin, string P_KinhPhiMax)
        {

            string querySQL = "SELECT * FROM TBLDeTai WHERE ";
            if (P_DeTai == "")
            {
                querySQL += " Tendt is not null ";
            }
            else
            {
                querySQL += " Tendt = '" + P_DeTai + "' ";
            }

            if (P_NoiThucTap == "")
            {
                querySQL += " and Noithuctap is not null ";
            }
            else
            {
                querySQL += " and Noithuctap = '" + P_NoiThucTap + "' ";
            }

            if (P_KinhPhiMin == "" && P_KinhPhiMax == "")
            {
                querySQL += " and Kinhphi is not null ";
            }

            if (P_KinhPhiMin == "" && P_KinhPhiMax != "")
            {
                querySQL += " and Kinhphi <= " + int.Parse(P_KinhPhiMax) + " ";
            }

            if (P_KinhPhiMin != "" && P_KinhPhiMax == "")
            {
                querySQL += " and Kinhphi >= " + int.Parse(P_KinhPhiMin) + " ";
            }

            if (P_KinhPhiMin != "" && P_KinhPhiMax != "")
            {
                querySQL += " and Kinhphi >= " + int.Parse(P_KinhPhiMin) + " and Kinhphi <= " + int.Parse(P_KinhPhiMax);
            }
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(querySQL, conn);
            dataAdapter.Fill(dataSet, "TBLDeTai");
            return dataSet;
        }
        public int InsertDeTai(string madt, string tendt, string kinhphi, string noithuctap)
        {
            conn.Open();
            string querySQL = "INSERT INTO TBLDeTai VALUES ('" + madt + "','" + tendt + "'," + int.Parse(kinhphi) + ",'" + noithuctap + "')";

            SqlCommand cmd = new SqlCommand(querySQL,conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public int KiemTraK9DeTai(string P_Madt)
        {
            conn.Open();
            string querySQL = "Select count(*) from TBLDeTai Where Madt = '" + P_Madt + "'";

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = (int)cmd.ExecuteScalar();
            conn.Close();
            return kq;
        }
        public int UpdateDeTai(string madt, string tendt, string kinhphi, string noithuctap)
        {
            conn.Open();
            string querySQL = "UPDATE TBLDeTai "
                              +" SET Tendt = '"+tendt+"', Kinhphi = '"+int.Parse(kinhphi)+"', Noithuctap = '"+noithuctap +"'"
                             + " WHERE Madt = '"+madt+"'";

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public int KiemTraKhoaNgoaiDeTai(string P_Madt)
        {
            conn.Open();
            string querySQL = "Select count(*) from TBLHuongDan Where Madt = '" + P_Madt + "'";

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = (int)cmd.ExecuteScalar();
            conn.Close();
            return kq;
        }
        public int DeleteDeTai(string madt)
        {
            conn.Open();
            string querySQL = "DELETE FROM TBLDeTai "
                             + " WHERE Madt = '" + madt + "'";

            SqlCommand cmd = new SqlCommand(querySQL, conn);

            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public void ToExcel(DataGridView dataGridView1, string fileName)
        {
            //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Quản lý Đề tài";

                // export header trong DataGridView
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
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
