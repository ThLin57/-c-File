using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocFileJson
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
    
        }
        QLSinhVien qlsv;
        private void ThemSV_LV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MSSV);
            string gt = "Nữ", mdk = "";

            lvitem.SubItems.Add(sv.HoTenLot);
            lvitem.SubItems.Add(sv.Ten);
            lvitem.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
           // lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SoCMND);
            lvitem.SubItems.Add(sv.SDT);
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.GioiTinh ? "Nam" : "Nữ"); // Sửa đổi để sử dụng điều kiện

            foreach (string mon in sv.MonHocDK)
            {
                mdk += mon + ", ";
            }

            if (mdk.Length > 0)
                mdk = mdk.TrimEnd(',', ' '); // Xóa dấu phẩy ở cuối

            lvitem.SubItems.Add(mdk);

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
    
        private void Form1_Load(object sender, EventArgs e)
        {
            qlsv = new QLSinhVien();
            //dssv.DocFile_TXT("du-lieu\\DSSinhVien.txt");
            qlsv.DocFileJson("..\\data.json");
            TaiListView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mssvTimKiem = txtMSSV.Text.Trim();

            // Kiểm tra nếu MSSV trống
            if (string.IsNullOrEmpty(mssvTimKiem))
            {
                MessageBox.Show("Vui lòng nhập MSSV để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa danh sách hiện tại trong ListView
            lstDanhSachSinhVien.Items.Clear();

            // Lọc danh sách sinh viên theo MSSV
            foreach (SinhVien sv in qlsv.DSSV)
            {
                if (sv.MSSV.Contains(mssvTimKiem))
                {
                    // Thêm sinh viên vào ListView nếu MSSV phù hợp
                    ThemSV_LV(sv);
                }
            }

            // Thông báo nếu không tìm thấy sinh viên nào
            if (lstDanhSachSinhVien.Items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sinh viên với MSSV đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

