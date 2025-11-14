using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad.Entidades
{
    public class Veterinario
    {
        public int IDVeterinario { get; set; }
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}
