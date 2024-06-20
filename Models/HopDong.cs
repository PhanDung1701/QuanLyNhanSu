using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class HopDong
    {
        public int MaHopDong { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? LoaiHopDong { get; set; }

        public virtual NhanVien? MaNhanVienNavigation { get; set; }
    }
}
