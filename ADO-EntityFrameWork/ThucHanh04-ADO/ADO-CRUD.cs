using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ThucHanh04_ADO
{
    internal class ADO_CRUD
    {
        SqlConnection conn = new SqlConnection("Data Source =DESKTOP-M1OAOMB\\MAIN; Initial Catalog = ThucTap; User ID =sa; Password =123");
        public int DemSoSVThucTap()
        {
            conn.Open();
            string querySQL = "SELECT COUNT(*) FROM TBLHuongDan";
            SqlCommand cmd = new SqlCommand(querySQL, conn);
            int SoLuongSVThucTap = (int)cmd.ExecuteScalar();
            conn.Close();
            return SoLuongSVThucTap;
        }

        public void InDanhSachSinhVien()
        {
            conn.Open();
            string querySQL = "SELECT Hotensv FROM TBLSinhVien";
            SqlCommand cmd = new SqlCommand(querySQL, conn);

            SqlDataReader ListSinhVien = cmd.ExecuteReader();
            Console.WriteLine("Danh sach sinh vien \n =============================");
            int i = 1;
            while (ListSinhVien.Read())
            {

                Console.WriteLine("   " + i + ". " + ListSinhVien["Hotensv"].ToString());
                i++;
            }
            conn.Close();
        }


        public int LayMaSinhVienCuoiCung()
        {
            conn.Open();
            string querySQL = "SELECT Masv FROM TBLSinhVien";
            SqlCommand cmd = new SqlCommand(querySQL, conn);
            SqlDataReader ListDemSV = cmd.ExecuteReader();
            int lastIndex = 1;
            while (ListDemSV.Read())
            {
                lastIndex = int.Parse(ListDemSV["Masv"].ToString());
            }
            conn.Close();
            return lastIndex;
        }

        public int ThemSinhVien(string hotenSV, string maKhoa, int namSinh, string queQuan)
        {           
            int maSv = LayMaSinhVienCuoiCung() + 1;
            conn.Open();
            string querySQL = "INSERT INTO TBLSinhVien VALUES ("+maSv +",N'"+hotenSV+"','"+maKhoa+"',"+namSinh+",'"+queQuan+"')";
            SqlCommand cmd = new SqlCommand(querySQL, conn);          
            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public int CapNhatSinhVien(string hotenSV, int updateNamSinh, string updateQueQuan)
        {
            conn.Open();
            string querySQL = "UPDATE TBLSinhVien " +
                              " SET Namsinh = "+updateNamSinh+", Quequan = '"+updateQueQuan+"'" +
                              " WHERE Hotensv = '"+hotenSV+"'";
            SqlCommand cmd = new SqlCommand(querySQL, conn);           
            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public bool KiemTraKhoaNgoaiDeTai(string maDT)
        {
            conn.Open();
            string querySQL = "SELECT Count(*) FROM TBLHuongdan WHERE Madt = '"+maDT+"'" ;
            SqlCommand cmd = new SqlCommand(querySQL, conn);
            int check = (int)cmd.ExecuteScalar();
            if(check > 0)
            {
                conn.Close();
                return false;               
            }
            else
            {
                conn.Close();
                return true;
            }
            
        }

        public int XoaDeTai(string maDT)
        {          
            bool kt = KiemTraKhoaNgoaiDeTai(maDT);
            conn.Open();
            if (kt == false)
            {
                conn.Close();
                return 0;
            }
            else
            {
                string querySQL = "DELETE FROM TBLDeTai " +
                                  " WHERE Madt = '" + maDT + "'";
                SqlCommand cmd = new SqlCommand(querySQL, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }        
        }
    }
}

