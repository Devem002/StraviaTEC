using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StraviaSqlApi.DbContexts;
using StraviaSqlApi.Dtos;
using StraviaSqlApi.Entities;
using StraviaSqlApi.Utilities;

namespace StraviaSqlApi.Controllers;

[ApiController]
public class AtletaController : ControllerBase
{
    private StraviaDbContext _straviaDb;
    private IMapper _mapper;

    public AtletaController(StraviaDbContext straviaDb, IMapper mapper)
    {
        _straviaDb = straviaDb;
        _mapper = mapper;
    }

    [HttpPost("/atleta")]
    public IActionResult CreateAtleta([FromBody] AtletaDto payload)
    {
        var model = _mapper.Map<Atleta>(payload);
        model.Contrasena = Utilities.Encriptador.Encrypt(model.Contrasena);
        Console.Out.WriteLine(model.Contrasena);
        var AtletaExist = _straviaDb.Atleta.Any(e => e.Usuario == model.Usuario);
        if (AtletaExist == true)
        {
            return Ok(new { Message = "Ya son amigos" });
            Console.Out.WriteLine(model.Contrasena);
        }

        _straviaDb.Add(model);
        _straviaDb.SaveChanges();

        return Ok(model);
    }
}