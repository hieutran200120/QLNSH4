using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class XacNhanChucDanhView
    {
        public int Ma { get; set; }
        public string TenHV { get; set; }
        public string ChucDanhCu { get; set; }
        public string DonViCu { get; set; }
        public string DonViMoi { get; set; }
        public string ChucDanhMoi { get; set; }
        public DateTime? NgayQD { get; set; }
    }
}