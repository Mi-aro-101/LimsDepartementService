using LimsDepartementService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsDepartementService.Data;

public class DepartementContext : DbContext
{
    public DepartementContext(DbContextOptions<DepartementContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RecettePrevisionnelle>()
        .HasMany(r => r.DetailsRecettePrevisionnelles)
        .WithOne(r => r.RecettePrevisionnelle)
        .HasForeignKey(r => r.IdRecettePrevisionnelle)
        .HasPrincipalKey(r => r.IdRecettePrevisionnelle);

        builder.Entity<VComparaisonRecette>().HasNoKey().ToView(null);
    }

    public DbSet<Departement> Departements { get; set; }
    public DbSet<Exercice> Exercices { get; set; }
    public DbSet<RecettePrevisionnelle> RecettePrevisionnelles { get; set; }
    public DbSet<DetailsRecettePrevisionnelle> DetailsRecettePrevisionnelles { get; set; }
    public DbSet<VComparaisonRecette> VComparaisonRecettes { get; set; }
}