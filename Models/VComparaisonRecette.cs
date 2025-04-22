namespace LimsDepartementService.Models;

public class VComparaisonRecette
{
    public int Annee { get; set; }
    public int IdDepartement { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal ChiffreAffaire { get; set; }
    public decimal Prevision { get; set; }
}