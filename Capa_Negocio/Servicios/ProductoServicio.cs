using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Servicios
{
    public class ProductoServicio : IProductoService
    {

        private readonly IProducto productoRepositorio;

        public ProductoServicio (IProducto productoRepositorio)
        {
            this.productoRepositorio = productoRepositorio;
        }

        public Producto ActualizarProducto(Producto producto)
        {
            return productoRepositorio.ActualizarProducto(producto);
        }

        public bool EliminarProducto(int id)
        {
            return productoRepositorio.EliminarProducto(id);
        }

        public List<Producto> Listado()
        {
            return productoRepositorio.Listado() ;
        }

        public Producto ObtenerPorId(int id)
        {
            return productoRepositorio.ObtenerPorId(id);
        }

        public Producto RegistrarProducto(Producto producto)
        {
            return productoRepositorio.RegistrarProducto(producto);
        }
    }
}