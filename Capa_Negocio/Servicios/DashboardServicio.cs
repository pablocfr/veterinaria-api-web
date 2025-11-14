using Capa_Datos.Interfaces;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Servicios
{
    public class DashboardServicio : IDashboardService
    {
        private readonly IDashboard repositorio;

        public DashboardServicio(IDashboard repositorio)
        {
            this.repositorio = repositorio;
        }

        public TotalesDashboard totalesDashboard()
        {
           return repositorio.totalesDashboard();
        }
    }
}