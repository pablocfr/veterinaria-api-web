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

    public class ProductoRepositorio : IProducto
    {

        private readonly string cadenaConexion;

        public ProductoRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }
        public Producto ActualizarProducto(Producto producto)
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_UpdateProducto", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@Tipo", producto.Tipo);
                    command.Parameters.AddWithValue("@Id_Proveedor", producto.IdProveedor);
                    command.Parameters.AddWithValue("@Estado", producto.Estado);

                    command.ExecuteNonQuery();
                }
            }
            return ObtenerPorId(producto.IdProducto);
        }

        public bool EliminarProducto(int idProducto)
        {
            bool exito = false;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_DeleteProducto", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_producto", idProducto);
                    int filas = command.ExecuteNonQuery();
                    exito = filas > 0;
                }
            }
            return exito;
        }

        public List<Producto> Listado()
        {
            var listado = new List<Producto>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_GetProductos", conexion))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(new Producto
                                {
                                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                    Tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                                    Estado = Convert.ToBoolean(reader["Estado"])
                                });
                            }
                        }
                    }
                }
            }
            return listado;
        }

        public Producto ObtenerPorId(int id)
        {
            Producto producto = null;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SP_GetProductoById", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Id", id);

                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            producto = new Producto()
                            {
                                IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                Tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                                IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                }
            }
            return producto;
        }


        public Producto RegistrarProducto(Producto producto)
        {
            int nuevoId = 0;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var command = new SqlCommand("SP_InsertProducto", conexion))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    command.Parameters.AddWithValue("@Stock", producto.Stock);
                    command.Parameters.AddWithValue("@Tipo", producto.Tipo);
                    command.Parameters.AddWithValue("@IdProveedor", producto.IdProveedor);
                    command.Parameters.AddWithValue("@Estado", producto.Estado);

                    nuevoId = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return ObtenerPorId(nuevoId);
        }
    }
}
