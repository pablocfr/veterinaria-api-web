using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;

namespace Capa_Datos.Mapper
{
    public static class HistorialMapper
    {
        public static Historial MapCompleto(SqlDataReader reader)
        {
            return new Historial
            {
                IdHistorial = reader.GetInt32(0),
                Diagnostico = reader.IsDBNull(1) ? "" : reader.GetString(1),
                Tratamiento = reader.IsDBNull(2) ? "" : reader.GetString(2),
                Observaciones = reader.IsDBNull(3) ? "" : reader.GetString(3),
                FechaAtencion = reader.GetDateTime(4),
                MotivoCita = reader.IsDBNull(5) ? "" : reader.GetString(5),
                EstadoCita = reader.IsDBNull(6) ? "" : reader.GetString(6),

                IdMascota = reader.GetInt32(7),
                NombreMascota = reader.GetString(8),
                EspecieMascota = reader.GetString(9),
                EdadMascota = reader.GetInt32(10),

                IdCliente = reader.GetInt32(11),
                NombreCliente = reader.GetString(12),
                DniCliente = reader.GetString(13),

                IdVeterinario = reader.GetInt32(14),
                NombreVeterinario = reader.GetString(15),
                EspecialidadVeterinario = reader.GetString(16)
            };
        }
    }
}