using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Negocio.Interfaces;
using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Servicios
{
    public class ServicioServicio : IServicioService
    {
        private readonly IServicio servicioRepositorio;

        public ServicioServicio(IServicio _servicioRepositorio)
        {
            servicioRepositorio = _servicioRepositorio;
        }

        public void Actualizar(Servicio servicio)
        {
            servicioRepositorio.SP_UpdateServicio(servicio);
        }

        public void Eliminar(int id)
        {
            servicioRepositorio.SP_DeleteServicio(id);
        }

        public List<Servicio> Listar()
        {
            return servicioRepositorio.SP_GetServicios();
        }

        public Servicio ObtenerPorId(int id)
        {
            return servicioRepositorio.SP_GetServicioById(id);
        }

        public Servicio Registrar(Servicio servicio)
        {
            return servicioRepositorio.SP_InsertServicio(servicio);
        }
    }
}