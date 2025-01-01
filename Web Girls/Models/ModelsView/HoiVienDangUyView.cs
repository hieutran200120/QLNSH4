using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Girls.Models.ModelsView
{
    public class HoiVienDangUyView
    {
        public int Ma { get; set; }
        public string Ten { get; set; }
        public string TenDangUy { get; set; }
        public string ViTri { get; set; }
        public DateTime? BatDau { get; set; }
        public DateTime? KetThuc { get; set; }
    }
}