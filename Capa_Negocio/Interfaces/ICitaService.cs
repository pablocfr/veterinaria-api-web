using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface ICitaService
    {
        List<Cita> Listar();
        Cita ObtenerPorId(int id);
        Cita Registrar(Cita cita);
        Cita Actualizar(Cita cita);
        void Eliminar(int id);
    }
}
