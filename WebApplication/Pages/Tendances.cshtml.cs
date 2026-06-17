using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models; // À ajuster selon ton namespace exact

namespace WebApplication.Pages
{
    public class TendancesModel : PageModel
    {
        private readonly WebApplication.Data.ApplicationDbContext _context; // À ajuster selon ton DbContext

        public TendancesModel(WebApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // Propriétés magiques que l'on va envoyer à la vue HTML
        public int TotalBiens { get; set; }
        public decimal PrixMoyen { get; set; } // Modifié en decimal à la place de double
        public double SurfaceMoyenne { get; set; }
        public List<StatVille> StatsParVille { get; set; } = new List<StatVille>();
        public List<StatType> StatsParType { get; set; } = new List<StatType>();

        public async Task OnGetAsync()
        {
            // 1. Récupération de la liste de tous les biens immobiliers
            var tousLesBiens = await _context.BienImmobiliers.ToListAsync();

            if (tousLesBiens.Any())
            {
                // 2. Calculs globaux de base
                TotalBiens = tousLesBiens.Count;
                PrixMoyen = tousLesBiens.Average(b => b.Prix);
                SurfaceMoyenne = tousLesBiens.Average(b => b.Surface);

                // 3. Groupement automatique en mémoire par Ville
                StatsParVille = tousLesBiens
                    .GroupBy(b => b.Ville)
                    .Select(g => new StatVille
                    {
                        NomVille = g.Key ?? "Non spécifié",
                        NombreDeBiens = g.Count(),
                        PrixMoyenVille = g.Average(b => b.Prix)
                    })
                    .OrderByDescending(v => v.NombreDeBiens)
                    .ToList();

                // 4. Groupement automatique en mémoire par Type (Maison / Appartement)
                StatsParType = tousLesBiens
                    .GroupBy(b => b.Type)
                    .Select(g => new StatType
                    {
                        NomType = g.Key ?? "Autre",
                        NombreDeBiens = g.Count(),
                        PrixMoyenType = g.Average(b => b.Prix)
                    })
                    .ToList();
            }
        }
    }

    // Classes d'objets temporaires pour structurer proprement nos statistiques
    public class StatVille
    {
        public string NomVille { get; set; } = string.Empty;
        public int NombreDeBiens { get; set; }
        public decimal PrixMoyenVille { get; set; } // Modifié en decimal
    }

    public class StatType
    {
        public string NomType { get; set; } = string.Empty;
        public int NombreDeBiens { get; set; }
        public decimal PrixMoyenType { get; set; } // Modifié en decimal
    }
}