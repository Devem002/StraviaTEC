using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Carrera), nameof(Atleta))]
public class Inscrito
{
    public String Carrera { set; get; }

    public String Atleta { set; get; }
}