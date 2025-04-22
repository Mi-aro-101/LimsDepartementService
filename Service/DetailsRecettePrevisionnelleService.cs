using LimsDepartementService.Data;
using LimsDepartementService.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

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

    public async Task<VComparaisonRecette[]> GetComparaisonRecetteRealiteDepartement(int annee)
    {
        var anneeParam = new MySqlParameter("@annee", annee);

        var cas = await _dbContect.VComparaisonRecettes.FromSqlRaw(
            @"SELECT annee, id_departement idDepartement, designation, chiffre_affaire chiffreAffaire
                , prevision FROM v_comparaison_recette_realite_departement
                where annee = @annee", anneeParam)
            .ToArrayAsync();

        return cas;
    }
}