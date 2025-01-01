using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Web_Girls.Models.Context
{
    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext")
        {
        }

        //public virtual DbSet<AnhTieuBieu> AnhTieuBieux { get; set; }
        //public virtual DbSet<BCH_CD> BCH_CD { get; set; }
        //public virtual DbSet<BCH_HPN> BCH_HPN { get; set; }
        public virtual DbSet<CapBac> CapBacs { get; set; }
        public virtual DbSet<CapLaiMatKhau> CapLaiMatKhaus { get; set; }
        //public virtual DbSet<ChiBo> ChiBoes { get; set; }
        public virtual DbSet<ChucDanh> ChucDanhs { get; set; }
        //public virtual DbSet<CongDoan> CongDoans { get; set; }
        //public virtual DbSet<CongVanBaoCao> CongVanBaoCaos { get; set; }
        //public virtual DbSet<DangUy> DangUys { get; set; }
        public virtual DbSet<DanhHieu> DanhHieux { get; set; }
        public virtual DbSet<DanhSachBieuDuongPTTD> DanhSachBieuDuongPTTDs { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<HoanCanhGD> HoanCanhGDs { get; set; }
        public virtual DbSet<HoatDong> HoatDongs { get; set; }
        public virtual DbSet<HoatDong_HoiVien> HoatDong_HoiVien { get; set; }
        //public virtual DbSet<SinhHoat> SinhHoats { get; set; }
        //public virtual DbSet<SinhHoat_Vang> SinhHoat_Vang { get; set; }
        //public virtual DbSet<HoiPhuNu> HoiPhuNus { get; set; }
        public virtual DbSet<HocVien> HoiViens { get; set; }
        //public virtual DbSet<HoiVien_ChiBo> HoiVien_ChiBo { get; set; }
        //public virtual DbSet<HoiVien_DangUy> HoiVien_DangUy { get; set; }
        public virtual DbSet<KhenThuong> KhenThuongs { get; set; }
        public virtual DbSet<KyLuat> KyLuats { get; set; }
        public virtual DbSet<LaoDongSangTao> LaoDongSangTaos { get; set; }
        public virtual DbSet<LichSuCapBac> LichSuCapBacs { get; set; }
        public virtual DbSet<LichSuChucDanh> LichSuChucDanhs { get; set; }
        public virtual DbSet<LichSuDanhHieu> LichSuDanhHieux { get; set; }
        public virtual DbSet<LichSuTrinhDo> LichSuTrinhDoes { get; set; }
        public virtual DbSet<LoaiDangVien> LoaiDangViens { get; set; }
        public virtual DbSet<LoaiHoatDong> LoaiHoatDongs { get; set; }
        //public virtual DbSet<LoaiSinhHoat> LoaiSinhHoats { get; set; }
        public virtual DbSet<LoaiHocVien> LoaiHoiViens { get; set; }
        public virtual DbSet<LoaiLDST> LoaiLDSTs { get; set; }
        //public virtual DbSet<LoaiTinBai> LoaiTinBais { get; set; }
        //public virtual DbSet<NoiNhan> NoiNhans { get; set; }
        public virtual DbSet<PhongTraoThiDua> PhongTraoThiDuas { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThamGia> ThamGias { get; set; }
        //public virtual DbSet<TinBai> TinBais { get; set; }
        public virtual DbSet<ToChucDoanThe> ToChucDoanThes { get; set; }
        public virtual DbSet<TrinhDo> TrinhDoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AnhTieuBieu>()
            //    .Property(e => e.UrlAnh)
            //    .IsUnicode(false);

            //modelBuilder.Entity<AnhTieuBieu>()
            //    .Property(e => e.MaHV)
            //    .IsUnicode(false);

            modelBuilder.Entity<BCH_CD>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            //modelBuilder.Entity<BCH_HPN>()
            //    .Property(e => e.MaHV)
            //    .IsUnicode(false);

            modelBuilder.Entity<CapBac>()
                .HasMany(e => e.LichSuCapBacs)
                .WithOptional(e => e.CapBac)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CapLaiMatKhau>()
                .Property(e => e.TenDN)
                .IsUnicode(false);

            modelBuilder.Entity<CapLaiMatKhau>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<ChucDanh>()
                .HasMany(e => e.LichSuChucDanhs)
                .WithOptional(e => e.ChucDanh)
                .WillCascadeOnDelete();

            //modelBuilder.Entity<CongVanBaoCao>()
            //    .HasMany(e => e.NoiNhans)
            //    .WithRequired(e => e.CongVanBaoCao)
            //    .HasForeignKey(e => e.MaCongVan)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<DangUy>()
            //    .HasMany(e => e.HoiVien_DangUy)
            //    .WithOptional(e => e.DangUy)
            //    .WillCascadeOnDelete();

            modelBuilder.Entity<DanhHieu>()
                .HasMany(e => e.LichSuDanhHieux)
                .WithOptional(e => e.DanhHieu)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DanhSachBieuDuongPTTD>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<HoanCanhGD>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<HoatDong_HoiVien>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

          
            modelBuilder.Entity<HocVien>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<HocVien>()
                .Property(e => e.avatar)
                .IsUnicode(false);
            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.DanhSachBieuDuongPTTDs)
                .WithRequired(e => e.HoiVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.HoanCanhGDs)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.HoatDong_HoiVien)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            //modelBuilder.Entity<HoiVien>()
            //    .HasMany(e => e.SinhHoat_Vang)
            //    .WithOptional(e => e.HoiVien)
            //    .WillCascadeOnDelete();

            //modelBuilder.Entity<HoiVien>()
            //    .HasMany(e => e.HoiPhuNus)
            //    .WithOptional(e => e.HoiVien)
            //    .HasForeignKey(e => e.MaHV);

            //modelBuilder.Entity<HoiVien>()
            //    .HasMany(e => e.HoiVien_ChiBo1)
            //    .WithOptional(e => e.HoiVien)
            //    .HasForeignKey(e => e.MaHV);

            //modelBuilder.Entity<HoiVien>()
            //    .HasMany(e => e.HoiVien_DangUy1)
            //    .WithOptional(e => e.HoiVien)
            //    .HasForeignKey(e => e.MaHV);

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.KhenThuongs)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.KyLuats)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.LaoDongSangTaos)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.LichSuCapBacs)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.LichSuChucDanhs)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.LichSuDanhHieux)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<HocVien>()
                .HasMany(e => e.LichSuTrinhDoes)
                .WithOptional(e => e.HoiVien)
                .WillCascadeOnDelete();

            //modelBuilder.Entity<HoiVien>()
            //    .HasMany(e => e.TinBais)
            //    .WithOptional(e => e.HoiVien)
            //    .WillCascadeOnDelete();

            //modelBuilder.Entity<HoiVien_ChiBo>()
            //    .Property(e => e.MaHV)
            //    .IsUnicode(false);

            //modelBuilder.Entity<HoiVien_ChiBo>()
            //    .HasMany(e => e.HoiViens)
            //    .WithOptional(e => e.HoiVien_ChiBo)
            //    .HasForeignKey(e => e.MaVTCB);

            //modelBuilder.Entity<HoiVien_DangUy>()
            //    .Property(e => e.MaHV)
            //    .IsUnicode(false);

            //modelBuilder.Entity<HoiVien_DangUy>()
            //    .HasMany(e => e.HoiViens)
            //    .WithOptional(e => e.HoiVien_DangUy)
            //    .HasForeignKey(e => e.MaVTDU);

            modelBuilder.Entity<KhenThuong>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<KyLuat>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LaoDongSangTao>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LaoDongSangTao>()
                .HasMany(e => e.ThamGias)
                .WithOptional(e => e.LaoDongSangTao)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LichSuCapBac>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuChucDanh>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuDanhHieu>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LichSuTrinhDo>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiHoatDong>()
                .HasMany(e => e.HoatDongs)
                .WithOptional(e => e.LoaiHoatDong)
                .WillCascadeOnDelete();

            //modelBuilder.Entity<LoaiSinhHoat>()
            //  .HasMany(e => e.SinhHoats)
            //  .WithOptional(e => e.LoaiSinhHoat)
            //  .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiHocVien>()
                .HasMany(e => e.HoiViens)
                .WithOptional(e => e.LoaiHoiVien)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiLDST>()
                .HasMany(e => e.LaoDongSangTaos)
                .WithOptional(e => e.LoaiLDST)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhongTraoThiDua>()
                .HasMany(e => e.DanhSachBieuDuongPTTDs)
                .WithRequired(e => e.PhongTraoThiDua)
                .HasForeignKey(e => e.MaPTTD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenDN)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            modelBuilder.Entity<ThamGia>()
                .Property(e => e.MaHV)
                .IsUnicode(false);

            //modelBuilder.Entity<TinBai>()
            //    .Property(e => e.Img)
            //    .IsUnicode(false);

            //modelBuilder.Entity<TinBai>()
            //    .Property(e => e.MaHV)
            //    .IsUnicode(false);

            //modelBuilder.Entity<TinBai>()
            //    .Property(e => e.Img1)
            //    .IsUnicode(false);

            modelBuilder.Entity<TrinhDo>()
                .HasMany(e => e.LichSuTrinhDoes)
                .WithOptional(e => e.TrinhDo)
                .WillCascadeOnDelete();
        }
    }
}
