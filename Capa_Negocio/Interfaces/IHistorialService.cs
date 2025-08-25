using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Negocio.Interfaces
{
    public interface IHistorialService
    {
        List<Historial> Listar();
        Historial ObtenerPorId(int id);
        List<Historial> BuscarPorNombre(string nombre);
    }
}