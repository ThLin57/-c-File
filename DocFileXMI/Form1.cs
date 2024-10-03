using DocFileJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocFileXMI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        QLSinhVien qlsv;
        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QLSinhVien();

            // Đọc dữ liệu từ file XML
            qlsv.DocFileXml("..\\DSSV.xml"); // Đường dẫn file XML

            // Tải dữ liệu lên ListView
            TaiListView();

        }
        private void ThemSV_LV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MSSV); // MSSV là cột đầu tiên
            string gt = sv.GioiTinh ? "Nam" : "Nữ"; // Hiển thị giới tính
            string mdk = ""; // Chuỗi để hiển thị danh sách môn học đăng ký

            // Thêm các thông tin sinh viên vào các cột tiếp theo
            lvitem.SubItems.Add(sv.HoTenLot); // Họ tên lót
            lvitem.SubItems.Add(sv.Ten);      // Tên
            lvitem.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy")); // Ngày sinh
            lvitem.SubItems.Add(sv.SoCMND);   // Số CMND
            lvitem.SubItems.Add(sv.SDT);      // Số điện thoại
            lvitem.SubItems.Add(sv.DiaChi);   // Địa chỉ
            lvitem.SubItems.Add(gt);          // Giới tính

            // Ghép các môn học đăng ký thành chuỗi
            foreach (string mon in sv.MonHocDK)
            {
                mdk += mon + ", ";
            }

            // Xóa dấu phẩy cuối cùng
            if (mdk.Length > 0)
                mdk = mdk.TrimEnd(',', ' ');

            lvitem.SubItems.Add(mdk); // Môn học đăng ký

            // Thêm dòng mới vào ListView
            this.lstDanhSachSinhVien.Items.Add(lvitem);
        }

        private void TaiListView()
        {
            this.lstDanhSachSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DSSV)
            {
                ThemSV_LV(sv);
            }
        }
        private void lstDanhSachSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
