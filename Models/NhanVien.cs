using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            BaoHiems = new HashSet<BaoHiem>();
            HopDongs = new HashSet<HopDong>();
            Luongs = new HashSet<Luong>();
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int MaNhanVien { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public int? MaChucVu { get; set; }
        public int? MaPhongBan { get; set; }
        public bool TruongPhong { get; set; }
        public string? TrangThai { get; set; }
        public long? Cmnd { get; set; }
        public string? DanToc { get; set; }
        public string? Anhdaidien { get; set; }

        public virtual ChucVu? MaChucVuNavigation { get; set; }
        public virtual PhongBan? MaPhongBanNavigation { get; set; }
        public virtual ICollection<BaoHiem> BaoHiems { get; set; }
        public virtual ICollection<HopDong> HopDongs { get; set; }
        public virtual ICollection<Luong> Luongs { get; set; }
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
