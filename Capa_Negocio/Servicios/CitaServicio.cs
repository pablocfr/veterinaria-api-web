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

        public void Actualizar(Cita cita)
        {
            citaRepositorio.actualizarCita(cita);
        }

        public void Eliminar(int id)
        {
            citaRepositorio.eliminarCita(id);
        }

        public List<Cita> Listar()
        {
            return citaRepositorio.listarCitas();

        }

        public List<Cita> ListarPorFecha(DateTime fecha)
        {
            return citaRepositorio.listarCitaPorFecha(fecha);
        }

        public Cita ObtenerPorId(int id)
        {
            return citaRepositorio.ObtenerPorId(id);
        }

        public Cita Registrar(Cita cita)
        {
            return citaRepositorio.registarCita(cita);
        }
    }
}
