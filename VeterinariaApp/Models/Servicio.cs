using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }

        [Display(Name = "Nombre del Servicio")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}