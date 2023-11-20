using System;

namespace StraviaSqlApi.DTOs.Responses
{
    public partial class RaceResponseDto
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public decimal Precio { get; set; }
        public int Kms_total { get; set; }
        public string Recorrido_gpx { get; set; }
        public bool Finalizado { get; set; }
        public string Clase_actividad { get; set; }
    }
}  