using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface IServicio
    {
        List<Servicio> SP_GetServicios();
        Servicio SP_GetServicioById(int id);
        Servicio SP_InsertServicio(Servicio servicio);
        void SP_UpdateServicio(Servicio servicio);
        void SP_DeleteServicio(int id);
    }
}