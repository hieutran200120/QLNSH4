namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhSachBieuDuongPTTD")]
    public partial class DanhSachBieuDuongPTTD
    {
        [Key]
        public int Ma { get; set; }

        [Required]
        [StringLength(15)]
        public string MaHV { get; set; }

        public int MaPTTD { get; set; }
        public bool? XacNhan { get; set; }

        public virtual PhongTraoThiDua PhongTraoThiDua { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
