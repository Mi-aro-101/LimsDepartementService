using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LimsDepartementService.Service;

namespace LimsDepartementService.Models;

[Table("Exercice")]
public class Exercice
{

    public async Task<Exercice> CreateExercice(RecettePrevisionnelle recettePrevisionnelle, IExerciceService exerciceService)
    {
        if(recettePrevisionnelle.Exercice == null)
        {
            throw new Exception("L'exercice est obligatoire");
        }
        DateDebut = recettePrevisionnelle.Exercice.DateDebut;
        DateFin = recettePrevisionnelle.Exercice.DateFin;
        return await exerciceService.CreateExercice(this);
    }
    

    [Key]
    [Column("id_exercice")]
    public int IdExercice { get; set; }
    [Column("date_debut")]
    public required DateOnly DateDebut { get; set; }
    [Column("date_fin")]
    public required DateOnly DateFin { get; set; }

}