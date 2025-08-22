using System;
using System.Collections.Generic;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface IMascota
    {
        List<Mascota> listarMascotas();
        Mascota ObtenerPorId(int id);
        List<Mascota> BuscarPorNombre(string nombre);
        Mascota RegistrarMascota(Mascota Mascota);
        void actualizarMascota(Mascota Mascota);
        void eliminarMascota(int id);
    }
}
