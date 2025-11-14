using System;
using System.Collections.Generic;
using System.Data;
using Capa_Datos.Interfaces;
using Capa_Datos.Mapper;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Capa_Datos.Repositorio
{
    public class MascotaRepositorio : IMascota
    {
        private readonly string cadenaConexion = string.Empty;

        public MascotaRepositorio(IConfiguration config)
        {
            //lee "ConnectionStrings:DB"
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Mascota> listarMascotas()
        {
            var listado = new List<Mascota>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetMascotas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(MascotaMapper.Map(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Mascota ObtenerPorId(int id)
        {
            Mascota mascota = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetMascotaById", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            lector.Read();
                            mascota = MascotaMapper.Map(lector);
                        }
                    }
                }
            }
            return mascota;
        }

        public List<Mascota> BuscarPorNombre(string nombre)
        {
            var listado = new List<Mascota>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_BuscarMascotaPorNombre", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(MascotaMapper.Map(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Mascota RegistrarMascota(Mascota nuevaMasc)
        {
            int nuevoID;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("sp_CrearMascota", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Nombre", nuevaMasc.Nombre);
                    comando.Parameters.AddWithValue("@Especie", nuevaMasc.Especie);
                    comando.Parameters.AddWithValue("@Raza", nuevaMasc.Raza);
                    comando.Parameters.AddWithValue("@Edad", nuevaMasc.Edad);
                    comando.Parameters.AddWithValue("@Sexo", nuevaMasc.Sexo);
                    comando.Parameters.AddWithValue("@IdCliente", nuevaMasc.IdCliente);

                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }
            }

            return ObtenerPorId(nuevoID);
        }

        public void actualizarMascota(Mascota Mascota)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                var comando = new SqlCommand("sp_ActualizarMascota", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdMascota", Mascota.IdMascota);
                comando.Parameters.AddWithValue("@Nombre", Mascota.Nombre);
                comando.Parameters.AddWithValue("@Especie", Mascota.Especie);
                comando.Parameters.AddWithValue("@Raza", Mascota.Raza);
                comando.Parameters.AddWithValue("@Edad", Mascota.Edad);
                comando.Parameters.AddWithValue("@Sexo", Mascota.Sexo);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void eliminarMascota(int id)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_EliminarMascota", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdMascota", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}