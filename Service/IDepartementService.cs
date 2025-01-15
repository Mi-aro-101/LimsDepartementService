using LimsDepartementService.Models;

namespace LimsDepartementService.Service;

public interface IDepartementService
{
    Task<List<Departement>> GetDepartementsFrom(int skiped, int size);
    Task<Departement> GetDepartement(int id);
    Task<Departement> CreateDepartement(Departement departement);
    Task<Departement> UpdateDepartement(int id, Departement departement);
    Task<bool> DeleteDepartement(int id);
    int CountDepartement();
    Task<List<Departement>> GetAllDepartements();
}