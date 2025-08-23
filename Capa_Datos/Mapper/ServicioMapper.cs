using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;

namespace Capa_Datos.Mapper
{
    public static class ServicioMapper
    {
        public static Servicio Map(SqlDataReader reader)
        {
            return new Servicio
            {
                IdServicio = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Precio = reader.GetDecimal(3),
                Estado = reader.GetByte(4) == 1
            };
        }
    }
}