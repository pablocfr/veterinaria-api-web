using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface IProveedor
    {
        List<Proveedor> SP_GetProveedores();
        Proveedor SP_GetProveedorById(int id);
        Proveedor SP_InsertProveedor(Proveedor proveedor);
        void SP_UpdateProveedor(Proveedor proveedor);
        void SP_DeleteProveedor(int id);
    }
}