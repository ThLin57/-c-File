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
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; }
        public List<string> MonHocDK { get; set; }

        public SinhVien()
        {
            MonHocDK = new List<string>();
        }

        public SinhVien(string mSSV, string hoTenLot, string ten, DateTime ngaySinh, string lop, string soCMND, string sDT, string diaChi, bool gioiTinh, List<string> monHocDK)
        {
            this.MSSV = mSSV;
            this.HoTenLot = hoTenLot;
            this.Ten = ten;
            this.NgaySinh = ngaySinh;
            this.SoCMND = soCMND;
            this.SDT = sDT;
            this.DiaChi = diaChi;
            this.GioiTinh = gioiTinh;
            this.MonHocDK = monHocDK;
        }
    }

}
