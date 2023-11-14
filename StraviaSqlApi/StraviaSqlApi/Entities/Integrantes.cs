using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_grupo), nameof(Integrante))]
public class Integrantes
{
    public String Nmbr_grupo { set; get; }

    public String Integrante { set; get; }
}