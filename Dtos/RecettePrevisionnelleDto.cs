using System.Text.Json.Serialization;
using LimsDepartementService.Models;

namespace LimsDepartementService.Dtos;

public class RecettePrevisionnelleDto
{
    [JsonPropertyName("idRecettePrevisionnelle")]
    public int IdRecettePrevisionnelle { get; set; }
    [JsonPropertyName("dateRecettePrevisionnelle")]
    public DateOnly DateRecettePrevisionnelle { get; set; }
    [JsonPropertyName("idExercice")]
    public int IdExercice { get; set; }
    [JsonPropertyName("exercice")]
    public Exercice? Exercice { get; set; }
    [JsonPropertyName("montantTotal")]
    public decimal MontantTotal { get; set; }
    [JsonPropertyName("recettePrevisionnelleDepartements")]
    public Dictionary<int, decimal> RecettePrevisionnelleDepartements { get; set; } = new Dictionary<int, decimal>();
}