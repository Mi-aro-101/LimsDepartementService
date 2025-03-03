using LimsDepartementService.Data;
using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public class ExerciceService : IExerciceService
{
    private readonly DepartementContext _dbContext;

    public ExerciceService(DepartementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Exercice> CreateExercice(Exercice exercice)
    {
        await _dbContext.Exercices.AddAsync(exercice);
        await _dbContext.SaveChangesAsync();

        return exercice;
    }
}