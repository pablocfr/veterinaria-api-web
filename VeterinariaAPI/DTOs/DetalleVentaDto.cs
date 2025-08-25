namespace VeterinariaAPI.DTOs
{
    public class DetalleVentaDto
    {
        public int? IdProducto { get; set; }   // Puede ser null
        public int? IdServicio { get; set; }   // Puede ser null
        public int Cantidad { get; set; }

    }
}
