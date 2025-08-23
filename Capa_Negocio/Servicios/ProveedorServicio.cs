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
    public class ProveedorServicio : IProveedorService
    {
        private readonly IProveedor proveedorRepositorio;

        public ProveedorServicio(IProveedor _proveedorRepositorio)
        {
            proveedorRepositorio = _proveedorRepositorio;
        }

        public void Actualizar(Proveedor proveedor)
        {
            proveedorRepositorio.SP_UpdateProveedor(proveedor);
        }

        public void Eliminar(int id)
        {
            proveedorRepositorio.SP_DeleteProveedor(id);
        }

        public List<Proveedor> Listar()
        {
            return proveedorRepositorio.SP_GetProveedores();
        }

        public Proveedor ObtenerPorId(int id)
        {
            return proveedorRepositorio.SP_GetProveedorById(id);
        }

        public Proveedor Registrar(Proveedor proveedor)
        {
            return proveedorRepositorio.SP_InsertProveedor(proveedor);
        }
    }
}