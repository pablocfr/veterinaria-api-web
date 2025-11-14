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
    public class ServicioRepositorio : IServicio
    {
        private readonly string cadenaConexion = string.Empty;

        public ServicioRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Servicio> SP_GetServicios()
        {
            var listado = new List<Servicio>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetServicios", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(ServicioMapper.Map(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Servicio SP_GetServicioById(int id)
        {
            Servicio servicio = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetServicioById", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdServicio", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            lector.Read();
                            servicio = ServicioMapper.Map(lector);
                        }
                    }
                }
            }

            return servicio;
        }

        public Servicio SP_InsertServicio(Servicio servicio)
        {
            Servicio nuevoServicio = null;
            int nuevoID = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_InsertServicio", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", servicio.Descripcion);
                    comando.Parameters.AddWithValue("@Precio", servicio.Precio);

                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }
            }

            nuevoServicio = SP_GetServicioById(nuevoID);
            return nuevoServicio;
        }

        public void SP_UpdateServicio(Servicio servicio)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                var comando = new SqlCommand("SP_UpdateServicio", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdServicio", servicio.IdServicio);
                comando.Parameters.AddWithValue("@Nombre", servicio.Nombre);
                comando.Parameters.AddWithValue("@Descripcion", servicio.Descripcion);
                comando.Parameters.AddWithValue("@Precio", servicio.Precio);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void SP_DeleteServicio(int id)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_DeleteServicio", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdServicio", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}