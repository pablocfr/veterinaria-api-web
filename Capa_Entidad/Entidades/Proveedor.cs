using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string  Nombre { get; set; }
        public string Ruc { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}
