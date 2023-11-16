using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StraviaSqlApi.Entities;

public class Nacionalidad
{
    [Key] public string Nacion {  get; }
    
    public virtual ICollection<Atleta> Atleta { get; set; } = new List<Atleta>();
}