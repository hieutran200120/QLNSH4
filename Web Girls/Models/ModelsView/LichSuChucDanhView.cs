using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class LichSuChucDanhView
    {
        public int Ma { get; set; }
        public string TenChucDanh { get; set; }
        public string TenDonVi { get; set; }
        public bool? XacNhan { get; set; }
        public DateTime? NgayQD { get; set; }
    }
}