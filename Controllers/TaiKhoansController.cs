using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Controllers
{
    public class TaiKhoansController : Controller
    {
        private readonly QuanLyNhanSuContext _context;

        public TaiKhoansController(QuanLyNhanSuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Giám đốc")]
        // GET: TaiKhoans
        public async Task<IActionResult> Index()
        {
            var quanLyNhanSuContext = _context.TaiKhoans.Include(t => t.MaNhanVienNavigation).Include(t => t.MaQuyenNavigation);
            return View(await quanLyNhanSuContext.ToListAsync());
        }

        // GET: TaiKhoans/Create
        public IActionResult Create()
        {
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen");
            ViewData["MaQuyen"] = new SelectList(_context.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: TaiKhoans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTaiKhoan,MaNhanVien,Email,MatKhau,MaQuyen")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", taiKhoan.MaNhanVien);
            ViewData["MaQuyen"] = new SelectList(_context.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", taiKhoan.MaNhanVien);
            ViewData["MaQuyen"] = new SelectList(_context.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTaiKhoan,MaNhanVien,Email,MatKhau,MaQuyen")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaTaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.MaTaiKhoan))
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
            ViewData["MaNhanVien"] = new SelectList(_context.NhanViens, "MaNhanVien", "HoTen", taiKhoan.MaNhanVien);
            ViewData["MaQuyen"] = new SelectList(_context.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiKhoans == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.MaNhanVienNavigation)
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(m => m.MaTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiKhoans == null)
            {
                return Problem("Entity set 'QuanLyNhanSuContext.TaiKhoans'  is null.");
            }
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
            return (_context.TaiKhoans?.Any(e => e.MaTaiKhoan == id)).GetValueOrDefault();
        }

        // GET: TaiKhoans/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: TaiKhoans/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string matKhau)
        {
            var user = await _context.TaiKhoans
                .Include(t => t.MaQuyenNavigation)
                .FirstOrDefaultAsync(m => m.Email == email && m.MatKhau == matKhau);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.MaQuyenNavigation.TenQuyen)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // POST: TaiKhoans/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "TaiKhoans");
        }

        // GET: TaiKhoans/ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: TaiKhoans/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var email = User.Identity.Name;
            var user = await _context.TaiKhoans.FirstOrDefaultAsync(m => m.Email == email);

            if (user != null && user.MatKhau == currentPassword)
            {
                user.MatKhau = newPassword;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Current password is incorrect.");
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
