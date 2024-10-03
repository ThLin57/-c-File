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

namespace DemoTXT
{
    public partial class Form1 : Form
    {
        List<SinhVien> dssv = new List<SinhVien>();
        public Form1()
        {
            InitializeComponent();
        }
        QLSinhVien qlsv;
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
            foreach (string mon in sv.DSMonHoc)
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
        private void ThemSV()
        {
            string mssv = txtMSSV.Text;
            string hoLot = txtHo.Text;
             string ten = txtTen.Text;
            string ngaySinh = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string soCMND = txCMND.Text;
            string sdt = txtSDT.Text;
            string diaChi = txtDiaChi.Text;
            string gioiTinh = radioButton1.Checked ? "Nam" : "Nữ";
         
           
            List<string> ds = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                ds.Add(item.ToString());
            }

            string monHoc = string.Join(", ", ds);

            // Check for duplicate MSSV
            foreach (ListViewItem item in lstDanhSachSinhVien.Items)
            {
                if (item.SubItems[0].Text == mssv)
                {
                    MessageBox.Show("MSSV đã tồn tại. Vui lòng nhập MSSV khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ListViewItem listView = new ListViewItem(mssv);
            listView.SubItems.Add(hoLot);
            listView.SubItems.Add(ten);
            listView.SubItems.Add(ngaySinh);
            listView.SubItems.Add(soCMND);
 listView.SubItems.Add(sdt);
            listView.SubItems.Add(diaChi);
            listView.SubItems.Add(gioiTinh);
           
           
            listView.SubItems.Add(monHoc);

            lstDanhSachSinhVien.Items.Add(listView);
        }

        private void TaiListView()
        {
            this.lstDanhSachSinhVien.Items.Clear();
            foreach (SinhVien sv in qlsv.DanhSach)
            {
                ThemSV_LV(sv);
            }
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QLSinhVien();
            qlsv.DocFile("..\\DSSV2.txt");
            TaiListView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemSV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (lstDanhSachSinhVien.SelectedItems.Count > 0)
            {
                // Lấy dòng đang được chọn trong ListView
                ListViewItem selectedItem = lstDanhSachSinhVien.SelectedItems[0];

                // Lấy thông tin cập nhật từ các control trên form
                string mssv = txtMSSV.Text;
                string hoLot = txtHo.Text;
                string ten = txtTen.Text;
                DateTime ngaySinh = dateTimePicker1.Value;
                string soCMND = txCMND.Text;
                string sdt = txtSDT.Text;
                string diaChi = txtDiaChi.Text;
                bool gioiTinh = radioButton1.Checked; // true nếu là Nam, false nếu là Nữ
                List<string> dsMonHoc = new List<string>();

                // Lấy danh sách môn học đã được đánh dấu
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    dsMonHoc.Add(item.ToString());
                }

                // Tạo đối tượng SinhVien mới với thông tin cập nhật
                SinhVien suaMoi = new SinhVien(mssv, hoLot, ngaySinh, soCMND, diaChi, gioiTinh, ten, sdt, dsMonHoc);
                qlsv.CapNhat(selectedItem, mssv, hoLot, ngaySinh, soCMND, diaChi, gioiTinh, ten, sdt, dsMonHoc);
                // Cập nhật đối tượng SinhVien trong danh sách dssv
                qlsv.SuaSinhVien(dssv, suaMoi);

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstDanhSachSinhVien_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lstDanhSachSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem sti = lstDanhSachSinhVien.SelectedItems[0];
                txtMSSV.Text = sti.SubItems[0].Text;
                txtHo.Text = sti.SubItems[1].Text;

                DateTime ngaySinh;
                if (DateTime.TryParseExact(sti.SubItems[2].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                {
                    dateTimePicker1.Value = ngaySinh;
                }
                txCMND.Text = sti.SubItems[3].Text;
                txtDiaChi.Text = sti.SubItems[4].Text;
                string gioiTinh = sti.SubItems[5].Text;
                if (gioiTinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                txtTen.Text = sti.SubItems[6].Text;
                txtSDT.Text = sti.SubItems[7].Text;
                string[] MH = sti.SubItems[8].Text.Split(new[] { ", " }, StringSplitOptions.None);
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, MH.Contains(checkedListBox1.Items[i].ToString()));
                }
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    string monHoc = checkedListBox1.Items[i].ToString().Trim();
                    checkedListBox1.SetItemChecked(i, MH.Any(m => m.Trim().Equals(monHoc, StringComparison.OrdinalIgnoreCase)));
                }

            }
        }
    }
}
