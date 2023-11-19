using Microsoft.AspNetCore.Mvc;
using StraviaSqlApi.Dtos;
using StraviaSqlApi.DALs;
using StraviaSqlApi.Configuration;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using StraviaSqlApi.Configuration.Token;

namespace StraviaSqlApi.Controllers;

[ApiController]
public class AtletaController : ControllerBase
{

    private readonly JwtConfig _jwtConfig;
    public AtletaController(IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    [HttpPost("/register")]
    public IActionResult CreateAtleta(
            [FromForm] string User,
            [FromForm] string FirstName,
            [FromForm] string LastName1,
            [FromForm] string LastName2,
            [FromForm] DateTime BirthDate,
            [FromForm] string Password,
            [FromForm] IFormFile Picture,
            [FromForm] string Nationality)
    {
        {
            try
            {
                var fileName = Path.GetFileName(Picture.FileName);
                var filePath = Path.Combine("Files\\Profiles", User, fileName);

                AtletaDto user = new()
                {
                    Usuario = User,
                    Nombre = FirstName,
                    Apellido_1 = LastName1,
                    Apellido_2 = LastName2,
                    Fecha_Nacimiento = BirthDate,
                    Contrasena = Password,
                    Foto = filePath,
                    Nacionalidad = Nationality
                };
                string result = AtletaDal.RegisterUserDB(user);
                if (result is "Error")
                    return Ok("Something went wrong in the registration.");
                else if (result is "Taken")
                    return Ok("User already exists.");
                Directory.CreateDirectory("Files\\Profiles\\" + User);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.Position = 0;
                    Picture.CopyTo(fileStream);
                }
                return Ok(new RespuestaRegistro()
                {
                    Token = GenerateJwtToken(User)
                });
            }
            catch (Exception err)
            {
                Console.Write(err);
                return Ok("Something went wrong in the registration.");
            }
        }
    }

    [HttpPost]
    [Route("login")]
    public IActionResult LoginAtleta(LoginAtletaDto user)
    {
        if (ModelState.IsValid)
        {
            string result = AtletaDal.LoginUserDB(user);
            if (result is "Error")
                return Ok("Something went wrong in the login.");
            else if (result is "NotFound")
                return Ok("User not found.");
            else if (result is "WrongPass")
                return Ok("Incorrect password.");
            return Ok(new RespuestaRegistro()
            {
                Token = GenerateJwtToken(user.User)
            });
        }
        return Ok("Invalid model for Login.");
    }

    private string GenerateJwtToken(string user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("User", user),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }

    [HttpGet("atleta")]
    public IActionResult GetAllAtletas()
    {
        List<AtletaDto> atletas = AtletaDal.GetUsers();
        return Ok(atletas);
    }

    [HttpGet("atleta/{id}")]
    public IActionResult GetAtletaById(string id) 
    {
        AtletaDto atleta = AtletaDal.GetUser(id);
        return Ok(atleta);
    }

    [HttpPut]
    [Route("atleta/editar")]
    public IActionResult UpdateUser(
            [FromForm] string User,
            [FromForm] string FirstName,
            [FromForm] string LastName1,
            [FromForm] string LastName2,
            [FromForm] DateTime BirthDate,
            [FromForm] string CurrentPassword,
            [FromForm] IFormFile? Picture,
            [FromForm] string CurrentPicture,
            [FromForm] string Nationality)
    {
        try
        {
            string fileName = "";
            string filePath = "";
            if (Picture is not null)
            {
                fileName = Path.GetFileName(Picture.FileName);
                filePath = Path.Combine("Files\\Profiles", User, fileName);
            }
            AtletaDto user = new()
            {
                Usuario = User,
                Nombre = FirstName,
                Apellido_1 = LastName1,
                Apellido_2 = LastName2,
                Fecha_Nacimiento = BirthDate,
                Contrasena = CurrentPassword,
                Foto = Picture is not null ? filePath : CurrentPicture,
                Nacionalidad = Nationality
            };

            string result = AtletaDal.UpdateUser(user, CurrentPassword);
            if (result is "Error")
                return Ok("Something went wrong in the registration.");
            else if (result is "NotFound")
                return Ok("User not found.");
            else if (result is "WrongPass")
                return Ok("Incorrect password.");
            if (Picture is not null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (System.IO.File.Exists(CurrentPicture))
                    {
                        System.IO.File.Delete(CurrentPicture);
                    }
                    fileStream.Position = 0;
                    Picture.CopyTo(fileStream);
                }
            }
            return Ok();
        }
        catch (Exception err)
        {
            Console.Write(err);
            return Ok("Something went wrong while updating.");
        }
    }



    [HttpDelete]
    [Route("eliminar/{user}")]
    public IActionResult DeleteUser(string user)
    {
        string result = AtletaDal.DeleteUser(user);
        if (result is "Error")
            return Ok("Something went wrong while deleting the user.");
        return Ok("User deleted.");
    }

}