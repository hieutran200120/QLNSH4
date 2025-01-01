namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BCH_CD
    {
        [Key]
        public int MaBCH { get; set; }

        public int? MaCD { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public int ViTri { get; set; }

        public int NhiemKy { get; set; }

        public int? NamBatDau { get; set; }

        public int? NamKetThuc { get; set; }

        public virtual CongDoan CongDoan { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
