using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Controllers
{
    public class LuongsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public LuongsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc, Quản lý")]
        // GET: Luongs
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.Luongs.Include(l => l.MaChucVuNavigation).Include(l => l.MaNhanVienNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: Luongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Luongs == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.MaChucVuNavigation)
                .Include(l => l.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // GET: Luongs/Create
        public IActionResult Create()
        {
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "MaChucVu");
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen");
            return View();
        }

        // POST: Luongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLuong,MaNhanVien,NgayNhan,MaChucVu,Thuong,KhauTru,NgayCong,TongLuong")] Luong luong)
        {
            if (ModelState.IsValid)
            {
                // Fetch ChucVu to calculate TongLuong
                _context.Add(luong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "MaChucVu", luong.MaChucVu);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", luong.MaNhanVien);
            return View(luong);
        }

        // GET: Luongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Luongs == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs.FindAsync(id);
            if (luong == null)
            {
                return NotFound();
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "MaChucVu", luong.MaChucVu);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", luong.MaNhanVien);
            return View(luong);
        }

        // POST: Luongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLuong,MaNhanVien,NgayNhan,MaChucVu,Thuong,KhauTru,NgayCong,TongLuong")] Luong luong)
        {
            if (id != luong.MaLuong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(luong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuongExists(luong.MaLuong))
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
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "MaChucVu", luong.MaChucVu);
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", luong.MaNhanVien);
            return View(luong);
        }

        // GET: Luongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Luongs == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs
                .Include(l => l.MaChucVuNavigation)
                .Include(l => l.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaLuong == id);
            if (luong == null)
            {
                return NotFound();
            }

            return View(luong);
        }

        // POST: Luongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Luongs == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.Luongs'  is null.");
            }
            var luong = await _context.Luongs.FindAsync(id);
            if (luong != null)
            {
                _context.Luongs.Remove(luong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LuongExists(int id)
        {
          return (_context.Luongs?.Any(e => e.MaLuong == id)).GetValueOrDefault();
        }
        [HttpGet]
        public async Task<IActionResult> GetLuongCoBan(int maChucVu)
        {
            var chucVu = await _context.ChucVus.FindAsync(maChucVu);
            if (chucVu != null)
            {
                return Json(chucVu.LuongCoBan);
            }
            return Json(0);
        }
        [HttpGet]
        public async Task<IActionResult> GetChucVuByNhanVien(int maNhanVien)
        {
            var nhanVien = await _context.NhanViens.Include(nv => nv.MaChucVuNavigation).FirstOrDefaultAsync(nv => nv.MaNhanVien == maNhanVien);
            if (nhanVien != null && nhanVien.MaChucVuNavigation != null)
            {
                return Json(new { MaChucVu = nhanVien.MaChucVuNavigation.MaChucVu, LuongCoBan = nhanVien.MaChucVuNavigation.LuongCoBan });
            }
            return Json(new { MaChucVu = 0, LuongCoBan = 0 });
        }
        public async Task<IActionResult> GetChucVuTuongUng(int maNhanVien)
        {
            var nhanVien = await _context.NhanViens
                .Include(nv => nv.MaChucVuNavigation)
                .FirstOrDefaultAsync(nv => nv.MaNhanVien == maNhanVien);

            if (nhanVien == null)
            {
                return NotFound();
            }

            var chucVuList = await _context.ChucVus
                .Where(cv => cv.MaPhongBan == nhanVien.MaPhongBan)
                .ToListAsync();

            var chucVuItems = chucVuList.Select(cv => new SelectListItem
            {
                Value = cv.MaChucVu.ToString(),
                Text = cv.TenChucVu
            }).ToList();

            return Json(chucVuItems);
        }

    }
}
