using System;

namespace StraviaSqlApi.DTOs.Responses
{
    public class ChallengeResponseDto
    {
        public string Nombre { get; set; }
        public int Kms_total { get; set; }
        public bool Finalizado { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Fondo_altura { get; set; }
        public string Clase_actividad { get; set; }

    }
}