namespace Web_Girls.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocVien")]
    public partial class HocVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocVien()
        {
            //AnhTieuBieux = new HashSet<AnhTieuBieu>();
            BCH_CD = new HashSet<BCH_CD>();
            //BCH_HPN = new HashSet<BCH_HPN>();
            CapLaiMatKhaus = new HashSet<CapLaiMatKhau>();
            DanhSachBieuDuongPTTDs = new HashSet<DanhSachBieuDuongPTTD>();
            HoanCanhGDs = new HashSet<HoanCanhGD>();
            HoatDong_HoiVien = new HashSet<HoatDong_HoiVien>();
            //SinhHoat_Vang = new HashSet<SinhHoat_Vang>();
            //HoiPhuNus = new HashSet<HoiPhuNu>();
            //HoiVien_ChiBo1 = new HashSet<HoiVien_ChiBo>();
            //HoiVien_DangUy1 = new HashSet<HoiVien_DangUy>();
            KhenThuongs = new HashSet<KhenThuong>();
            KyLuats = new HashSet<KyLuat>();
            LaoDongSangTaos = new HashSet<LaoDongSangTao>();
            LichSuCapBacs = new HashSet<LichSuCapBac>();
            LichSuChucDanhs = new HashSet<LichSuChucDanh>();
            LichSuDanhHieux = new HashSet<LichSuDanhHieu>();
            LichSuTrinhDoes = new HashSet<LichSuTrinhDo>();
            TaiKhoans = new HashSet<TaiKhoan>();
            ThamGias = new HashSet<ThamGia>();
            //TinBais = new HashSet<TinBai>();
        }

        [Key]
        [StringLength(15)]
        public string MaHV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(25)]
        public string DanToc { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        public int? NamNhapNgu { get; set; }

        public short? BacTho { get; set; }

        public int? MaLDV { get; set; }

        public int? MaLHV { get; set; }

        public int? MaDV { get; set; }

        //public int? MaHPN { get; set; }

        public int? MaCD { get; set; }

        //public int? MaVTDU { get; set; }

        //public int? MaVTCB { get; set; }

        public int? MaVTDT { get; set; }

        public DateTime? EditTime { get; set; }

        [StringLength(50)]
        public string Quequan { get; set; }

        [StringLength(50)]
        public string Noiohiennay { get; set; }

        //[StringLength(50)]
        public string avatar { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayVaoDang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayChinhThuc { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<AnhTieuBieu> AnhTieuBieux { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BCH_CD> BCH_CD { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BCH_HPN> BCH_HPN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CapLaiMatKhau> CapLaiMatKhaus { get; set; }

        public virtual CongDoan CongDoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhSachBieuDuongPTTD> DanhSachBieuDuongPTTDs { get; set; }

        public virtual DonVi DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoanCanhGD> HoanCanhGDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoatDong_HoiVien> HoatDong_HoiVien { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SinhHoat_Vang> SinhHoat_Vang { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HoiPhuNu> HoiPhuNus { get; set; }

        //public virtual HoiPhuNu HoiPhuNu { get; set; }

        public virtual LoaiDangVien LoaiDangVien { get; set; }

        public virtual LoaiHocVien LoaiHoiVien { get; set; }

        //public virtual HoiVien_ChiBo HoiVien_ChiBo { get; set; }

        public virtual ToChucDoanThe ToChucDoanThe { get; set; }

        //public virtual HoiVien_DangUy HoiVien_DangUy { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HoiVien_ChiBo> HoiVien_ChiBo1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HoiVien_DangUy> HoiVien_DangUy1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhenThuong> KhenThuongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KyLuat> KyLuats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LaoDongSangTao> LaoDongSangTaos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuCapBac> LichSuCapBacs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuChucDanh> LichSuChucDanhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuDanhHieu> LichSuDanhHieux { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuTrinhDo> LichSuTrinhDoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGia> ThamGias { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TinBai> TinBais { get; set; }
    }
}
