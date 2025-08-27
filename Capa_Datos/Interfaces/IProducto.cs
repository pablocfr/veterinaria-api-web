using Capa_Entidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Interfaces
{
    public interface IProducto
    {
        Producto ActualizarProducto(Producto producto);
        bool EliminarProducto(int id);
        List<Producto> Listado();
        Producto ObtenerPorId(int id);
        Producto RegistrarProducto(Producto producto);
    }
}
