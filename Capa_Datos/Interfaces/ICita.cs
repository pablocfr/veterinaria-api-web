using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface ICita
    {
        List<Cita> listarCitas();
        Cita ObtenerPorId(int id);

        List<Cita> listarCitaPorFecha(DateTime fecha);

        Cita registarCita(Cita cita);

        void actualizarCita(Cita cita);

        void eliminarCita(int id);
    }
}
