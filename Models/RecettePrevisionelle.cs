using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsDepartementService.Models;

[Table("Recette_previsionnelle")]
public class RecettePrevisionnelle
{
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
}