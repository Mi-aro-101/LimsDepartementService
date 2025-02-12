using LimsDepartementService.Data;
using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public class DetailsRecettePrevisionnelleService : IDetailsRecettePrevisionnelleService
{
    private readonly DepartementContext _dbContext;

    public DetailsRecettePrevisionnelleService(DepartementContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<DetailsRecettePrevisionnelle> CreateDetailsRecettePrevisionnelle(DetailsRecettePrevisionnelle detailsRecettePrevisionnelle)
    {
        _dbContext.DetailsRecettePrevisionnelles.Add(detailsRecettePrevisionnelle);
        await _dbContext.SaveChangesAsync();
        return detailsRecettePrevisionnelle;
    }
}