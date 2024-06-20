using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly QuanLyNhanSuContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NhanViensController(QuanLyNhanSuContext context, IWebHostEnvironment webHostEnvironment)
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Giám đốc, Quản lý")]
        // GET: NhanViens
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.NhanViens.Include(n => n.MaChucVuNavigation).Include(n => n.MaPhongBanNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.MaChucVuNavigation)
                .Include(n => n.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu");
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhanVien,HoTen,NgaySinh,DiaChi,SoDienThoai,Email,MaChucVu,MaPhongBan,TruongPhong,TrangThai,Cmnd,DanToc,Anhdaidien")] NhanVien nhanVien, IFormFile Anhdaidien)
        {
            if (ModelState.IsValid)
            {
                if (Anhdaidien != null)
                {
                    nhanVien.Anhdaidien = await UploadFile(Anhdaidien);
                }
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNhanVien,HoTen,NgaySinh,DiaChi,SoDienThoai,Email,MaChucVu,MaPhongBan,TruongPhong,TrangThai,Cmnd,DanToc,Anhdaidien")] NhanVien nhanVien, IFormFile Anhdaidien)
        {
            if (id != nhanVien.MaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Anhdaidien != null)
                    {
                        nhanVien.Anhdaidien = await UploadFile(Anhdaidien);
                    }
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.MaNhanVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.MaChucVuNavigation)
                .Include(n => n.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NhanViens == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.NhanViens'  is null.");
            }
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
          return (_context.NhanViens?.Any(e => e.MaNhanVien == id)).GetValueOrDefault();
        }
        private async Task<string> UploadFile(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/nhanvien");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
}
