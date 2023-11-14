using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_Reto),nameof(Nmbr_Actividad),nameof(Atleta))]
public class ActReto
{
    public String Nmbr_Reto { set; get; }

    public String Nmbr_Actividad { set; get; }

    public String Atleta { set; get; }
}