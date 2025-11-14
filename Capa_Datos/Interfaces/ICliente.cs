using System.Collections.Generic;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface ICliente
    {
        List<Cliente> listarClientes();
        Cliente ObtenerPorId(int id);
        List<Cliente> BuscarPorNombre(string nombre);
        Cliente RegistrarCliente(Cliente cliente);   
        void actualizarCliente(Cliente cliente);
        void eliminarCliente(int id);  
    }
}
