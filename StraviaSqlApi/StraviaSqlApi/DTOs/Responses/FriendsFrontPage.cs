using System;

namespace StraviaSqlApi.DTOs.Responses
{
    public class FriendsFrontPage
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido_1 { get; set; }
        public string Apellido_2 { get; set; }
        public string Nmbr_Actividad { get; set; }
        public string Clase_Actividad { get; set; }
        public DateTime Hora { get; set; }
        public string Recorrido_gpx { get; set; }
        public decimal Kms_hechos { get; set; }
    }
}