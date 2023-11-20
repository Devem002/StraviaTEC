using System;
using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.DTOs.Requests
{
    public class ChallengeRegisterDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Kms_total { get; set; }
        [Required]
        public bool Finalizado { get; set; }
        [Required]
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        [Required]
        public string Fondo_altura { get; set; }
        [Required]
        public string Clase_actividad { get; set; }
        [Required]
        public string Grupo { get; set; }
        
        public bool Privado { get; set; }
    }
}