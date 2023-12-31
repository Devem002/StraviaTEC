using Microsoft.AspNetCore.Mvc;
using System;
using StraviaSqlApi.DAL;
using StraviaSqlApi.DTOs.Requests;
using StraviaSqlApi.DTOs.Responses;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using StraviaSqlApi.Configuration.Token;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace StraviaSqlApi.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        public AuthenticationController(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult RegisterUser(
            [FromForm] string User,
            [FromForm] string FirstName,
            [FromForm] string LastName1,
            [FromForm] string LastName2,
            [FromForm] DateTime BirthDate,
            [FromForm] string Password,
            [FromForm] IFormFile Picture,
            [FromForm] string Nationality)
        {
            try
            {
                var fileName = Path.GetFileName(Picture.FileName);
                var filePath = Path.Combine("Files\\Profiles", User, fileName);
                UserRegisterDto user = new()
                {
                    User = User,
                    FirstName = FirstName,
                    LastName1 = LastName1,
                    LastName2 = LastName2,
                    BirthDate = BirthDate,
                    Password = Password,
                    Picture = filePath,
                    Nationality = Nationality
                };

                string result = UserDAL.RegisterUserDB(user);
                if (result is "Error")
                    return new JsonResult("Something went wrong in the registration.") { StatusCode = 500 };
                else if (result is "Taken")
                    return new JsonResult("User already exists.") { StatusCode = 409 };
                Directory.CreateDirectory("Files\\Profiles\\" + User);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.Position = 0;
                    Picture.CopyTo(fileStream);
                }
                return new JsonResult("Registrado") { StatusCode = 200};
            }
            catch (Exception err)
            {
                Console.Write(err);
                return new JsonResult("Something went wrong in the registration.") { StatusCode = 500 };
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser(UserLoginDto user)
        {
            if (ModelState.IsValid)
            {
                string result = UserDAL.LoginUserDB(user);
                if (result is "Error")
                    return new JsonResult("Something went wrong in the login.") { StatusCode = 500 };
                else if (result is "NotFound")
                    return new JsonResult("User not found.") { StatusCode = 403 };
                else if (result is "WrongPass")
                    return new JsonResult("Incorrect password.") { StatusCode = 403 };
                return new JsonResult("Logged In") { StatusCode = 200 };
            }
            return new JsonResult("Invalid model for Login.") { StatusCode = 400 };
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
    }
}