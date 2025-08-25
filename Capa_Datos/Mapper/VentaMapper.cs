using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;

namespace Capa_Datos.Mapper
{
    public static class VentaMapper
    {
        public static Detalle_Venta Map(SqlDataReader reader)
        {
            return new Detalle_Venta
            {
                IdDetalle = (int)reader["Detalle_IdDetalle"],
                IdVenta = (int)reader["Detalle_IdVenta"],
                IdProducto = reader["Detalle_IdProducto"] == DBNull.Value ? (int?)null : (int)reader["Detalle_IdProducto"],
                IdServicio = reader["Detalle_IdServicio"] == DBNull.Value ? (int?)null : (int)reader["Detalle_IdServicio"],
                Cantidad = (int)reader["Detalle_Cantidad"],
                SubTotal = (decimal)reader["Detalle_SubTotal"],

                Venta = new Venta
                {
                    IdVenta = (int)reader["Venta_IdVenta"],
                    Fecha = (DateTime)reader["Venta_Fecha"],
                    IdCliente = (int)reader["Venta_IdCliente"],
                    Total = (decimal)reader["Venta_Total"],

                    Cliente = new Cliente
                    {
                        IdCliente = (int)reader["Cliente_IdCliente"],
                        Nombre = (string)reader["Cliente_Nombre"],
                        DNI = (string)reader["Cliente_DNI"],
                        Telefono = (string)reader["Cliente_Telefono"],
                        Direccion = (string)reader["Cliente_Direccion"],
                        Correo = (string)reader["Cliente_Correo"],
                    }
                },

                Producto = reader["Producto_IdProducto"] == DBNull.Value ? null : new Producto
                {
                    IdProducto = (int)reader["Producto_IdProducto"],
                    Nombre = reader["Producto_Nombre"] == DBNull.Value ? null : (string)reader["Producto_Nombre"],
                    Descripcion = reader["Producto_Descripcion"] == DBNull.Value ? null : (string)reader["Producto_Descripcion"],
                    Precio = reader["Producto_Precio"] == DBNull.Value ? 0 : (decimal)reader["Producto_Precio"],
                    Stock = reader["Producto_Stock"] == DBNull.Value ? 0 : (int)reader["Producto_Stock"],
                    Tipo = reader["Producto_Tipo"] == DBNull.Value ? null : (string)reader["Producto_Tipo"],
                    IdProveedor = reader["Producto_IdProveedor"] == DBNull.Value ? 0 : (int)reader["Producto_IdProveedor"],
                },

                Servicio = reader["Servicio_IdServicio"] == DBNull.Value ? null : new Servicio
                {
                    IdServicio = (int)reader["Servicio_IdServicio"],
                    Nombre = reader["Servicio_Nombre"] == DBNull.Value ? null : (string)reader["Servicio_Nombre"],
                    Descripcion = reader["Servicio_Descripcion"] == DBNull.Value ? null : (string)reader["Servicio_Descripcion"],
                    Precio = reader["Servicio_Precio"] == DBNull.Value ? 0 : (decimal)reader["Servicio_Precio"],
                }
            };

        }
    }
}
