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
    public class HistorialServicio : IHistorialService
    {
        private readonly IHistorial historialRepositorio;

        public HistorialServicio(IHistorial _historialRepositorio)
        {
            historialRepositorio = _historialRepositorio;
        }

        public List<Historial> BuscarPorNombre(string nombre)
        {
            return historialRepositorio.SP_BuscarHistorialPorNombre(nombre);
        }

        public List<Historial> Listar()
        {
            return historialRepositorio.SP_GetHistoriales();
        }

        public Historial ObtenerPorId(int id)
        {
            return historialRepositorio.SP_GetHistorialById(id);
        }
    }
}