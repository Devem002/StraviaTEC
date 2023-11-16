using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace StraviaSqlApi.Entities;

public class Patrocinador
{
    [Key] 
    public String Nmbr_comercial { set; get; }

    public String Nmbr_rep_legal { set; get; }

    public String Logo { set; get; }

    public int Num_rep_legal { set; get; }
}