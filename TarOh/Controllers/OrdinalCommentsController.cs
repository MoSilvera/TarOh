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
using TarOh.Models.ViewModels;

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

            public async Task<IActionResult> GetUserOrdinalComments(int? id)
        {
            var user = await GetCurrentUserAsync();
            List<OrdinalCommentWithId> viewModelList = new List<OrdinalCommentWithId>();

            OrdinalCommentWithId ordinalCommentDataViewModel = new OrdinalCommentWithId
            {
                OrdinalComments = await _context.OrdinalComment
                .Include(o => o.OrdinalPosition)
                .Include(o => o.User)
                .Where(o => o.OrdinalPositionId == id && o.User.Id == user.Id).ToListAsync(),
                OrdinalPositionId = id,
                OrdinalPosition = _context.OrdinalPosition.Where(op => op.OrdinalPositionId == id).Last()
               

            };
            viewModelList.Add(ordinalCommentDataViewModel);

            

            return View( viewModelList.AsEnumerable());
        }
      

        // GET: OrdinalComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordinalComment = await _context.OrdinalComment
                .Include(o => o.OrdinalPosition)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrdinalCommentId == id);
            if (ordinalComment == null)
            {
                return NotFound();
            }

            return View(ordinalComment);
        }

        // GET: OrdinalComments/Create
        public IActionResult Create(int id)
        {
            ViewData["OrdinalPositionId"] = id;
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: OrdinalComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("OrdinalCommentId,Comment")] OrdinalComment ordinalComment)
        {
            var user = await GetCurrentUserAsync();
            ordinalComment.OrdinalPositionId = id;
            ordinalComment.UserId = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(ordinalComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetUserOrdinalComments", new { id = id });
            }
            
            
            return View(ordinalComment);
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
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdinalCommentId,Comment")] OrdinalComment ordinalComment)
        {
            var user = await GetCurrentUserAsync();
            ordinalComment.UserId = user.Id;
            ordinalComment.OrdinalPositionId = _context.OrdinalComment.AsNoTracking().FirstOrDefault(oc => oc.OrdinalCommentId == id).OrdinalPositionId;
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
                return RedirectToAction("GetUserOrdinalComments", new { id = ordinalComment.OrdinalPositionId });
            }
            ViewData["CardId"] = new SelectList(_context.Card, "CardId", "CardId", ordinalComment.OrdinalPositionId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ordinalComment.UserId);
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
                .Include(o => o.OrdinalPosition)
                .Include(o => o.User)
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
            return RedirectToAction("GetUserOrdinalComments", new { id = ordinalComment.OrdinalPositionId });
        }

        private bool OrdinalCommentExists(int id)
        {
            return _context.OrdinalComment.Any(e => e.OrdinalCommentId == id);
        }
    }
}
