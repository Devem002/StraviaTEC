using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_Grupo), nameof(Nmbr_Carrera))]
public class PrivCarrera
{
    public String Nmbr_Carrera { set; get; }

    public String Nmbr_Grupo { set; get; }
}