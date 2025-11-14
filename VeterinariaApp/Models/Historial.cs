using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Models
{
    public class Historial
    {
        public int IdHistorial { get; set; }

        [Display(Name = "Diagnóstico")]
        public string Diagnostico { get; set; } = string.Empty;

        [Display(Name = "Tratamiento")]
        public string Tratamiento { get; set; } = string.Empty;

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; } = string.Empty;

        [Display(Name = "Fecha de Atención")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime FechaAtencion { get; set; }

        [Display(Name = "Motivo de Cita")]
        public string MotivoCita { get; set; } = string.Empty;

        [Display(Name = "Estado de la Cita")]
        public string EstadoCita { get; set; } = string.Empty;

        /* Mascota */
        [Display(Name = "ID Mascota")]
        public int IdMascota { get; set; }

        [Display(Name = "Mascota")]
        public string NombreMascota { get; set; } = string.Empty;

        [Display(Name = "Especie")]
        public string EspecieMascota { get; set; } = string.Empty;

        [Display(Name = "Edad")]
        public int EdadMascota { get; set; }

        /* Cliente */
        [Display(Name = "ID Cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Cliente")]
        public string NombreCliente { get; set; } = string.Empty;

        [Display(Name = "DNI Cliente")]
        public string DniCliente { get; set; } = string.Empty;

        /* Veterinario */
        [Display(Name = "ID Veterinario")]
        public int IdVeterinario { get; set; }

        [Display(Name = "Veterinario")]
        public string NombreVeterinario { get; set; } = string.Empty;

        [Display(Name = "Especialidad Vet.")]
        public string EspecialidadVeterinario { get; set; } = string.Empty;
    }
}