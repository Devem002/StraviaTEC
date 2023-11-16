using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StraviaSqlApi.Entities;

public class Atleta
{
    [Key]
    public string usuario { set; get; }
    
    public string Contrasena { set; get; }
    
    public string Foto { set; get; }
    
    public string Nombre { set; get; }
    
    public string Apellido_1 { set; get; }
    
    public string Apellido_2 { set; get; }
    
    public DateOnly Fecha_nacimiento { set; get; }
    
    public string Nacionalidad { set; get; }
    
    public string Clasificacion { set; get; }
    
    public List<Amigos> AmigosList { get; }
    
    public virtual Nacionalidad NacionalidadNav { get; set; } = null!;
}