using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Nacionalidad
{
    [Key] 
    public String Nacion { set; get; }
    
    public List<Atleta> Atletas { get; }
}