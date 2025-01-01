namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoanCanhGD")]
    public partial class HoanCanhGD
    {
        [Key]
        public int MaHCGD { get; set; }

        [StringLength(15)]
        public string MaHV { get; set; }

        public int Nam { get; set; }

        public bool? MatChongVo { get; set; }

        public bool? LyHon { get; set; }

        public bool? NuoiConMotMinh { get; set; }

        public bool? HoNgheo { get; set; }

        public bool? HoCanNgheo { get; set; }

        [StringLength(100)]
        public string BenhAnBanThan { get; set; }

        [StringLength(100)]
        public string BenhAnConCai { get; set; }

        public bool? NhaCua { get; set; }

        public bool? VoChong { get; set; }

        public bool? DaCoChong { get; set; }

        public bool? ConBiBenh { get; set; }

        public bool? ConDuoi16Tuoi { get; set; }

        public bool? MacBenhHiemNgheo { get; set; }

        public virtual HocVien HoiVien { get; set; }
    }
}
