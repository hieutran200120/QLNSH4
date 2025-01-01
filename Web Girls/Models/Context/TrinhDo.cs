namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrinhDo")]
    public partial class TrinhDo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrinhDo()
        {
            LichSuTrinhDoes = new HashSet<LichSuTrinhDo>();
        }

        [Key]
        public int MaTD { get; set; }

        [Required]
        [StringLength(25)]
        public string TenTD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuTrinhDo> LichSuTrinhDoes { get; set; }
    }
}
