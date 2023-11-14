using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class Retos
{
    [Key]
    public String Nombre { set; get; }

    public int Kms { set; get; }

    public int Completitud { set; get; }

    public bool Finalizado { set; get; }

    public DateOnly Fecha_inicio { set; get; }

    public DateOnly Fecha_fin { set; get; }

    public String Fondo_altura { set; get; }

    public String Clase_actividad { set; get; }
}