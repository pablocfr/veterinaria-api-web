using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;

namespace Capa_Negocio
{
    public class UsuarioNegocio
    {

        private static List<Usuario> listaUsuarios = new List<Usuario>
        {
            new Usuario { UsuarioID = "admin", Clave = "1234", Nombre = "Administrador", Rol = "Administrador" },
            new Usuario { UsuarioID = "vendedor", Clave = "1111", Nombre = "Vendedor Juan", Rol = "Vendedor" }
        };

        public Usuario ValidarLogin(string user, string pass)
        {
            return listaUsuarios.FirstOrDefault(u => u.UsuarioID == user && u.Clave == pass);
        }
    }
}
