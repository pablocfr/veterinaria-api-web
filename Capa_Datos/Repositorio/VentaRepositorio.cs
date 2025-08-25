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
    public class VentaRepositorio : IVenta
    {

        private readonly string cadenaConexion;

        public VentaRepositorio(IConfiguration config)
        {
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public void eliminarDetalleVenta(int id)
        {
            throw new NotImplementedException();
        }

        public void eliminarVenta(int id)
        {
            throw new NotImplementedException();
        }

        public string GrabarVenta(int id, decimal total, List<Detalle_Venta> detalle)
        {
            using(var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var transaccion = conexion.BeginTransaction()) 
                {
                    try
                    {
                        int idVenta;

                        // 1. Insertar CABECERA
                        using (var comando = new SqlCommand("sp_AgregarVentaCabecera", conexion, transaccion))
                        {
                            comando.CommandType = System.Data.CommandType.StoredProcedure;

                            comando.Parameters.AddWithValue("@id_cliente", id);
                            comando.Parameters.AddWithValue("@total", total);

                            var outputId = new SqlParameter("@idVenta", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            comando.Parameters.Add(outputId);

                            comando.ExecuteNonQuery();
                            idVenta = (int)outputId.Value;
                        }

                        // 2. Insertar DETALLE
                        foreach (var venta in detalle)
                        {
                            using (var comando = new SqlCommand("sp_AgregarVentaDetalle", conexion, transaccion))
                            {
                                comando.CommandType = CommandType.StoredProcedure;

                                comando.Parameters.AddWithValue("@id_venta", idVenta);
                                comando.Parameters.AddWithValue("@id_producto", (object?)venta.IdProducto ?? DBNull.Value);
                                comando.Parameters.AddWithValue("@id_servicio", (object?)venta.IdServicio ?? DBNull.Value);
                                comando.Parameters.AddWithValue("@cantidad", venta.Cantidad);

                                comando.ExecuteNonQuery() ;
                            }
                        }

                        // Confirmar transacción
                        transaccion.Commit();

                        return $"La venta {idVenta} se realizó correctamente";
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        return $"Error al grabar venta: {ex.Message}";
                    }
                }

            }
        }

        public List<Venta> ListaVentas()
        {
            throw new NotImplementedException();
        }

        public List<Detalle_Venta> ListDetalleVentas()
        {
            var listado = new List<Detalle_Venta>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var cmd = new SqlCommand("SP_GetDetalleVentas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listado.Add(VentaMapper.Map(reader));
                        }
                    }
                }
            }

            return listado;
        }

        public Detalle_Venta ObtenerVentaPorId(int id)
        {
            Detalle_Venta nuevaConsulta = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                using (var cmd = new SqlCommand("SP_GetDetalleVentasById", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            nuevaConsulta = VentaMapper.Map(reader);
                        }
                    }
                }
            }

            return nuevaConsulta;
        }

        public Detalle_Venta RegistrarDetalleVenta(Detalle_Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}
