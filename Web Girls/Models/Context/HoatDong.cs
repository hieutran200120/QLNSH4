namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoatDong")]
    public partial class HoatDong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoatDong()
        {
            HoatDong_HoiVien = new HashSet<HoatDong_HoiVien>();
        }

        [Key]
        public int MaHoatDong { get; set; }

        public int? MaLoaiHoatDong { get; set; }

        public int? TongTienThuDuoc { get; set; }

        public string TenHoatDong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGian { get; set; }

        //public int? MaHPN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoatDong_HoiVien> HoatDong_HoiVien { get; set; }

        public virtual LoaiHoatDong LoaiHoatDong { get; set; }

        //public virtual HoiPhuNu HoiPhuNu { get; set; }
    }
}
