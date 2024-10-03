using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFileJson
{
    internal class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
         public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCMND { get; set; }
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; } // true: Nam, false: Nữ
   
        public string SDT { get; set; }
        public List<string> DSMonHoc { get; set; }

        public SinhVien(string mssv, string hoTenLot, DateTime ngaySinh, string soCMND, string diaChi, bool gioiTinh, string ten, string sdt, List<string> dsMonHoc)
        {
            MSSV = mssv;
            HoTenLot = hoTenLot;
            NgaySinh = ngaySinh;
            SoCMND = soCMND;
            DiaChi = diaChi;
            GioiTinh = gioiTinh;
            Ten = ten;
            SDT = sdt;
            DSMonHoc = dsMonHoc;
        }
    }

}
