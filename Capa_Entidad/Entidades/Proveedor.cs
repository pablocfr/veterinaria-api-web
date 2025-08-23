using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Ruc { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}