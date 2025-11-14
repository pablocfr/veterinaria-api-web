using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IServicioService
    {
        List<Servicio> Listar();
        Servicio ObtenerPorId(int id);
        Servicio Registrar(Servicio servicio);
        void Actualizar(Servicio servicio);
        void Eliminar(int id);
    }
}