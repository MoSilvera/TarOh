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
    public class CardCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CardCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: CardComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CardComment.Include(c => c.Card).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CardComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardComment = await _context.CardComment
                .Include(c => c.Card)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CardCommentId == id);
            if (cardComment == null)
            {
                return NotFound();
            }

            return View(cardComment);
        }

        public  async Task<IActionResult> GetUserCardComments( int? id)
        {
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.CardComment
                .Include(c => c.Card)
                .Include(c => c.User)
                .Where(c => c.CardId == id && c.User.Id == user.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CardComments/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: CardComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardCommentId,CardId,UserId,Comment")] CardComment cardComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", cardComment.CardId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", cardComment.UserId);
            return View(cardComment);
        }

        // GET: CardComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardComment = await _context.CardComment.FindAsync(id);
            if (cardComment == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", cardComment.CardId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", cardComment.UserId);
            return View(cardComment);
        }

        // POST: CardComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardCommentId,CardId,UserId,Comment")] CardComment cardComment)
        {
            if (id != cardComment.CardCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardCommentExists(cardComment.CardCommentId))
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
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", cardComment.CardId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", cardComment.UserId);
            return View(cardComment);
        }

        // GET: CardComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardComment = await _context.CardComment
                .Include(c => c.Card)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CardCommentId == id);
            if (cardComment == null)
            {
                return NotFound();
            }

            return View(cardComment);
        }

        // POST: CardComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardComment = await _context.CardComment.FindAsync(id);
            _context.CardComment.Remove(cardComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardCommentExists(int id)
        {
            return _context.CardComment.Any(e => e.CardCommentId == id);
        }
    }
}
