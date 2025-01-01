using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class HoiVienVangView
    {
        public string MaHV { get; set; }
        public int MaSH_V { get; set; }
        public int MaSinhHoat { get; set; }
        public string TenHV { get; set; }
        public string SDT { get; set; }
        public int SoLuongVang { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}