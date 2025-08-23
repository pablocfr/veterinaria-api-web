using Capa_Entidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Interfaces
{
   public interface IVeterinario
    {
        Veterinario ActualizarVeterinario(Veterinario veterinario);
        bool EliminarVeterinario(int id);
        List<Veterinario> Listado();
        Veterinario ObtenerPorId(int id);
        Veterinario RegistrarVeterinario(Veterinario veterinario);
    }
}
