using System.Collections.Generic;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IClienteService
    {
        List<Cliente> ListarClientes();
        Cliente ObtenerPorId(int id);
        List<Cliente> BuscarPorNombre(string nombre);
        Cliente RegistrarCliente(Cliente cliente);
        void ActualizarCliente(Cliente cliente);
        void Eliminar(int id);   // lógico (estado = 0)
    }
}
