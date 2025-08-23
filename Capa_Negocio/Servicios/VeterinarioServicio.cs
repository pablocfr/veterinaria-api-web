using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Servicios
{
    public class VeterinarioServicio : IVeterinarioService
    {
        private readonly IVeterinario veterinarioRepositorio;

        public VeterinarioServicio(IVeterinario veterinarioRepositorio)
        {
            this.veterinarioRepositorio = veterinarioRepositorio;
        }

        public List<Veterinario> Listado()
        {
            return veterinarioRepositorio.Listado();
        }

        // Obtener veterinario por ID
        public Veterinario ObtenerPorId(int id)
        {
            return veterinarioRepositorio.ObtenerPorId(id);
        }

        public Veterinario RegistrarVeterinario(Veterinario veterinario)
        {
            return veterinarioRepositorio.RegistrarVeterinario(veterinario);
        }

       
        public Veterinario ActualizarVeterinario(Veterinario veterinario)
        {
            return veterinarioRepositorio.ActualizarVeterinario(veterinario);
        }

        public bool EliminarVeterinario(int id)
        {
            return veterinarioRepositorio.EliminarVeterinario(id);
        }
    }
}
