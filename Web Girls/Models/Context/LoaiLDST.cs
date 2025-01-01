namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiLDST")]
    public partial class LoaiLDST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiLDST()
        {
            LaoDongSangTaos = new HashSet<LaoDongSangTao>();
        }

        [Key]
        public int MaLoaiLDST { get; set; }

        [StringLength(50)]
        public string TenLoaiLDST { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LaoDongSangTao> LaoDongSangTaos { get; set; }
    }
}
