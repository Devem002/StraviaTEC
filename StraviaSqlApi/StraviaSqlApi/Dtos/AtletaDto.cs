namespace StraviaSqlApi.Dtos;

public class AtletaDto
{
    public string Usuario { set; get; }
    
    public string Contrasena { set; get; }
    
    public string? Foto { set; get; }
    
    public string Nombre { set; get; }
    
    public string Apellido_1 { set; get; }
    
    public string Apellido_2 { set; get; }
    
    public DateTime Fecha_Nacimiento { set; get; }
    
    public string Nacionalidad { set; get; }
    
    public string Clasificacion { set; get; }
}