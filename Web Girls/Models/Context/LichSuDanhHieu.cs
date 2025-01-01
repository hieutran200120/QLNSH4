namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuDanhHieu")]
    public partial class LichSuDanhHieu
    {
        [Key]
        public int MaLSDH { get; set; }

        public int? MaDH { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayQD { get; set; }

        public bool? XacNhan { get; set; }

        public virtual DanhHieu DanhHieu { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
