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
    public class ProveedorRepositorio : IProveedor
    {
        private readonly string cadenaConexion = string.Empty;

        public ProveedorRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Proveedor> SP_GetProveedores()
        {
            var listado = new List<Proveedor>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetProveedores", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (var lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listado.Add(ProveedorMapper.Map(lector));
                        }
                    }
                }
            }
            return listado;
        }

        public Proveedor SP_GetProveedorById(int id)
        {
            Proveedor proveedor = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_GetProveedorById", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedor", id);

                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            lector.Read();
                            proveedor = ProveedorMapper.Map(lector);
                        }
                    }
                }
            }

            return proveedor;
        }

        public Proveedor SP_InsertProveedor(Proveedor proveedor)
        {
            Proveedor nuevoProveedor = null;
            int nuevoID = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_InsertProveedor", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                    comando.Parameters.AddWithValue("@Ruc", proveedor.Ruc);
                    comando.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                    comando.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                    comando.Parameters.AddWithValue("@Correo", proveedor.Correo);

                    nuevoID = Convert.ToInt32(comando.ExecuteScalar());
                }
            }

            nuevoProveedor = SP_GetProveedorById(nuevoID);
            return nuevoProveedor;
        }

        public void SP_UpdateProveedor(Proveedor proveedor)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                var comando = new SqlCommand("SP_UpdateProveedor", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                comando.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                comando.Parameters.AddWithValue("@Ruc", proveedor.Ruc);
                comando.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                comando.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                comando.Parameters.AddWithValue("@Correo", proveedor.Correo);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void SP_DeleteProveedor(int id)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var comando = new SqlCommand("SP_DeleteProveedor", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProveedor", id);
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}