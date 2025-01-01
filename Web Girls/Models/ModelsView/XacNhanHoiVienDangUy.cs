using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class XacNhanHoiVienDangUy
    {
        public int Ma { get; set; }
        public string Hoten { get; set; }
        public string ChucVuCu { get; set; }
        public string DangUyCu { get; set; }
        public string ChucVuMoi { get; set; }
        public string DangUyMoi { get; set; }
        public DateTime? NgayQD { get; set; }
    }
}