using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            Luongs = new HashSet<Luong>();
            NhanViens = new HashSet<NhanVien>();
        }

        public int MaChucVu { get; set; }
        public string TenChucVu { get; set; } = null!;
        public decimal? LuongCoBan { get; set; }
        public int? MaPhongBan { get; set; }

        public virtual PhongBan? MaPhongBanNavigation { get; set; }
        public virtual ICollection<Luong> Luongs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
