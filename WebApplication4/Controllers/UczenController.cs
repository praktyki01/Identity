using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uczen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uczen.Include(u => u.UczenUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uczen/Details/5
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

        // GET: Uczen/Create
        public IActionResult Create()
        {
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Uczen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Street,PostalCode,City,Birthdate,UczenUserId")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Email", uczen.UczenUserId);
            return View(uczen);
        }

        // GET: Uczen/Edit/5
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
            ViewData["UczenUserId"] = new SelectList(_context.Users, "Id", "Email", uczen.UczenUserId);
            return View(uczen);
        }

        // POST: Uczen/Edit/5
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

        // GET: Uczen/Delete/5
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

        // POST: Uczen/Delete/5
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
