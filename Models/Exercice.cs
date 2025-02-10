using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsDepartementService.Models;

[Table("Exercice")]
public class Exercice
{
    [Key]
    [Column("id_exercice")]
    public int IdExercice { get; set; }
    [Column("date_debut")]
    public DateOnly DateDebut { get; set; }
    [Column("date_fin")]
    public DateOnly? DateFin { get; set; }
}