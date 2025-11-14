using System;
using System.Data;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Mapper
{
    public static class MascotaMapper
    {
        public static Mascota Map(IDataReader r)
        {
            var m = new Mascota
            {
                IdMascota = r["IdMascota"] != DBNull.Value ? Convert.ToInt32(r["IdMascota"]) : 0,
                Nombre = r["Nombre"] != DBNull.Value ? Convert.ToString(r["Nombre"]) : string.Empty,
                Especie = r["Especie"] != DBNull.Value ? Convert.ToString(r["Especie"]) : string.Empty,
                Raza = r["Raza"] != DBNull.Value ? Convert.ToString(r["Raza"]) : string.Empty,  // ← NUEVO
                Edad = r["Edad"] != DBNull.Value ? Convert.ToInt32(r["Edad"]) : 0,
                Sexo = r["Sexo"] != DBNull.Value ? Convert.ToString(r["Sexo"]) : string.Empty,
                IdCliente = r["IdCliente"] != DBNull.Value ? Convert.ToInt32(r["IdCliente"]) : 0,
                Estado = r["Estado"] != DBNull.Value && Convert.ToBoolean(r["Estado"]),
                Cliente = null
            };

            if (r.HasColumn("ClienteId"))
            {
                //si el cliente no viene informado se retorna null
                var hasAnyClienteField =
                    (r["ClienteId"] != DBNull.Value) ||
                    (r.HasColumn("ClienteNombre") && r["ClienteNombre"] != DBNull.Value) ||
                    (r.HasColumn("ClienteDNI") && r["ClienteDNI"] != DBNull.Value) ||
                    (r.HasColumn("ClienteTelefono") && r["ClienteTelefono"] != DBNull.Value) ||
                    (r.HasColumn("ClienteDireccion") && r["ClienteDireccion"] != DBNull.Value) ||
                    (r.HasColumn("ClienteCorreo") && r["ClienteCorreo"] != DBNull.Value) ||
                    (r.HasColumn("ClienteEstado") && r["ClienteEstado"] != DBNull.Value);

                if (hasAnyClienteField)
                {
                    m.Cliente = new Cliente
                    {
                        IdCliente = r["ClienteId"] != DBNull.Value ? Convert.ToInt32(r["ClienteId"]) : 0,
                        Nombre = r.HasColumn("ClienteNombre") && r["ClienteNombre"] != DBNull.Value ? Convert.ToString(r["ClienteNombre"]) : string.Empty,
                        DNI = r.HasColumn("ClienteDNI") && r["ClienteDNI"] != DBNull.Value ? Convert.ToString(r["ClienteDNI"]) : string.Empty,
                        Telefono = r.HasColumn("ClienteTelefono") && r["ClienteTelefono"] != DBNull.Value ? Convert.ToString(r["ClienteTelefono"]) : string.Empty,
                        Direccion = r.HasColumn("ClienteDireccion") && r["ClienteDireccion"] != DBNull.Value ? Convert.ToString(r["ClienteDireccion"]) : string.Empty,
                        Correo = r.HasColumn("ClienteCorreo") && r["ClienteCorreo"] != DBNull.Value ? Convert.ToString(r["ClienteCorreo"]) : string.Empty,
                        Estado = r.HasColumn("ClienteEstado") && r["ClienteEstado"] != DBNull.Value && Convert.ToBoolean(r["ClienteEstado"])
                    };
                }
            }

            return m;
        }

        // Helper: verificar si existe la columna (para alias opcionales)
        public static bool HasColumn(this IDataRecord r, string columnName)
        {
            for (int i = 0; i < r.FieldCount; i++)
                if (r.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }
    }
}
