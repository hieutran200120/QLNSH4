namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapLaiMatKhau")]
    public partial class CapLaiMatKhau
    {
        [Key]
        public int MaCLMK { get; set; }

        [StringLength(25)]
        public string TenDN { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public int Code { get; set; }

        public bool TinhTrang { get; set; }

        public DateTime EditTime { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
