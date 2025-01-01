using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class XacNhanTrinhDoView
    {
        public int Ma { get; set; }
        public string TenHV { get; set; }
        public string TrinhDoCu { get; set; }
        public string ChuyenNganhCu { get; set; }
        public string TrinhDoMoi { get; set; }
        public string ChuyenNganhMoi { get; set; }
        public DateTime? NgayQD { get; set; }
    }
}