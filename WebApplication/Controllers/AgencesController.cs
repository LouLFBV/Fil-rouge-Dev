using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class AgencesController : Controller
    {
        // GET: Agences
        public IActionResult Index()
        {
            // Liste complète des 12 agences requises par le sujet d'examen Ymmo
            var agences = new List<object>
            {
                new { Id = 1, Nom = "Siège Social - Aix-en-Provence", Adresse = "10 Rue de la République, 13100 Aix", Effectif = "30 collaborateurs", Type = "Siège + Direction" },
                new { Id = 2, Nom = "Agence Paris Centre", Adresse = "75 Boulevard Haussmann, 75008 Paris", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 3, Nom = "Agence Lyon Lumière", Adresse = "22 Avenue Jean Jaurès, 69007 Lyon", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 4, Nom = "Agence Marseille Vieux-Port", Adresse = "40 Quai du Port, 13002 Marseille", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 5, Nom = "Agence Bordeaux Quinconces", Adresse = "12 Place des Quinconces, 33000 Bordeaux", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 6, Nom = "Agence Lille Grand-Place", Adresse = "5 Rue de la Monnaie, 59000 Lille", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 7, Nom = "Agence Nantes Machines", Adresse = "18 Quai de la Fosse, 44000 Nantes", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 8, Nom = "Agence Strasbourg Europe", Adresse = "8 Rue du Dôme, 67000 Strasbourg", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 9, Nom = "Agence Toulouse Capitole", Adresse = "3 Place du Capitole, 31000 Toulouse", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 10, Nom = "Agence Nice Promenade", Adresse = "105 Promenade des Anglais, 06000 Nice", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 11, Nom = "Agence Rennes République", Adresse = "14 Rue de la Monnaie, 35000 Rennes", Effectif = "5 commerciaux", Type = "Agence Locale" },
                new { Id = 12, Nom = "Agence Montpellier Comédie", Adresse = "2 Place de la Comédie, 34000 Montpellier", Effectif = "5 commerciaux", Type = "Agence Locale" }
            };

            ViewBag.Agences = agences;
            return View();
        }
    }
}