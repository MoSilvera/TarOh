using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TarOh.Data;
using TarOh.Models;

namespace TarOh.Controllers
{
    public class SavedSpreadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SavedSpreadsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: SavedSpreads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SavedSpread.Include(s => s.Card);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SavedSpreads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedSpread = await _context.SavedSpread
                .Include(s => s.Card)
                .FirstOrDefaultAsync(m => m.SavedSpreadId == id);
            if (savedSpread == null)
            {
                return NotFound();
            }

            return View(savedSpread);
        }

        // GET: SavedSpreads/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId");
            return View();
        }

        // POST: SavedSpreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SavedSpreadId,SpreadId,CardId,OrdinalId,CardDirection")] SavedSpread savedSpread)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedSpread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", savedSpread.CardId);
            return View(savedSpread);
        }

        // GET: SavedSpreads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedSpread = await _context.SavedSpread.FindAsync(id);
            if (savedSpread == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", savedSpread.CardId);
            return View(savedSpread);
        }

        // POST: SavedSpreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SavedSpreadId,SpreadId,CardId,OrdinalId,CardDirection")] SavedSpread savedSpread)
        {
            if (id != savedSpread.SavedSpreadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedSpread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedSpreadExists(savedSpread.SavedSpreadId))
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
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", savedSpread.CardId);
            return View(savedSpread);
        }

        // GET: SavedSpreads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedSpread = await _context.SavedSpread
                .Include(s => s.Card)
                .FirstOrDefaultAsync(m => m.SavedSpreadId == id);
            if (savedSpread == null)
            {
                return NotFound();
            }

            return View(savedSpread);
        }

        // POST: SavedSpreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedSpread = await _context.SavedSpread.FindAsync(id);
            _context.SavedSpread.Remove(savedSpread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedSpreadExists(int id)
        {
            return _context.SavedSpread.Any(e => e.SavedSpreadId == id);
        }
    }
}
