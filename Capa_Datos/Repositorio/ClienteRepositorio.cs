using Capa_Datos.Interfaces;
using Capa_Datos.Mapper;
using Capa_Entidad.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace Capa_Datos.Repositorio
{
    public class ClienteRepositorio : ICliente
    {
        
        private readonly string cadenaConexion = string.Empty;

        public ClienteRepositorio(IConfiguration config)
        {
            //lee "ConnectionStrings:DB"
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Cliente> listarClientes()
        {
            var lista = new List<Cliente>();
            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SP_GetCliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                            lista.Add(ClienteMapper.Map(rd));
                    }
                }
            }
            return lista;
        }

        public Cliente ObtenerPorId(int id)
        {
            Cliente cli = null;
            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SP_GetClienteById", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                            cli = ClienteMapper.Map(rd);
                    }
                }
            }
            return cli;
        }

        public List<Cliente> BuscarPorNombre(string nombre)
        {
            var lista = new List<Cliente>();
            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("SP_BuscarClientePorNombre", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre ?? string.Empty);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                            lista.Add(ClienteMapper.Map(rd));
                    }
                }
            }
            return lista;
        }

        public Cliente RegistrarCliente(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));

            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_CrearCliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // OJO: el SP usa @Nombre_completo (con guion bajo)
                    cmd.Parameters.AddWithValue("@Nombre_completo", cliente.Nombre ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DNI", (object?)cliente.DNI ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", (object?)cliente.Correo ?? DBNull.Value);

                    var nuevoId = Convert.ToInt32(cmd.ExecuteScalar());
                    cliente.IdCliente = nuevoId;
                    // Estado: el SP no lo recibe; asume default (activo) en tabla
                    return cliente;
                }
            }
        }

        public void actualizarCliente(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));

            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_ActualizarCliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@Nombre", (object?)cliente.Nombre ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DNI", (object?)cliente.DNI ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", (object?)cliente.Correo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void eliminarCliente(int id)
        {
            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_EliminarCliente", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
