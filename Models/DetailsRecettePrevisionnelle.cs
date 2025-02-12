using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LimsDepartementService.Models;

[Table("Details_recettes_previsionnelles")]
public class DetailsRecettePrevisionnelle
{
    [Key]
    [Column("id_details_recette_previsionnelles")]
    public int IdDetailsRecettePrevisionnelles { get; set; }
    [Column("id_departement")]
    public int IdDepartement { get; set; }
    [ForeignKey("IdDepartement")]
    public Departement? Departement { get; set; }
    [Column("id_recettes_previsionnelle")]
    public int IdRecettePrevisionnelle { get; set; }
    [ForeignKey("IdRecettePrevisionnelle")]
    [JsonIgnore]
    public RecettePrevisionnelle? RecettePrevisionnelle { get; set; }

    [Column("montant")]
    public decimal Montant { get; set; }
}