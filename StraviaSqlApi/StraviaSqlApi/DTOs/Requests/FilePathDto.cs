using System;
using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.DTOs.Requests
{
    public class FilePathDto
    {
        [Required]
        public string Path { get; set; }
    }
}