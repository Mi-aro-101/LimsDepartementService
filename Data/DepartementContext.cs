using LimsDepartementService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsDepartementService.Data;

public class DepartementContext : DbContext
{
    public DepartementContext(DbContextOptions<DepartementContext> options) : base(options)
    {}

    public DbSet<Departement> Departements { get; set; }
}