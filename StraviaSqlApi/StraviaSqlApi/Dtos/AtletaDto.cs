namespace StraviaSqlApi.Dtos;

public class AtletaDto
{
    public String Usuario { set; get; }
    
    public String Contrasena { set; get; }
    
    public String[] Foto { set; get; }
    
    public String Nombre { set; get; }
    
    public String Apellido_1 { set; get; }
    
    public String Apellido_2 { set; get; }
    
    public DateOnly Fecha_nacimiento { set; get; }
    
    public String Nacionalidad { set; get; }
    
    public String Clasificacion { set; get; }
}