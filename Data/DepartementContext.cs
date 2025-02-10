using LimsDepartementService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsDepartementService.Data;

public class DepartementContext : DbContext
{
    public DepartementContext(DbContextOptions<DepartementContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<DetailsRecettePrevisionnelle>()
        .HasNoKey();
    }

    public DbSet<Departement> Departements { get; set; }
    public DbSet<Exercice> Exercices { get; set; }
    public DbSet<RecettePrevisionnelle> RecettePrevisionnelles { get; set; }
    public DbSet<DetailsRecettePrevisionnelle> DetailsRecettePrevisionnelles { get; set; }
}