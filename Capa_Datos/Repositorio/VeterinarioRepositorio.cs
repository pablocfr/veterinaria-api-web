using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Repositorio
{
    public class VeterinarioRepositorio : IVeterinario
    {
        private readonly string cadenaConexion;

        public VeterinarioRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }
        public Veterinario ActualizarVeterinario(Veterinario veterinario)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_UpdateVeterinario", conexion))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_veterinario", veterinario.IDVeterinario);
                    command.Parameters.AddWithValue("@nombre_completo", veterinario.NombreCompleto);
                    command.Parameters.AddWithValue("@dni", veterinario.DNI);
                    command.Parameters.AddWithValue("@telefono", veterinario.Telefono);
                    command.Parameters.AddWithValue("@especialidad", veterinario.Especialidad);
                    command.Parameters.AddWithValue("@correo", veterinario.Correo);
                    command.Parameters.AddWithValue("@Estado", veterinario.Estado);
                    command.ExecuteScalar();
                }
            }
            return ObtenerPorId(veterinario.IDVeterinario);
        }

        public bool EliminarVeterinario(int IdVeterinario)
        {
            bool exito = false;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_DeleteVeterinario", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@id_veterinario", IdVeterinario);

                    int filas = command.ExecuteNonQuery();
                    exito = filas > 0;   
                }
            }
            return exito;
        }

        public List<Veterinario> Listado()
        {
            var listado = new List<Veterinario>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var command = new SqlCommand("SP_GetVeterinarios", conexion))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(new Veterinario
                                {
                                    IDVeterinario = reader.GetInt32(reader.GetOrdinal("IdVeterinario")),
                                    NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                    DNI = reader.GetString(reader.GetOrdinal("DNI")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Especialidad = reader.GetString(reader.GetOrdinal("Especialidad")),
                                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                    Estado = Convert.ToBoolean(reader.GetOrdinal("Estado"))
                                });
                            }
                        }
                    }
                }
            }

            return listado;
        }

        public Veterinario ObtenerPorId(int id)
        {
            Veterinario veterinario = null;
            using (var conexion = new SqlConnection(cadenaConexion))

            {
                conexion.Open();
                using (var comando = new SqlCommand("SP_GetVeterinarioById", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@ID", id);
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();

                            veterinario = new Veterinario()
                            {
                                IDVeterinario = reader.GetInt32(reader.GetOrdinal("IDVeterinario")),
                                NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                                DNI = reader.GetString(reader.GetOrdinal("DNI")),
                                Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                Especialidad = reader.GetString(reader.GetOrdinal("Especialidad")),
                                Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }
            return veterinario;
        }

        public Veterinario RegistrarVeterinario(Veterinario veterinario)
        {
            Veterinario nuevoVeterinario = null;
            int nuevoID = 0;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_InsertVeterinario", conexion))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nombre_completo", veterinario.NombreCompleto);
                    command.Parameters.AddWithValue("@DNI", veterinario.DNI);
                    command.Parameters.AddWithValue("@Telefono", veterinario.Telefono);
                    command.Parameters.AddWithValue("@Especialidad", veterinario.Especialidad);
                    command.Parameters.AddWithValue("@Correo", veterinario.Correo);
                    command.Parameters.AddWithValue("@Estado", veterinario.Estado);
                    nuevoID = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            nuevoVeterinario = ObtenerPorId(nuevoID);
            return nuevoVeterinario;
        }

    }
}

