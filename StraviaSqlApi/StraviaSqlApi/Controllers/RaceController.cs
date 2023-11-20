using Microsoft.AspNetCore.Mvc;
using System;
using StraviaSqlApi.DAL;
using StraviaSqlApi.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StraviaSqlApi.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RaceController : ControllerBase
    {
        public RaceController()
        {
        }

        [HttpGet]
        [Route("races")]
        public IActionResult GetRaces()
        {
            var races = RaceDAL.GetRaces();
            if (races is null)
                return new JsonResult("Something went wrong") { StatusCode = 500 };

            return Ok(races);
        }


        [HttpGet]
        [Route("racesByUser/{user}")]
        public IActionResult GetRacesByUser(string user)
        {
            var races = RaceDAL.GetRacesByUser(user);
            if (races is null)
                return new JsonResult("Something went wrong") { StatusCode = 500 };

            return Ok(races);
        }

        [HttpPost]
        [Route("reraces")]
        public IActionResult RegisterRace(RaceRegisterDto race)
        {
            Console.Write(race);
            if (ModelState.IsValid)
            {
                var result = RaceDAL.RegisterRaceDB(race);
                if (result is "Error")
                {
                    return new JsonResult("Something went wrong in the registration.") { StatusCode = 500 };
                }
                else if (result is "Already Exists")
                {
                    return new JsonResult("Challenge already exists.") { StatusCode = 409 };
                }
                else if (result is "Activity Type not found")
                {
                    return new JsonResult("Activity Type not found") { StatusCode = 408 };
                }
                return new JsonResult("Registration completed") { StatusCode = 201 };
            }
            return new JsonResult("Invalid model for an User.") { StatusCode = 400 };
        }

        [HttpGet]
        [Route("RaceVisibility")]
        public IActionResult GetRaceVisibility()
        {
            var races =RaceDAL.GetRaceVisibility();
            if (races is null)
                return new JsonResult("Something went wrong retrieving the groups.") { StatusCode = 500 };
            return Ok(races);
        }


        [HttpGet]
        [Route("RaceRegister/{User}/{id}/{optionselect}")]
        public IActionResult RaceRegister(string User,int id, string optionselect)
        {
            var categories = RaceDAL.RaceRegister(User, id, optionselect);
            if (categories is null)
                return new JsonResult("Something went wrong retrieving the groups.") { StatusCode = 500 };
            return Ok(categories);
        }

    }
}