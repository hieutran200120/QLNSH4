namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            CapLaiMatKhaus = new HashSet<CapLaiMatKhau>();
        }

        [Key]
        [StringLength(25)]
        public string TenDN { get; set; }

        [Required]  
        public string MatKhau { get; set; }

        public int Quyen { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public DateTime EditTime { get; set; }

        public bool Khoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapLaiMatKhau> CapLaiMatKhaus { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
