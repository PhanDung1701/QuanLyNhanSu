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
    public class HopDongsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public HopDongsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc, Quản lý")]
        // GET: HopDongs
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.HopDongs.Include(h => h.MaNhanVienNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: HopDongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHopDong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: HopDongs/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen");
            return View();
        }

        // POST: HopDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHopDong,MaNhanVien,NgayBatDau,NgayKetThuc,LoaiHopDong")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", hopDong.MaNhanVien);
            return View(hopDong);
        }

        // GET: HopDongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", hopDong.MaNhanVien);
            return View(hopDong);
        }

        // POST: HopDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHopDong,MaNhanVien,NgayBatDau,NgayKetThuc,LoaiHopDong")] HopDong hopDong)
        {
            if (id != hopDong.MaHopDong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.MaHopDong))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", hopDong.MaNhanVien);
            return View(hopDong);
        }

        // GET: HopDongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HopDongs == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHopDong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: HopDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HopDongs == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.HopDongs'  is null.");
            }
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDongs.Remove(hopDong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(int id)
        {
          return (_context.HopDongs?.Any(e => e.MaHopDong == id)).GetValueOrDefault();
        }
    }
}
