using System;

namespace WebPasajero.Models
{
    public class Pasajero
    {
        public int PasajeroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ciudad { get; set; }

    }
}
