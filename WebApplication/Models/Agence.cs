using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Agence
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom de l'agence")]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public string Ville { get; set; } = string.Empty;
    }
}