namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapBac")]
    public partial class CapBac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CapBac()
        {
            LichSuCapBacs = new HashSet<LichSuCapBac>();
        }

        [Key]
        public int MaCB { get; set; }

        [Required]
        [StringLength(25)]
        public string TenCB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuCapBac> LichSuCapBacs { get; set; }
    }
}
