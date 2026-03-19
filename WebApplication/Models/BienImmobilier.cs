using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class BienImmobilier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titre de l'annonce")]
        public string Titre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

        public string Ville { get; set; }

        [Display(Name = "Type de bien")]
        public string Type { get; set; } // Résidentiel ou Professionnel [cite: 63]

        public double Surface { get; set; }

        // Liaison avec l'agence (pour le côté centralisé)
        public int AgenceId { get; set; }
    }
}
