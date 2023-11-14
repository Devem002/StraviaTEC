using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Usuario), nameof(Amigo))]
public class Amigos
{
    public String Usuario{ set; get; }

    public String Amigo{ set; get; }
    
    public Atleta AmigoDe { set; get; }
    
    public Atleta User { set; get; }
}