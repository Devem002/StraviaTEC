using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_Actividad),nameof(Atleta))]
public class Actividad
{
    public String Nmbr_Actividad { set; get; }
    
    public DateOnly Fecha { set; get; }
    
    public TimeOnly Hora { set; get; }
    
    public int Kms { set; get; }
    
    public TimeOnly Duracion { set; get; }
    
    public string Recorrido_gpx { set; get; }
    
    public String Atleta { set; get; }
    
    public String Clase_actividad { set; get; }
}