using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public interface IExerciceService
{
    Task<Exercice> CreateExercice(Exercice exercice);
}