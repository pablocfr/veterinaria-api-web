using Capa_Datos.Interfaces;
using Capa_Datos.Repositorio;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Servicios
{
    public class ProximaCitaServicio : IProximaCitaService
    {
        private readonly IProximaCita proximaCitaRepositorio;

        public ProximaCitaServicio(IProximaCita _proximacitaRepositorio)
        {
            proximaCitaRepositorio = _proximacitaRepositorio;
        }

        public ProximaCita ObtenerPorId(int id)
        {
            return proximaCitaRepositorio.ObtenerPorId(id);
        }

        public List<ProximaCita> listarProximasCitas()
        {
            return proximaCitaRepositorio.listarProximasCitas();
        }
    }

}
