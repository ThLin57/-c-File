using DocFileJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DocFileXMI
{
    internal class QLSinhVien
    {
        public List<SinhVien> DSSV { get; set; } = new List<SinhVien>();

        public void ThemSV(SinhVien sv)
        {
            this.DSSV.Add(sv);
        }

        public void DocFileXml(string tenFile)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(tenFile); // Load file XML

            XmlNodeList nodeList = xmlDoc.SelectNodes("/root/sinhvien"); // Lấy danh sách sinh viên

            foreach (XmlNode node in nodeList)
            {
                string mssv = node["mssv"].InnerText;
                string hoTenLot = node["hotenlot"].InnerText;
                string ten = node["ten"].InnerText;
                DateTime ngaySinh = DateTime.Parse(node["ngaysinh"].InnerText);
                string lop = node["lop"].InnerText;
                string soCMND = node["socmnd"].InnerText;
                string sdt = node["sdt"].InnerText;
                string diaChi = node["diachi"].InnerText;
                bool gioiTinh = bool.Parse(node["gioitinh"].InnerText);

                    List<string> monHocDK = new List<string>();
                    XmlNodeList monHocList = node.SelectNodes("monhocdk/item");
                    foreach (XmlNode monHoc in monHocList)
                    {
                        monHocDK.Add(monHoc.InnerText);
                    }


                    // Tạo đối tượng SinhVien và thêm vào danh sách
                    SinhVien sv = new SinhVien(mssv, hoTenLot, ten, ngaySinh, lop, soCMND, sdt, diaChi, gioiTinh, monHocDK);
                ThemSV(sv);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi đọc file XML: " + ex.Message);
        }
    }

}
}
