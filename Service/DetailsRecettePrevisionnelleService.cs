using LimsDepartementService.Data;
using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public class DetailsRecettePrevisionnelleService : IDetailsRecettePrevisionnelleService
{
    private readonly DepartementContext _dbContect;
    public DetailsRecettePrevisionnelleService(DepartementContext dbContect)
    {
        _dbContect = dbContect;
    }

    public async Task<DetailsRecettePrevisionnelle> CreateRecettePrevisionnelle(DetailsRecettePrevisionnelle detailsRecettePrevisionnelle)
    {
        await _dbContect.DetailsRecettePrevisionnelles.AddAsync(detailsRecettePrevisionnelle);
        await _dbContect.SaveChangesAsync();

        return await Task.FromResult(detailsRecettePrevisionnelle);
    }
}