using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IProveedorService
    {
        List<Proveedor> Listar();
        Proveedor ObtenerPorId(int id);
        Proveedor Registrar(Proveedor proveedor);
        void Actualizar(Proveedor proveedor);
        void Eliminar(int id);
    }
}