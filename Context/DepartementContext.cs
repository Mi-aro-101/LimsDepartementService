using DepartementService.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartementService.Context;

public class DepartementContext : DbContext
{
    public DepartementContext(DbContextOptions<DepartementContext> options) : base(options)
    {}

    public DbSet<Departement> Departements { get; set; }
}