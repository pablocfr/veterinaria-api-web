using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad.Entidades;

namespace Capa_Datos.Interfaces
{
    public interface IHistorial
    {
        List<Historial> SP_GetHistoriales();
        Historial SP_GetHistorialById(int id);
        List<Historial> SP_BuscarHistorialPorNombre(string nombre);
    }
}