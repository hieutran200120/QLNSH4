using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class HoiVienView
    {
        public int STT { get; set; }
        public string MaHV { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DanToc { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public int? BacTho { get; set; }
        public int? NamNhapNgu { get; set; }
        public DateTime? NgayVaoDang { get; set; }
        public DateTime? NgayChinhThuc { get; set; }
        
        public string QueQuan { get; set; }
        public string avatar { get; set; }
        public string ChiHoiPN { get; set; }
        public string NoiOHienNay { get; set; }
        public string LoaiHoiVien { get; set; }
        public string LoaiDangVien { get; set; }
        public int ViTri { get; set; }

        public string TenHV { get; set; }
        public int MaCB { get; set; }
        public string CapBac { get; set; }
        public string TrinhDo { get; set; }
        public string DonVi { get; set; }
        public string DanhHieu { get; set; }
        public string ChucDanh { get; set; }
    }
}