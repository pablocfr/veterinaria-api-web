using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Repositorio
{
    public class DashboardRepositorio : IDashboard
    {
        private readonly string cadenaConexion;

        public DashboardRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }
        public TotalesDashboard totalesDashboard()
        {
            using var conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            using var cmd = new SqlCommand("SP_GetTotalesDashboard", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            reader.Read();

            return new TotalesDashboard
            {
                TotalClientes = reader.GetInt32(0),
                TotalMascotas = reader.GetInt32(1),
                CitasHoy = reader.GetInt32(2),
                IngresosMensuales = reader.GetDecimal(3)
            };
        }
    }

}
