namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChucDanh")]
    public partial class ChucDanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChucDanh()
        {
            LichSuChucDanhs = new HashSet<LichSuChucDanh>();
        }

        [Key]
        public int MaCD { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuChucDanh> LichSuChucDanhs { get; set; }
    }
}
