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
    public class ChucVusController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public ChucVusController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc, Quản lý")]
        // GET: ChucVus
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.ChucVus.Include(c => c.MaPhongBanNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: ChucVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .Include(c => c.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaChucVu == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // GET: ChucVus/Create
        public IActionResult Create()
        {
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan");
            return View();
        }

        // POST: ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChucVu,TenChucVu,LuongCoBan,MaPhongBan")] ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", chucVu.MaPhongBan);
            return View(chucVu);
        }

        // GET: ChucVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", chucVu.MaPhongBan);
            return View(chucVu);
        }

        // POST: ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaChucVu,TenChucVu,LuongCoBan,MaPhongBan")] ChucVu chucVu)
        {
            if (id != chucVu.MaChucVu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucVuExists(chucVu.MaChucVu))
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
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "TenPhongBan", chucVu.MaPhongBan);
            return View(chucVu);
        }

        // GET: ChucVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .Include(c => c.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaChucVu == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // POST: ChucVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChucVus == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.ChucVus'  is null.");
            }
            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu != null)
            {
                _context.ChucVus.Remove(chucVu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucVuExists(int id)
        {
          return (_context.ChucVus?.Any(e => e.MaChucVu == id)).GetValueOrDefault();
        }
    }
}
