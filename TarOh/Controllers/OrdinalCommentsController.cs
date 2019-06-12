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
    public class OrdinalCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdinalCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: OrdinalComments
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrdinalComment.ToListAsync());
        }

        // GET: OrdinalComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordinalComment = await _context.OrdinalComment
                .FirstOrDefaultAsync(m => m.OrdinalCommentId == id);
            if (ordinalComment == null)
            {
                return NotFound();
            }

            return View(ordinalComment);
        }

        // GET: OrdinalComments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdinalComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdinalCommentId")] OrdinalComment ordinalComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordinalComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordinalComment);
        }

        public async Task<IActionResult> GetUserOrdinalComments(int? id)
        {
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.OrdinalComment
                .Include(o => o.Card)
                .Include(o => o.User)
                .Where(o => o.CardId == id && o.User.Id == user.Id);

            return View(await applicationDbContext.ToListAsync());
        }


        // GET: OrdinalComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordinalComment = await _context.OrdinalComment.FindAsync(id);
            if (ordinalComment == null)
            {
                return NotFound();
            }
            return View(ordinalComment);
        }

        // POST: OrdinalComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdinalCommentId")] OrdinalComment ordinalComment)
        {
            if (id != ordinalComment.OrdinalCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordinalComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdinalCommentExists(ordinalComment.OrdinalCommentId))
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
            return View(ordinalComment);
        }

        // GET: OrdinalComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordinalComment = await _context.OrdinalComment
                .FirstOrDefaultAsync(m => m.OrdinalCommentId == id);
            if (ordinalComment == null)
            {
                return NotFound();
            }

            return View(ordinalComment);
        }

        // POST: OrdinalComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordinalComment = await _context.OrdinalComment.FindAsync(id);
            _context.OrdinalComment.Remove(ordinalComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdinalCommentExists(int id)
        {
            return _context.OrdinalComment.Any(e => e.OrdinalCommentId == id);
        }
    }
}
