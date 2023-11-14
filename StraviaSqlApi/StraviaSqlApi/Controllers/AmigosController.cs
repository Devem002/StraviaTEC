using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StraviaSqlApi.DbContexts;
using StraviaSqlApi.Dtos;
using StraviaSqlApi.Entities;

namespace StraviaSqlApi.Controllers;

[ApiController]
public class AmigosController : ControllerBase
{
     private StraviaDbContext _straviaDb;
     private IMapper _mapper;
    
    //Constructor del controlador
    public AmigosController (StraviaDbContext stravias, IMapper mapper)
    {
        _straviaDb = stravias;
        _mapper = mapper;
    }
    
    //Metodo de creacion para el amigo
    [HttpPost ("/amigos")]
    public IActionResult CreateAmigos([FromBody] AmigosDto payload)
    {
        var model =_mapper.Map<Amigos>(payload);
        var AmigoExist = _straviaDb.Amigos.Any(e => e.Usuario == model.Usuario);
        if (AmigoExist == true)
        {
            return Ok(new { Message = "Ya son amigos" });
        }

        _straviaDb.Add(model);
        _straviaDb.SaveChanges();

        return Ok(model);
    }
    
    //Metodo de obtencion de todos los amigos
    [HttpGet("/amigo")]
    public IActionResult GetAllamigos()
    {
        var amigos = _straviaDb.Amigos;
        return Ok(amigos);
    }

    //Metodo de obtencion de amigos por llave primaria
    [HttpGet("/{id}/amigo")]
    public Amigos GetById(string id)
    {
        var amigo = _straviaDb.Amigos.Find(id);
        return amigo;
    }

    //Metodo de actualizacion de amigos
    [HttpPut("/amigo/id")]
    public IActionResult Put(string id, [FromBody] AmigosDto payload)
    {
        var model = _mapper.Map<Amigos>(payload);
        _straviaDb.Amigos.Attach(model);
        _straviaDb.Entry(model).State = EntityState.Modified;

        _straviaDb.SaveChanges();

        return Ok(new { MESSAGE = "amigo actualizado" });
    }
    
    //Metodo para borrar un amigo
    [HttpDelete("/amigo/id")]
    public IActionResult Delete(string id)
    {
        var amigo = GetById(id);

        _straviaDb.Amigos.Remove(amigo);
        _straviaDb.SaveChanges();

        return Ok(new { MESSAGE = "amigo Deleted" });
    }
}