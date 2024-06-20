using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class PhongBan
    {
        public PhongBan()
        {
            ChucVus = new HashSet<ChucVu>();
            NhanViens = new HashSet<NhanVien>();
        }

        public int MaPhongBan { get; set; }
        public string? TenPhongBan { get; set; }

        public virtual ICollection<ChucVu> ChucVus { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
