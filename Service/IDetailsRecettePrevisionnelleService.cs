using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public interface IDetailsRecettePrevisionnelleService
{
    Task<DetailsRecettePrevisionnelle> CreateRecettePrevisionnelle(DetailsRecettePrevisionnelle detailsRecettePrevisionnelle);
}