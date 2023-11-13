using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UczenRController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UczenRController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UczenR
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uczen.Include(u => u.UczenUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UczenR/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .Include(u => u.UczenUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // GET: UczenR/Create
        public IActionResult Create()
        {
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UczenR/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Street,PostalCode,City,Birthdate,UczenUserId")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser();
                user.Email = uczen.UczenUserId;
                await _userManager.CreateAsync(user, "Haslo123!");
                uczen.UczenUserId = user.Id;
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Id", uczen.UczenUserId);
            return View(uczen);
        }

        // GET: UczenR/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen.FindAsync(id);
            if (uczen == null)
            {
                return NotFound();
            }
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Id", uczen.UczenUserId);
            return View(uczen);
        }

        // POST: UczenR/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Street,PostalCode,City,Birthdate,UczenUserId")] Uczen uczen)
        {
            if (id != uczen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UczenExists(uczen.Id))
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
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Id", uczen.UczenUserId);
            return View(uczen);
        }

        // GET: UczenR/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .Include(u => u.UczenUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // POST: UczenR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uczen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Uczen'  is null.");
            }
            var uczen = await _context.Uczen.FindAsync(id);
            if (uczen != null)
            {
                _context.Uczen.Remove(uczen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UczenExists(int id)
        {
          return (_context.Uczen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
