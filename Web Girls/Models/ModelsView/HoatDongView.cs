using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class HoatDongView
    {
        public int Ma { get; set; }
        public string TenHoatDong { get; set; }
        public DateTime? ThoiGian { get; set; }
        public int? SoTien  { get; set; }
        public int MaLoaiHoatDong { get; set; }
        public int? MaHoi { get; set; }
        public string DVToChuc { get; set; }
        public string LoaiHoatDong { get; set; }
       
    }
}