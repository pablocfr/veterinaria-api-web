using System.Collections.Generic;
using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;

namespace Capa_Negocio.Servicios
{
    public class MascotaServicio : IMascotaService
    {
        private readonly IMascota _repo;

        public MascotaServicio(IMascota repo)
        {
            _repo = repo;
        }

        public List<Mascota> listarMascotas() => _repo.listarMascotas();

        public Mascota ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public List<Mascota> BuscarPorNombre(string nombre) => _repo.BuscarPorNombre(nombre);

        public Mascota RegistrarMascota(Mascota Mascota)
        {
           return  _repo.RegistrarMascota(Mascota);
        }

        public void ActualizarMascota(Mascota Mascota)
        {
            _repo.actualizarMascota(Mascota);
        }

        public void Eliminar(int id)
        {
            _repo.eliminarMascota(id);
        }
    }
}
