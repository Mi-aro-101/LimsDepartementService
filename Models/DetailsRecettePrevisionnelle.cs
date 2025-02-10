using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsDepartementService.Models;

[Table("Details_recettes_previsionnelles")]
public class DetailsRecettePrevisionnelle
{
    [Column("id_departement")]
    public int IdDepartement { get; set; }
    [ForeignKey("IdDepartement")]
    public Departement? Departement { get; set; }
    [Column("id_recette_previsionnelle")]
    public int IdRecettePrevisionnelle { get; set; }
    [ForeignKey("IdRecettePrevisionnelle")]
    public RecettePrevisionnelle? RecettePrevisionnelle { get; set; }

    [Column("montant")]
    public decimal Montant { get; set; }
}