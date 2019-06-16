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
    public class SpreadCommentsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SpreadCommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> GetUserSpreadComments(int id)
        {
            var user = await GetCurrentUserAsync();
            var applicaitonDbContext = _context.SpreadComment.Include(s => s.Spread).Where(s => s.UserId == user.Id && s.SpreadId == id);

            return View(await applicaitonDbContext.ToListAsync());
        }

        // GET: SpreadComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SpreadComment.Include(s => s.Spread).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SpreadComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spreadComment = await _context.SpreadComment
                .Include(s => s.Spread)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SpreadCommentId == id);
            if (spreadComment == null)
            {
                return NotFound();
            }

            return View(spreadComment);
        }


        public IActionResult Create(int id)
        {
           
            return View();
        }

        // POST: SpreadComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("SpreadCommentId,Comment")] SpreadComment spreadComment)
        {
            var user = await GetCurrentUserAsync();
            spreadComment.SpreadId = id;
            spreadComment.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(spreadComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetUserSpreadComments", new { id = id });
            }
         
            return View(spreadComment);
        }

        // GET: SpreadComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spreadComment = await _context.SpreadComment.FindAsync(id);
            if (spreadComment == null)
            {
                return NotFound();
            }
           
            return View(spreadComment);
        }

        // POST: SpreadComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpreadCommentId,Comment")] SpreadComment spreadComment)
        {
            var user = await GetCurrentUserAsync();
            spreadComment.UserId = user.Id;
            spreadComment.SpreadId = _context.SpreadComment.FirstOrDefault(sc => sc.UserId == user.Id && spreadComment.SpreadCommentId == id).SpreadId;
            if (id != spreadComment.SpreadCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spreadComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpreadCommentExists(spreadComment.SpreadCommentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetUserSpreadComments", new { id = id });
            }
           
            return View(spreadComment);
        }

        // GET: SpreadComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spreadComment = await _context.SpreadComment
                .Include(s => s.Spread)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SpreadCommentId == id);
            if (spreadComment == null)
            {
                return NotFound();
            }

            return View(spreadComment);
        }

        // POST: SpreadComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spreadComment = await _context.SpreadComment.FindAsync(id);
            _context.SpreadComment.Remove(spreadComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetUserOrdinalComments", new { id = spreadComment.SpreadId});
        }

        private bool SpreadCommentExists(int id)
        {
            return _context.SpreadComment.Any(e => e.SpreadCommentId == id);
        }
    }
}
