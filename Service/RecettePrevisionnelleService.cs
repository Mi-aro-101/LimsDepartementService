using LimsDepartementService.Models;
using LimsDepartementService.Data;
using Microsoft.EntityFrameworkCore;
using LimsDepartementService.Dtos;
using LimsUtils.Utility;
using System.Text.Json;

namespace LimsDepartementService.Service;

public class RecettePrevisionnelleService : IRecettePrevisionnelleService
{
    private readonly DepartementContext _dbContext;
    private readonly IExerciceService _exerciceService;
    IDetailsRecettePrevisionnelleService _detailsRecettePrevisionnelleService;
    public RecettePrevisionnelleService(DepartementContext dbContext, IExerciceService exerciceService, IDetailsRecettePrevisionnelleService detailsRecettePrevisionnelleService)
    {
        _dbContext = dbContext;
        _exerciceService = exerciceService;
        _detailsRecettePrevisionnelleService = detailsRecettePrevisionnelleService;
    }

    public async Task<RecettePrevisionnelle> GetRecettePrevisionnelle(int id)
    {
        RecettePrevisionnelle? recettePrevisionnelle = await _dbContext.RecettePrevisionnelles
           .Where(r => r.IdRecettePrevisionnelle == id)
           .Include(r => r.Exercice)
           .Include(r => r.DetailsRecettePrevisionnelles)
           .ThenInclude(dr => dr.Departement)
           .FirstOrDefaultAsync();
        
        if(recettePrevisionnelle == null)
        {
            throw new Exception("Il n'y a pas de données dans votre base");
        }

        return recettePrevisionnelle;
    }

    public async Task<RecettePrevisionnelle> GetRecettePrevisionnelleByExercice(int IdExercice)
    {
        RecettePrevisionnelle? recettePrevisionnelle = new RecettePrevisionnelle();
        recettePrevisionnelle = await _dbContext.RecettePrevisionnelles
        .Where(rp => rp.IdExercice == IdExercice)
        .FirstOrDefaultAsync();

        if(recettePrevisionnelle == null)
        {
            throw new Exception("Une recette previsionnelle relié à cet exercice n'existe pas");
        }

        return recettePrevisionnelle;
    }

    public async Task<RecettePrevisionnelle> CreateRecettePrevisionnelle(RecettePrevisionnelleDto recettePrevisionnelleDto)
    {
        int currentYear = DateTime.Now.Year;
        Exercice exercice = new Exercice{
            DateDebut = new DateOnly(currentYear, 1, 1),
            DateFin = new DateOnly(currentYear, 12, 31)
        };
        RecettePrevisionnelle recettePrevisionnelle = new RecettePrevisionnelle{
            Exercice = exercice
        };
        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                recettePrevisionnelle = DtoHandler.DtoToObject<RecettePrevisionnelle, RecettePrevisionnelleDto>(recettePrevisionnelleDto);
                exercice = await exercice.CreateExercice(recettePrevisionnelle, _exerciceService);
                recettePrevisionnelle.LoadDetailsRecettePrevisionnelle(_dbContext, recettePrevisionnelleDto, _detailsRecettePrevisionnelleService);
                Console.WriteLine(JsonSerializer.Serialize(recettePrevisionnelle.DetailsRecettePrevisionnelles));
                recettePrevisionnelle = await recettePrevisionnelle.CreateRecettePrevisionnelle(_dbContext, exercice);

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