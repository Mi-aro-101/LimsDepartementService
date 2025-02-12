using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public interface IDetailsRecettePrevisionnelleService
{
        Task<DetailsRecettePrevisionnelle> CreateDetailsRecettePrevisionnelle(DetailsRecettePrevisionnelle detailsRecettePrevisionnelle);
}