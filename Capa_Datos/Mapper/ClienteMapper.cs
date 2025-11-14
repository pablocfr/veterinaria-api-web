using System;
using System.Data;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Mapper
{
    public static class ClienteMapper
    {
        public static Cliente Map(IDataReader r)
        {
            return new Cliente
            {
                IdCliente = r["IdCliente"] != DBNull.Value ? Convert.ToInt32(r["IdCliente"]) : 0,
                Nombre = r["Nombre"] != DBNull.Value ? Convert.ToString(r["Nombre"]) : string.Empty,
                DNI = r["DNI"] != DBNull.Value ? Convert.ToString(r["DNI"]) : string.Empty,
                Telefono = r["Telefono"] != DBNull.Value ? Convert.ToString(r["Telefono"]) : string.Empty,
                Direccion = r["Direccion"] != DBNull.Value ? Convert.ToString(r["Direccion"]) : string.Empty,
                Correo = r["Correo"] != DBNull.Value ? Convert.ToString(r["Correo"]) : string.Empty,
                Estado = r["Estado"] != DBNull.Value && Convert.ToBoolean(r["Estado"])
            };
        }

       
    }
}
