using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class ProximaCita
    {
        public int IDCita { get; set; }
        public string Cliente { get; set; }
        public string Mascota { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
    }
}
