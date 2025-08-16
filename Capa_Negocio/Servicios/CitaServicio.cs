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
    public class CitaServicio : ICitaService
    {

        private readonly ICita citaRepositorio;

        public CitaServicio(ICita _citaRepositorio)
        {
            citaRepositorio = _citaRepositorio;
        }

        public Cita Actualizar(Cita cita)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cita> Listar()
        {
            return citaRepositorio.listarCitas();
        }

        public Cita ObtenerPorId(int id)
        {
            return citaRepositorio.ObtenerPorId(id);
        }

        public Cita Registrar(Cita cita)
        {
            throw new NotImplementedException();
        }
    }
}
