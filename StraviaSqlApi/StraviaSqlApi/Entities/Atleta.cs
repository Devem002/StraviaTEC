using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Atleta
{
    [Key]
    public String Usuario { set; get; }
    
    public String Contrasena { set; get; }
    
    public String[] Foto { set; get; }
    
    public String Nombre { set; get; }
    
    public String Apellido_1 { set; get; }
    
    public String Apellido_2 { set; get; }
    
    public DateOnly Fecha_nacimiento { set; get; }
    
    public String Nacionalidad { set; get; }
    
    public String Clasificacion { set; get; }
    
    public List<Amigos> AmigosList { get; }
}