using System.Collections.Generic;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IMascotaService
    {
        List<Mascota> listarMascotas();
        Mascota ObtenerPorId(int id);
        List<Mascota> BuscarPorNombre(string nombre);
        Mascota RegistrarMascota(Mascota Mascota);
        void ActualizarMascota(Mascota Mascota); 
        void Eliminar(int id);
    }
}
