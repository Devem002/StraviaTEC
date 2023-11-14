using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Grupos
{
    [Key] 
    public String Nombre { set; get; }

    public string Atleta_admin { set; get; }
}