namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuTrinhDo")]
    public partial class LichSuTrinhDo
    {
        [Key]
        public int MaLSTD { get; set; }

        public int? MaTD { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        [StringLength(100)]
        public string NganhHoc { get; set; }

        [StringLength(100)]
        public string TruongCap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayQD { get; set; }

        public bool? XacNhan { get; set; }

        public virtual HocVien HoiVien { get; set; }

        public virtual TrinhDo TrinhDo { get; set; }
    }
}
