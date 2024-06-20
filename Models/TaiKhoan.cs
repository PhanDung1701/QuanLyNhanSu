using System;
using System.Collections.Generic;

namespace QuanLyNhanSu.Models
{
    public partial class TaiKhoan
    {
        public int MaTaiKhoan { get; set; }
        public int? MaNhanVien { get; set; }
        public string? Email { get; set; }
        public string? MatKhau { get; set; }
        public int? MaQuyen { get; set; }

        public virtual NhanVien? MaNhanVienNavigation { get; set; }
        public virtual Quyen? MaQuyenNavigation { get; set; }
    }
}
