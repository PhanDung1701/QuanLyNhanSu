using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class BaoHiem
    {
        public int MaBaoHiem { get; set; }
        public int? MaNhanVien { get; set; }
        public string? LoaiBaoHiem { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual NhanVien? MaNhanVienNavigation { get; set; }
    }
}
