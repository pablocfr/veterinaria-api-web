using Capa_Entidad.Entidades;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Models
{
    public class Cita
    {
        public int IdCita { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public int IdMascota { get; set; }
        public int IDVeterinario { get; set; }
        public string EstadoCita { get; set; }
        public bool Estado { get; set; }


        public Mascota Mascota { get; set; }
        public Veterinario Veterinario { get; set; }
    }
}
