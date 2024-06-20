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
    public class BaoHiemsController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public BaoHiemsController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc, Quản lý")]
        // GET: BaoHiems
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.BaoHiems.Include(b => b.MaNhanVienNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: BaoHiems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BaoHiems == null)
            {
                return NotFound();
            }

            var baoHiem = await _context.BaoHiems
                .Include(b => b.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaBaoHiem == id);
            if (baoHiem == null)
            {
                return NotFound();
            }

            return View(baoHiem);
        }

        // GET: BaoHiems/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen");
            return View();
        }

        // POST: BaoHiems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaBaoHiem,MaNhanVien,LoaiBaoHiem,NgayBatDau,NgayKetThuc")] BaoHiem baoHiem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baoHiem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", baoHiem.MaNhanVien);
            return View(baoHiem);
        }

        // GET: BaoHiems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BaoHiems == null)
            {
                return NotFound();
            }

            var baoHiem = await _context.BaoHiems.FindAsync(id);
            if (baoHiem == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", baoHiem.MaNhanVien);
            return View(baoHiem);
        }

        // POST: BaoHiems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBaoHiem,MaNhanVien,LoaiBaoHiem,NgayBatDau,NgayKetThuc")] BaoHiem baoHiem)
        {
            if (id != baoHiem.MaBaoHiem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baoHiem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaoHiemExists(baoHiem.MaBaoHiem))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", baoHiem.MaNhanVien);
            return View(baoHiem);
        }

        // GET: BaoHiems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BaoHiems == null)
            {
                return NotFound();
            }

            var baoHiem = await _context.BaoHiems
                .Include(b => b.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaBaoHiem == id);
            if (baoHiem == null)
            {
                return NotFound();
            }

            return View(baoHiem);
        }

        // POST: BaoHiems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BaoHiems == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.BaoHiems'  is null.");
            }
            var baoHiem = await _context.BaoHiems.FindAsync(id);
            if (baoHiem != null)
            {
                _context.BaoHiems.Remove(baoHiem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaoHiemExists(int id)
        {
          return (_context.BaoHiems?.Any(e => e.MaBaoHiem == id)).GetValueOrDefault();
        }
    }
}
