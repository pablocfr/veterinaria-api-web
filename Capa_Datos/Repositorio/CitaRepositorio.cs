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
    public class CitaRepositorio : ICita
    {
        private readonly string cadenaConexion = string.Empty;

        public CitaRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public void actualizarCita(Cita cita)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                var comando = new SqlCommand("sp_ActualizarCita", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdCita", cita.IdCita);
                comando.Parameters.AddWithValue("@Fecha", cita.Fecha);
                comando.Parameters.AddWithValue("@Motivo", cita.Motivo);
                comando.Parameters.AddWithValue("@IdMascota", cita.IdMascota);
                comando.Parameters.AddWithValue("@IdVeterinario", cita.IdVeterinario);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void eliminarCita(int id)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("sp_EliminarCita", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@IdCita", id);
                    comando.ExecuteNonQuery ();
                }
            }
        }

        public List<Cita> listarCitaPorFecha(DateTime fecha)
        {
            var listado = new List<Cita>();

            using (var conexion = new SqlConnection(cadenaConexion)) 
            { 
                conexion.Open();

                using(var comando = new SqlCommand("SP_GetCitasByFecha", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Fecha", fecha.Date);

                    using (var reader = comando.ExecuteReader()) 
                    {
                        while (reader.Read()) 
                        {
                            listado.Add(CitaMapper.Map(reader));
                        }
                    }
                }
            }

            return listado;
        }

        public List<Cita> listarCitas()
        {
            var listado = new List<Cita>();

            using (var conexion = new SqlConnection(cadenaConexion)) 
            { 
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetCitas", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(CitaMapper.Map(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Cita ObtenerPorId(int id)
        {
            Cita cita = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using(var comando = new SqlCommand("SP_GetCitasByID", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@ID", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            lector.Read();
                            cita = CitaMapper.Map(lector);
                        }
                    }
                }
            }

            return cita;
        }

        public Cita registarCita(Cita cita)
        {
            Cita nuevaCita = null;
            int nuevoID = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("sp_CrearCita", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    comando.Parameters.AddWithValue("@Motivo", cita.Motivo);
                    comando.Parameters.AddWithValue("@IdMascota", cita.IdMascota);
                    comando.Parameters.AddWithValue("@IdVeterinario", cita.IdVeterinario);

                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }

            }

            nuevaCita = ObtenerPorId(nuevoID);

            return nuevaCita;
        }


        //Eliminar, se realizo un Mapper de Cita
        private Cita ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Cita
            {
                IdCita = reader.GetInt32(0),
                Fecha = reader.GetDateTime(1),
                Motivo = reader.IsDBNull(2) ? "" : reader.GetString(2),
                IdMascota = reader.GetInt32(3),
                IdVeterinario = reader.GetInt32(4),
                EstadoCita = reader.GetString(5),
                Estado = reader.GetBoolean(6),

                Mascota = new Mascota
                {
                    IdMascota = reader.GetInt32(7),
                    Nombre = reader.GetString(8),
                    Especie = reader.GetString(9),
                    Edad = reader.GetInt32(10),
                    Sexo = reader.GetString(11),
                    IdCliente = reader.GetInt32(12),
                    Estado = reader.GetBoolean(13),

                    Cliente = new Cliente
                    {
                        IdCliente = reader.GetInt32(14),
                        Nombre = reader.GetString(15),
                        DNI = reader.GetString(16),
                        Telefono = reader.GetString(17),
                        Direccion = reader.GetString(18),
                        Correo = reader.GetString(19),
                        Estado = reader.GetBoolean(20),
                    }
                },

                Veterinario = new Veterinario
                {
                    IdVeterinario = reader.GetInt32(21),
                    Nombre = reader.GetString(22),
                    DNI = reader.GetString(23),
                    Telefono = reader.GetString(24),
                    Especialidad = reader.GetString(25),
                    Correo = reader.GetString(26),
                    Estado = reader.GetBoolean(27),
                }
            };
        }
    }
}
