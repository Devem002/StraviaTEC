using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StraviaSqlApi.Entities;

[PrimaryKey(nameof(Usuario), nameof(Amigo))]
public class Amigos
{
    public string Usuario{ set; get; }

    public string Amigo{ set; get; }
    
}