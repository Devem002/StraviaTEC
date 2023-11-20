using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StraviaSqlApi.DTOs.Responses
{
    public class RouteResponseDto
    {
        public List<string> Center { get; set; }
        public FileContentResult File { get; set; }
    }
}