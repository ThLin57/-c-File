using DocFileJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoTXT
{
    internal class QLSinhVien
    {
        public List<SinhVien> DanhSach { get; set; }

        public QLSinhVien()
        {
            DanhSach = new List<SinhVien>();
        }
        public void SuaSinhVien(List<SinhVien> dssv, SinhVien suaMoi)
        {
            for (int i = 0; i < dssv.Count; i++)
            {
                if (dssv[i].MSSV == suaMoi.MSSV)
                {
                    dssv[i] = suaMoi;
                    break;
                }
            }
        }
        public void CapNhat(ListViewItem data, string mssv, string ho, DateTime ngaySinh, string soCMND,
           string diaChi, bool gioiTinh, string ten, string sdt, List<String> dsMH)
        {
            if (data != null)
            {
                data.SubItems[0].Text = mssv;
                data.SubItems[1].Text = ho;
                data.SubItems[2].Text = ten;
                data.SubItems[3].Text = ngaySinh.ToString("dd/mm/yyyy");
                data.SubItems[4].Text = soCMND;
                data.SubItems[5].Text = sdt;
                data.SubItems[6].Text = diaChi;
                data.SubItems[7].Text = gioiTinh ? "Nam" : "Nữ";
                data.SubItems[8].Text = string.Join(", ", dsMH);
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void DocFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 9)
                        {
                            // Phân tách danh sách môn học
                            List<string> listMonHoc = new List<string>(data[8].Split(';'));

                            // Xác định giới tính
                            bool gioiTinh = data[5].Trim().ToLower() == "nam";

                            // Chuyển đổi ngày sinh
                            DateTime ngaySinh;
                            if (!DateTime.TryParseExact(data[2], "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                            {
                               // MessageBox.Show($"Ngày sinh không hợp lệ: {data[2]}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                            // Tạo đối tượng SinhVien và thêm vào danh sách
                            SinhVien sv = new SinhVien(
                                data[0], // MSSV
                                data[1], // HoTenLot
                                ngaySinh, // NgaySinh
                                data[3], // SoCMND
                                data[4], // DiaChi
                                gioiTinh, // GioiTinh
                                data[6], // Ten
                                data[7], // SDT
                                listMonHoc // DSMonHoc
                            );

                            DanhSach.Add(sv);
                        }
                        else
                        {
                            MessageBox.Show($"Dòng không hợp lệ: {line}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
