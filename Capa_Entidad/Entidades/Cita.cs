using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Cita
    {
        public int IdCita { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }
        public string EstadoCita { get; set; }
        public bool Estado { get; set; }


        public Mascota Mascota { get; set; }
        public Veterinario Veterinario { get; set; }
    }
}
