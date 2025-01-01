namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThamGia")]
    public partial class ThamGia
    {
        [Key]
        public int Ma { get; set; }

        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(100)]
        public string DonVi { get; set; }

        public int? MaLDST { get; set; }

        [StringLength(50)]
        public string VaiTro { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public virtual HocVien HoiVien { get; set; }

        public virtual LaoDongSangTao LaoDongSangTao { get; set; }
    }
}
