using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BienImmobiliersController : Controller
    {
        private readonly WebApplicationContext _context;

        public BienImmobiliersController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: BienImmobiliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.BienImmobilier.ToListAsync());
        }


        // GET: BienImmobiliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmobilier = await _context.BienImmobilier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bienImmobilier == null)
            {
                return NotFound();
            }

            return View(bienImmobilier);
        }

        // GET: BienImmobiliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BienImmobiliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,Prix,Ville,Type,Surface,AgenceId")] BienImmobilier bienImmobilier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bienImmobilier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bienImmobilier);
        }

        // GET: BienImmobiliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmobilier = await _context.BienImmobilier.FindAsync(id);
            if (bienImmobilier == null)
            {
                return NotFound();
            }
            return View(bienImmobilier);
        }

        // POST: BienImmobiliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,Prix,Ville,Type,Surface,AgenceId")] BienImmobilier bienImmobilier)
        {
            if (id != bienImmobilier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bienImmobilier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BienImmobilierExists(bienImmobilier.Id))
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
            return View(bienImmobilier);
        }

        // GET: BienImmobiliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bienImmobilier = await _context.BienImmobilier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bienImmobilier == null)
            {
                return NotFound();
            }

            return View(bienImmobilier);
        }

        // POST: BienImmobiliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bienImmobilier = await _context.BienImmobilier.FindAsync(id);
            if (bienImmobilier != null)
            {
                _context.BienImmobilier.Remove(bienImmobilier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BienImmobilierExists(int id)
        {
            return _context.BienImmobilier.Any(e => e.Id == id);
        }
    }
}
