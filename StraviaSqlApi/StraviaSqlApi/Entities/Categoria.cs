using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Categoria
{
    [Key]
    public String Clasificacion { set; get; }
}