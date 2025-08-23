using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;

namespace Capa_Datos.Mapper
{
    public static class ProveedorMapper
    {
        public static Proveedor Map(SqlDataReader reader)
        {
            return new Proveedor
            {
                IdProveedor = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Ruc = reader.GetString(2),
                Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Direccion = reader.IsDBNull(4) ? "" : reader.GetString(4),
                Correo = reader.IsDBNull(5) ? "" : reader.GetString(5),
                Estado = reader.GetByte(6) == 1
            };
        }
    }
}