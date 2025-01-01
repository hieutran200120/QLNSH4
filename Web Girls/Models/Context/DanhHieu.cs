namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhHieu")]
    public partial class DanhHieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhHieu()
        {
            LichSuDanhHieux = new HashSet<LichSuDanhHieu>();
        }

        [Key]
        public int MaDH { get; set; }

        [Required]
        [StringLength(25)]
        public string TenDH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuDanhHieu> LichSuDanhHieux { get; set; }
    }
}
