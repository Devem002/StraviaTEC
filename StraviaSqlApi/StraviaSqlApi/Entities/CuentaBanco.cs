using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class CuentaBanco
{
    [Key]
    public int Numero_cuenta { set; get; }

    public String Carrera_dueno { set; get; }
}