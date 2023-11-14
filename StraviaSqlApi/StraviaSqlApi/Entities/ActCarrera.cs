using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_Carrera),nameof(Nmbr_Actividad),nameof(Atleta))]
public class ActCarrera
{
    public String Nmbr_Carrera { set; get; }

    public String Nmbr_Actividad { set; get; }

    public String Atleta { set; get; }
}