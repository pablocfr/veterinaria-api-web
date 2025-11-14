namespace VeterinariaApp.Models.ViewModelVenta
{
    public class ViewModelVenta
    {
        public string numeroventa { get; set; }
        public string documentocliente { get; set; }
        public string nombrecliente { get; set; }
        public string subtotal { get; set; }
        public string impuesto { get; set; }
        public string total { get; set; }
        public List<ViewModelDetalleVenta> detalleventa { get; set; }
    }
}
