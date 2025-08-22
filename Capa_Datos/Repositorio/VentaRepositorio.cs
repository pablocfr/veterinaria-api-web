using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Repositorio
{
    public class VentaRepositorio : IVenta
    {
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
            throw new NotImplementedException();
        }

        public List<Venta> ListaVentas()
        {
            throw new NotImplementedException();
        }

        public List<Detalle_Venta> ListDetalleVentas()
        {
            throw new NotImplementedException();
        }

        public Venta ObtenerVentaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Detalle_Venta RegistrarDetalleVenta(Detalle_Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}
