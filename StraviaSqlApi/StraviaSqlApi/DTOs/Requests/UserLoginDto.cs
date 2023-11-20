using System;
using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.DTOs.Requests
{
    public class UserLoginDto
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}