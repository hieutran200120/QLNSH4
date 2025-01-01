namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HoatDong_HoiVien
    {
        [Key]
        public int MaHD_HV { get; set; }

        public int MaHoatDong { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public int? SoTien { get; set; }

        public bool? XacNhan { get; set; }

        public virtual HoatDong HoatDong { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
