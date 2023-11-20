using System;
using System.ComponentModel.DataAnnotations;

namespace StraviaSqlApi.DTOs.Requests
{
    public partial class RaceRegisterDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public DateTime Hora { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int Kms_total { get; set; }
        public string Recorrido_gpx { get; set; }
        [Required]
        public Boolean Finalizado { get; set; }
        [Required]
        public string Clase_actividad { get; set; }
        [Required]
        public bool Privado { get; set; }
        public string Grupo { get; set; }
    }
}