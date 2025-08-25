using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaAtencion { get; set; }
        public string MotivoCita { get; set; }
        public string EstadoCita { get; set; }

        // Información de la mascota
        public int IdMascota { get; set; }
        public string NombreMascota { get; set; }
        public string EspecieMascota { get; set; }
        public int EdadMascota { get; set; }

        // Información del cliente
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string DniCliente { get; set; }

        // Información del veterinario
        public int IdVeterinario { get; set; }
        public string NombreVeterinario { get; set; }
        public string EspecialidadVeterinario { get; set; }
    }
}