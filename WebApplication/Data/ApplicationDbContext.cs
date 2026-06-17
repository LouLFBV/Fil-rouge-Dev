using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models; // Permet d'accéder à tes classes de dossiers Models

namespace WebApplication.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // On déclare explicitement les tables à Entity Framework pour qu'il puisse les interroger
    public DbSet<BienImmobilier> BienImmobiliers { get; set; }
    public DbSet<Agence> Agences { get; set; }
}