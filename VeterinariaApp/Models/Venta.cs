using Capa_Entidad.Entidades;

namespace VeterinariaApp.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

        public Cliente Cliente { get; set; }
    }
}
