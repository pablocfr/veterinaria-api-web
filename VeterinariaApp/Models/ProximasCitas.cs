using Capa_Entidad.Entidades;

namespace VeterinariaApp.Models
{
    public class ProximasCitas
    {
        public string Mascota { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
        public string EstadoCita { get; set; }
    }
}
