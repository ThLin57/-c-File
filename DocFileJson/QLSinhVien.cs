using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace DocFileJson
{
    internal class QLSinhVien
    {
        public List<SinhVien> DSSV { get; set; } = new List<SinhVien>();

        public void ThemSV(SinhVien sv) 
        { 
            
            this.DSSV.Add(sv);
        }

        public QLSinhVien()
        {
            DSSV = new List<SinhVien>();
        }
        public SinhVien this[int index]
        {
            get { return DSSV[index]; }
            set { DSSV[index] = value; }
        }
        public void DocFileJson(string tenFile)
        {
            try
            {
                using (StreamReader reader = new StreamReader(tenFile))
                {
                    string json = reader.ReadToEnd();
                    // Deserialize thành Dictionary với các thuộc tính khác nhau
                    var jsonData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

                    // Lấy danh sách sinh viên từ trường "sinhvien"
                    var ds = jsonData["sinhvien"].ToObject<List<SinhVien>>();

                    if (ds != null)
                    {
                        foreach (var sv in ds)
                        {
                            ThemSV(sv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file JSON: " + ex.Message);
            }
        }



    }

}
