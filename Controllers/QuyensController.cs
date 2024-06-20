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
    public class QuyensController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public QuyensController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc")]
        // GET: Quyens
        public async Task<IActionResult> Index()
        {
              return _context.Quyens != null ? 
                          View(await _context.Quyens.ToListAsync()) :
                          Problem("Entity set 'QuanLyNhanSuContext.Quyens'  is null.");
        }

        // GET: Quyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quyens == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens
                .FirstOrDefaultAsync(m => m.MaQuyen == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // GET: Quyens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaQuyen,TenQuyen")] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quyen);
        }

        // GET: Quyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quyens == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens.FindAsync(id);
            if (quyen == null)
            {
                return NotFound();
            }
            return View(quyen);
        }

        // POST: Quyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaQuyen,TenQuyen")] Quyen quyen)
        {
            if (id != quyen.MaQuyen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuyenExists(quyen.MaQuyen))
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
            return View(quyen);
        }

        // GET: Quyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quyens == null)
            {
                return NotFound();
            }

            var quyen = await _context.Quyens
                .FirstOrDefaultAsync(m => m.MaQuyen == id);
            if (quyen == null)
            {
                return NotFound();
            }

            return View(quyen);
        }

        // POST: Quyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quyens == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.Quyens'  is null.");
            }
            var quyen = await _context.Quyens.FindAsync(id);
            if (quyen != null)
            {
                _context.Quyens.Remove(quyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuyenExists(int id)
        {
          return (_context.Quyens?.Any(e => e.MaQuyen == id)).GetValueOrDefault();
        }
    }
}
