namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToChucDoanThe")]
    public partial class ToChucDoanThe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ToChucDoanThe()
        {
            HoiViens = new HashSet<HocVien>();
        }

        [Key]
        public int MaVTDT { get; set; }

        [Required]
        [StringLength(25)]
        public string TenVTDT { get; set; }

        public int? NamBatDau { get; set; }

        public int? NamKetThuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocVien> HoiViens { get; set; }
    }
}
