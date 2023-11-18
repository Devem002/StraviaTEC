using Microsoft.AspNetCore.Mvc;
using StraviaSqlApi.Dtos;

namespace StraviaSqlApi.Controllers;

[ApiController]
public class AtletaController : ControllerBase
{

    /*
    public AtletaController(StraviaDbContext straviaDb, IMapper mapper)
    {
        _straviaDb = straviaDb;
        _mapper = mapper;
    }

    [HttpPost("/atleta")]
    public IActionResult CreateAtleta([FromBody] AtletaDto payload)
    {
        var model = _mapper.Map<Atleta>(payload);
        Console.Out.WriteLine(model.Contrasena);
        var AtletaExist = _straviaDb.ATLETA.Any(e => e.Usuario == model.Usuario);
        if (AtletaExist == true)
        {
            return Ok(new { Message = "Ya son amigos" });
            Console.Out.WriteLine(model.Contrasena);
        }

        _straviaDb.Add(model);
        _straviaDb.SaveChanges();

        return Ok(model);
    }
    */
    [HttpGet("atleta")]
    public IActionResult GetAtletas()
    {
        return Ok(GetAtletas());
    }
}