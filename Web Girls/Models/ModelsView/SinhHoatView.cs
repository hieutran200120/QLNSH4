using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class SinhHoatView
    {
        public int Ma { get; set; }
        public string TenSinhHoat { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgaySinhHoat { get; set; }
        public int? SoLuongVang  { get; set; }
        public int MaLoaiSinhHoat { get; set; }
        public int? MaHoi { get; set; }
        public string DVToChuc { get; set; }
        public string LoaiSinhHoat { get; set; }
       
    }
}