using Capa_Entidad.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos.Interfaces
{
    public interface IProximaCita
    {
        List<ProximaCita> listarProximasCitas();
        ProximaCita ObtenerPorId(int id);
    }
}
