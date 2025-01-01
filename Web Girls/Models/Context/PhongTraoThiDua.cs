namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongTraoThiDua")]
    public partial class PhongTraoThiDua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhongTraoThiDua()
        {
            DanhSachBieuDuongPTTDs = new HashSet<DanhSachBieuDuongPTTD>();
        }

        [Key]
        public int Ma { get; set; }
        public int? SoLuongDeCu { get; set; }

        public string TenPhongTraoThiDua { get; set; }
        public bool? Khoa { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhSachBieuDuongPTTD> DanhSachBieuDuongPTTDs { get; set; }
    }
}
