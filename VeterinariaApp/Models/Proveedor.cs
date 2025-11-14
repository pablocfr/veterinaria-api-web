using System.ComponentModel.DataAnnotations;

namespace VeterinariaApp.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "RUC")]
        public string Ruc { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress]
        public string Correo { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}