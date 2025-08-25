using Capa_Entidad.Entidades;

namespace VeterinariaAPI.DTOs
{
    public class VentasRegistrarDTO
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public List<Detalle_Venta> Detalles { get; set; }
    }
}
