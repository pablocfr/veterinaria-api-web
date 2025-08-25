﻿namespace VeterinariaApp.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Tipo { get; set; }
        public int IdProveedor { get; set; }
        public bool Estado { get; set; }
    }
}
