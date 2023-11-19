using Microsoft.AspNetCore.Mvc;
using StraviaSqlApi.DALs;
using StraviaSqlApi.Dtos;
using System.Globalization;


namespace StraviaSqlApi.Controllers
{
    public class ActivitiesController
    {
        /*
        [HttpPost]
        [Route("addActivity2")]
        public IActionResult PostActivity1(ActividadDto activity)
        {
            if (ModelState.IsValid)
            {
                string result = AtletaDal.AddActivity(activity);
                if (result is "Error")
                    return new JsonResult("Something went wrong while adding the activity.") { StatusCode = 500 };
                else if (result is "Taken")
                    return new JsonResult("There is another activity registered during that period.") { StatusCode = 409 };
                else if (result is "CurrentDate")
                    return new JsonResult("The period of the activity does not match with the current time") { StatusCode = 409 };
                return new JsonResult("Activity added.") { StatusCode = 201 };
            }
            return new JsonResult("Invalid model for Activity.") { StatusCode = 400 };
        }
        */
        [HttpPost]
        [Route("addActivity")]
        public IActionResult PostActivity(
            [FromForm] string UserId,
            [FromForm] string Distance,
            [FromForm] DateTime Duration,
            [FromForm] IFormFile Route,
            [FromForm] DateTime Start,
            [FromForm] string Type,
            [FromForm] string Roc,
            [FromForm] string RocName)
        {
            try
            {
                var format = new NumberFormatInfo();
                format.NegativeSign = "-";
                format.NumberDecimalSeparator = ".";
                ActividadDto activity = new()
                {
                    Nmbr_Actividad = UserId,
                    Kms = Math.Round((decimal)Double.Parse(Distance, format), 3),
                    Duracion = Duration,
                    Recorrido_gpx = "default",
                    Fecha = Start,
                    Atleta = Type,
                    RoC = Roc,
                    RoCName = RocName
                };

                string result = UserDAL.AddActivity(activity);
                if (result is "Error")
                    return new JsonResult("Something went wrong while adding the activity.") { StatusCode = 500 };
                else if (result is "Taken")
                    return new JsonResult("There is another activity registered during that period.") { StatusCode = 409 };
                else if (result is "CurrentDate")
                    return new JsonResult("The period of the activity does not match with the current time") { StatusCode = 409 };

                if (Route is not null)
                {
                    int actId = UserDAL.GetActivityId(activity);
                    string fileName = "";
                    string filePath = "";
                    fileName = Path.GetFileName(Route.FileName);
                    filePath = Path.Combine("Files\\Routes", "Activities", actId.ToString(), fileName);
                    Directory.CreateDirectory("Files\\Routes\\Activities\\" + actId.ToString());
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        fileStream.Position = 0;
                        Route.CopyTo(fileStream);
                    }
                    activity.Route = filePath;
                    UserDAL.UpdateActivityRoute(activity, actId);
                }
                return new JsonResult("Activity added.") { StatusCode = 201 };
            }
            catch (Exception err)
            {
                Console.Write(err);
                return new JsonResult("Something went wrong while updating.") { StatusCode = 500 };
            }
        }
    }
}
