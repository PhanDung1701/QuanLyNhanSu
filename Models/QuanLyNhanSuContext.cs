using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyNhanSu.Models
{
    public partial class QuanLyNhanSuContext : DbContext
    {
        public QuanLyNhanSuContext()
        {
        }

        public QuanLyNhanSuContext(DbContextOptions<QuanLyNhanSuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaoHiem> BaoHiems { get; set; } = null!;
        public virtual DbSet<ChucVu> ChucVus { get; set; } = null!;
        public virtual DbSet<HopDong> HopDongs { get; set; } = null!;
        public virtual DbSet<Luong> Luongs { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<PhongBan> PhongBans { get; set; } = null!;
        public virtual DbSet<Quyen> Quyens { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LD8D1MH\\PHANDUNG;Database=QuanLyNhanSu;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoHiem>(entity =>
            {
                entity.HasKey(e => e.MaBaoHiem);

                entity.ToTable("BaoHiem");

                entity.Property(e => e.LoaiBaoHiem).HasMaxLength(50);

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.BaoHiems)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_BaoHiem_NhanVien");
            });

            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.MaChucVu)
                    .HasName("PK__ChucVu__D4639533455FF378");

                entity.ToTable("ChucVu");

                entity.Property(e => e.LuongCoBan).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TenChucVu).HasMaxLength(100);

                entity.HasOne(d => d.MaPhongBanNavigation)
                    .WithMany(p => p.ChucVus)
                    .HasForeignKey(d => d.MaPhongBan)
                    .HasConstraintName("FK_ChucVu_PhongBan");
            });

            modelBuilder.Entity<HopDong>(entity =>
            {
                entity.HasKey(e => e.MaHopDong);

                entity.ToTable("HopDong");

                entity.Property(e => e.LoaiHopDong).HasMaxLength(50);

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.HopDongs)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_HopDong_NhanVien");
            });

            modelBuilder.Entity<Luong>(entity =>
            {
                entity.HasKey(e => e.MaLuong);

                entity.ToTable("Luong");

                entity.Property(e => e.KhauTru).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.NgayNhan).HasColumnType("datetime");

                entity.Property(e => e.Thuong).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.MaChucVuNavigation)
                    .WithMany(p => p.Luongs)
                    .HasForeignKey(d => d.MaChucVu)
                    .HasConstraintName("FK_Luong_ChucVu");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Luongs)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_Luong_NhanVien");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien);

                entity.ToTable("NhanVien");

                entity.Property(e => e.Anhdaidien).HasMaxLength(50);

                entity.Property(e => e.Cmnd).HasColumnName("CMND");

                entity.Property(e => e.DanToc).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.SoDienThoai).HasMaxLength(50);

                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(d => d.MaChucVuNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaChucVu)
                    .HasConstraintName("FK_NhanVien_ChucVu");

                entity.HasOne(d => d.MaPhongBanNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaPhongBan)
                    .HasConstraintName("FK_NhanVien_PhongBan");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.HasKey(e => e.MaPhongBan);

                entity.ToTable("PhongBan");

                entity.Property(e => e.TenPhongBan).HasMaxLength(50);
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen);

                entity.ToTable("Quyen");

                entity.Property(e => e.TenQuyen).HasMaxLength(50);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan);

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_TaiKhoan_NhanVien");

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaQuyen)
                    .HasConstraintName("FK_TaiKhoan_Quyen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
