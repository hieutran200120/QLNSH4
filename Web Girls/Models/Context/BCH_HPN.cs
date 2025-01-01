namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BCH_HPN
    {
        [Key]
        public int MaBCH { get; set; }

        public int? MaHPN { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public int ViTri { get; set; }

        public int NhiemKy { get; set; }

        public int? NamBatDau { get; set; }

        public int? NamKetThuc { get; set; }

        //public virtual HoiPhuNu HoiPhuNu { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
