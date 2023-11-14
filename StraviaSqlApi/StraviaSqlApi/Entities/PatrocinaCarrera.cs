using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_patrocinador),nameof(Nmbr_carrera))]
public class PatrocinaCarrera
{
    public String Nmbr_patrocinador { set; get; }

    public String Nmbr_carrera { set; get; }
}