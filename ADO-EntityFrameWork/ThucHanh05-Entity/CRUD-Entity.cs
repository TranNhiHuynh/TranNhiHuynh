using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ThucHanh05_Entity
{
    internal class CRUD_Entity
    {
        QuanLiSinhVien db = new QuanLiSinhVien();

        public List<TBLSinhVien> ListSinhVienThucTap0()
        {
            List<TBLHuongDan> ListHuongDan = db.TBLHuongDans.Where(t => t.KetQua == 0).ToList();
            List<TBLSinhVien> ListSinhVien = db.TBLSinhViens.ToList();
            List<TBLSinhVien> ListSVThucTap0 = new List<TBLSinhVien>();
            foreach(var sv in ListSinhVien)
            {
                foreach(var hd in ListHuongDan)
                {
                    if(sv.Masv == hd.Masv)
                    {
                        ListSVThucTap0.Add(sv);
                    }
                }
            }

            return ListSVThucTap0;
        }
        public int DemSoSVThucTap()
        {
            return db.TBLHuongDans.Count();
        }

        public void InDanhSachSinhVien()
        {
            List<TBLSinhVien> ListSinhVien = db.TBLSinhViens.ToList();
            Console.WriteLine("Danh sach sinh vien \n =============================");
            int i = 1;
            foreach (var sv in ListSinhVien)
            {
                Console.WriteLine("   " + i + ". " + sv.Hotensv);
                i++;
            }
        }
        public int ThemSinhVien(string hotenSV, string maKhoa, int namSinh, string queQuan)
        {
            TBLSinhVien SinhVien = new TBLSinhVien();
            List<TBLSinhVien> ListSinhVien = db.TBLSinhViens.ToList();
            int lastIndex = ListSinhVien[ListSinhVien.Count() - 1].Masv; // Lấy Masv cuối danh sách
            SinhVien.Masv = lastIndex + 1;
            SinhVien.Hotensv = hotenSV;
            SinhVien.Makhoa = maKhoa;
            SinhVien.Namsinh = namSinh;
            SinhVien.Quequan = queQuan;

            db.TBLSinhViens.Add(SinhVien);
            int kq = db.SaveChanges();
            return kq;
        }

        public int CapNhatSinhVien(string hotenSV, int updateNamSinh, string updateQueQuan)
        {
            List<TBLSinhVien> listSVUpdate = db.TBLSinhViens.Where(t => t.Hotensv == hotenSV).ToList();
            foreach (var sv in listSVUpdate)
            {
                sv.Namsinh = updateNamSinh;
                sv.Quequan = updateQueQuan;
            }
            int kq = db.SaveChanges();
            return kq;
        }

        public bool KiemTraKhoaNgoaiDeTai(string maDT)
        {
            TBLHuongDan hd = db.TBLHuongDans.FirstOrDefault(t => t.Madt == maDT);
            if (hd != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int XoaDeTai(string maDT)
        {
            if(KiemTraKhoaNgoaiDeTai(maDT) == false)
            {
                return 0;
            }
            TBLDeTai DeTaiDelete = db.TBLDeTais.Where(t => t.Madt == "Dt04").FirstOrDefault();
            if (DeTaiDelete != null)
            {
                db.TBLDeTais.Remove(DeTaiDelete);
                db.SaveChanges();
                return 1;
            }
            return 0;         
        }

        public void DemSoLuongSVMoiDeTai()
        {
            List<TBLDeTai> ListDeTai = db.TBLDeTais.ToList();
            List<TBLHuongDan> ListHuongDan = db.TBLHuongDans.ToList();
            Console.WriteLine("==== So luong sinh vien moi de tai ====");
            foreach (var dt in ListDeTai)
            {
                int dem = 0;
                foreach(var hd in ListHuongDan)
                {
                    if (dt.Madt == hd.Madt)
                        dem++;
                }
                Console.WriteLine("--De tai: " + dt.Madt + " co " + dem + " sinh vien");
            }
        }
    }
}
