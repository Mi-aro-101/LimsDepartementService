using LimsDepartementService.Dtos;
using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public interface IRecettePrevisionnelleService
{
    Task<RecettePrevisionnelle> GetRecettePrevisionnelle(int id);
    Task<RecettePrevisionnelle> GetRecettePrevisionnelleByExercice(int IdExercice);
    Task<RecettePrevisionnelle> CreateRecettePrevisionnelle(RecettePrevisionnelleDto recettePrevisionnelle);
}