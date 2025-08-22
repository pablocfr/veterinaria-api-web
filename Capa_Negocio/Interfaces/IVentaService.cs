using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IVentaService
    {
        List<Venta> ListaVentas();
        List<Detalle_Venta> ListDetalleVentas();
        Venta ObtenerVentaPorId(int id);
        Detalle_Venta ObtenerDetallePorId(int id);
        Venta RegistrarVenta(Venta venta);
        Detalle_Venta RegistrarDetalleVenta(Detalle_Venta venta);
        void eliminarVenta(int id);
        void eliminarDetalleVenta(int id);
    }
}
