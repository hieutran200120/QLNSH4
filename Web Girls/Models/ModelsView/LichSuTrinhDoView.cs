using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class LichSuTrinhDoView
    {
        public int Ma { get; set; }
        public string TenTrinhDo { get; set; }
        public string NganhHoc { get; set; }
        public string TrươngCap { get; set; }
        public bool? XacNhan { get; set; }
        public DateTime? NgayQD { get; set; }

    }
}