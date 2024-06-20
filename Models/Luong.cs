using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class Luong
    {
        public int MaLuong { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime? NgayNhan { get; set; }
        public int? MaChucVu { get; set; }
        public decimal Thuong { get; set; }
        public decimal KhauTru { get; set; }
        public int? NgayCong { get; set; }
        public int TongLuong { get; set; }

        public virtual ChucVu? MaChucVuNavigation { get; set; }
        public virtual NhanVien? MaNhanVienNavigation { get; set; }
    }
}
