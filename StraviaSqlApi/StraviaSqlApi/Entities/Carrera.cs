
using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Carrera
{
    [Key]
    public String Nombre { set; get; }
    
    public DateOnly Fecha { set; get; }
    
    public TimeOnly Hora { set; get; }
    
    public decimal Precio { set; get; }
    
    public int Kms { set; get; }
    
    public String Recorrido_gpx { set; get; }
    
    public int Finalizado { set; get; }
    
    public String Clase_actividad { set; get; }
}