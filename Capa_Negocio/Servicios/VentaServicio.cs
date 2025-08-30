using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;

namespace Capa_Negocio.Servicios
{
    public class VentaServicio : IVentaService
    {
        private readonly IVenta ventaRepositorio;

        public VentaServicio(IVenta _ventaRepositorio)
        {
            this.ventaRepositorio = _ventaRepositorio;
        }

        public void eliminarDetalleVenta(int id)
        {
            throw new NotImplementedException();
        }

        public void eliminarVenta(int id)
        {
            throw new NotImplementedException();
        }

        public string GrabarVenta(int id, decimal total, List<Detalle_Venta> detalle)
        {
            return ventaRepositorio.GrabarVenta(id, total, detalle);
        }

        public List<Venta> ListaVentas()
        {
            return ventaRepositorio.ListaVentas();
        }

        public List<Detalle_Venta> ListDetalleVentas()
        {
            return ventaRepositorio.ListDetalleVentas();
        }

        public Detalle_Venta ObtenerVentaPorId(int id)
        {
            return ventaRepositorio.ObtenerVentaPorId(id);
        }

        public Detalle_Venta RegistrarDetalleVenta(Detalle_Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}
