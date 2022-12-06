using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.SymbolStore;

namespace ThucHanh04_ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADO_CRUD tt = new ADO_CRUD();
            //1. Đếm số lượng sinh viên thực thập

            if(tt.DemSoSVThucTap() > 0)
            {
                Console.WriteLine("So luong sinh vien thuc tap: " + tt.DemSoSVThucTap());
            }
            else
            {
                Console.WriteLine("Khong co sinh vien thuc tap ! ");
            }

            //2. In ra màn hình danh sách HoTen sinh viên

            tt.InDanhSachSinhVien();

            //3. Thêm một sinh viên tên: Trần Nam Dương/ Geo/ 1995/ Ho Chi Minh

             int insert = tt.ThemSinhVien("Trần Nam Duong1555353", "Geo", 1995, "Ho Chi Minh");
             if (insert > 0)
                 {
                   Console.WriteLine("Them thanh cong !");
                 }
             else
                 {
                    Console.WriteLine("Them that bai");
                 }

            //4. Cập nhật sinh viên Le Thi Van năm sinh 2018, Quê quán Ha nam

            int update = tt.CapNhatSinhVien("Le Thi Van", 2018, "Ha Nam");
            if (update > 0)
            {
                Console.WriteLine("Cap nhat thanh cong !");
            }
            else
            {
                Console.WriteLine("Cap nhat that bai");
            }

            //5. Xóa đề tài Dt04

            int delete = tt.XoaDeTai("Dt04");
            if(delete > 0)
            {
                Console.WriteLine("Xoa thanh cong");
            }
            else
            {
                Console.WriteLine("Xoa that bai");
            }


            Console.ReadKey();
        }
    }
}
