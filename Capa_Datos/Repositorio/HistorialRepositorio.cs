using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos.Interfaces;
using Capa_Datos.Mapper;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Capa_Datos.Repositorio
{
    public class HistorialRepositorio : IHistorial
    {
        private readonly string cadenaConexion = string.Empty;

        public HistorialRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Historial> SP_GetHistoriales()
        {
            var listado = new List<Historial>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetHistoriales", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(HistorialMapper.MapCompleto(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Historial SP_GetHistorialById(int id)
        {
            Historial historial = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetHistorialById", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdAtencion", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            lector.Read();
                            historial = HistorialMapper.MapCompleto(lector);
                        }
                    }
                }
            }

            return historial;
        }

        public List<Historial> SP_BuscarHistorialPorNombre(string nombre)
        {
            var listado = new List<Historial>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_BuscarHistorialPorNombre", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(HistorialMapper.MapCompleto(lector));
                        }
                    }
                }
            }
            return listado;
        }
    }
}