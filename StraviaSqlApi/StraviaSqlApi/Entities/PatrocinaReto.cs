using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_patrocinador),nameof(Nmbr_reto))]
public class PatrocinaReto
{
    public String Nmbr_patrocinador { set; get; }

    public String Nmbr_reto { set; get; }
}