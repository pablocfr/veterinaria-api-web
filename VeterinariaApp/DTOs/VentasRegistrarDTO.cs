namespace VeterinariaApp.DTOs
{
    public class VentasRegistrarDTO
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDto> Detalles { get; set; }
    }
}
