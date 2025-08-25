using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface IVenta
    {
        List<Venta> ListaVentas();
        List<Detalle_Venta> ListDetalleVentas();
        Detalle_Venta ObtenerVentaPorId(int id);
        string GrabarVenta(int id, decimal total, List<Detalle_Venta> detalle);
        Detalle_Venta RegistrarDetalleVenta(Detalle_Venta venta);
        void eliminarVenta(int id);
        void eliminarDetalleVenta(int id);

    }
}
