using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.Dtos;

public class PatrocinadorDto
{
    public String Nmbr_comercio { set; get; }

    public String Nmbr_rep_legal { set; get; }

    public String[] Logo { set; get; }

    public int Num_rep_legal { set; get; }
}