namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LaoDongSangTao")]
    public partial class LaoDongSangTao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LaoDongSangTao()
        {
            ThamGias = new HashSet<ThamGia>();
        }

        [Key]
        public int MaLDST { get; set; }

        [StringLength(200)]
        public string TenDeTai { get; set; }

        public int? MaLoaiLDST { get; set; }

        [StringLength(100)]
        public string KetQua { get; set; }

        public int? Nam { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public bool? XacNhan { get; set; }

        public virtual HocVien HoiVien { get; set; }

        public virtual LoaiLDST LoaiLDST { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGia> ThamGias { get; set; }
    }
}
