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
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class BienImmobiliersController : Controller
    {
        private readonly WebApplicationContext _context;

        public BienImmobiliersController(WebApplicationContext context)
        {
            _context = context;
        }

        // GET: BienImmobiliers
        public async Task<IActionResult> Index(string recherche, string ville, string type, int? agenceId)
        {
            // 1. On récupère tous les biens de la base de données
            var biensQuery = from b in _context.BienImmobilier select b;

            // 2. Filtre sur la recherche textuelle (titre/description)
            if (!string.IsNullOrEmpty(recherche))
            {
                biensQuery = biensQuery.Where(b => b.Titre!.Contains(recherche) || b.Description!.Contains(recherche));
            }

            // 3. Filtre sur la ville
            if (!string.IsNullOrEmpty(ville))
            {
                biensQuery = biensQuery.Where(b => b.Ville!.Contains(ville));
            }

            // 4. Filtre sur le type de bien
            if (!string.IsNullOrEmpty(type))
            {
                biensQuery = biensQuery.Where(b => b.Type == type);
            }

            // 5. NOUVEAU : Filtre strict sur l'ID de l'agence Ymmo
            if (agenceId.HasValue)
            {
                biensQuery = biensQuery.Where(b => b.AgenceId == agenceId.Value);
            }

            // On renvoie la liste filtrée finale à la vue du catalogue
            return View(await biensQuery.ToListAsync());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,Prix,Ville,Type,Surface,AgenceId,ImageUrl")] BienImmobilier bienImmobilier)
        {
            if (ModelState.IsValid)
            {
                // Si l'utilisateur n'a pas mis de lien, on met une image de maison par défaut
                if (string.IsNullOrEmpty(bienImmobilier.ImageUrl))
                {
                    bienImmobilier.ImageUrl = "https://images.unsplash.com/photo-1560518883-ce09059eeffa?auto=format&fit=crop&w=800&q=80";
                }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,Prix,Ville,Type,Surface,AgenceId,ImageUrl")] BienImmobilier bienImmobilier)
        {
            if (id != bienImmobilier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Sécurité : si le lien de l'image a été complètement effacé, on remet l'image par défaut
                    if (string.IsNullOrEmpty(bienImmobilier.ImageUrl))
                    {
                        bienImmobilier.ImageUrl = "https://images.unsplash.com/photo-1560518883-ce09059eeffa?auto=format&fit=crop&w=800&q=80";
                    }

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
