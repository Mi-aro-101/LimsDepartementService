using LimsDepartementService.Data;
using LimsDepartementService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsDepartementService.Service;

public class DepartementService : IDepartementService
{
    private readonly DepartementContext _dbContext;

    public DepartementService(DepartementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int CountDepartement()
    {
        int result = _dbContext.Departements.Count();
        return result;
    }

    public async Task<Departement> CreateDepartement(Departement departement)
    {
        _dbContext.Departements.Add(departement);
        await _dbContext.SaveChangesAsync();
        Departement result = await _dbContext.Departements.OrderBy(d => d.IdDepartement).LastAsync();

        return result;
    }

    public async Task<bool> DeleteDepartement(int id)
    {
        bool isDeleted = false;
        Departement? departement = await _dbContext.Departements.FirstOrDefaultAsync(d => d.IdDepartement == id);
        if(departement == null)
        {
            throw new ArgumentException("Le departement que vous souhaitez supprimer n'est pas dans la base de données");
        }
        _dbContext.Departements.Remove(departement);
        await _dbContext.SaveChangesAsync();
        isDeleted = true;
        return isDeleted;
    }

    public async Task<List<Departement>> GetAllDepartements()
    {
        List<Departement> results = await _dbContext.Departements.OrderByDescending(d => d.IdDepartement).ToListAsync();
        return results;
    }

    public async Task<Departement> GetDepartement(int id)
    {
        Departement? result = await _dbContext.Departements
            .Where(d => d.IdDepartement == id)
            .FirstOrDefaultAsync();
        if(result == null)
        {
            throw new Exception("Vous n'avez de données departement dans votre base");
        }
        return result;
    }

    public async Task<List<Departement>> GetDepartementsFrom(int skiped, int size)
    {
        List<Departement> results = await _dbContext.Departements.OrderByDescending(d => d.IdDepartement).Skip(skiped).Take(size)
            .ToListAsync();

        return results;
    }

    public async Task<Departement> UpdateDepartement(int id, Departement departement)
    {
        _dbContext.Departements.Update(departement);
        await _dbContext.SaveChangesAsync();

        Departement result = await this.GetDepartement(id);
        return result;
    }
}