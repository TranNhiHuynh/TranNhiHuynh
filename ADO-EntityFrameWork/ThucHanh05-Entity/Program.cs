using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThucHanh05_Entity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuanLiSinhVien db = new QuanLiSinhVien();
            CRUD_Entity tt = new CRUD_Entity();

            //1. Đưa ra danh sách gồm mã số, họ tên các sinh viên có điểm thực tập bằng 0 

            List<TBLSinhVien> kq = tt.ListSinhVienThucTap0();
            Console.WriteLine("====Danh sach sinh vien co diem thuc tap bang 0 ====");
            foreach(var sv in kq)
            {
                Console.WriteLine("--------");
                Console.WriteLine("Ma so SV: " + sv.Masv);
                Console.WriteLine("Ho ten SV: " + sv.Hotensv);
            }


            //2. Đếm số lượng sinh viên thực tập

            Console.WriteLine("So luong sinh vien thuc tap: " +tt.DemSoSVThucTap());

            //3. In ra danh sách họ tên sinh viên

            tt.InDanhSachSinhVien();

            //4. Thêm một sinh viên tên: Ngô Nhật Long/Bio/1993/Vung Tau

            int insert = tt.ThemSinhVien("Ngô Nhật Long", "Bio", 1993, "Vung Tau");
            if(insert > 0)
            {
                Console.WriteLine("Them thanh cong !");
            }
            else
            {
                Console.WriteLine("Them that bai ! ");
            }

            //5. Cập nhật sinh viên 'Tran Khac Trong' năm sinh 2018, Quê quán Ha nam

            int update = tt.CapNhatSinhVien("Tran Khac Trong", 2018, "Ha Nam232323232");
            if (update > 0)
            {
                Console.WriteLine("Cap nhat thanh cong !");
            }
            else
            {
                Console.WriteLine("Cap nhat that bai");
            }


            //6. Xóa đề tài Dt03

            int delete = tt.XoaDeTai("Dt03");
            if (delete > 0)
            {
                Console.WriteLine("Xoa thanh cong !");
            }
            else
            {
                Console.WriteLine("Xoa that bai");
            }

            //7. Đếm số lượng sinh viên của mỗi đề tài

            tt.DemSoLuongSVMoiDeTai();

            Console.ReadKey();
        
    }
    }
}
