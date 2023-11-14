using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Entities;

public class FondoAltura
{
    [Key]
    public String Tipo_f_a { set; get; }
}