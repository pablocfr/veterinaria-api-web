using Capa_Entidad.Entidades;

namespace VeterinariaApp.Models
{
    public class Detalle_Venta
    {
        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public int? IdServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }


        //Objetos
        public Venta Venta { get; set; }
        public Producto? Producto { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
