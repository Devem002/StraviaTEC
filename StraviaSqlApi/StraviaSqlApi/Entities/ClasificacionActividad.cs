using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class ClasificacionActividad
{
    [Key] 
    public String Clasificacion { set; get; }
}