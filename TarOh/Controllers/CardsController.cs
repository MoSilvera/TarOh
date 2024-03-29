﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TarOh.Data;
using TarOh.Models;
using TarOh.Models.ViewModels;
using TarOh.Controllers;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace TarOh.Controllers
{
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CardsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult SpreadName()
        {
            return View(); 
        }
        [Authorize]
        public async Task<IActionResult> Reading(int deckId, string name)
        {
            
            var user = await GetCurrentUserAsync();
            var applicationDbContext = _context.Card.Include(c => c.CardType);
            Random rand = new Random();
            var randomizedList = from item in applicationDbContext
                                 orderby rand.Next()
                                 select item;
            List<Reading> readingViewModelList = new List<Reading>();

            Reading readingViewModel = new Reading()
                {
                    Cards = await randomizedList.Take(11).ToListAsync(),
                    OrdinalPositions = await _context.OrdinalPosition.ToListAsync(),
                    OrdinalComments = await _context.OrdinalComment.Where( oc => oc.User.Id == user.Id).ToListAsync(),
                    CardComments = await _context.CardComment.Where( cc => cc.User.Id == user.Id).ToListAsync()
                };

            Spread dummySpread = new Spread()
                {
                    Name = name,
                    UserId = user.Id
                  };

                _context.Add(dummySpread);
                await _context.SaveChangesAsync();


                int count = 0;
                foreach (var card in readingViewModel.Cards)
                {
                    int ordinalId = count + 1;
                    Random rnd = new Random();
                    int cardDirection = rnd.Next(1, 3);

                if (cardDirection % 2 == 0)
                    {
                        card.CardDirection = true;
                    }
                    else
                    {
                        card.CardDirection = false;
                    }
                    SavedSpread dummySavedSpread = new SavedSpread()
                    {
                        SpreadId = dummySpread.SpreadId,
                        CardId = card.CardId,
                        OrdinalPositionId = ordinalId,
                        CardDirection = card.CardDirection,
                    };
                    _context.Add(dummySavedSpread);
                    await _context.SaveChangesAsync();

                    count++;
                }

            readingViewModel.SpreadId = dummySpread.SpreadId;
                readingViewModelList.Add(readingViewModel);

                return View(readingViewModelList.AsEnumerable());
            }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Card.Include(c => c.CardType);
            
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.CardType)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["CardTypeId"] = new SelectList(_context.CardType, "CardTypeId", "CardTypeId");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,ImagePath,UpDefinition,DownDefinition,CardTypeId")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardTypeId"] = new SelectList(_context.CardType, "CardTypeId", "CardTypeId", card.CardTypeId);
            return View(card);
        }

        // GET: Cards/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["CardTypeId"] = new SelectList(_context.CardType, "CardTypeId", "CardTypeId", card.CardTypeId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,ImagePath,UpDefinition,DownDefinition,CardTypeId")] Card card)
        {
            if (id != card.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardId))
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
            ViewData["CardTypeId"] = new SelectList(_context.CardType, "CardTypeId", "CardTypeId", card.CardTypeId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .Include(c => c.CardType)
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = await _context.Card.FindAsync(id);
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.CardId == id);
        }
    }
}
