using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using LimsDepartementService.Data;
using LimsDepartementService.Dtos;
using LimsDepartementService.Service;

namespace LimsDepartementService.Models;

[Table("Recette_previsionnelle")]
public class RecettePrevisionnelle
{
    public async Task<RecettePrevisionnelle> CreateRecettePrevisionnelle(DepartementContext dbContext, Exercice exercice)
    {
        if(this.DetailsRecettePrevisionnelles.Any())
        {
            throw new Exception("Vous n'avez pas fourni de details concernant les departements dans la recette");
        }
        Exercice = null;
        IdExercice = exercice.IdExercice;
        await dbContext.RecettePrevisionnelles.AddAsync(this);
        await dbContext.SaveChangesAsync();

        return this;
    }

    public void LoadDetailsRecettePrevisionnelle(DepartementContext dbContext, RecettePrevisionnelleDto recettePrevisionnelleDto
    , IDetailsRecettePrevisionnelleService detailsRecettePrevisionnelleService)
    {
        decimal sumDetails = 0;
        foreach (var departementId in recettePrevisionnelleDto.RecettePrevisionnelleDepartements.Keys)
        {
            DetailsRecettePrevisionnelle details = new DetailsRecettePrevisionnelle();
            details.IdDepartement = departementId;
            details.Montant = recettePrevisionnelleDto.RecettePrevisionnelleDepartements[departementId];
            // Adding details to recette
            DetailsRecettePrevisionnelles.Add(details);
            sumDetails += details.Montant;
        }
        if(sumDetails != this.MontantTotal)
        {
            throw new Exception("Les montants des détails ne correspondent pas au montant total de la recette");
        }

    }

    [Key]
    [Column("id_recette_previsionnelle")]
    public int IdRecettePrevisionnelle { get; set; }
    [Column("date_recette_previsionnelle")]
    public DateOnly DateRecettePrevisionnelle { get; set; }
    [Column("id_exercice")]
    public int IdExercice { get; set; }
    [ForeignKey("IdExercice")]
    public Exercice? Exercice { get; set; }
    [Column("montant_total")]
    public decimal MontantTotal { get; set; }
    public ICollection<DetailsRecettePrevisionnelle> DetailsRecettePrevisionnelles { get; set; } = new List<DetailsRecettePrevisionnelle>();

}