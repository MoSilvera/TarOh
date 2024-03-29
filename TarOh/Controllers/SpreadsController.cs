﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TarOh.Data;
using TarOh.Models;

namespace TarOh.Controllers
{
    public class SpreadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SpreadsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Spreads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Spread.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> MySavedSpreads()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Spread.Include(s => s.SavedSpreads)
                .Include(s => s.User)
                .Where(s => s.UserId == user.Id);
            
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> ViewSavedSpread(int? id)
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Spread
                .Include(s => s.SavedSpreads)
                .ThenInclude(ss => ss.Card)
                .ThenInclude(ct => ct.CardType)
                .Include(s => s.SavedSpreads)
                .ThenInclude(ss => ss.Card)
                .ThenInclude(ct => ct.CardComments)
                .Include(s => s.SavedSpreads)
                .ThenInclude(ss => ss.OrdinalPosition)
                .ThenInclude(op => op.OrdinalComments)
                .Where(s => s.SpreadId == id);

             return View(await applicationDbContext.ToListAsync());
        }

        

        // GET: Spreads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spread = await _context.Spread
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SpreadId == id);
            if (spread == null)
            {
                return NotFound();
            }

            return View(spread);
        }

        // GET: Spreads/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Spreads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpreadId,Name,ReadingDate,UserId")] Spread spread)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", spread.UserId);
            return View(spread);
        }

        // GET: Spreads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spread = await _context.Spread.FindAsync(id);
            if (spread == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", spread.UserId);
            return View(spread);
        }

        // POST: Spreads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpreadId,Name,ReadingDate,UserId")] Spread spread)
        {
            if (id != spread.SpreadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpreadExists(spread.SpreadId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", spread.UserId);
            return View(spread);
        }

        // GET: Spreads/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spread = await _context.Spread
        //        .Include(s => s.User)
        //        .FirstOrDefaultAsync(m => m.SpreadId == id);
        //    if (spread == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(spread);
        //}

        // POST: Spreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = GetCurrentUserAsync();
            var spread = await _context.Spread.FindAsync(id);
            _context.Spread.Remove(spread);
            await _context.SaveChangesAsync();
            return RedirectToAction("MySavedSpreads", new {id = user.Id });
        }

        private bool SpreadExists(int id)
        {
            return _context.Spread.Any(e => e.SpreadId == id);
        }
    }
}
