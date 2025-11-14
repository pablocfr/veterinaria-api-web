using System.Collections.Generic;
using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;

namespace Capa_Negocio.Servicios
{
    public class ClienteServicio : IClienteService
    {
        private readonly ICliente _repo;

        public ClienteServicio(ICliente repo)
        {
            _repo = repo;
        }

        public List<Cliente> ListarClientes() => _repo.listarClientes();

        public Cliente ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public List<Cliente> BuscarPorNombre(string nombre) => _repo.BuscarPorNombre(nombre);

        public Cliente RegistrarCliente(Cliente cliente) => _repo.RegistrarCliente(cliente);

        public void ActualizarCliente(Cliente cliente) => _repo.actualizarCliente(cliente);

        public void Eliminar(int id) => _repo.eliminarCliente(id);
    }
}
