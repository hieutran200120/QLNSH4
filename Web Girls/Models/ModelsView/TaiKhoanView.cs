using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class TaiKhoanView
    {
        public string TenDN { get; set; }

        public string TenHV { get; set; }

        public string MatKhau { get; set; }

        public int Quyen { get; set; }

        public string MaHV { get; set; }

        public DateTime EditTime { get; set; }

        public bool Khoa { get; set; }
    }
}