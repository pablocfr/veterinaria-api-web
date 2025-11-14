namespace VeterinariaApp.Models
{
    public class Cabecera_Venta
    {
        public int idVenta { get; set; }
        public DateTime fecha { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string telefono { get; set; }
        public string dni { get; set; }
        public string direccion { get; set; }
        public decimal total { get; set; }
    }
}
