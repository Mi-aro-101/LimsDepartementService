using LimsDepartementService.Models;
using LimsDepartementService.Data;
using Microsoft.EntityFrameworkCore;

namespace LimsDepartementService.Service;

public class RecettePrevisionnelleService : IRecettePrevisionnelleService
{
    private readonly DepartementContext _dbContext;
    public RecettePrevisionnelleService(DepartementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RecettePrevisionnelle> GetRecettePrevisionnelle(int id)
    {
        RecettePrevisionnelle recettePrevisionnelle = await _dbContext.RecettePrevisionnelles
           .Where(r => r.IdRecettePrevisionnelle == id)
           .FirstOrDefaultAsync();

        return recettePrevisionnelle;
    }

    // TODO : Define
    public async Task<RecettePrevisionnelle> GetRecettePrevisionnelleByExercice(int IdExercice)
    {
        RecettePrevisionnelle recettePrevisionnelle = new RecettePrevisionnelle();

        return recettePrevisionnelle;
    }

    public async Task<RecettePrevisionnelle> CreateRecettePrevisionnelle(RecettePrevisionnelle recettePrevisionnelle)
    {
        Exercice exercice = new Exercice();
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                exercice.DateDebut = recettePrevisionnelle.Exercice.DateDebut;
                exercice.DateFin = recettePrevisionnelle.Exercice.DateFin;
                await _dbContext.Exercices.AddAsync(exercice);
                await _dbContext.SaveChangesAsync();

                recettePrevisionnelle.Exercice = null;
                recettePrevisionnelle.IdExercice = exercice.IdExercice;
                await _dbContext.RecettePrevisionnelles.AddAsync(recettePrevisionnelle);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });

        return await this.GetRecettePrevisionnelle(recettePrevisionnelle.IdRecettePrevisionnelle);
    }
}