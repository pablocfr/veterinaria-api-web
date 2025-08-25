using Capa_Datos.Interfaces;
using Capa_Datos.Mapper;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Repositorio
{
    public class ProximaCitaRepositorio : IProximaCita
    {

        private readonly string cadenaConexion;
        public ProximaCitaRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<ProximaCita> listarProximasCitas()
        {
            var proximacita = new List<ProximaCita>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                using (var cmd = new SqlCommand("SP_GetProximasCitas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proximacita.Add(new ProximaCita
                            {

                                Mascota = reader.GetString(reader.GetOrdinal("Mascota")),
                                Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                                FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                                Motivo = reader.GetString(reader.GetOrdinal("Motivo")),
                                EstadoCita = reader.GetString(reader.GetOrdinal("EstadoCita"))
                            });
                        }
                    }
                }
            }

            return (proximacita);
        }

        public ProximaCita ObtenerPorId(int id)
        {
            ProximaCita proximacita = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                using (var cmd = new SqlCommand("SP_GetProximaCitaPorId", conexion)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    conexion.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            proximacita = new ProximaCita
                            {

                                Mascota = reader.GetString(reader.GetOrdinal("Mascota")),
                                Cliente = reader.GetString(reader.GetOrdinal("Cliente")),
                                FechaHora = reader.GetDateTime(reader.GetOrdinal("FechaHora")),
                                Motivo = reader.GetString(reader.GetOrdinal("Motivo")),
                                EstadoCita = reader.GetString(reader.GetOrdinal("EstadoCita"))
                            };
                        }
                    }
                }
            }

            return proximacita;
        }
    }

}
