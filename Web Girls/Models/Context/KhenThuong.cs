namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhenThuong")]
    public partial class KhenThuong
    {
        [Key]
        public int Ma { get; set; }

        [StringLength(50)]
        public string TenKhenThuong { get; set; }

        public string LyDo { get; set; }

        public int? Nam { get; set; }

        public string GhiChu { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public bool? XacNhan { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
