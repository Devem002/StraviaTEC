using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Nmbr_reto), nameof(Nmbr_Grupo))]
public class PrivReto
{
    public String Nmbr_reto { set; get; }

    public String Nmbr_Grupo { set; get; }
}