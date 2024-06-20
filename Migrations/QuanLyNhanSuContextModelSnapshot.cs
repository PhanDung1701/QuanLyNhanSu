﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyNhanSu.Models;

#nullable disable

namespace QuanLyNhanSu.Migrations
{
    [DbContext(typeof(QuanLyNhanSuContext))]
    partial class QuanLyNhanSuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuanLyNhanSu.Models.BaoHiem", b =>
                {
                    b.Property<int>("MaBaoHiem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBaoHiem"), 1L, 1);

                    b.Property<string>("LoaiBaoHiem")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaNhanVien")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.HasKey("MaBaoHiem");

                    b.HasIndex("MaNhanVien");

                    b.ToTable("BaoHiem", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.ChucVu", b =>
                {
                    b.Property<int>("MaChucVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaChucVu"), 1L, 1);

                    b.Property<decimal?>("LuongCoBan")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("MaPhongBan")
                        .HasColumnType("int");

                    b.Property<string>("TenChucVu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaChucVu")
                        .HasName("PK__ChucVu__D4639533455FF378");

                    b.HasIndex("MaPhongBan");

                    b.ToTable("ChucVu", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.HopDong", b =>
                {
                    b.Property<int>("MaHopDong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHopDong"), 1L, 1);

                    b.Property<string>("LoaiHopDong")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaNhanVien")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.HasKey("MaHopDong");

                    b.HasIndex("MaNhanVien");

                    b.ToTable("HopDong", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.Luong", b =>
                {
                    b.Property<int>("MaLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLuong"), 1L, 1);

                    b.Property<decimal>("KhauTru")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("MaChucVu")
                        .HasColumnType("int");

                    b.Property<int?>("MaNhanVien")
                        .HasColumnType("int");

                    b.Property<int?>("NgayCong")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayNhan")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Thuong")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MaLuong");

                    b.HasIndex("MaChucVu");

                    b.HasIndex("MaNhanVien");

                    b.ToTable("Luong", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.Property<int>("MaNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhanVien"), 1L, 1);

                    b.Property<string>("Anhdaidien")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long?>("Cmnd")
                        .HasColumnType("bigint")
                        .HasColumnName("CMND");

                    b.Property<string>("DanToc")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HoTen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaChucVu")
                        .HasColumnType("int");

                    b.Property<int?>("MaPhongBan")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TrangThai")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TruongPhong")
                        .HasColumnType("bit");

                    b.HasKey("MaNhanVien");

                    b.HasIndex("MaChucVu");

                    b.HasIndex("MaPhongBan");

                    b.ToTable("NhanVien", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.PhongBan", b =>
                {
                    b.Property<int>("MaPhongBan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPhongBan"), 1L, 1);

                    b.Property<string>("TenPhongBan")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaPhongBan");

                    b.ToTable("PhongBan", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.Quyen", b =>
                {
                    b.Property<int>("MaQuyen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaQuyen"), 1L, 1);

                    b.Property<string>("TenQuyen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaQuyen");

                    b.ToTable("Quyen", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TaiKhoan", b =>
                {
                    b.Property<int>("MaTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTaiKhoan"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("MaNhanVien")
                        .HasColumnType("int");

                    b.Property<int?>("MaQuyen")
                        .HasColumnType("int");

                    b.Property<string>("MatKhau")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaTaiKhoan");

                    b.HasIndex("MaNhanVien");

                    b.HasIndex("MaQuyen");

                    b.ToTable("TaiKhoan", (string)null);
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.BaoHiem", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.NhanVien", "MaNhanVienNavigation")
                        .WithMany("BaoHiems")
                        .HasForeignKey("MaNhanVien")
                        .HasConstraintName("FK_BaoHiem_NhanVien");

                    b.Navigation("MaNhanVienNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.ChucVu", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.PhongBan", "MaPhongBanNavigation")
                        .WithMany("ChucVus")
                        .HasForeignKey("MaPhongBan")
                        .HasConstraintName("FK_ChucVu_PhongBan");

                    b.Navigation("MaPhongBanNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.HopDong", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.NhanVien", "MaNhanVienNavigation")
                        .WithMany("HopDongs")
                        .HasForeignKey("MaNhanVien")
                        .HasConstraintName("FK_HopDong_NhanVien");

                    b.Navigation("MaNhanVienNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.Luong", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.ChucVu", "MaChucVuNavigation")
                        .WithMany("Luongs")
                        .HasForeignKey("MaChucVu")
                        .HasConstraintName("FK_Luong_ChucVu");

                    b.HasOne("QuanLyNhanSu.Models.NhanVien", "MaNhanVienNavigation")
                        .WithMany("Luongs")
                        .HasForeignKey("MaNhanVien")
                        .HasConstraintName("FK_Luong_NhanVien");

                    b.Navigation("MaChucVuNavigation");

                    b.Navigation("MaNhanVienNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.ChucVu", "MaChucVuNavigation")
                        .WithMany("NhanViens")
                        .HasForeignKey("MaChucVu")
                        .HasConstraintName("FK_NhanVien_ChucVu");

                    b.HasOne("QuanLyNhanSu.Models.PhongBan", "MaPhongBanNavigation")
                        .WithMany("NhanViens")
                        .HasForeignKey("MaPhongBan")
                        .HasConstraintName("FK_NhanVien_PhongBan");

                    b.Navigation("MaChucVuNavigation");

                    b.Navigation("MaPhongBanNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.TaiKhoan", b =>
                {
                    b.HasOne("QuanLyNhanSu.Models.NhanVien", "MaNhanVienNavigation")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("MaNhanVien")
                        .HasConstraintName("FK_TaiKhoan_NhanVien");

                    b.HasOne("QuanLyNhanSu.Models.Quyen", "MaQuyenNavigation")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("MaQuyen")
                        .HasConstraintName("FK_TaiKhoan_Quyen");

                    b.Navigation("MaNhanVienNavigation");

                    b.Navigation("MaQuyenNavigation");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.ChucVu", b =>
                {
                    b.Navigation("Luongs");

                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.NhanVien", b =>
                {
                    b.Navigation("BaoHiems");

                    b.Navigation("HopDongs");

                    b.Navigation("Luongs");

                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.PhongBan", b =>
                {
                    b.Navigation("ChucVus");

                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("QuanLyNhanSu.Models.Quyen", b =>
                {
                    b.Navigation("TaiKhoans");
                });
#pragma warning restore 612, 618
        }
    }
}
